using System;
using System.Runtime.InteropServices;

namespace Noesis
{

    public partial class FrameworkPropertyMetadata
    {
        public FrameworkPropertyMetadata(object defaultValue)
            : this(Create(defaultValue, FrameworkPropertyMetadataOptions.None, null), true)
        {
        }

        public FrameworkPropertyMetadata(object defaultValue,
            PropertyChangedCallback propertyChangedCallback)
            : this(Create(defaultValue, FrameworkPropertyMetadataOptions.None, propertyChangedCallback), true)
        {
        }

        public FrameworkPropertyMetadata(object defaultValue, FrameworkPropertyMetadataOptions options)
            : this(Create(defaultValue, options, null), true)
        {
        }

        public FrameworkPropertyMetadata(object defaultValue, FrameworkPropertyMetadataOptions options,
            PropertyChangedCallback propertyChangedCallback)
            : this(Create(defaultValue, options, propertyChangedCallback), true)
        {
        }

        private static IntPtr Create(object defaultValue, FrameworkPropertyMetadataOptions options,
            PropertyChangedCallback propertyChangedCallback)
        {
            return Create(defaultValue, propertyChangedCallback,
                (def, invoke) => Noesis_CreateFrameworkPropertyMetadata_(def, (int)options, invoke));
        }

        #region Imports
        private static IntPtr Noesis_CreateFrameworkPropertyMetadata_(HandleRef defaultValue,
            int options, DelegateInvokePropertyChangedCallback invokePropertyChangedCallback)
        {
            IntPtr result = Noesis_CreateFrameworkPropertyMetadata(defaultValue,
                options, invokePropertyChangedCallback);
            Error.Check();
            return result;
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////
        [DllImport(Library.Name)]
        private static extern IntPtr Noesis_CreateFrameworkPropertyMetadata(HandleRef defaultValue,
            int options, DelegateInvokePropertyChangedCallback invokePropertyChangedCallback);

        #endregion
    }

}
