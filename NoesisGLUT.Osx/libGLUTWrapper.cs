// If USE_FULLY_MANAGED is unset, libGLUTWrapper.cs works as an managed wrapper for libGLUTWrapper.so.
// If USE_FULLY_MANAGED is set, libGLUTWrapper.cs works completely managed based on OpenGL.cs.
#define USE_FULLY_MANAGED

namespace NoesisGLUT.Osx
{

	#region Define callback delegate prototypes

	/// <summary>Define a prototype for a native Noesis GUI appropriate 'Close' callback function.</summary>
	internal delegate void CloseCallback();

	/// <summary>Define a prototype for a native Noesis GUI appropriate 'Tick' callback function.</summary>
	internal delegate void TickCallback();

	/// <summary>Define a prototype for a native Noesis GUI appropriate 'Log' callback function.</summary>
	internal delegate void LogCallback(int n);

	/// <summary>Define a prototype for a native Noesis GUI appropriate 'PreRender' callback function.</summary>
	internal delegate void PreRenderCallback();

	/// <summary>Define a prototype for a native Noesis GUI appropriate 'PostRender' callback function.</summary>
	internal delegate void PostRenderCallback();

	/// <summary>Define a prototype for a native Noesis GUI appropriate 'Resize' callback function.</summary>
	internal delegate void ResizeCallback(int width, int height);

	/// <summary>Define a prototype for a native Noesis GUI appropriate 'MouseMove' callback function.</summary>
	internal delegate void MouseMoveCallback(int x, int y);

	/// <summary>Define a prototype for a native Noesis GUI appropriate 'MouseButton' callback function.</summary>
	internal delegate void MouseButtonCallback(int x, int y, int button);

	/// <summary>Define a prototype for a native Noesis GUI appropriate 'Key' callback function.</summary>
	internal delegate void KeyboardCallback(byte key, int specialKey, int modifiers);

	#endregion Define callback delegate prototypes

	/// <summary>
	/// The <see cref="GLUTWrapperLib"/> class is either
	/// - the access layer to the unmanaged libGlutWrapper, compiled from "GL/GLUTWrapper/GLUTWrapper.cpp"
	///   from Noesis GUI sample code within "NoesisGUI-ManagedSDK-2.0.2f2.zip".
	/// - the managed counterpart of the "GL/GLUTWrapper/GLUTWrapper.cpp" from Noesis GUI sample code
	///   within "NoesisGUI-ManagedSDK-2.0.2f2.zip".
	/// </summary>
	internal static class libGLUTWrapper
	{
#if USE_FULLY_MANAGED

		///////////////////////////////////////////////////////////////////////////////////////////////////
		/// D e f i n i t i o n s
		///////////////////////////////////////////////////////////////////////////////////////////////////

		#region Define Noesis GUI enumerations

		internal enum MouseButton : int
		{
			MouseButton_Left,
			MouseButton_Right,
			MouseButton_Middle,
			MouseButton_XButton1,
			MouseButton_XButton2
		}

		internal enum SpecialKey : int
		{
			SpecialKey_None,
			SpecialKey_F1, SpecialKey_F2, SpecialKey_F3, SpecialKey_F4, SpecialKey_F5, SpecialKey_F6,
			SpecialKey_F7, SpecialKey_F8, SpecialKey_F9, SpecialKey_F10, SpecialKey_F11, SpecialKey_F12,
			SpecialKey_PageUp, SpecialKey_PageDown,
			SpecialKey_Home, SpecialKey_End, SpecialKey_Insert,
			SpecialKey_Left, SpecialKey_Right, SpecialKey_Up, SpecialKey_Down
		}

		internal enum ModifierKey : int
		{
			ModifierKey_Shift = 1,
			ModifierKey_Ctrl = 2,
			ModifierKey_Alt = 4
		}

		#endregion Define Noesis GUI enumerations

		///////////////////////////////////////////////////////////////////////////////////////////////////
		/// A t t r i b u t e s
		///////////////////////////////////////////////////////////////////////////////////////////////////

		#region Declare callback delegates

		/// <summary>The currently registered native Noesis GUI appropriate 'Close' callback function.</summary>
		private static CloseCallback gCloseCallback = null;

		/// <summary>The currently registered native Noesis GUI appropriate 'Tick' callback function.</summary>
		private static TickCallback gTickCallback = null;

		/// <summary>The currently registered native Noesis GUI appropriate 'Log' callback function.</summary>
		private static LogCallback gLogCallback = null;

		/// <summary>The currently registered native Noesis GUI appropriate 'PreRender' callback function.</summary>
		private static PreRenderCallback gPreRenderCallback = null;

		/// <summary>The currently registered native Noesis GUI appropriate 'PostRender' callback function.</summary>
		private static PostRenderCallback gPostRenderCallback = null;

		/// <summary>The currently registered native Noesis GUI appropriate 'Resize' callback function.</summary>
		private static ResizeCallback gResizeCallback = null;

		/// <summary>The currently registered native Noesis GUI appropriate 'MouseMove' MouseMove function.</summary>
		private static MouseMoveCallback gMouseMoveCallback = null;

		/// <summary>The currently registered native Noesis GUI appropriate 'MouseDown' callback function.</summary>
		private static MouseButtonCallback gMouseDownCallback = null;

		/// <summary>The currently registered native Noesis GUI appropriate 'MouseUp' callback function.</summary>
		private static MouseButtonCallback gMouseUpCallback = null;

		/// <summary>The currently registered native Noesis GUI appropriate 'KeyyDown' callback function.</summary>
		private static KeyboardCallback gKeyDownCallback = null;

		/// <summary>The currently registered native Noesis GUI appropriate 'KeyUp' callback function.</summary>
		private static KeyboardCallback gKeyUpCallback = null;

		#endregion Declare callback delegates

		#region Attributes

		/// <summary>
		/// The GL window client area width, set by GLUT_Resize() and consumed by GLUT_Display().
		/// </summary>
		private static int gWidth = 0;

		/// <summary>
		/// The GL window client area height, set by GLUT_Resize() and consumed by GLUT_Display().
		/// </summary>
		private static int gHeight = 0;

		#endregion Attributes

		///////////////////////////////////////////////////////////////////////////////////////////////////
		/// M e t h o d e s
		///////////////////////////////////////////////////////////////////////////////////////////////////

		#region Button and key transformation

		/// <summary>Determine the NoesisGUI appropriate mouse button from the indicated GLUT mouse button.</summary>
		/// <param name="button">The GLUT mouse button.</param>
		/// <returns>The NoesisGUI appropriate mouse button.</returns>
		private static MouseButton GetMouseButton(int button)
		{
			switch (button)
			{
				case GLUT.GLUT_RIGHT_BUTTON:
					return MouseButton.MouseButton_Right;
				case GLUT.GLUT_MIDDLE_BUTTON:
					return MouseButton.MouseButton_Middle;
				case GLUT.GLUT_LEFT_BUTTON:
					return MouseButton.MouseButton_Left;
				default:
					return MouseButton.MouseButton_Left;
			}
		}

		/// <summary>Determine the NoesisGUI appropriate special key from the indicated GLUT special key.</summary>
		/// <param name="button">The GLUTspecial key.</param>
		/// <returns>The NoesisGUI appropriate special key.</returns>
		private static SpecialKey GetSpecialKey(int key)
		{
			switch (key)
			{
				case GLUT.GLUT_KEY_F1:
					return SpecialKey.SpecialKey_F1;
				case GLUT.GLUT_KEY_F2:
					return SpecialKey.SpecialKey_F2;
				case GLUT.GLUT_KEY_F3:
					return SpecialKey.SpecialKey_F3;
				case GLUT.GLUT_KEY_F4:
					return SpecialKey.SpecialKey_F4;
				case GLUT.GLUT_KEY_F5:
					return SpecialKey.SpecialKey_F5;
				case GLUT.GLUT_KEY_F6:
					return SpecialKey.SpecialKey_F6;
				case GLUT.GLUT_KEY_F7:
					return SpecialKey.SpecialKey_F7;
				case GLUT.GLUT_KEY_F8:
					return SpecialKey.SpecialKey_F8;
				case GLUT.GLUT_KEY_F9:
					return SpecialKey.SpecialKey_F9;
				case GLUT.GLUT_KEY_F10:
					return SpecialKey.SpecialKey_F10;
				case GLUT.GLUT_KEY_F11:
					return SpecialKey.SpecialKey_F11;
				case GLUT.GLUT_KEY_F12:
					return SpecialKey.SpecialKey_F12;
				case GLUT.GLUT_KEY_PAGE_UP:
					return SpecialKey.SpecialKey_PageUp;
				case GLUT.GLUT_KEY_PAGE_DOWN:
					return SpecialKey.SpecialKey_PageDown;
				case GLUT.GLUT_KEY_HOME:
					return SpecialKey.SpecialKey_Home;
				case GLUT.GLUT_KEY_END:
					return SpecialKey.SpecialKey_End;
				case GLUT.GLUT_KEY_INSERT:
					return SpecialKey.SpecialKey_Insert;
				case GLUT.GLUT_KEY_LEFT:
					return SpecialKey.SpecialKey_Left;
				case GLUT.GLUT_KEY_RIGHT:
					return SpecialKey.SpecialKey_Right;
				case GLUT.GLUT_KEY_UP:
					return SpecialKey.SpecialKey_Up;
				case GLUT.GLUT_KEY_DOWN:
					return SpecialKey.SpecialKey_Down;
				default:
					return SpecialKey.SpecialKey_None;
			}
		}

		#endregion Button and key transformation

		#region Register callback delegates

		/// <summary>Register close callback.</summary>
		internal static void GLUT_RegisterClose(CloseCallback closeCallback)
		{
			gCloseCallback = closeCallback;
		}


		/// <summary>Register tick callback.</summary>
		internal static void  GLUT_RegisterTick(TickCallback tickCallback)
		{
			gTickCallback = tickCallback;
		}

		/// <summary>Register log callback.</summary>
		internal static void GLUT_RegisterLog(LogCallback logCallback)
		{
			gLogCallback = logCallback;
		}

		/// <summary>Register pre-render callback.</summary>
		internal static void GLUT_RegisterPreRender(PreRenderCallback preRenderCallback)
		{
			gPreRenderCallback = preRenderCallback;
		}

		/// <summary>Register post-render callback.</summary>
		internal static void GLUT_RegisterPostRender(PostRenderCallback postRenderCallback)
		{
			gPostRenderCallback = postRenderCallback;
		}

		/// <summary>Register resize callback.</summary>
		internal static void GLUT_RegisterResize(ResizeCallback resizeCallback)
		{
			gResizeCallback = resizeCallback;
		}

		/// <summary>Register mouse callback.</summary>
		/// <param name="mouseMoveCallback">The mouse move callback to register.</param>
		/// <param name="mouseDownCallback">The mouse down callback to register.</param>
		/// <param name="mouseUpCallback">The mouse up callback to register.</param>
		internal static void GLUT_RegisterMouse(MouseMoveCallback mouseMoveCallback,
			MouseButtonCallback mouseDownCallback,
			MouseButtonCallback mouseUpCallback)
		{
			gMouseMoveCallback = mouseMoveCallback;
			gMouseDownCallback = mouseDownCallback;
			gMouseUpCallback = mouseUpCallback;
		}

		/// <summary>Register key callback.</summary>
		/// <param name="keyDownCallback">The key down callback to register.</param>
		/// <param name="keyUpCallback">The key up callback to register.</param>
		internal static void GLUT_RegisterKeyboard(KeyboardCallback keyDownCallback, KeyboardCallback keyUpCallback)
		{
			gKeyDownCallback = keyDownCallback;
			gKeyUpCallback = keyUpCallback;
		}

		#endregion Register callback delegates

		#region Invoke registered callback delegates

		/// <summary>Invoke the registered close callback.</summary>
		internal static void GLUT_Close()
		{
			if (gCloseCallback != null)
			{
				gCloseCallback();
			}
		}

		/// <summary>Invoke the registered callbacks for tick, pre-render and post-render.</summary>
		internal static void GLUT_Display()
		{
			// Frame tick
			if (gTickCallback != null)
			{
				gTickCallback();
			}

			// PreRender
			if (gPreRenderCallback != null)
			{
				gPreRenderCallback();
			}

			// Render Scene (now only a clear)
			GL.Enable(GL.GL_DEPTH_TEST);
			GL.DepthFunc(GL.GL_LESS);
			GL.ClearDepth(1.0f);
			GL.DepthMask(GL.GL_TRUE);

			GL.Disable(GL.GL_CULL_FACE);
			GL.Disable(GL.GL_ALPHA_TEST);
			GL.Disable(GL.GL_STENCIL_TEST);
			GL.Disable(GL.GL_BLEND);
			GL.Disable(GL.GL_SCISSOR_TEST);

			GL.UseProgram(0);
			GL.BindFramebuffer(GL.GL_FRAMEBUFFER, 0);
			GL.Viewport(0, 0, gWidth, gHeight);
			GL.ColorMask(true, true, true, true);

			GL.ClearColor(0.0f, 0.0f, 0.0f, 0.0f);
			GL.Clear(GL.GL_COLOR_BUFFER_BIT | GL.GL_DEPTH_BUFFER_BIT);
			GL.LoadIdentity();
			GL.Ortho(0.0, 1.0, 0.0, 1.0, -1.0, 1.0);

			// PostRender
			if (gPostRenderCallback != null)
			{
				gPostRenderCallback();
			}

			// End
			GL.Flush();
			GLUT.SwapBuffers();

			// Update again and again...
			GLUT.PostRedisplay();
		}

		/// <summary>Invoke the registered log callback.</summary>
		/// <param name="n">The parameter n.</param>
		internal static void GLUT_Log(int n)
		{
			if (gLogCallback != null)
			{
				gLogCallback(n);
			}
		}

		/// <summary>Invoke the registered resize callback.</summary>
		/// <param name="width">The GL window client area width.</param>
		/// <param name="height">The GL window client area height.</param>
		internal static void GLUT_Resize(int width, int height)
		{
			gWidth = width;
			gHeight = height;

			if (gResizeCallback != null)
			{
				gResizeCallback(gWidth, gHeight);
			}

			// Update
			GLUT.PostRedisplay();
		}

		/// <summary>Invoke the registered mouse move callback.</summary>
		internal static void GLUT_MouseMove(int x, int y)
		{
			if (gMouseMoveCallback != null)
			{
				gMouseMoveCallback(x, y);
			}
		}

		/// <summary>Invoke the registered mouse button callback.</summary>
		internal static void GLUT_MouseButton(int button, int state, int x, int y)
		{
			if (state == GLUT.GLUT_DOWN)
			{
				if (gMouseDownCallback != null)
				{
					gMouseDownCallback(x, y, (int)GetMouseButton(button));
				}
			}
			else // state == GLUT.GLUT_UP
			{
				if (gMouseUpCallback != null)
				{
					gMouseUpCallback(x, y, (int)GetMouseButton(button));
				}
			}
		}

		/// <summary>Invoke the registered key down callback.</summary>
		internal static void GLUT_KeyboardDown(byte key, int x, int y)
		{
			if (gKeyDownCallback != null)
			{
				int modifiers = GLUT.GetModifiers();
				gKeyDownCallback(key, (int)SpecialKey.SpecialKey_None, modifiers);
			}
		}

		/// <summary>Invoke the registered key up callback.</summary>
		internal static void GLUT_KeyboardUp(byte key, int x, int y)
		{
			if (gKeyUpCallback != null)
			{
				int modifiers = GLUT.GetModifiers();
				gKeyUpCallback(key, (int)SpecialKey.SpecialKey_None, modifiers);
			}
		}

		/// <summary>Invoke the registered special key down callback.</summary>
		internal static void GLUT_KeyboardSpecialDown(int key, int x, int y)
		{
			if (gKeyDownCallback != null)
			{
				int modifiers = GLUT.GetModifiers();
				gKeyDownCallback(0, (int)GetSpecialKey(key), modifiers);
			}
		}

		/// <summary>Invoke the registered special key up callback.</summary>
		internal static void GLUT_KeyboardSpecialUp(int key, int x, int y)
		{
			if (gKeyUpCallback != null)
			{
				int modifiers = GLUT.GetModifiers();
				gKeyUpCallback(0, (int)GetSpecialKey(key), modifiers);
			}
		}

		#endregion Invoke registered callback delegates

		#region Run GL window

		/// <summary>
		/// Init the GL window.
		/// </summary>
		/// <param name="windowWidth">The GL window client area width.</param>
		/// <param name="windowHeight">The GL window client area height.</param>
		/// <param name="title">The GL window title.</param>
		internal static void GLUT_Init(int windowWidth, int windowHeight, string title)
		{
			string[] myargv = new string[] { "GLUTWrapper" };
			int myargc = 1;

			GLUT.Init(myargc, myargv);
			GLUT.InitDisplayMode(GLUT.GLUT_RGB | GLUT.GLUT_DOUBLE | GLUT.GLUT_DEPTH | GLUT.GLUT_STENCIL);

			// GLUT_Resize() is automatically called before GLUT_Display() - window dimension have not to be saved here.
			GLUT.InitWindowSize(windowWidth, windowHeight);
			GLUT.InitWindowPosition(0, 0);

			GLUT.CreateWindow(title);
		}

		/// <summary>
		/// Register all callbacks and run the GL message loop.
		/// </summary>
		internal static void GLUT_Run()
		{
			GLUT.DisplayFunc(GLUT_Display);
			GLUT.ReshapeFunc(GLUT_Resize);
			GLUT.MouseFunc(GLUT_MouseButton);
			GLUT.MotionFunc(GLUT_MouseMove);
			GLUT.PassiveMotionFunc(GLUT_MouseMove);
			GLUT.KeyboardFunc(GLUT_KeyboardDown);
			GLUT.KeyboardUpFunc(GLUT_KeyboardUp);
			GLUT.SpecialFunc(GLUT_KeyboardSpecialDown);
			GLUT.SpecialUpFunc(GLUT_KeyboardSpecialUp);

			// ==== Register an exit proc only from native code (never from managed code)! ====
			// The purpose of an exit proc is to clean up/free resources directly before the
			// library is unloaded. To avoid concurrency issues or unexpected behavior, it must
			// be guaranteed that NO other threads may still access the resources to free by
			// now. And this can not be guaranteed for managed code.
			// atexit(new ExitProcDelegate(GLUT_Close));

			GLUT.MainLoop();
		}

		#endregion Run GL window

#else

		///////////////////////////////////////////////////////////////////////////////////////////////////
		/// M e t h o d e s
		///////////////////////////////////////////////////////////////////////////////////////////////////

		#region Register callback delegates

		[DllImport("libGlutWrapper", EntryPoint = "GLUT_RegisterClose")]
		internal static extern void GLUT_RegisterClose(CloseCallback tickCallback);

		[DllImport("libGlutWrapper", EntryPoint = "GLUT_RegisterTick")]
		internal static extern void GLUT_RegisterTick(TickCallback tickCallback);

		[DllImport("libGlutWrapper", EntryPoint = "GLUT_RegisterPreRender")]
		internal static extern void GLUT_RegisterPreRender(PreRenderCallback renderCallback);

		[DllImport("libGlutWrapper", EntryPoint = "GLUT_RegisterPostRender")]
		internal static extern void GLUT_RegisterPostRender(PostRenderCallback renderCallback);

		[DllImport("libGlutWrapper", EntryPoint = "GLUT_RegisterResize")]
		internal static extern void GLUT_RegisterResize(ResizeCallback resizeCallback);

		[DllImport("libGlutWrapper", EntryPoint = "GLUT_RegisterMouse")]
		internal static extern void GLUT_RegisterMouse(
			MouseMoveCallback mouseMoveCallback,
			MouseButtonCallback mouseDownCallback,
			MouseButtonCallback mouseUpCallback);

		[DllImport("libGlutWrapper", EntryPoint = "GLUT_RegisterKeyboard")]
		internal static extern void GLUT_RegisterKeyboard(
			KeyboardCallback keyDownCallback,
			KeyboardCallback keyUpCallback);

		#endregion Register callback delegates

		#region Run GL window

		[DllImport("libGlutWrapper", EntryPoint = "GLUT_Init")]
		internal static extern void GLUT_Init(int width, int height, [MarshalAs(UnmanagedType.LPStr)] string title);

		[DllImport("libGlutWrapper", EntryPoint = "GLUT_Run")]
		internal static extern void GLUT_Run();

		#endregion Run GL window

#endif
	}
}