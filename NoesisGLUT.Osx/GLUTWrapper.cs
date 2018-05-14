using System;

namespace NoesisGLUT.Osx
{

	/// <summary>
	/// The <see cref="GLUTWrapper"/> class is the managed counterpart of the <see cref="GLUTWrapperLib"/>
	/// from Noesis GUI sample code within "NoesisGUI-ManagedSDK-2.0.2f2.zip".
	/// </summary>
	public static class GLUTWrapper
	{

		///////////////////////////////////////////////////////////////////////////////////////////////////
		/// D e f i n i t i o n s
		///////////////////////////////////////////////////////////////////////////////////////////////////

		#region Define Noesis GUI enumerations

		/// <summary>The <see cref="MouseButton"/> enumeration provides the supported mouse buttons.</summary>
		public enum MouseButton
		{
			Left,
			Right,
			Middle
		}

		/// <summary>The <see cref="SpecialKey"/> enumeration provides the supported special keys.</summary>
		public enum SpecialKey
		{
			None,
			F1, F2, F3, F4, F5, F6, F7, F8, F9, F10, F11, F12,
			PageUp, PageDown,
			Home, End, Insert,
			Left, Right, Up, Down
		}

		/// <summary>The <see cref="ModifierKey"/> enumeration provides the supported modifier keys.</summary>
		[Flags]
		public enum ModifierKey
		{
			Shift = 1,
			Ctrl = 2,
			Alt = 4
		}

		#endregion Define Noesis GUI enumerations

		#region Define event handler prototypes

		/// <summary>Define a prototype for a managed Noesis GUI appropriate 'Close' event handler.</summary>
		public delegate void CloseEventHandler();

		/// <summary>Define a prototype for a managed Noesis GUI appropriate 'Tick' event handler.</summary>
		public delegate void TickEventHandler(double timeInSeconds);

		/// <summary>Define a prototype for a managed Noesis GUI appropriate 'Render' event handler.</summary>
		public delegate void RenderEventHandler();

		/// <summary>Define a prototype for a managed Noesis GUI appropriate 'Resize' event handler.</summary>
		public delegate void ResizeEventHandler(int width, int height);
		
		/// <summary>Define a prototype for a managed Noesis GUI appropriate 'MouseMove' event handler.</summary>
		public delegate void MouseMoveEventHandler(int x, int y);

		/// <summary>Define a prototype for a managed Noesis GUI appropriate 'MouseButton' event handler.</summary>
		public delegate void MouseButtonEventHandler(int x, int y, MouseButton button);

		/// <summary>Define a prototype for a managed Noesis GUI appropriate 'Key' event handler.</summary>
		public delegate void KeyEventHandler(byte key, SpecialKey specialKey, ModifierKey modifiers);

		#endregion Define event handler prototypes

		///////////////////////////////////////////////////////////////////////////////////////////////////
		/// A t t r i b u t e s
		///////////////////////////////////////////////////////////////////////////////////////////////////

		#region Declare event handler

		/// <summary>The currently registered managed Noesis GUI appropriate 'Close' event handler.</summary>
		public static event CloseEventHandler Close;

		/// <summary>The currently registered managed Noesis GUI appropriate 'Tick' event handler.</summary>
		public static event TickEventHandler Tick;

		/// <summary>The currently registered managed Noesis GUI appropriate 'PreRender' event handler.</summary>
		public static event RenderEventHandler PreRender;

		/// <summary>The currently registered managed Noesis GUI appropriate 'PostRender' event handler.</summary>
		public static event RenderEventHandler PostRender;

		/// <summary>The currently registered managed Noesis GUI appropriate 'Resize' event handler.</summary>
		public static event ResizeEventHandler Resize;

		/// <summary>The currently registered managed Noesis GUI appropriate 'MouseMove' event handler.</summary>
		public static event MouseMoveEventHandler MouseMove;

		/// <summary>The currently registered managed Noesis GUI appropriate 'MouseDown' event handler.</summary>
		public static event MouseButtonEventHandler MouseDown;

		/// <summary>The currently registered managed Noesis GUI appropriate 'MouseUp' event handler.</summary>
		public static event MouseButtonEventHandler MouseUp;

		/// <summary>The currently registered managed Noesis GUI appropriate 'KeyDown' event handler.</summary>
		public static event KeyEventHandler KeyDown;

		/// <summary>The currently registered managed Noesis GUI appropriate 'KeyUp' event handler.</summary>
		public static event KeyEventHandler KeyUp;

		#endregion Declare event handler

		#region Private attributes

		/// <summary>The GL window start time.</summary>
		static DateTime _startTime;

		#endregion Private attributes

		///////////////////////////////////////////////////////////////////////////////////////////////////
		/// M e t h o d e s
		///////////////////////////////////////////////////////////////////////////////////////////////////

		#region Event handler

		/// <summary>Raises the managed Noesis GUI 'Close' event from the native Noesis GUI 'Close' delegate callback.</summary>
		static void OnClose()
		{
			CloseEventHandler handler = Close;
			if (handler != null)
			{
				handler();
			}
		}

		/// <summary>Raises the managed Noesis GUI 'Tick' event from the native Noesis GUI 'Tick' delegate callback.</summary>
		static void OnTick()
		{
			TickEventHandler handler = Tick;
			if (handler != null)
			{
				handler((DateTime.Now - _startTime).TotalSeconds);
			}
		}

		/// <summary>Raises the managed Noesis GUI 'PreRender' event from the native Noesis GUI 'PreRender' delegate callback.</summary>
		static void OnPreRender()
		{
			RenderEventHandler handler = PreRender;
			if (handler != null)
			{
				handler();
			}
		}

		/// <summary>Raises the managed Noesis GUI 'PostRender' event from the native Noesis GUI 'PostRender' delegate callback.</summary>
		static void OnPostRender()
		{
			RenderEventHandler handler = PostRender;
			if (handler != null)
			{
				handler();
			}
		}

		/// <summary>Raises the managed Noesis GUI 'Resize' event from the native Noesis GUI 'Resize' delegate callback.</summary>
		static void OnResize(int width, int height)
		{
			ResizeEventHandler handler = Resize;
			if (handler != null)
			{
				handler(width, height);
			}
		}

		/// <summary>Raises the managed Noesis GUI 'MouseMove' event from the native Noesis GUI 'MouseMove' delegate callback.</summary>
		static void OnMouseMove(int x, int y)
		{
			MouseMoveEventHandler handler = MouseMove;
			if (handler != null)
			{
				handler(x, y);
			}
		}

		/// <summary>Raises the managed Noesis GUI 'MouseDown' event from the native Noesis GUI 'MouseDown' delegate callback.</summary>
		static void OnMouseDown(int x, int y, int button)
		{
			MouseButtonEventHandler handler = MouseDown;
			if (handler != null)
			{
				handler(x, y, (MouseButton)button);
			}
		}

		/// <summary>Raises the managed Noesis GUI 'MouseUp' event from the native Noesis GUI 'MouseUp' delegate callback.</summary>
		static void OnMouseUp(int x, int y, int button)
		{
			MouseButtonEventHandler handler = MouseUp;
			if (handler != null)
			{
				handler(x, y, (MouseButton)button);
			}
		}

		/// <summary>Raises the managed Noesis GUI 'KeyDown' event from the native Noesis GUI 'KeyDown' delegate callback.</summary>
		static void OnKeyDown(byte key, int specialKey, int modifiers)
		{
			KeyEventHandler handler = KeyDown;
			if (handler != null)
			{
				handler(key, (SpecialKey)specialKey, (ModifierKey)modifiers);
			}
		}

		/// <summary>Raises the managed Noesis GUI 'KeyUp' event from the native Noesis GUI 'KeyUp' delegate callback.</summary>
		static void OnKeyUp(byte key, int specialKey, int modifiers)
		{
			KeyEventHandler handler = KeyUp;
			if (handler != null)
			{
				handler(key, (SpecialKey)specialKey, (ModifierKey)modifiers);
			}
		}

		#endregion Event handler

		#region GL window handling

		private static CloseCallback _CloseCallback = OnClose;
		private static TickCallback _TickCallback = OnTick;
		private static PreRenderCallback _PreRenderCallback = OnPreRender;
		private static PostRenderCallback _PostRenderCallback = OnPostRender;
		private static ResizeCallback _ResizeCallback = OnResize;
		private static MouseMoveCallback _MouseMoveCallback = OnMouseMove;
		private static MouseButtonCallback _MouseDownCallback = OnMouseDown;
		private static MouseButtonCallback _MouseUpCallback = OnMouseUp;
		private static KeyboardCallback _KeyDownCallback = OnKeyDown;
		private static KeyboardCallback _KeyUpCallback = OnKeyUp;

		/// <summary>Init the GL window.</summary>
		/// <param name="width">The GL window client area width.</param>
		/// <param name="height">The GL window client area height.</param>
		/// <param name="title">The GL window title.</param>
		public static void Init(int width, int height, string title)
		{
			libGLUTWrapper.GLUT_Init(width, height, title);

			libGLUTWrapper.GLUT_RegisterClose(_CloseCallback);
			libGLUTWrapper.GLUT_RegisterTick(_TickCallback);
			libGLUTWrapper.GLUT_RegisterPreRender(_PreRenderCallback);
			libGLUTWrapper.GLUT_RegisterPostRender(_PostRenderCallback);
			libGLUTWrapper.GLUT_RegisterResize(_ResizeCallback);
			libGLUTWrapper.GLUT_RegisterMouse(_MouseMoveCallback, _MouseDownCallback, _MouseUpCallback);
			libGLUTWrapper.GLUT_RegisterKeyboard(_KeyDownCallback, _KeyUpCallback);

			_startTime = DateTime.Now;
		}

		/// <summary>Shutdown the GL window.</summary>
		public static void Shutdown()
		{
			libGLUTWrapper.GLUT_RegisterClose(null);
			libGLUTWrapper.GLUT_RegisterTick(null);
			libGLUTWrapper.GLUT_RegisterPreRender(null);
			libGLUTWrapper.GLUT_RegisterPostRender(null);
			libGLUTWrapper.GLUT_RegisterResize(null);
			libGLUTWrapper.GLUT_RegisterMouse(null, null, null);
			libGLUTWrapper.GLUT_RegisterKeyboard(null, null);
		}

		/// <summary>Run the GL window.</summary>
		public static void Run()
		{
			libGLUTWrapper.GLUT_Run();
		}

		#endregion GL window handling

	}
}