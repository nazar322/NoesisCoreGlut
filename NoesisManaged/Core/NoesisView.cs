﻿using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;

namespace Noesis
{
    public delegate void RenderingEventHandler(object sender, EventArgs e);

    public class View
    {
        #region Public properties
        /// <summary>
        /// Returns the renderer, to be used in the render thread.
        /// </summary>
        public Renderer Renderer
        {
            get
            {
                return _renderer;
            }
        }

        /// <summary>
        /// Returns the root element.
        /// </summary>
        public FrameworkElement Content
        {
            get
            {
                return _content;
            }
        }

        #endregion

        #region Configuration
        /// <summary>
        /// Sets the size of the surface where UI elements will layout and render.
        /// </summary>
        /// <param name="width">Surface width in pixels.</param>
        /// <param name="height">Surface height in pixels.</param>
        public void SetSize(int width, int height)
        {
            Noesis_View_SetSize_(CPtr, width, height);
        }

        /// <summary>
        /// Per-Primitive Antialiasing (PPAA) implements antialiasing by extruding the contours of
        /// the geometry and smoothing them. Useful when GPU MSAA cannot be used.
        /// </summary>
        /// <param name="mode"></param>
        public void SetIsPPAAEnabled(bool enabled)
        {
            Noesis_View_SetIsPPAAEnabled_(CPtr, enabled);
        }

        /// <summary>
        /// Indicates the tessellation quality applied to vector curves.
        /// </summary>
        public enum TessellationQuality
        {
            /// The lowest tessellation quality for curves.
            Low,

            /// Medium tessellation quality for curves.
            Medium,

            /// The highest tessellation quality for curves.
            High
        }

        /// <summary>
        /// Sets tessellation quality. Medium is the default.
        /// </summary>
        public void SetTessellationQuality(TessellationQuality quality)
        {
            Noesis_View_SetTessellationQuality_(CPtr, (int)quality);
        }

        /// <summary>
        /// Flags to debug UI render.
        /// </summary>
        [Flags]
        public enum RenderFlags
        {
            /// <summary>Toggles wireframe mode when rendering triangles.</summary>
            Wireframe = 1,

            /// <summary>Each batch submitted to the GPU is given a unique solid color.</summary>
            ColorBatches = 2,

            /// <summary>
            /// Display pixel overdraw using blending layers. Different colors are used for each
            /// type of triangle. Green for normal, Red for opacities and Blue for clipping masks.
            /// </summary>
            Overdraw = 4,

            /// <summary>Inverts the render vertically.</summary>
            FlipY = 8,
        };

        /// <summary>
        /// Enables debugging flags. No debug flags are active by default
        /// </summary>
        public void SetFlags(RenderFlags flags)
        {
            Noesis_View_SetFlags_(CPtr, (int)flags);
        }
        #endregion

        #region Input management
        /// <summary>
        /// Activates the view and recovers keyboard focus
        /// </summary>
        public void Activate()
        {
            Noesis_View_Activate_(CPtr);
        }

        /// <summary>
        /// Deactivates the view and removes keyboard focus
        /// </summary>
        public void Deactivate()
        {
            Noesis_View_Deactivate_(CPtr);
        }

        #region Mouse input events
        /// <summary>
        /// Notifies the View that mouse was moved. The mouse position is specified in renderer
        /// surface pixel coordinates.
        /// </summary>
        /// <param name="x">Mouse x-coordinate.</param>
        /// <param name="y">Mouse y-coordinate.</param>
        public void MouseMove(int x, int y)
        {
            Noesis_View_MouseMove_(CPtr, x, y);
        }

        /// <summary>
        /// Notifies the View that a mouse button was pressed. The mouse position is specified in
        /// renderer surface pixel coordinates.
        /// </summary>
        /// <param name="x">Mouse x-coordinate.</param>
        /// <param name="y">Mouse y-coordinate.</param>
        /// <param name="button">Indicates which button was pressed.</param>
        public void MouseButtonDown(int x, int y, Noesis.MouseButton button)
        {
            Noesis_View_MouseButtonDown_(CPtr, x, y, (int)button);
        }

        /// <summary>
        /// Notifies the View that a mouse button was released. The mouse position is specified in
        /// renderer surface pixel coordinates.
        /// </summary>
        /// <param name="x">Mouse x-coordinate.</param>
        /// <param name="y">Mouse y-coordinate.</param>
        /// <param name="button">Indicates which button was released.</param>
        public void MouseButtonUp(int x, int y, Noesis.MouseButton button)
        {
            Noesis_View_MouseButtonUp_(CPtr, x, y, (int)button);
        }

        /// <summary>
        /// Notifies the View of a mouse button double click. The mouse position is specified in
        /// renderer surface pixel coordinates.
        /// </summary>
        /// <param name="x">Mouse x-coordinate.</param>
        /// <param name="y">Mouse y-coordinate.</param>
        /// <param name="button">Indicates which button was pressed.</param>
        public void MouseDoubleClick(int x, int y, Noesis.MouseButton button)
        {
            Noesis_View_MouseDoubleClick_(CPtr, x, y, (int)button);
        }

        /// <summary>
        /// Notifies the View that mouse wheel was rotated. The mouse position is specified in
        /// renderer surface pixel coordinates.
        /// </summary>
        /// <param name="x">Mouse x-coordinate.</param>
        /// <param name="y">Mouse y-coordinate.</param>
        /// <param name="wheelRotation">Indicates the amount mouse wheel has changed.</param>
        public void MouseWheel(int x, int y, int wheelRotation)
        {
            Noesis_View_MouseWheel_(CPtr, x, y, wheelRotation);
        }

        /// <summary>
        /// Notifies the View that mouse wheel was horizontally rotated. The mouse position is
        /// specified in renderer surface pixel coordinates.
        /// </summary>
        /// <param name="x">Mouse x-coordinate.</param>
        /// <param name="y">Mouse y-coordinate.</param>
        /// <param name="wheelRotation">Indicates the amount mouse wheel has changed.</param>
        public void MouseHWheel(int x, int y, int wheelRotation)
        {
            Noesis_View_MouseHWheel_(CPtr, x, y, wheelRotation);
        }

        /// <summary>
        /// Notifies the View to scroll vertically. Value typically ranges from -1 to 1.
        /// <summary>
        public virtual void Scroll(float value)
        {
            Noesis_View_Scroll_(CPtr, value);
        }

        /// <summary>
        /// Notifies the View to scroll horizontally. Value typically ranges from -1 to 1.
        /// <summary>
        public virtual void HScroll(float value)
        {
            Noesis_View_HScroll_(CPtr, value);
        }
        #endregion

        #region Touch input events
        /// <summary>
        /// Notifies the View that a finger is moving on the screen. The finger position is
        /// specified in renderer surface pixel coordinates.
        /// </summary>
        /// <param name="x">Finger x-coordinate.</param>
        /// <param name="y">Finger y-coordinate.</param>
        /// <param name="touchId">Finger identifier.</param>
        public void TouchMove(int x, int y, ulong touchId)
        {
            Noesis_View_TouchMove_(CPtr, x, y, touchId);
        }

        /// <summary>
        /// Notifies the View that a finger touches the screen. The finger position is
        /// specified in renderer surface pixel coordinates.
        /// </summary>
        /// <param name="x">Finger x-coordinate.</param>
        /// <param name="y">Finger y-coordinate.</param>
        /// <param name="touchId">Finger identifier.</param>
        public void TouchDown(int x, int y, ulong touchId)
        {
            Noesis_View_TouchDown_(CPtr, x, y, touchId);
        }

        /// <summary>
        /// Notifies the View that a finger is raised off the screen. The finger position is
        /// specified in renderer surface pixel coordinates.
        /// </summary>
        /// <param name="x">Finger x-coordinate.</param>
        /// <param name="y">Finger y-coordinate.</param>
        /// <param name="touchId">Finger identifier.</param>
        public void TouchUp(int x, int y, ulong touchId)
        {
            Noesis_View_TouchUp_(CPtr, x, y, touchId);
        }
        #endregion

        #region Keyboard input events
        /// <summary>
        /// Notifies the View that a key was pressed.
        /// </summary>
        /// <param name="key">Key identifier.</param>
        public void KeyDown(Noesis.Key key)
        {
            Noesis_View_KeyDown_(CPtr, (int)key);
        }

        /// <summary>
        /// Notifies the View that a key was released.
        /// </summary>
        /// <param name="key">Key identifier.</param>
        public void KeyUp(Noesis.Key key)
        {
            Noesis_View_KeyUp_(CPtr, (int)key);
        }

        /// <summary>
        /// Notifies Renderer that a key was translated to the corresponding character.
        /// </summary>
        /// <param name="ch">Unicode character value.</param>
        public void Char(uint ch)
        {
            Noesis_View_Char_(CPtr, ch);
        }
        #endregion
        #endregion

        #region Render process
        /// <summary>
        /// Performs a layout pass and sends updates to the render tree.
        /// </summary>
        /// <param name="timeInSeconds">Time elapsed since the start of the application.</param>
        public void Update(double timeInSeconds)
        {
            Extend.Update();
            GUI.SoftwareKeyboard.Update();
            Noesis_View_Update_(CPtr, timeInSeconds);
        }

        /// <summary>
        /// Occurs just before the objects in the composition tree are rendered.
        /// </summary>
        public event RenderingEventHandler Rendering
        {
            add
            {
                if (!_Rendering.ContainsKey(CPtr.Handle))
                {
                    _Rendering.Add(CPtr.Handle, new RenderingEventInfo { view = this });
                    Noesis_View_BindRenderingEvent_(CPtr, _raiseRendering);
                }

                _Rendering[CPtr.Handle].handler += value;
            }
            remove
            {
                if (_Rendering.ContainsKey(CPtr.Handle))
                {
                    _Rendering[CPtr.Handle].handler -= value;

                    if (_Rendering[CPtr.Handle].handler == null)
                    {
                        Noesis_View_UnbindRenderingEvent_(CPtr, _raiseRendering);
                        _Rendering.Remove(CPtr.Handle);
                    }
                }
            }
        }

        #region Rendering event implementation
        internal delegate void RaiseRenderingCallback(IntPtr cPtr, IntPtr sender);
        private static RaiseRenderingCallback _raiseRendering = RaiseRendering;

        [MonoPInvokeCallback(typeof(RaiseRenderingCallback))]
        private static void RaiseRendering(IntPtr cPtr, IntPtr sender)
        {
            try
            {
                RenderingEventInfo info = null;
                if (!_Rendering.TryGetValue(cPtr, out info))
                {
                    throw new InvalidOperationException(
                        "Delegate not registered for Rendering event");
                }
                if (sender == IntPtr.Zero)
                {
                    _Rendering.Remove(cPtr);
                    return;
                }
                if (Noesis.Extend.Initialized)
                {
                    RenderingEventHandler handler = info.handler;
                    if (handler != null)
                    {
                        handler(info.view, EventArgs.Empty);
                    }
                }
            }
            catch (Exception exception)
            {
                Noesis.Error.SetNativePendingError(exception);
            }
        }

        private class RenderingEventInfo
        {
            public RenderingEventHandler handler;
            public View view;
        }

        static Dictionary<IntPtr, RenderingEventInfo> _Rendering =
            new Dictionary<IntPtr, RenderingEventInfo>();
        #endregion

        /// <summary>
        /// Gets stats counters
        /// </summary>
        public ViewStats GetStats()
        {
            ViewStats stats = new ViewStats();
            Noesis_View_GetStats_(CPtr, ref stats);
            return stats;
        }
        #endregion

        #region Private members
        internal View(FrameworkElement content)
        {
            _view = new BaseComponent(Noesis_View_Create_(BaseComponent.getCPtr(content)), true);
            _renderer = new Renderer(this);
            _content = content;
        }

        internal HandleRef CPtr { get { return BaseComponent.getCPtr(_view); } }

        BaseComponent _view;
        Renderer _renderer;
        FrameworkElement _content;
        #endregion

        #region Imports
        static IntPtr Noesis_View_Create_(HandleRef content)
        {
            IntPtr result = Noesis_View_Create(content);
            Error.Check();
            return result;
        }

        static void Noesis_View_SetSize_(HandleRef view, int width, int height)
        {
            Noesis_View_SetSize(view, width, height);
            Error.Check();
        }

        static void Noesis_View_SetIsPPAAEnabled_(HandleRef view, bool enabled)
        {
            Noesis_View_SetIsPPAAEnabled(view, enabled);
            Error.Check();
        }

        static void Noesis_View_SetTessellationQuality_(HandleRef view, int tessQuality)
        {
            Noesis_View_SetTessellationQuality(view, tessQuality);
            Error.Check();
        }

        static void Noesis_View_SetFlags_(HandleRef view, int flags)
        {
            Noesis_View_SetFlags(view, flags);
            Error.Check();
        }

        static void Noesis_View_Activate_(HandleRef view)
        {
            Noesis_View_Activate(view);
            Error.Check();
        }

        static void Noesis_View_Deactivate_(HandleRef view)
        {
            Noesis_View_Deactivate(view);
            Error.Check();
        }

        static void Noesis_View_MouseMove_(HandleRef view, int x, int y)
        {
            Noesis_View_MouseMove(view, x, y);
            Error.Check();
        }

        static void Noesis_View_MouseButtonDown_(HandleRef view, int x, int y, int button)
        {
            Noesis_View_MouseButtonDown(view, x, y, button);
            Error.Check();
        }

        static void Noesis_View_MouseButtonUp_(HandleRef view, int x, int y, int button)
        {
            Noesis_View_MouseButtonUp(view, x, y, button);
            Error.Check();
        }

        static void Noesis_View_MouseDoubleClick_(HandleRef view, int x, int y, int button)
        {
            Noesis_View_MouseDoubleClick(view, x, y, button);
            Error.Check();
        }

        static void Noesis_View_MouseWheel_(HandleRef view, int x, int y, int wheelRotation)
        {
            Noesis_View_MouseWheel(view, x, y, wheelRotation);
            Error.Check();
        }

        static void Noesis_View_MouseHWheel_(HandleRef view, int x, int y, int wheelRotation)
        {
            Noesis_View_MouseHWheel(view, x, y, wheelRotation);
            Error.Check();
        }

        static void Noesis_View_Scroll_(HandleRef view, float value)
        {
            Noesis_View_Scroll(view, value);
            Error.Check();
        }

        static void Noesis_View_HScroll_(HandleRef view, float value)
        {
            Noesis_View_HScroll(view, value);
            Error.Check();
        }

        static void Noesis_View_TouchMove_(HandleRef view, int x, int y, ulong touchId)
        {
            Noesis_View_TouchMove(view, x, y, touchId);
            Error.Check();
        }

        static void Noesis_View_TouchDown_(HandleRef view, int x, int y, ulong touchId)
        {
            Noesis_View_TouchDown(view, x, y, touchId);
            Error.Check();
        }

        static void Noesis_View_TouchUp_(HandleRef view, int x, int y, ulong touchId)
        {
            Noesis_View_TouchUp(view, x, y, touchId);
            Error.Check();
        }

        static void Noesis_View_KeyDown_(HandleRef view, int key)
        {
            Noesis_View_KeyDown(view, key);
            Error.Check();
        }

        static void Noesis_View_KeyUp_(HandleRef view, int key)
        {
            Noesis_View_KeyUp(view, key);
            Error.Check();
        }

        static void Noesis_View_Char_(HandleRef view, uint ch)
        {
            Noesis_View_Char(view, ch);
            Error.Check();
        }

        static void Noesis_View_Update_(HandleRef view, double timeInSeconds)
        {
            Noesis_View_Update(view, timeInSeconds);
            Error.Check();
        }

        static void Noesis_View_BindRenderingEvent_(HandleRef view, RaiseRenderingCallback callback)
        {
            Noesis_View_BindRenderingEvent(view, callback);
            Error.Check();
        }

        static void Noesis_View_UnbindRenderingEvent_(HandleRef view,
            RaiseRenderingCallback callback)
        {
            Noesis_View_UnbindRenderingEvent(view, callback);
            Error.Check();
        }

        static void Noesis_View_GetStats_(HandleRef view, ref ViewStats stats)
        {
            Noesis_View_GetStats(view, ref stats);
            Error.Check();
        }

        [DllImport(Library.Name)]
        static extern IntPtr Noesis_View_Create(HandleRef contenttheme);

        [DllImport(Library.Name)]
        static extern void Noesis_View_SetSize(HandleRef view, int width, int height);

        [DllImport(Library.Name)]
        static extern void Noesis_View_SetIsPPAAEnabled(HandleRef view, bool enabled);

        [DllImport(Library.Name)]
        static extern void Noesis_View_SetTessellationQuality(HandleRef view, int tessQuality);

        [DllImport(Library.Name)]
        static extern void Noesis_View_SetFlags(HandleRef view, int flags);

        [DllImport(Library.Name)]
        static extern void Noesis_View_Activate(HandleRef view);

        [DllImport(Library.Name)]
        static extern void Noesis_View_Deactivate(HandleRef view);

        [DllImport(Library.Name)]
        static extern void Noesis_View_MouseMove(HandleRef view, int x, int y);

        [DllImport(Library.Name)]
        static extern void Noesis_View_MouseButtonDown(HandleRef view, int x, int y, int button);

        [DllImport(Library.Name)]
        static extern void Noesis_View_MouseButtonUp(HandleRef view, int x, int y, int button);

        [DllImport(Library.Name)]
        static extern void Noesis_View_MouseDoubleClick(HandleRef view, int x, int y, int button);

        [DllImport(Library.Name)]
        static extern void Noesis_View_MouseWheel(HandleRef view, int x, int y, int wheelRotation);

        [DllImport(Library.Name)]
        static extern void Noesis_View_MouseHWheel(HandleRef view, int x, int y, int wheelRotation);

        [DllImport(Library.Name)]
        static extern void Noesis_View_Scroll(HandleRef view, float value);

        [DllImport(Library.Name)]
        static extern void Noesis_View_HScroll(HandleRef view, float value);

        [DllImport(Library.Name)]
        static extern void Noesis_View_TouchMove(HandleRef view, int x, int y, ulong touchId);

        [DllImport(Library.Name)]
        static extern void Noesis_View_TouchDown(HandleRef view, int x, int y, ulong touchId);

        [DllImport(Library.Name)]
        static extern void Noesis_View_TouchUp(HandleRef view, int x, int y, ulong touchId);

        [DllImport(Library.Name)]
        static extern void Noesis_View_KeyDown(HandleRef view, int key);

        [DllImport(Library.Name)]
        static extern void Noesis_View_KeyUp(HandleRef view, int key);

        [DllImport(Library.Name)]
        static extern void Noesis_View_Char(HandleRef view, uint ch);

        [DllImport(Library.Name)]
        static extern void Noesis_View_Update(HandleRef view, double timeInSeconds);

        [DllImport(Library.Name)]
        static extern void Noesis_View_BindRenderingEvent(HandleRef view,
            RaiseRenderingCallback callback);

        [DllImport(Library.Name)]
        static extern void Noesis_View_UnbindRenderingEvent(HandleRef view,
            RaiseRenderingCallback callback);

        [DllImport(Library.Name)]
        static extern void Noesis_View_GetStats(HandleRef view, ref ViewStats stats);
        #endregion
    }

    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
    public struct ViewStats
    {
        public float FrameTime;
        public float UpdateTime;
        public float RenderTime;

        public uint Batches;

        public uint Paths;
        public uint Images;
        public uint Texts;
        public uint Glyphs;
        public uint Masks;

        public uint Triangles;
        public uint MaskTriangles;
        public uint SolidTriangles;
        public uint LinearTriangles;
        public uint RadialTriangles;
        public uint PatternTriangles;

        public uint Fills;
        public uint Strokes;

        public uint RasterizedGlyphs;
        public uint TessellatedGlyphs;
        public uint DiscardedGlyphTiles;

        public uint UploadedRamps;

        public uint RenderTargetSwitches;
    };
}
