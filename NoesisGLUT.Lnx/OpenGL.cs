using System;
using System.Runtime.InteropServices;

namespace NoesisGLUT.Lnx
{

	//using GLboolean = System.Boolean;
	//using GLenum = System.UInt32;
	//using GLbitfield = System.UInt32;
	//using GLintptr = System.Int32;
	//using GLsizeiptr = System.Int32;
	//using GLsync = System.UInt32;
	//using GLhandle = System.UInt32;
	//using GLhalf = System.UInt16;
	//using GLvdpauSurface = System.Int32;
	//using GLfixed = System.Int32;

	//using GLchar = System.Byte;         // 1 byte
	//using GLbyte = System.SByte;        // 1 byte
	//using GLubyte = System.Byte;        // 1 byte
	//using GLshort = System.Int16;       // 2 bytes
	//using GLushort = System.UInt16;     // 2 bytes
	//using GLsizei = System.Int32;       // 4 bytes
	//using GLint = System.Int32;         // 4 bytes
	//using GLuint = System.UInt32;       // 4 bytes
	//using GLint64 = System.Int64;       // 8 bytes
	//using GLuint64 = System.UInt64;     // 8 bytes
	//using GLclampf = System.Single;     // single precision float in [0,1]
	//using GLclampd = System.Double;     // double precision float in [0,1]
	//using GLfloat = System.Single;      // 4 bytes
	//using GLdouble = System.Double;     // 8 bytes

	public static class FreeGLUT
	{
		// UInt64 fg_time_t

		private const string LIB_FreeGLUT = "libglut.so.3"; // "/usr/lib64/libglut.so.3"

		// fg_internal.h
		public enum fgDesiredVisibility
		{
			DesireHiddenState,
			DesireIconicState,
			DesireNormalState
		}

		// fg_internal_X11.h
		public struct SFG_PlatformWindowState
		{
			int             OldWidth;           /* Window width from before a resize */
			int             OldHeight;          /*   "    height  "    "    "   "    */
			bool            KeyRepeating;       /* Currently in repeat mode?         */    
		};

		// fg_internal.h
		public struct SFG_Node
		{
			IntPtr Next;
			IntPtr Prev;
		}

		// fg_internal.h
		public struct SFG_List
		{
			IntPtr First;
			IntPtr Last;
		};

		// fg_internal.h + fg_internal_X11.h
		public struct SFG_Context
		{
			public IntPtr    /* SFG_WindowHandleType */  Handle;    /* The window's handle                 */
			public IntPtr    /* SFG_WindowContextType */ Context;   /* The window's OpenGL/WGL context     */

			public IntPtr    /* SFG_PlatformContext */   pContext;    /* The window's FBConfig (X11) or device context (Windows) */

			public int                                   DoubleBuffered;  /* Treat the window as double-buffered */

			/* When drawing geometry to vertex attribute buffers, user specifies
			* the attribute indices for vertices, normals and/or texture coords
				* to freeglut. Those are stored here. */
				public int                                   attribute_v_coord;
			public int                                   attribute_v_normal;
			public int                                   attribute_v_texture;
		}

		// fg_internal.h
		public struct SFG_WindowState                        /* as per notes above, sizes always refer to the client area (thus without the window decorations) */
		{
			/* window state - size, position, look */
			int                   Xpos;               /* Window's top-left of client area, X-coordinate */
			int                   Ypos;               /* Window's top-left of client area, Y-coordinate */
			int                   Width;              /* Window's width in pixels          */
			int                   Height;             /* The same about the height         */
			bool                  Visible;            /* Is the window visible now? Not using fgVisibilityState as we only care if visible or not */
			int                   Cursor;             /* The currently selected cursor style */
			bool                  IsFullscreen;       /* is the window fullscreen?         */

			/* FreeGLUT operations are deferred, that is, window moving, resizing,
			* Z-order changing, making full screen or not do not happen immediately
				* upon the user's request, but only in the next iteration of the main
				* loop, before the display callback is called. This allows multiple
				* reshape, position, etc requests to be combined into one and is
				* compatible with the way GLUT does things. Callbacks get triggered
				* based on the feedback/messages/notifications from the window manager.
				* Below here we define what work should be done, as well as the relevant
				* parameters for this work. */
					uint                          WorkMask;           /* work (resize, etc) to be done on the window */
			int                           DesiredXpos;        /* desired X location */
			int                           DesiredYpos;        /* desired Y location */
			int                           DesiredWidth;       /* desired window width */
			int                           DesiredHeight;      /* desired window height */
			int                           DesiredZOrder;      /* desired window Z Order position */
			fgDesiredVisibility           DesiredVisibility;  /* desired visibility (hidden, iconic, shown/normal) */

			SFG_PlatformWindowState       pWState;    /* Window width/height (X11) or rectangle/style (Windows) from before a resize, and other stuff only needed on specific platforms */

			long                          JoystickPollRate;   /* The joystick polling rate         */
			UInt64                        JoystickLastPoll;   /* When the last poll happened       */
			int                           MouseX, MouseY;     /* The most recent mouse position    */
			bool                          IgnoreKeyRepeat;    /* Whether to ignore key repeat.     */
			bool                          VisualizeNormals;   /* When drawing objects, draw vectors representing the normals as well? */
		}

		// fg_internal.h
		public struct SFG_Menu
		{
			SFG_Node                       Node;
			IntPtr                         UserData;     /* User data passed back at callback   */
			int                            ID;           /* The global menu ID                  */
			SFG_List                       Entries;      /* The menu entries list               */
			IntPtr                         Callback;     /* The menu callback                   */
			IntPtr                         Destroy;      /* Destruction callback                */
			bool                           IsActive;     /* Is the menu selected?               */
			IntPtr                         Font;         /* Font to be used for displaying this menu */
			int                            Width;        /* Menu box width in pixels            */
			int                            Height;       /* Menu box height in pixels           */
			int                            X, Y;         /* Menu box raster position            */

			IntPtr    /* SFG_MenuEntry* */ ActiveEntry;  /* Currently active entry in the menu  */
			IntPtr    /* SFG_Window* */    Window;       /* Window for menu                     */
			IntPtr    /* SFG_Window*  */   ParentWindow; /* Window in which the menu is invoked */
		}

		// fg_internal.h
		public struct SFG_Window
		{
			public SFG_Node                      Node;
			public int                           ID;                     /* Window's ID number        */

			public SFG_Context                   Window;                 /* Window and OpenGL context */
			public SFG_WindowState               State;                  /* The window state          */
			public IntPtr    /* SFG_Proc[] */    CallBacks;              /* Array of window callbacks */
			public IntPtr                        UserData ;              /* For use by user           */

			public IntPtr    /* SFG_Menu*[] */   Menu;                   /* Menus appended to window  */
			public IntPtr    /* SFG_Menu* */     ActiveMenu;             /* The window's active menu  */

			public IntPtr    /* SFG_Window* */   Parent;                 /* The parent to this window */
			public SFG_List                      Children;               /* The subwindows d.l. list  */

			public bool                          IsMenu;                 /* Set to 1 if we are a menu */
		}

		#region Option flags (only FreeGLUT)

		public const uint GLUT_ACTION_ON_WINDOW_CLOSE    = 0x01F9;

		#endregion Option flags (only FreeGLUT)

		#region Close action behaviour (only FreeGLUT)

		public const int GLUT_ACTION_EXIT                 = 0;
		public const int GLUT_ACTION_GLUTMAINLOOP_RETURNS = 1;
		public const int GLUT_ACTION_CONTINUE_EXECUTION   = 2;

		#endregion Close action behaviour (only FreeGLUT)

		[DllImport(LIB_FreeGLUT, EntryPoint="fgWindowByID", CallingConvention=CallingConvention.Cdecl)]
		public static extern IntPtr WindowByID (int parentID);

		[DllImport(LIB_FreeGLUT, EntryPoint="glutGetWindow", CallingConvention=CallingConvention.Cdecl)]
		public static extern int glutGetWindow();

		[DllImport(LIB_FreeGLUT, EntryPoint="glutInit", CallingConvention=CallingConvention.Cdecl)]
		[System.Security.SuppressUnmanagedCodeSecurity()]
		private static extern void glutMainLoopEvent();

		[DllImport(LIB_FreeGLUT, EntryPoint="glutInit", CallingConvention=CallingConvention.Cdecl)]
		[System.Security.SuppressUnmanagedCodeSecurity()]
		private static extern void glutLeaveMainLoop();

		[DllImport(LIB_FreeGLUT, EntryPoint="glutInit", CallingConvention=CallingConvention.Cdecl)]
		[System.Security.SuppressUnmanagedCodeSecurity()]
		private static extern void glutExit();
	}

	// https://searchcode.com/file/75042320/include/gl/glut.h
	// https://github.com/carmack78/OpenGLDotNet/blob/master/Source/OpenGL/FG.Functions.cs
	public static class GLUT
	{
		private const string LIB_GLUT = "libglut.so.3"; // "/usr/lib64/libglut.so.3"

		public enum GetParam : Int32
		{
			GLUT_WINDOW_X                     = 0x0064,
			GLUT_WINDOW_Y                     = 0x0065,
			GLUT_WINDOW_WIDTH                 = 0x0066,
			GLUT_WINDOW_HEIGHT                = 0x0067,
			GLUT_WINDOW_BUFFER_SIZE           = 0x0068,
			GLUT_WINDOW_STENCIL_SIZE          = 0x0069,
			GLUT_WINDOW_DEPTH_SIZE            = 0x006A,
			GLUT_WINDOW_RED_SIZE              = 0x006B,
			GLUT_WINDOW_GREEN_SIZE            = 0x006C,
			GLUT_WINDOW_BLUE_SIZE             = 0x006D,
			GLUT_WINDOW_ALPHA_SIZE            = 0x006E,
			GLUT_WINDOW_ACCUM_RED_SIZE        = 0x006F,
			GLUT_WINDOW_ACCUM_GREEN_SIZE      = 0x0070,
			GLUT_WINDOW_ACCUM_BLUE_SIZE       = 0x0071,
			GLUT_WINDOW_ACCUM_ALPHA_SIZE      = 0x0072,
			GLUT_WINDOW_DOUBLEBUFFER          = 0x0073,
			GLUT_WINDOW_RGBA                  = 0x0074,
			GLUT_WINDOW_PARENT                = 0x0075,
			GLUT_WINDOW_NUM_CHILDREN          = 0x0076,
			GLUT_WINDOW_COLORMAP_SIZE         = 0x0077,
			GLUT_WINDOW_NUM_SAMPLES           = 0x0078,
			GLUT_WINDOW_STEREO                = 0x0079,
			GLUT_WINDOW_CURSOR                = 0x007A,

			GLUT_SCREEN_WIDTH                 = 0x00C8,
			GLUT_SCREEN_HEIGHT                = 0x00C9,
			GLUT_SCREEN_WIDTH_MM              = 0x00CA,
			GLUT_SCREEN_HEIGHT_MM             = 0x00CB,
			GLUT_MENU_NUM_ITEMS               = 0x012C,
			GLUT_DISPLAY_MODE_POSSIBLE        = 0x0190,
			GLUT_INIT_WINDOW_X                = 0x01F4,
			GLUT_INIT_WINDOW_Y                = 0x01F5,
			GLUT_INIT_WINDOW_WIDTH            = 0x01F6,
			GLUT_INIT_WINDOW_HEIGHT           = 0x01F7,
			GLUT_INIT_DISPLAY_MODE            = 0x01F8,
			GLUT_ELAPSED_TIME                 = 0x02BC,
			GLUT_WINDOW_FORMAT_ID             = 0x007B
		}

		public enum DeviceParam
		{
			GLUT_HAS_KEYBOARD                 = 0x0258,
			GLUT_HAS_MOUSE                    = 0x0259,
			GLUT_HAS_SPACEBALL                = 0x025A,
			GLUT_HAS_DIAL_AND_BUTTON_BOX      = 0x025B,
			GLUT_HAS_TABLET                   = 0x025C,
			GLUT_NUM_MOUSE_BUTTONS            = 0x025D,
			GLUT_NUM_SPACEBALL_BUTTONS        = 0x025E,
			GLUT_NUM_BUTTON_BOX_BUTTONS       = 0x025F,
			GLUT_NUM_DIALS                    = 0x0260,
			GLUT_NUM_TABLET_BUTTONS           = 0x0261,
			GLUT_DEVICE_IGNORE_KEY_REPEAT     = 0x0262,
			GLUT_DEVICE_KEY_REPEAT            = 0x0263,
			GLUT_HAS_JOYSTICK                 = 0x0264,
			GLUT_OWNS_JOYSTICK                = 0x0265,
			GLUT_JOYSTICK_BUTTONS             = 0x0266,
			GLUT_JOYSTICK_AXES                = 0x0267,
			GLUT_JOYSTICK_POLL_RATE           = 0x0268
		}

		#region Display mode bit masks

		public const int GLUT_RGB                       = 0;
		public const int GLUT_RGBA                      = GLUT_RGB;
		public const int GLUT_INDEX                     = 1;
		public const int GLUT_SINGLE                    = 0;
		public const int GLUT_DOUBLE                    = 2;
		public const int GLUT_ACCUM                     = 4;
		public const int GLUT_ALPHA                     = 8;
		public const int GLUT_DEPTH                     = 16;
		public const int GLUT_STENCIL                   = 32;
		// GLUT_API_VERSION >= 2
		public const int GLUT_MULTISAMPLE               = 128;
		public const int GLUT_STEREO                    = 256;
		// GLUT_API_VERSION >= 3
		public const int GLUT_LUMINANCE                 = 512;

		#endregion Display mode bit masks

		#region Mouse buttons

		public const int GLUT_LEFT_BUTTON               = 0;
		public const int GLUT_MIDDLE_BUTTON             = 1;
		public const int GLUT_RIGHT_BUTTON              = 2;

		#endregion Mouse buttons

		#region Mouse buttons callback state

		public const int GLUT_DOWN                      = 0;
		public const int GLUT_UP                        = 1;

		#endregion Mouse buttons callback state

		#region Special keys

		/* function keys */
		// GLUT_API_VERSION >= 2
		public const int GLUT_KEY_F1                    = 1;
		public const int GLUT_KEY_F2                    = 2;
		public const int GLUT_KEY_F3                    = 3;
		public const int GLUT_KEY_F4                    = 4;
		public const int GLUT_KEY_F5                    = 5;
		public const int GLUT_KEY_F6                    = 6;
		public const int GLUT_KEY_F7                    = 7;
		public const int GLUT_KEY_F8                    = 8;
		public const int GLUT_KEY_F9                    = 9;
		public const int GLUT_KEY_F10                   = 10;
		public const int GLUT_KEY_F11                   = 11;
		public const int GLUT_KEY_F12                   = 12;

		/* directional keys */
		// GLUT_API_VERSION >= 2
		public const int GLUT_KEY_LEFT                  = 100;
		public const int GLUT_KEY_UP                    = 101;
		public const int GLUT_KEY_RIGHT                 = 102;
		public const int GLUT_KEY_DOWN                  = 103;
		public const int GLUT_KEY_PAGE_UP               = 104;
		public const int GLUT_KEY_PAGE_DOWN             = 105;
		public const int GLUT_KEY_HOME                  = 106;
		public const int GLUT_KEY_END                   = 107;
		public const int GLUT_KEY_INSERT                = 108;

		#endregion Special keys

		#region Modifiers return mask

		public const int GLUT_ACTIVE_SHIFT              = 1;
		public const int GLUT_ACTIVE_CTRL               = 2;
		public const int GLUT_ACTIVE_ALT                = 4;

		#endregion Modifiers return mask

		#region Initialization and cleanup

		[DllImport(LIB_GLUT, EntryPoint="glutInit", CallingConvention=CallingConvention.Cdecl)]
		[System.Security.SuppressUnmanagedCodeSecurity()]
		private static extern void glutInit(ref int argcp, string[] argv);

		public static void Init(int argc, string[] argv)
		{ int argcp = argc; glutInit(ref argcp, argv); }

		// void glutExitFunc(void (* callback)(int arg));
		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate void ExitProcDelegate(int arg);

		[DllImport(LIB_GLUT, EntryPoint="__glutInitWithExit", CallingConvention=CallingConvention.Cdecl)]
		[System.Security.SuppressUnmanagedCodeSecurity()]
		private static extern void glutInitWithExit(ref int argcp, string[] argv, ExitProcDelegate exitFunc);

		[DllImport(LIB_GLUT, EntryPoint="glutInitDisplayMode", CallingConvention=CallingConvention.Cdecl)]
		[System.Security.SuppressUnmanagedCodeSecurity()]
		public static extern void InitDisplayMode(int argcp);

		[DllImport(LIB_GLUT, EntryPoint="glutInitWindowPosition", CallingConvention=CallingConvention.Cdecl)]
		[System.Security.SuppressUnmanagedCodeSecurity()]
		public static extern void InitWindowPosition(int x, int y);

		[DllImport(LIB_GLUT, EntryPoint="glutInitWindowSize", CallingConvention=CallingConvention.Cdecl)]
		[System.Security.SuppressUnmanagedCodeSecurity()]
		public static extern void InitWindowSize(int widt, int height);

		[DllImport(LIB_GLUT, EntryPoint="glutExit", CallingConvention=CallingConvention.Cdecl)]
		[System.Security.SuppressUnmanagedCodeSecurity()]
		public static extern void Exit();

		#endregion Initialization and cleanup

		#region Window handling

		[DllImport(LIB_GLUT, EntryPoint="glutCreateWindow", CallingConvention=CallingConvention.Cdecl)]
		[System.Security.SuppressUnmanagedCodeSecurity()]
		public static extern int CreateWindow(string title);

		[DllImport(LIB_GLUT, EntryPoint="glutCreateSubWindow", CallingConvention=CallingConvention.Cdecl)]
		[System.Security.SuppressUnmanagedCodeSecurity()]
		public static extern int CreateSubWindow(int parentWindowHandle, int x, int y, int width, int height);

		[DllImport(LIB_GLUT, EntryPoint="glutDestroyWindow", CallingConvention=CallingConvention.Cdecl)]
		[System.Security.SuppressUnmanagedCodeSecurity()]
		public static extern void DestroyWindow(int win);

		[DllImport(LIB_GLUT, EntryPoint="glutPostRedisplay", CallingConvention=CallingConvention.Cdecl)]
		[System.Security.SuppressUnmanagedCodeSecurity()]
		public static extern void PostRedisplay();

		[DllImport(LIB_GLUT, EntryPoint="glutSwapBuffers", CallingConvention=CallingConvention.Cdecl)]
		[System.Security.SuppressUnmanagedCodeSecurity()]
		public static extern void SwapBuffers();

		[DllImport(LIB_GLUT, EntryPoint="glutGetWindow", CallingConvention=CallingConvention.Cdecl)]
		[System.Security.SuppressUnmanagedCodeSecurity()]
		public static extern int GetWindow();

		[DllImport(LIB_GLUT, EntryPoint="glutSetWindow", CallingConvention=CallingConvention.Cdecl)]
		[System.Security.SuppressUnmanagedCodeSecurity()]
		public static extern void SetWindow(int windowHandle);

		[DllImport(LIB_GLUT, EntryPoint="glutSetWindowTitle", CallingConvention=CallingConvention.Cdecl)]
		[System.Security.SuppressUnmanagedCodeSecurity()]
		public static extern void SetWindowTitle(string title);

		[DllImport(LIB_GLUT, EntryPoint="glutSetIconTitle", CallingConvention=CallingConvention.Cdecl)]
		[System.Security.SuppressUnmanagedCodeSecurity()]
		public static extern void SetIconTitle(string title);

		[DllImport(LIB_GLUT, EntryPoint="glutPositionWindow", CallingConvention=CallingConvention.Cdecl)]
		[System.Security.SuppressUnmanagedCodeSecurity()]
		public static extern void PositionWindow(int x, int y);

		[DllImport(LIB_GLUT, EntryPoint="glutReshapeWindow", CallingConvention=CallingConvention.Cdecl)]
		[System.Security.SuppressUnmanagedCodeSecurity()]
		public static extern void ReshapeWindow(int width, int height);

		[DllImport(LIB_GLUT, EntryPoint="glutPopWindow", CallingConvention=CallingConvention.Cdecl)]
		[System.Security.SuppressUnmanagedCodeSecurity()]
		public static extern void PopWindow();

		[DllImport(LIB_GLUT, EntryPoint="glutPushWindow", CallingConvention=CallingConvention.Cdecl)]
		[System.Security.SuppressUnmanagedCodeSecurity()]
		public static extern void PushWindow();

		[DllImport(LIB_GLUT, EntryPoint="glutIconifyWindow", CallingConvention=CallingConvention.Cdecl)]
		[System.Security.SuppressUnmanagedCodeSecurity()]
		public static extern void IconifyWindow();

		[DllImport(LIB_GLUT, EntryPoint="glutShowWindow", CallingConvention=CallingConvention.Cdecl)]
		[System.Security.SuppressUnmanagedCodeSecurity()]
		public static extern void ShowWindow();

		[DllImport(LIB_GLUT, EntryPoint="glutHideWindow", CallingConvention=CallingConvention.Cdecl)]
		[System.Security.SuppressUnmanagedCodeSecurity()]
		public static extern void HideWindow();

		// GLUT_API_VERSION >= 3
		[DllImport(LIB_GLUT, EntryPoint="glutFullScreen", CallingConvention=CallingConvention.Cdecl)]
		[System.Security.SuppressUnmanagedCodeSecurity()]
		public static extern void FullScreen();

		[DllImport(LIB_GLUT, EntryPoint="glutSetCursor", CallingConvention=CallingConvention.Cdecl)]
		[System.Security.SuppressUnmanagedCodeSecurity()]
		public static extern void SetCursor(int cursor);

		#endregion Window handling

		[DllImport(LIB_GLUT, EntryPoint="glutGet", CallingConvention=CallingConvention.Cdecl)]
		public static extern int Get([MarshalAs(UnmanagedType.I4)] GetParam param);

		[DllImport(LIB_GLUT, EntryPoint="glutDeviceGet", CallingConvention=CallingConvention.Cdecl)]
		public static extern int DeviceGet([MarshalAs(UnmanagedType.I4)] DeviceParam param);

		[DllImport(LIB_GLUT, EntryPoint="glutSetOption", CallingConvention=CallingConvention.Cdecl)]
		public static extern void SetOption(uint optionFlag, int value);

		[DllImport(LIB_GLUT, EntryPoint="glutMainLoop", CallingConvention=CallingConvention.Cdecl)]
		[System.Security.SuppressUnmanagedCodeSecurity()]
		public static extern void MainLoop();

		#region Callbacks (keyboard)

		// void glutKeyboardFunc(void (* callback)(unsigned char key, int x, int y));
		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate void KeyboardProcDelegate(byte key, int x, int y);

		[DllImport(LIB_GLUT, EntryPoint="glutKeyboardFunc", CallingConvention=CallingConvention.Cdecl)]
		[System.Security.SuppressUnmanagedCodeSecurity()]
		public static extern void KeyboardFunc(KeyboardProcDelegate keyboardProc);

		// void glutKeyboardUpFunc(void (* callback)(unsigned char key, int x, int y));
		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate void KeyboardUpProcDelegate(byte key, int x, int y);

		[DllImport(LIB_GLUT, EntryPoint="glutKeyboardUpFunc", CallingConvention=CallingConvention.Cdecl)]
		[System.Security.SuppressUnmanagedCodeSecurity()]
		public static extern void KeyboardUpFunc(KeyboardUpProcDelegate keyboardUpProc);

		// void glutSpecialFunc(void (* callback)(int key, int x, int y));
		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate void SpecialProcDelegate(int key, int x, int y);

		[DllImport(LIB_GLUT, EntryPoint="glutSpecialFunc", CallingConvention=CallingConvention.Cdecl)]
		[System.Security.SuppressUnmanagedCodeSecurity()]
		public static extern void SpecialFunc(SpecialProcDelegate specialProc);

		// void glutSpecialUpFunc(void (* callback)(int key, int x, int y));
		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate void SpecialUpProcDelegate(int key, int x, int y);

		[DllImport(LIB_GLUT, EntryPoint="glutSpecialUpFunc", CallingConvention=CallingConvention.Cdecl)]
		[System.Security.SuppressUnmanagedCodeSecurity()]
		public static extern void SpecialUpFunc(SpecialUpProcDelegate specialUpProc);

		#endregion Callbacks (keyboard)

		#region Callbacks

		// void glutDisplayFunc(void (* callback)(void));
		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate void DisplayProcDelegate();

		[DllImport(LIB_GLUT, EntryPoint="glutDisplayFunc", CallingConvention=CallingConvention.Cdecl)]
		[System.Security.SuppressUnmanagedCodeSecurity()]
		public static extern void DisplayFunc(DisplayProcDelegate displayProc);

		// void glutReshapeFunc(void (* callback)(int width, int height));
		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate void ReshapeProcDelegate(int width, int height);

		[DllImport(LIB_GLUT, EntryPoint="glutReshapeFunc", CallingConvention=CallingConvention.Cdecl)]
		[System.Security.SuppressUnmanagedCodeSecurity()]
		public static extern void ReshapeFunc(ReshapeProcDelegate reshapeProc);

		// void glutMouseFunc(void (* callback)(int button, int state, int x, int y));
		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate void MouseProcDelegate(int button, int state, int x, int y);

		[DllImport(LIB_GLUT, EntryPoint="glutMouseFunc", CallingConvention=CallingConvention.Cdecl)]
		[System.Security.SuppressUnmanagedCodeSecurity()]
		public static extern void MouseFunc(MouseProcDelegate mouseProc);

		// void glutMotionFunc(void (* callback)(int x, int y));
		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate void MotionProcDelegate(int x, int y);

		[DllImport(LIB_GLUT, EntryPoint="glutMotionFunc", CallingConvention=CallingConvention.Cdecl)]
		[System.Security.SuppressUnmanagedCodeSecurity()]
		public static extern void MotionFunc(MotionProcDelegate motionProc);

		// void glutPassiveMotionFunc(void (* callback)(int x, int y));
		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate void PassiveMotionProcDelegate(int x, int y);

		[DllImport(LIB_GLUT, EntryPoint="glutPassiveMotionFunc", CallingConvention=CallingConvention.Cdecl)]
		[System.Security.SuppressUnmanagedCodeSecurity()]
		public static extern void PassiveMotionFunc(PassiveMotionProcDelegate passiveMotionProc);

		#endregion Callbacks

		[DllImport(LIB_GLUT, EntryPoint="glutGetModifiers", CallingConvention=CallingConvention.Cdecl)]
		[System.Security.SuppressUnmanagedCodeSecurity()]
		public static extern int GetModifiers();
	}

	public static class GLU
	{
		private const string LIB_GLU = "libGLU.so"; // "/usr/lib64/libGLU.so"

		[DllImport(LIB_GLU, EntryPoint="gluOrtho2D", CallingConvention=CallingConvention.Cdecl)]
		[System.Security.SuppressUnmanagedCodeSecurity()]
		public static extern void Ortho2D (double left, double right, double bottom, double top);
	}

	// https://github.com/carmack78/OpenGLDotNet/blob/master/Source/OpenGL/GL.CoreDelegates.cs
	public static class GL
	{
		private const string LIB_GL = "libGL.so"; // "/usr/lib64/libGL.so"

		#region Boolean values

		public const bool GL_FALSE             = false;
		public const bool GL_TRUE              = true;

		#endregion Boolean values

		#region Data types

		public const int GL_BYTE                = 0x1400;
		public const int GL_UNSIGNED_BYTE       = 0x1401;
		public const int GL_SHORT               = 0x1402;
		public const int GL_UNSIGNED_SHORT      = 0x1403;
		public const int GL_INT                 = 0x1404;
		public const int GL_UNSIGNED_INT        = 0x1405;
		public const int GL_FLOAT               = 0x1406;
		public const int GL_DOUBLE              = 0x140A;
		public const int GL_2_BYTES             = 0x1407;
		public const int GL_3_BYTES             = 0x1408;
		public const int GL_4_BYTES             = 0x1409;

		#endregion Data types

		public const uint GL_DEPTH_BUFFER_BIT   = 0x00000100;
		public const uint GL_STENCIL_BUFFER_BIT = 0x00000400;
		public const uint GL_COLOR_BUFFER_BIT   = 0x00004000;

		public const int GL_FRAMEBUFFER            = 0x8D40;

		#region Primitives

		public const int GL_POINTS                 = 0x0000;
		public const int GL_LINES                  = 0x0001;
		public const int GL_LINE_LOOP              = 0x0002;
		public const int GL_LINE_STRIP             = 0x0003;
		public const int GL_TRIANGLES              = 0x0004;
		public const int GL_TRIANGLE_STRIP         = 0x0005;
		public const int GL_TRIANGLE_FAN           = 0x0006;
		public const int GL_QUADS                  = 0x0007;
		public const int GL_QUAD_STRIP             = 0x0008;
		public const int GL_POLYGON                = 0x0009;

		#endregion Primitives

		#region Lines

		public const int GL_LINE_SMOOTH            = 0x0B20;
		public const int GL_LINE_STIPPLE           = 0x0B24;
		public const int GL_LINE_STIPPLE_PATTERN   = 0x0B25;
		public const int GL_LINE_STIPPLE_REPEAT    = 0x0B26;
		public const int GL_LINE_WIDTH             = 0x0B21;
		public const int GL_LINE_WIDTH_GRANULARITY = 0x0B23;
		public const int GL_LINE_WIDTH_RANGE       = 0x0B22;

		#endregion Lines

		#region Polygons

		public const int GL_POINT                  = 0x1B00;
		public const int GL_LINE                   = 0x1B01;
		public const int GL_FILL                   = 0x1B02;
		public const int GL_CCW                    = 0x0901;
		public const int GL_CW                     = 0x0900;
		public const int GL_FRONT                  = 0x0404;
		public const int GL_BACK                   = 0x0405;
		public const int GL_CULL_FACE              = 0x0B44;
		public const int GL_CULL_FACE_MODE         = 0x0B45;
		public const int GL_POLYGON_SMOOTH         = 0x0B41;
		public const int GL_POLYGON_STIPPLE        = 0x0B42;
		public const int GL_FRONT_FACE             = 0x0B46;
		public const int GL_POLYGON_MODE           = 0x0B40;
		public const int GL_POLYGON_OFFSET_FACTOR  = 0x3038;
		public const int GL_POLYGON_OFFSET_UNITS   = 0x2A00;
		public const int GL_POLYGON_OFFSET_POINT   = 0x2A01;
		public const int GL_POLYGON_OFFSET_LINE    = 0x2A02;
		public const int GL_POLYGON_OFFSET_FILL    = 0x8037;

		#endregion Polygons

		#region Display lists

		public const int GL_COMPILE             = 0x1300;
		public const int GL_COMPILE_AND_EXECUTE = 0x1301;
		public const int GL_LIST_BASE           = 0x0B32;
		public const int GL_LIST_INDEX          = 0x0B33;
		public const int GL_LIST_MODE           = 0x0B30;

		#endregion Display lists

		#region Depth buffer

		public const int GL_NEVER              = 0x0200;
		public const int GL_LESS               = 0x0201;
		public const int GL_GEQUAL             = 0x0206;
		public const int GL_LEQUAL             = 0x0203;
		public const int GL_GREATER            = 0x0204;
		public const int GL_NOTEQUAL           = 0x0205;
		public const int GL_EQUAL              = 0x0202;
		public const int GL_ALWAYS             = 0x0207;
		public const int GL_DEPTH_TEST         = 0x0B71;
		public const int GL_DEPTH_BITS         = 0x0D56;
		public const int GL_DEPTH_CLEAR_VALUE  = 0x0B73;
		public const int GL_DEPTH_FUNC         = 0x0B74;
		public const int GL_DEPTH_RANGE        = 0x0B70;
		public const int GL_DEPTH_WRITEMASK    = 0x0B72;
		public const int GL_DEPTH_COMPONENT    = 0x1902;

		#endregion Depth buffer

		#region Lighting

		public const int GL_LIGHTING                 = 0x0B50;
		public const int GL_LIGHT0                   = 0x4000;
		public const int GL_LIGHT1                   = 0x4001;
		public const int GL_LIGHT2                   = 0x4002;
		public const int GL_LIGHT3                   = 0x4003;
		public const int GL_LIGHT4                   = 0x4004;
		public const int GL_LIGHT5                   = 0x4005;
		public const int GL_LIGHT6                   = 0x4006;
		public const int GL_LIGHT7                   = 0x4007;
		public const int GL_SPOT_EXPONENT            = 0x1205;
		public const int GL_SPOT_CUTOFF              = 0x1206;
		public const int GL_CONSTANT_ATTENUATION     = 0x1207;
		public const int GL_LINEAR_ATTENUATION       = 0x1208;
		public const int GL_QUADRATIC_ATTENUATION    = 0x1209;
		public const int GL_AMBIENT                  = 0x1200;
		public const int GL_DIFFUSE                  = 0x1201;
		public const int GL_SPECULAR                 = 0x1202;
		public const int GL_SHININESS                = 0x1601;
		public const int GL_EMISSION                 = 0x1600;
		public const int GL_POSITION                 = 0x1203;
		public const int GL_SPOT_DIRECTION           = 0x1204;
		public const int GL_AMBIENT_AND_DIFFUSE      = 0x1602;
		public const int GL_COLOR_INDEXES            = 0x1603;
		public const int GL_LIGHT_MODEL_TWO_SIDE     = 0x0B52;
		public const int GL_LIGHT_MODEL_LOCAL_VIEWER = 0x0B51;
		public const int GL_LIGHT_MODEL_AMBIENT      = 0x0B53;
		public const int GL_FRONT_AND_BACK           = 0x0408;
		public const int GL_SHADE_MODEL              = 0x0B54;
		public const int GL_FLAT                     = 0x1D00;
		public const int GL_SMOOTH                   = 0x1D01;
		public const int GL_COLOR_MATERIAL           = 0x0B57;
		public const int GL_COLOR_MATERIAL_FACE      = 0x0B55;
		public const int GL_COLOR_MATERIAL_PARAMETER = 0x0B56;
		public const int GL_NORMALIZE                = 0x0BA1;

		#endregion Lighting

		#region User clipping planes

		public const int GL_CLIP_PLANE0              = 0x3000;
		public const int GL_CLIP_PLANE1              = 0x3001;
		public const int GL_CLIP_PLANE2              = 0x3002;
		public const int GL_CLIP_PLANE3              = 0x3003;
		public const int GL_CLIP_PLANE4              = 0x3004;
		public const int GL_CLIP_PLANE5              = 0x3005;

		#endregion User clipping planes

		#region Accumulation buffer

		public const int GL_ACCUM_RED_BITS           = 0x0D58;
		public const int GL_ACCUM_GREEN_BITS         = 0x0D59;
		public const int GL_ACCUM_BLUE_BITS          = 0x0D5A;
		public const int GL_ACCUM_ALPHA_BITS         = 0x0D5B;
		public const int GL_ACCUM_CLEAR_VALUE        = 0x0B80;
		public const int GL_ACCUM                    = 0x0100;
		public const int GL_ADD                      = 0x0104;
		public const int GL_LOAD                     = 0x0101;
		public const int GL_MULT                     = 0x0103;
		public const int GL_RETURN                   = 0x0102;

		#endregion Accumulation buffer

		#region Alpha testing

		public const int GL_ALPHA_TEST               = 0x0BC0;
		public const int GL_ALPHA_TEST_REF           = 0x0BC2;
		public const int GL_ALPHA_TEST_FUNC          = 0x0BC1;

		#endregion Alpha testing

		#region Blending

		public const int GL_BLEND                    = 0x0BE2;
		public const int GL_BLEND_SRC                = 0x0BE1;
		public const int GL_BLEND_DST                = 0x0BE0;
		public const int GL_ZERO                     = 0;
		public const int GL_ONE                      = 1;
		public const int GL_SRC_COLOR                = 0x0300;
		public const int GL_ONE_MINUS_SRC_COLOR      = 0x0301;
		public const int GL_DST_COLOR                = 0x0306;
		public const int GL_ONE_MINUS_DST_COLOR      = 0x0307;
		public const int GL_SRC_ALPHA                = 0x0302;
		public const int GL_ONE_MINUS_SRC_ALPHA      = 0x0303;
		public const int GL_DST_ALPHA                = 0x0304;
		public const int GL_ONE_MINUS_DST_ALPHA      = 0x0305;
		public const int GL_SRC_ALPHA_SATURATE       = 0x0308;
		public const int GL_CONSTANT_COLOR           = 0x8001;
		public const int GL_ONE_MINUS_CONSTANT_COLOR = 0x8002;
		public const int GL_CONSTANT_ALPHA           = 0x8003;
		public const int GL_ONE_MINUS_CONSTANT_ALPHA = 0x8004;

		#endregion Blending

		#region Render Mode

		public const int GL_FEEDBACK                 = 0x1C01;
		public const int GL_RENDER                   = 0x1C00;
		public const int GL_SELECT                   = 0x1C02;

		#endregion Render Mode

		#region Alpha logic ops

		public const int GL_LOGIC_OP                 = 0x0BF1;
		public const int GL_LOGIC_OP_MODE            = 0x0BF0;
		public const int GL_CLEAR                    = 0x1500;
		public const int GL_SET                      = 0x150F;
		public const int GL_COPY                     = 0x1503;
		public const int GL_COPY_INVERTED            = 0x150C;
		public const int GL_NOOP                     = 0x1505;
		public const int GL_INVERT                   = 0x150A;
		public const int GL_AND                      = 0x1501;
		public const int GL_NAND                     = 0x150E;
		public const int GL_OR                       = 0x1507;
		public const int GL_NOR                      = 0x1508;
		public const int GL_XOR                      = 0x1506;
		public const int GL_EQUIV                    = 0x1509;
		public const int GL_AND_REVERSE              = 0x1502;
		public const int GL_AND_INVERTED             = 0x1504;
		public const int GL_OR_REVERSE               = 0x150B;
		public const int GL_OR_INVERTED              = 0x150D;

		#endregion Alpha logic ops

		#region Alpha stencil

		public const int GL_STENCIL_TEST             = 0x0B90;
		public const int GL_STENCIL_WRITEMASK        = 0x0B98;
		public const int GL_STENCIL_BITS             = 0x0D57;
		public const int GL_STENCIL_FUNC             = 0x0B92;
		public const int GL_STENCIL_VALUE_MASK       = 0x0B93;
		public const int GL_STENCIL_REF              = 0x0B97;
		public const int GL_STENCIL_FAIL             = 0x0B94;
		public const int GL_STENCIL_PASS_DEPTH_PASS  = 0x0B96;
		public const int GL_STENCIL_PASS_DEPTH_FAIL  = 0x0B95;
		public const int GL_STENCIL_CLEAR_VALUE      = 0x0B91;
		public const int GL_STENCIL_INDEX            = 0x1901;
		public const int GL_KEEP                     = 0x1E00;
		public const int GL_REPLACE                  = 0x1E01;
		public const int GL_INCR                     = 0x1E02;
		public const int GL_DECR                     = 0x1E03;

		#endregion Alpha stencil

		#region Hints

		public const int GL_FOG_HINT                     = 0x0C54;
		public const int GL_LINE_SMOOTH_HINT             = 0x0C52;
		public const int GL_PERSPECTIVE_CORRECTION_HINT  = 0x0C50;
		public const int GL_POINT_SMOOTH_HINT            = 0x0C51;
		public const int GL_POLYGON_SMOOTH_HINT          = 0x0C53;
		public const int GL_DONT_CARE                    = 0x1100;
		public const int GL_FASTEST                      = 0x1101;
		public const int GL_NICEST                       = 0x1102;

		#endregion Hints

		#region Scissor box

		public const int GL_SCISSOR_TEST                 = 0x0C11;
		public const int GL_SCISSOR_BOX                  = 0x0C10;

		#endregion Scissor box

		/*
         * 1.0 Extensions
        */

		#region GL_EXT_blend_minmax and GL_EXT_blend_color

		public const int GL_CONSTANT_COLOR_EXT           = 0x8001;
		public const int GL_ONE_MINUS_CONSTANT_COLOR_EXT = 0x8002;
		public const int GL_CONSTANT_ALPHA_EXT           = 0x8003;
		public const int GL_ONE_MINUS_CONSTANT_ALPHA_EXT = 0x8004;
		public const int GL_BLEND_EQUATION_EXT           = 0x8009;
		public const int GL_MIN_EXT                      = 0x8007;
		public const int GL_MAX_EXT                      = 0x8008;
		public const int GL_FUNC_ADD_EXT                 = 0x8006;
		public const int GL_FUNC_SUBTRACT_EXT            = 0x800A;
		public const int GL_FUNC_REVERSE_SUBTRACT_EXT    = 0x800B;
		public const int GL_BLEND_COLOR_EXT              = 0x8005;

		#endregion GL_EXT_blend_minmax and GL_EXT_blend_color

		#region GL_EXT_polygon_offset

		public const int GL_POLYGON_OFFSET_EXT           = 0x8037;
		public const int GL_POLYGON_OFFSET_FACTOR_EXT    = 0x8038;
		public const int GL_POLYGON_OFFSET_BIAS_EXT      = 0x8039;

		#endregion GL_EXT_blend_minmax and GL_EXT_blend_color

		[DllImport(LIB_GL, EntryPoint="glClearColor", CallingConvention=CallingConvention.Cdecl)]
		[System.Security.SuppressUnmanagedCodeSecurity()]
		public static extern void ClearColor(float red, float green, float blue, float alpha);

		[DllImport(LIB_GL, EntryPoint="glClear", CallingConvention=CallingConvention.Cdecl)]
		[System.Security.SuppressUnmanagedCodeSecurity()]
		public static extern void Clear(uint mask);

		[DllImport(LIB_GL, EntryPoint="glColor3f", CallingConvention=CallingConvention.Cdecl)]
		[System.Security.SuppressUnmanagedCodeSecurity()]
		public static extern void Color3f(float red, float green, float blue);

		[DllImport(LIB_GL, EntryPoint="glBegin", CallingConvention=CallingConvention.Cdecl)]
		[System.Security.SuppressUnmanagedCodeSecurity()]
		public static extern void Begin(int mode);

		[DllImport(LIB_GL, EntryPoint="glVertex2i", CallingConvention=CallingConvention.Cdecl)]
		[System.Security.SuppressUnmanagedCodeSecurity()]
		public static extern void Vertex2i(int x, int y);

		[DllImport(LIB_GL, EntryPoint="glEnd", CallingConvention=CallingConvention.Cdecl)]
		[System.Security.SuppressUnmanagedCodeSecurity()]
		public static extern void End();

		[DllImport(LIB_GL, EntryPoint="glFlush", CallingConvention=CallingConvention.Cdecl)]
		[System.Security.SuppressUnmanagedCodeSecurity()]
		public static extern void Flush();

		[DllImport(LIB_GL, EntryPoint="glEnable", CallingConvention=CallingConvention.Cdecl)]
		[System.Security.SuppressUnmanagedCodeSecurity()]
		public static extern void Enable(uint cap);

		[DllImport(LIB_GL, EntryPoint="glDepthFunc", CallingConvention=CallingConvention.Cdecl)]
		[System.Security.SuppressUnmanagedCodeSecurity()]
		public static extern void DepthFunc(uint func);

		[DllImport(LIB_GL, EntryPoint="glClearDepth", CallingConvention=CallingConvention.Cdecl)]
		[System.Security.SuppressUnmanagedCodeSecurity()]
		public static extern void ClearDepth(double depth);

		[DllImport(LIB_GL, EntryPoint="glClearDepthf", CallingConvention=CallingConvention.Cdecl)]
		[System.Security.SuppressUnmanagedCodeSecurity()]
		public static extern void ClearDepthf(float depth);

		[DllImport(LIB_GL, EntryPoint="glDepthMask", CallingConvention=CallingConvention.Cdecl)]
		[System.Security.SuppressUnmanagedCodeSecurity()]
		public static extern void DepthMask(bool flag);

		[DllImport(LIB_GL, EntryPoint="glDisable", CallingConvention=CallingConvention.Cdecl)]
		[System.Security.SuppressUnmanagedCodeSecurity()]
		public static extern void Disable(uint cap);

		[DllImport(LIB_GL, EntryPoint="glUseProgram", CallingConvention=CallingConvention.Cdecl)]
		[System.Security.SuppressUnmanagedCodeSecurity()]
		public static extern void UseProgram(uint program);

		[DllImport(LIB_GL, EntryPoint="glBindFramebuffer", CallingConvention=CallingConvention.Cdecl)]
		[System.Security.SuppressUnmanagedCodeSecurity()]
		public static extern void BindFramebuffer(uint target, uint framebuffer);

		[DllImport(LIB_GL, EntryPoint="glViewport", CallingConvention=CallingConvention.Cdecl)]
		[System.Security.SuppressUnmanagedCodeSecurity()]
		public static extern void Viewport(int x, int y, int width, int height);

		[DllImport(LIB_GL, EntryPoint="glColorMask", CallingConvention=CallingConvention.Cdecl)]
		[System.Security.SuppressUnmanagedCodeSecurity()]
		public static extern void ColorMask(bool red, bool green, bool blue, bool alpha);

		[DllImport(LIB_GL, EntryPoint="glLoadIdentity", CallingConvention=CallingConvention.Cdecl)]
		[System.Security.SuppressUnmanagedCodeSecurity()]
		public static extern void LoadIdentity();

		[DllImport(LIB_GL, EntryPoint="glOrtho", CallingConvention=CallingConvention.Cdecl)]
		[System.Security.SuppressUnmanagedCodeSecurity()]
		public static extern void Ortho(double left, double right, double bottom, double top, double zNear, double zFar);
	}

	public static class C
	{
		private const string LIB_C = "libc";

		public enum LocaleCategory
		{

			///<summary>Identify the entire C locale.</summary>
			LC_ALL = 0,

			///<summary>Identify the collation category of the C locale.</summary>
			LC_COLLATE = 1,

			///<summary>Identify the character classification category of the C locale.</summary>
			LC_CTYPE = 2,

			///<summary>Identify the monetary formatting category of the C locale.</summary>
			LC_MONETARY = 3,

			///<summary>Identify selects the numeric formatting category of the C locale.</summary>
			LC_NUMERIC = 4,

			///<summary>Identify the time formatting category of the C locale.</summary>
			LC_TIME = 5,

			///<summary>Identify themessage formatting category of the C locale.</summary>
			LC_MESSAGES = 6
		}

		/// <summary>This function is used to set or query the program's current locale.</summary>
		/// <param name="category">Determine which parts of the program's current locale should be modified.<see cref="TInt"/></param>
		/// <param name="localeToSet">The parts of the program's current locale to be set.</param>
		/// <returns>The current (new) locale on success, or IntPtr.Zero on a wrong/unsupported 'locale' string.
		/// The return string is owned by the runtime library - DO NOT FREE OR MANIPULATE it.<see cref="IntPtr"/></returns>
		/// <remarks>The effects, a locale has:
		/// - What multibyte character sequences are valid, and how they are interpreted.
		/// - Classification of which characters in the local character set are considered alphabetic, and upper- and lower-case conversion conventions.
		/// - The collating sequence for the local language and character set (see section Collation Functions).
		/// - Formatting of numbers and currency amounts.
		/// - Formatting of dates and times (see section Formatting Date and Time).
		/// - What language to use for output, including error messages. (The C library doesn't yet help you implement this.)
		/// - What language to use for user answers to yes-or-no questions.
		/// - What language to use for more complex user input. (The C library doesn't yet help you implement this.) 
		/// See 'http://www.cs.utah.edu/dept/old/texinfo/glibc-manual-0.02/library_7.html' for details.</remarks>
		[DllImport(LIB_C, EntryPoint="setlocale", CallingConvention=CallingConvention.Cdecl)]
		extern public static IntPtr setlocale (int category, sbyte[] localeToSet);

		/// <summary>Convert a string to a null-terminated signed byte array. Does not support unicode character mith charachter code > 255.</summary></summary>
		/// <param name="text">The text to convert.</param>
		/// <returns>The text converted to a null-terminated signed byte array on success, or a null-terminated empty signed byte array.</returns>
		public static sbyte[] StringToSByteArray (string text)
		{
			if (string.IsNullOrWhiteSpace(text))
				return new sbyte[] {(sbyte)0};

			sbyte[] result = new sbyte[text.Length + 1];
			for (int index = 0; index < text.Length; index++)
				result[index] = (sbyte)text[index];
			result[text.Length] = (sbyte)0;

			return result;
		}
	}

	public static class X11
	{
		private const string LIB_X11 = "libX11";

		/// <summary>Check if Xlib functions are capable of operating under the current locale.</summary>
		/// <returns>True, if Xlib functions are capable of operating under the current locale, or false otherwise.<see cref="Boolean"/></returns>
		/// <remarks>If it returns False, Xlib locale-dependent functions, for which the XLocaleNotSupported return status is defined,
		/// will return XLocaleNotSupported. Other Xlib locale-dependent routines will operate in the 'C' locale.</remarks>
		[DllImport (LIB_X11, EntryPoint="XSupportsLocale", CallingConvention=CallingConvention.Cdecl)]
		extern public static bool XSupportsLocale ();

		/// <summary>Set the X modifiers for the current locale setting.</summary>
		/// <param name="modifiers">The null-terminated string of the form '{@category=value}', that is, having zero or more concatenated
		/// '@category=value' entries, where category is a category name and value is the (possibly empty) setting for that category.
		/// The values are encoded in the current locale. Category names are restricted to the POSIX Portable Filename Character Set.<see cref="TChar[]"/></param>
		/// <returns>The string reprecenting the current modifierrs on success (can be empty, if only implementation-dependent default modifiers are activated),
		/// or IntPtr.Zero if invalid values are given for one or more modifier categories and none of the current modifiers are changed.<see cref="IntPtr"/></returns>
		[DllImport (LIB_X11, EntryPoint="XSetLocaleModifiers", CallingConvention=CallingConvention.Cdecl)]
		extern public static IntPtr XSetLocaleModifiers (sbyte[] modifiers);

		/// <summary> Open a connection to the X server that controls a display. </summary>
		/// <param name="x11displayName"> Display name syntax is: hostname:number.screen_number; like: dual-headed:0.1; or empty sting for default. <see cref="String"/> </param>
		/// <returns> The display pointer on success, or IntPtr.Zero otherwise. <see cref="IntPtr"/> </returns>
		[DllImport (LIB_X11, EntryPoint="XOpenDisplay", CallingConvention=CallingConvention.Cdecl)]
		extern public static IntPtr XOpenDisplay (String x11displayName);

		/// <summary> Close a connection to the X server that controls a display. </summary>
		/// <param name="x11display"> The display pointer, that specifies the connection to the X server. <see cref="IntPtr"/> </param>
		[DllImport (LIB_X11, EntryPoint="XCloseDisplay", CallingConvention=CallingConvention.Cdecl)]
		extern public static void XCloseDisplay (IntPtr x11display);

		/// <summary> Get the min-keycodes and max-keycodes supported by the indicated display. </summary>
		/// <param name="x11display"> The display pointer, that specifies the connection to the X server. <see cref="IntPtr"/> </param>
		/// <param name="minKeycodes">The minimum keycodes.</param>
		/// <param name="maxKeycodes">The maximim keycodes.</param>
		[DllImport (LIB_X11, EntryPoint="XSupportsLocale", CallingConvention=CallingConvention.Cdecl)]
		extern public static void XDisplayKeycodes(IntPtr x11Display, out int minKeycodes, out int maxKeycodes);

	}

}