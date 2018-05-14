﻿using System;
using System.Runtime.InteropServices;

namespace Noesis
{
    public class Renderer
    {
        /// <summary>
        /// Initializes the Renderer with the specified render device.
        /// </summary>
        /// <param name="vgOptions">Vector graphics options.</param>
        public void Init(RenderDevice device)
        {
            if (device == null)
            {
                throw new ArgumentNullException("device");
            }

            Noesis_Renderer_Init_(CPtr, device.CPtr);
        }

        /// <summary>
        /// Free allocated render resources and render tree
        /// </summary>
        public void Shutdown()
        {
            Noesis_Renderer_Shutdown_(CPtr);
        }

        /// <summary>
        /// Determines the visible region. By default it is set to cover the view dimensions.
        /// </summary>
        /// <param name="x">Horizontal start of visible region.</param>
        /// <param name="y">Vertical start of visible region.</param>
        /// <param name="width">Horizontal size of visible region.</param>
        /// <param name="height">Vertical size of visible region.</param>
        public void SetRenderRegion(float x, float y, float width, float height)
        {
            Noesis_Renderer_SetRenderRegion_(CPtr, x, y, width, height);
        }

        /// <summary>
        /// Applies last changes happened in the view. This function does not interacts with the
        /// render device. Returns whether the render tree really changed.
        /// </summary>
        public bool UpdateRenderTree()
        {
            return Noesis_Renderer_UpdateRenderTree_(CPtr);
        }

        /// <summary>
        /// Indicates if offscreen textures are needed at the current render tree state. When this
        /// function returns true, it is mandatory to call RenderOffscreen() before Render()
        /// </summary>
        public bool NeedsOffscreen()
        {
            return Noesis_Renderer_NeedsOffscreen_(CPtr);
        }

        /// <summary>
        /// Generates offscreen textures. This function fills internal textures and must be invoked
        /// before binding the main render target. This is especially critical in tiled
        /// architectures.
        /// </summary>
        public void RenderOffscreen()
        {
            Noesis_Renderer_RenderOffscreen_(CPtr);
        }

        /// <summary>
        /// Renders UI in the active render target and viewport dimensions
        /// </summary>
        public void Render()
        {
            Noesis_Renderer_Render_(CPtr);
        }

        #region Private members
        internal Renderer(View view)
        {
            _view = view;
        }

        private HandleRef CPtr { get { return _view.CPtr; } }

        View _view;
        #endregion

        #region Imports
        static void Noesis_Renderer_Init_(HandleRef renderer, HandleRef device)
        {
            Noesis_Renderer_Init(renderer, device);
            Error.Check();
        }

        static void Noesis_Renderer_Shutdown_(HandleRef renderer)
        {
            Noesis_Renderer_Shutdown(renderer);
            Error.Check();
        }

        static void Noesis_Renderer_SetRenderRegion_(HandleRef renderer,
            float x, float y, float width, float height)
        {
            Noesis_Renderer_SetRenderRegion(renderer, x, y, width, height);
            Error.Check();
        }

        static bool Noesis_Renderer_UpdateRenderTree_(HandleRef renderer)
        {
            bool ret = Noesis_Renderer_UpdateRenderTree(renderer);
            Error.Check();
            return ret;
        }

        static bool Noesis_Renderer_NeedsOffscreen_(HandleRef renderer)
        {
            bool ret = Noesis_Renderer_NeedsOffscreen(renderer);
            Error.Check();
            return ret;
        }

        static void Noesis_Renderer_RenderOffscreen_(HandleRef renderer)
        {
            Noesis_Renderer_RenderOffscreen(renderer);
            Error.Check();
        }

        static void Noesis_Renderer_Render_(HandleRef renderer)
        {
            Noesis_Renderer_Render(renderer);
            Error.Check();
        }

        [DllImport(Library.Name)]
        static extern void Noesis_Renderer_Init(HandleRef renderer, HandleRef device);

        [DllImport(Library.Name)]
        static extern void Noesis_Renderer_Shutdown(HandleRef renderer);

        [DllImport(Library.Name)]
        static extern void Noesis_Renderer_SetRenderRegion(HandleRef renderer,
            float x, float y, float width, float height);

        [DllImport(Library.Name)]
        [return: MarshalAs(UnmanagedType.U1)]
        static extern bool Noesis_Renderer_UpdateRenderTree(HandleRef renderer);

        [DllImport(Library.Name)]
        [return: MarshalAs(UnmanagedType.U1)]
        static extern bool Noesis_Renderer_NeedsOffscreen(HandleRef renderer);

        [DllImport(Library.Name)]
        static extern void Noesis_Renderer_RenderOffscreen(HandleRef renderer);

        [DllImport(Library.Name)]
        static extern void Noesis_Renderer_Render(HandleRef renderer);
        #endregion
    }
}
