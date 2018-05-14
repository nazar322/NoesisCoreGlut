using System;
using Noesis;
using NoesisApp;

namespace NoesisApp
{
    public class StartupEventArgs : System.EventArgs
    {
    }

    public delegate void StartupEventHandler(object sender, StartupEventArgs e);

    public class ExitEventArgs : System.EventArgs
    {
        public int ApplicationExitCode { get; internal set; }
    }

    public delegate void ExitEventHandler(object sender, ExitEventArgs e);
}

/// <summary>
/// Application base class
/// </summary>
public class Application
{
    public static Application Current { get; private set; }

    public string Uri { get; set; }

    public string StartupUri { get; set; }

    public Display Display { get; set; }

    public ResourceDictionary Resources { get; set; }

    public event StartupEventHandler Startup;

    public event ExitEventHandler Exit;

    public Application()
    {
        if (Current != null)
        {
            throw new InvalidOperationException("An Application was already created");
        }

        Current = this;

        _exitCode = 0;

        Log.LogCallback += LogCallback;
        GUI.Init();
    }

    ///<summary>
    /// Run is called to start an application with a render loop
    /// Once run has been called - an application's OnStartup override and Startup event is
    /// called immediately afterwards.
    ///</summary>
    /// <returns>ExitCode of the application</returns>
    public int Run()
    {
        Start();

        Display.Show();

        DateTime startTime = DateTime.Now;
        Display.Render += (d) =>
        {
            _mainWindow.Render((DateTime.Now - startTime).TotalSeconds);
        };

        Display.EnterMessageLoop();

        Finish();

        return _exitCode;
    }

    private void Start()
    {
        // Create application display
        Display = CreateDisplay();

        // Set resource providers
        GUI.SetXamlProvider(GetXamlProvider());
        GUI.SetTextureProvider(GetTextureProvider());
        GUI.SetFontProvider(GetFontProvider());

        // Load App.xaml
        if (string.IsNullOrEmpty(Uri))
        {
            throw new InvalidOperationException("App xaml uri not defined");
        }

        GUI.LoadComponent(this, Uri);
        GUI.SetApplicationResources(Resources);

        // Load StartupUri xaml as MainWindow
        if (string.IsNullOrEmpty(StartupUri))
        {
            throw new InvalidOperationException("Startup window not defined");
        }

        object root = GUI.LoadXaml(StartupUri);
        _mainWindow = root as Window;

        if (_mainWindow == null)
        {
            // non window roots are allowed
            _mainWindow = new Window();
            _mainWindow.Content = root;
            _mainWindow.Title = GetTitle();
        }

        uint samples = GetSamples();
        bool vsync = GetVSync();
        bool sRGB = GetsRGB();

        RenderContext renderContext = GetRenderContext();
        renderContext.Init(Display.NativeHandle, samples, vsync, sRGB);
        renderContext.Device.OffscreenSampleCount = samples;

        bool ppaa = GetPPAA();
        _mainWindow.Init(Display, renderContext, samples, ppaa);

        StartupEventArgs e = new StartupEventArgs();
        OnStartup(e);
    }

    private void Finish()
    {
        ExitEventArgs e = new ExitEventArgs { ApplicationExitCode = _exitCode };
        try
        {
            OnExit(e);
        }
        finally
        {
            _exitCode = e.ApplicationExitCode;

            _mainWindow.Shutdown();
            _mainWindow = null;

            Resources = null;

            Current = null;

            // Shut down Noesis
            GUI.Shutdown();
        }
    }

    public void Shutdown()
    {
        Shutdown(0);
    }

    public void Shutdown(int exitCode)
    {
        _exitCode = exitCode;
        _mainWindow.Close();
    }

    private void LogCallback(LogLevel level, string channel, string message)
    {
        if (string.IsNullOrEmpty(channel) || channel == "Binding")
        {
            string[] prefixes = new string[] { "T", "D", "I", "W", "E" };
            string prefix = (int)level < prefixes.Length ? prefixes[(int)level] : " ";
            System.Diagnostics.Debug.WriteLine("[NOESIS/" + prefix + "] " + message);
        }
    }

    private Display CreateDisplay()
    {
#if __ANDROID__
        return new AndroidDisplay();
#elif __IOS__
    // TODO

#elif __UWP__
    // TODO

#else // Windows
        return new Win32Display();
#endif
    }

    protected virtual XamlProvider GetXamlProvider()
    {
        return null;
    }

    protected virtual TextureProvider GetTextureProvider()
    {
        // TODO: Expose native FileTextureProvider to C# and implement LocalTextureProvider on top
        return null;
    }

    protected virtual FontProvider GetFontProvider()
    {
        return null;
    }

    protected virtual RenderContext GetRenderContext()
    {
#if __ANDROID__
        return new RenderContextGL();
#else
        return new RenderContextD3D11();
#endif
    }

    protected virtual string GetTitle()
    {
        return "";
    }

    protected virtual uint GetSamples()
    {
        return 1;
    }

    protected virtual bool GetVSync()
    {
        return false;
    }

    protected virtual bool GetsRGB()
    {
        return false;
    }

    protected virtual bool GetPPAA()
    {
        return GetSamples() == 1;
    }

    protected virtual void OnStartup(StartupEventArgs e)
    {
        Startup?.Invoke(this, e);
    }

    protected virtual void OnExit(ExitEventArgs e)
    {
        Exit?.Invoke(this, e);
    }

#region Private members
    Window _mainWindow;
    int _exitCode;
#endregion
}

