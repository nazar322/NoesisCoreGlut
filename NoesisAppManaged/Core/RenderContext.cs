using System;
using System.Runtime.InteropServices;
using Noesis;

namespace NoesisApp
{
    public class RenderContext
    {
        public RenderDevice Device
        {
            get { return new RenderDevice(Noesis_RenderContext_Device(CPtr), false); }
        }

        public void Init(IntPtr window, uint samples, bool vsync, bool sRGB)
        {
            Noesis_RenderContext_Init(CPtr, window, samples, vsync, sRGB);
        }

        public void BeginRender()
        {
            Noesis_RenderContext_BeginRender(CPtr);
        }

        public void EndRender()
        {
            Noesis_RenderContext_EndRender(CPtr);
        }

        public void SetDefaultRenderTarget(int width, int height)
        {
            Noesis_RenderContext_SetDefaultRenderTarget(CPtr, width, height);
        }

        public void Swap()
        {
            Noesis_RenderContext_Swap(CPtr);
        }

        public void Resize()
        {
            Noesis_RenderContext_Resize(CPtr);
        }

        #region Private members
        protected RenderContext(IntPtr cPtr, bool memoryOwn)
        {
            _renderContext = new BaseComponent(cPtr, memoryOwn);
        }
        private HandleRef CPtr { get { return BaseComponent.getCPtr(_renderContext); } }

        private BaseComponent _renderContext;
        #endregion

        #region Imports
        [DllImport(Library.Name)]
        static extern void Noesis_RenderContext_Init(HandleRef context, IntPtr window,
            uint samples, bool vsync, bool sRGB);

        [DllImport(Library.Name)]
        static extern IntPtr Noesis_RenderContext_Device(HandleRef context);

        [DllImport(Library.Name)]
        static extern void Noesis_RenderContext_BeginRender(HandleRef context);

        [DllImport(Library.Name)]
        static extern void Noesis_RenderContext_EndRender(HandleRef context);

        [DllImport(Library.Name)]
        static extern void Noesis_RenderContext_SetDefaultRenderTarget(HandleRef context,
            int width, int height);

        [DllImport(Library.Name)]
        static extern void Noesis_RenderContext_Swap(HandleRef context);

        [DllImport(Library.Name)]
        static extern void Noesis_RenderContext_Resize(HandleRef context);
        #endregion
    }

    public class RenderContextGL : RenderContext
    {
        public RenderContextGL() : base(Noesis_RenderContext_CreateGL_(), true)
        {
        }

        #region Imports
        private static IntPtr Noesis_RenderContext_CreateGL_()
        {
            IntPtr cPtr = Noesis_RenderContext_CreateGL();
            Error.Check();
            return cPtr;
        }

        [DllImport(Library.Name)]
        static extern IntPtr Noesis_RenderContext_CreateGL();
        #endregion
    }

    public class RenderContextD3D11 : RenderContext
    {
        public RenderContextD3D11() : base(Noesis_RenderContext_CreateD3D11_(), true)
        {
        }

        #region Imports
        private static IntPtr Noesis_RenderContext_CreateD3D11_()
        {
            IntPtr cPtr = Noesis_RenderContext_CreateD3D11();
            Error.Check();
            return cPtr;
        }

        [DllImport(Library.Name)]
        static extern IntPtr Noesis_RenderContext_CreateD3D11();
        #endregion
    }
}
