using System;
using System.Runtime.InteropServices;
using Noesis;

namespace NoesisApp
{
    public class Win32Display : Display
    {
        public Win32Display()
        {
            IntPtr cPtr = Noesis_Win32Display_Create_();
            _display = new BaseComponent(cPtr, true);
            _displays[cPtr] = this;
        }

        ~Win32Display()
        {
            RemoveDisplay(BaseComponent.getCPtr(_display).Handle);
        }

        public override IntPtr NativeHandle
        {
            get { return Noesis_Win32Display_NativeHandle(CPtr); }
        }

        public override int ClientWidth
        {
            get { return Noesis_Win32Display_ClientWidth(CPtr); }
        }

        public override int ClientHeight
        {
            get { return Noesis_Win32Display_ClientHeight(CPtr); }
        }

        public override void SetTitle(string title)
        {
            Noesis_Win32Display_SetTitle(CPtr, title);
        }

        public override void SetLocation(int x, int y)
        {
            Noesis_Win32Display_SetLocation(CPtr, x, y);
        }

        public override void SetSize(int width, int height)
        {
            Noesis_Win32Display_SetSize(CPtr, width, height);
        }

        public override void SetWindowStyle(WindowStyle windowStyle)
        {
            Noesis_Win32Display_SetWindowStyle(CPtr, (int)windowStyle);
        }

        public override void SetWindowState(WindowState windowState)
        {
            Noesis_Win32Display_SetWindowState(CPtr, (int)windowState);
        }

        public override void SetResizeMode(ResizeMode resizeMode)
        {
            Noesis_Win32Display_SetResizeMode(CPtr, (int)resizeMode);
        }

        public override void SetShowInTaskbar(bool showInTaskbar)
        {
            Noesis_Win32Display_SetShowInTaskbar(CPtr, showInTaskbar);
        }

        public override void SetTopmost(bool topmost)
        {
            Noesis_Win32Display_SetTopmost(CPtr, topmost);
        }

        public override void SetAllowFileDrop(bool allowFileDrop)
        {
            Noesis_Win32Display_SetAllowFileDrop(CPtr, allowFileDrop);
        }

        public override void SetWindowStartupLocation(WindowStartupLocation location)
        {
            Noesis_Win32Display_SetWindowStartupLocation(CPtr, (int)location);
        }

        public override void Show()
        {
            Noesis_Win32Display_Show_(CPtr);
        }

        public override void Close()
        {
            Noesis_Win32Display_Close_(CPtr);
        }

        public override void EnterMessageLoop()
        {
            Noesis_Win32Display_EnterMessageLoop_(CPtr);
        }

        #region Private members
        private HandleRef CPtr { get { return BaseComponent.getCPtr(_display); } }

        private BaseComponent _display;
        #endregion

        #region Event callbacks
        static Win32Display()
        {
            _displays = new System.Collections.Generic.Dictionary<IntPtr, Win32Display>();

            Noesis_Win32Display_RegisterCallbacks(
                _locationChangedCallback,
                _sizeChangedCallback,
                _stateChangedCallback,
                _fileDroppedCallback,
                _activatedCallback,
                _deactivatedCallback,
                _renderCallback,
                _mouseMoveCallback,
                _mouseButtonDownCallback,
                _mouseButtonUpCallback,
                _mouseDoubleClickCallback,
                _mouseWheelCallback,
                _keyDownCallback,
                _keyUpCallback,
                _charCallback,
                _touchMoveCallback,
                _touchDownCallback,
                _touchUpCallback);
        }

        private delegate void LocationChangedCallback(IntPtr cPtr, int x, int y);
        private static LocationChangedCallback _locationChangedCallback = OnLocationChanged;

        [MonoPInvokeCallback(typeof(LocationChangedCallback))]
        private static void OnLocationChanged(IntPtr cPtr, int x, int y)
        {
            try
            {
                Win32Display display = GetDisplay(cPtr);
                display.LocationChanged?.Invoke(display, x, y);
            }
            catch (Exception e)
            {
                Error.SetNativePendingError(e);
            }
        }

        private delegate void SizeChangedCallback(IntPtr cPtr, int width, int height);
        private static SizeChangedCallback _sizeChangedCallback = OnSizeChanged;

        [MonoPInvokeCallback(typeof(SizeChangedCallback))]
        private static void OnSizeChanged(IntPtr cPtr, int width, int height)
        {
            try
            {
                Win32Display display = GetDisplay(cPtr);
                display.SizeChanged?.Invoke(display, width, height);
            }
            catch (Exception e)
            {
                Error.SetNativePendingError(e);
            }
        }

        private delegate void StateChangedCallback(IntPtr cPtr, int state);
        private static StateChangedCallback _stateChangedCallback = OnStateChanged;

        [MonoPInvokeCallback(typeof(StateChangedCallback))]
        private static void OnStateChanged(IntPtr cPtr, int state)
        {
            try
            {
                Win32Display display = GetDisplay(cPtr);
                display.StateChanged?.Invoke(display, (WindowState)state);
            }
            catch (Exception e)
            {
                Error.SetNativePendingError(e);
            }
        }

        private delegate void FileDroppedCallback(IntPtr cPtr, string filename);
        private static FileDroppedCallback _fileDroppedCallback = OnFileDropped;

        [MonoPInvokeCallback(typeof(FileDroppedCallback))]
        private static void OnFileDropped(IntPtr cPtr, string filename)
        {
            try
            {
                Win32Display display = GetDisplay(cPtr);
                display.FileDropped?.Invoke(display, filename);
            }
            catch (Exception e)
            {
                Error.SetNativePendingError(e);
            }
        }

        private delegate void ActivatedCallback(IntPtr cPtr);
        private static ActivatedCallback _activatedCallback = OnActivated;

        [MonoPInvokeCallback(typeof(ActivatedCallback))]
        private static void OnActivated(IntPtr cPtr)
        {
            try
            {
                Win32Display display = GetDisplay(cPtr);
                display.Activated?.Invoke(display);
            }
            catch (Exception e)
            {
                Error.SetNativePendingError(e);
            }
        }

        private delegate void DeactivatedCallback(IntPtr cPtr);
        private static DeactivatedCallback _deactivatedCallback = OnDeactivated;

        [MonoPInvokeCallback(typeof(DeactivatedCallback))]
        private static void OnDeactivated(IntPtr cPtr)
        {
            try
            {
                Win32Display display = GetDisplay(cPtr);
                display.Deactivated?.Invoke(display);
            }
            catch (Exception e)
            {
                Error.SetNativePendingError(e);
            }
        }

        private delegate void RenderCallback(IntPtr cPtr);
        private static RenderCallback _renderCallback = OnRender;

        [MonoPInvokeCallback(typeof(RenderCallback))]
        private static void OnRender(IntPtr cPtr)
        {
            try
            {
                Win32Display display = GetDisplay(cPtr);
                display.Render?.Invoke(display);
            }
            catch (Exception e)
            {
                Error.SetNativePendingError(e);
            }
        }

        private delegate void MouseMoveCallback(IntPtr cPtr, int x, int y);
        private static MouseMoveCallback _mouseMoveCallback = OnMouseMove;

        [MonoPInvokeCallback(typeof(MouseMoveCallback))]
        private static void OnMouseMove(IntPtr cPtr, int x, int y)
        {
            try
            {
                Win32Display display = GetDisplay(cPtr);
                display.MouseMove?.Invoke(display, x, y);
            }
            catch (Exception e)
            {
                Error.SetNativePendingError(e);
            }
        }

        private delegate void MouseButtonDownCallback(IntPtr cPtr, int x, int y, int b);
        private static MouseButtonDownCallback _mouseButtonDownCallback = OnMouseButtonDown;

        [MonoPInvokeCallback(typeof(MouseButtonDownCallback))]
        private static void OnMouseButtonDown(IntPtr cPtr, int x, int y, int b)
        {
            try
            {
                Win32Display display = GetDisplay(cPtr);
                display.MouseButtonDown?.Invoke(display, x, y, (MouseButton)b);
            }
            catch (Exception e)
            {
                Error.SetNativePendingError(e);
            }
        }

        private delegate void MouseButtonUpCallback(IntPtr cPtr, int x, int y, int b);
        private static MouseButtonUpCallback _mouseButtonUpCallback = OnMouseButtonUp;

        [MonoPInvokeCallback(typeof(MouseButtonUpCallback))]
        private static void OnMouseButtonUp(IntPtr cPtr, int x, int y, int b)
        {
            try
            {
                Win32Display display = GetDisplay(cPtr);
                display.MouseButtonUp?.Invoke(display, x, y, (MouseButton)b);
            }
            catch (Exception e)
            {
                Error.SetNativePendingError(e);
            }
        }

        private delegate void MouseDoubleClickCallback(IntPtr cPtr, int x, int y, int b);
        private static MouseDoubleClickCallback _mouseDoubleClickCallback = OnMouseDoubleClick;

        [MonoPInvokeCallback(typeof(MouseDoubleClickCallback))]
        private static void OnMouseDoubleClick(IntPtr cPtr, int x, int y, int b)
        {
            try
            {
                Win32Display display = GetDisplay(cPtr);
                display.MouseDoubleClick?.Invoke(display, x, y, (MouseButton)b);
            }
            catch (Exception e)
            {
                Error.SetNativePendingError(e);
            }
        }

        private delegate void MouseWheelCallback(IntPtr cPtr, int x, int y, int delta);
        private static MouseWheelCallback _mouseWheelCallback = OnMouseWheel;

        [MonoPInvokeCallback(typeof(MouseWheelCallback))]
        private static void OnMouseWheel(IntPtr cPtr, int x, int y, int delta)
        {
            try
            {
                Win32Display display = GetDisplay(cPtr);
                display.MouseWheel?.Invoke(display, x, y, delta);
            }
            catch (Exception e)
            {
                Error.SetNativePendingError(e);
            }
        }

        private delegate void KeyDownCallback(IntPtr cPtr, int key);
        private static KeyDownCallback _keyDownCallback = OnKeyDown;

        [MonoPInvokeCallback(typeof(KeyDownCallback))]
        private static void OnKeyDown(IntPtr cPtr, int key)
        {
            try
            {
                Win32Display display = GetDisplay(cPtr);
                display.KeyDown?.Invoke(display, (Key)key);
            }
            catch (Exception e)
            {
                Error.SetNativePendingError(e);
            }
        }

        private delegate void KeyUpCallback(IntPtr cPtr, int key);
        private static KeyUpCallback _keyUpCallback = OnKeyUp;

        [MonoPInvokeCallback(typeof(KeyUpCallback))]
        private static void OnKeyUp(IntPtr cPtr, int key)
        {
            try
            {
                Win32Display display = GetDisplay(cPtr);
                display.KeyUp?.Invoke(display, (Key)key);
            }
            catch (Exception e)
            {
                Error.SetNativePendingError(e);
            }
        }

        private delegate void CharCallback(IntPtr cPtr, uint c);
        private static CharCallback _charCallback = OnChar;

        [MonoPInvokeCallback(typeof(CharCallback))]
        private static void OnChar(IntPtr cPtr, uint c)
        {
            try
            {
                Win32Display display = GetDisplay(cPtr);
                display.Char?.Invoke(display, c);
            }
            catch (Exception e)
            {
                Error.SetNativePendingError(e);
            }
        }

        private delegate void TouchMoveCallback(IntPtr cPtr, int x, int y, ulong id);
        private static TouchMoveCallback _touchMoveCallback = OnTouchMove;

        [MonoPInvokeCallback(typeof(TouchMoveCallback))]
        private static void OnTouchMove(IntPtr cPtr, int x, int y, ulong id)
        {
            try
            {
                Win32Display display = GetDisplay(cPtr);
                display.TouchMove?.Invoke(display, x, y, id);
            }
            catch (Exception e)
            {
                Error.SetNativePendingError(e);
            }
        }

        private delegate void TouchDownCallback(IntPtr cPtr, int x, int y, ulong id);
        private static TouchDownCallback _touchDownCallback = OnTouchDown;

        [MonoPInvokeCallback(typeof(TouchDownCallback))]
        private static void OnTouchDown(IntPtr cPtr, int x, int y, ulong id)
        {
            try
            {
                Win32Display display = GetDisplay(cPtr);
                display.TouchDown?.Invoke(display, x, y, id);
            }
            catch (Exception e)
            {
                Error.SetNativePendingError(e);
            }
        }

        private delegate void TouchUpCallback(IntPtr cPtr, int x, int y, ulong id);
        private static TouchUpCallback _touchUpCallback = OnTouchUp;

        [MonoPInvokeCallback(typeof(TouchUpCallback))]
        private static void OnTouchUp(IntPtr cPtr, int x, int y, ulong id)
        {
            try
            {
                Win32Display display = GetDisplay(cPtr);
                display.TouchUp?.Invoke(display, x, y, id);
            }
            catch (Exception e)
            {
                Error.SetNativePendingError(e);
            }
        }

        private void AddDisplay(IntPtr cPtr, Win32Display display)
        {
            lock (_displaysLock)
            {
                _displays[cPtr] = display;
            }
        }

        private void RemoveDisplay(IntPtr cPtr)
        {
            lock (_displaysLock)
            {
                _displays.Remove(cPtr);
            }
        }

        private static Win32Display GetDisplay(IntPtr cPtr)
        {
            lock (_displaysLock)
            {
                return _displays[cPtr];
            }
        }

        private static System.Collections.Generic.Dictionary<IntPtr, Win32Display> _displays;
        private static object _displaysLock = new object();

        #endregion

        #region Imports
        private static IntPtr Noesis_Win32Display_Create_()
        {
            IntPtr cPtr = Noesis_Win32Display_Create();
            Error.Check();
            return cPtr;
        }

        private static void Noesis_Win32Display_Show_(HandleRef display)
        {
            Noesis_Win32Display_Show(display);
            Error.Check();
        }

        private static void Noesis_Win32Display_Close_(HandleRef display)
        {
            Noesis_Win32Display_Close(display);
            Error.Check();
        }

        private static void Noesis_Win32Display_EnterMessageLoop_(HandleRef display)
        {
            Noesis_Win32Display_EnterMessageLoop(display);
            Error.Check();
        }

        [DllImport(Library.Name)]
        static extern void Noesis_Win32Display_RegisterCallbacks(
            LocationChangedCallback locationChangedCallback,
            SizeChangedCallback sizeChangedCallback,
            StateChangedCallback stateChangedCallback,
            FileDroppedCallback fileDroppedCallback,
            ActivatedCallback activatedCallback,
            DeactivatedCallback deactivatedCallback,
            RenderCallback renderCallback,
            MouseMoveCallback mouseMoveCallback,
            MouseButtonDownCallback mouseButtonDownCallback,
            MouseButtonUpCallback mouseButtonUpCallback,
            MouseDoubleClickCallback mouseDoubleClickCallback,
            MouseWheelCallback mouseWheelCallback,
            KeyDownCallback keyDownCallback,
            KeyUpCallback keyUpCallback,
            CharCallback charCallback,
            TouchMoveCallback touchMoveCallback,
            TouchDownCallback touchDownCallback,
            TouchUpCallback touchUpCallback);

        [DllImport(Library.Name)]
        static extern IntPtr Noesis_Win32Display_Create();

        [DllImport(Library.Name)]
        static extern IntPtr Noesis_Win32Display_NativeHandle(HandleRef display);

        [DllImport(Library.Name)]
        static extern int Noesis_Win32Display_ClientWidth(HandleRef display);

        [DllImport(Library.Name)]
        static extern int Noesis_Win32Display_ClientHeight(HandleRef display);

        [DllImport(Library.Name)]
        static extern void Noesis_Win32Display_SetTitle(HandleRef display,
            [MarshalAs(UnmanagedType.LPStr)]string title);

        [DllImport(Library.Name)]
        static extern void Noesis_Win32Display_SetLocation(HandleRef display, int x, int y);

        [DllImport(Library.Name)]
        static extern void Noesis_Win32Display_SetSize(HandleRef display, int width, int height);

        [DllImport(Library.Name)]
        static extern void Noesis_Win32Display_SetWindowStyle(HandleRef display, int style);

        [DllImport(Library.Name)]
        static extern void Noesis_Win32Display_SetWindowState(HandleRef display, int state);

        [DllImport(Library.Name)]
        static extern void Noesis_Win32Display_SetResizeMode(HandleRef display, int mode);

        [DllImport(Library.Name)]
        static extern void Noesis_Win32Display_SetShowInTaskbar(HandleRef display, bool show);

        [DllImport(Library.Name)]
        static extern void Noesis_Win32Display_SetTopmost(HandleRef display, bool topmost);

        [DllImport(Library.Name)]
        static extern void Noesis_Win32Display_SetAllowFileDrop(HandleRef display, bool allow);

        [DllImport(Library.Name)]
        static extern void Noesis_Win32Display_SetWindowStartupLocation(HandleRef display, int loc);

        [DllImport(Library.Name)]
        static extern void Noesis_Win32Display_Show(HandleRef display);

        [DllImport(Library.Name)]
        static extern void Noesis_Win32Display_Close(HandleRef display);

        [DllImport(Library.Name)]
        static extern void Noesis_Win32Display_EnterMessageLoop(HandleRef display);
        #endregion
    }
}
