//------------------------------------------------------------------------------
// <auto-generated />
//
// This file was automatically generated by SWIG (http://www.swig.org).
// Version 3.0.10
//
// Do not make changes to this file unless you know what you are doing--modify
// the SWIG interface file instead.
//------------------------------------------------------------------------------


using System;
using System.Runtime.InteropServices;

namespace Noesis
{

public class EasingThicknessKeyFrame : ThicknessKeyFrame {
  internal new static EasingThicknessKeyFrame CreateProxy(IntPtr cPtr, bool cMemoryOwn) {
    return new EasingThicknessKeyFrame(cPtr, cMemoryOwn);
  }

  internal EasingThicknessKeyFrame(IntPtr cPtr, bool cMemoryOwn) : base(cPtr, cMemoryOwn) {
  }

  internal static HandleRef getCPtr(EasingThicknessKeyFrame obj) {
    return (obj == null) ? new HandleRef(null, IntPtr.Zero) : obj.swigCPtr;
  }

  public EasingThicknessKeyFrame() {
  }

  protected override IntPtr CreateCPtr(Type type, out bool registerExtend) {
    registerExtend = false;
    return NoesisGUI_PINVOKE.new_EasingThicknessKeyFrame();
  }

  public static DependencyProperty EasingFunctionProperty {
    get {
      IntPtr cPtr = NoesisGUI_PINVOKE.EasingThicknessKeyFrame_EasingFunctionProperty_get();
      if (NoesisGUI_PINVOKE.SWIGPendingException.Pending) throw NoesisGUI_PINVOKE.SWIGPendingException.Retrieve();
      return (DependencyProperty)Noesis.Extend.GetProxy(cPtr, false);
    }
  }

  public EasingFunctionBase EasingFunction {
    set {
      NoesisGUI_PINVOKE.EasingThicknessKeyFrame_EasingFunction_set(swigCPtr, EasingFunctionBase.getCPtr(value));
      if (NoesisGUI_PINVOKE.SWIGPendingException.Pending) throw NoesisGUI_PINVOKE.SWIGPendingException.Retrieve();
    } 
    get {
      IntPtr cPtr = NoesisGUI_PINVOKE.EasingThicknessKeyFrame_EasingFunction_get(swigCPtr);
      if (NoesisGUI_PINVOKE.SWIGPendingException.Pending) throw NoesisGUI_PINVOKE.SWIGPendingException.Retrieve();
      return (EasingFunctionBase)Noesis.Extend.GetProxy(cPtr, false);
    }
  }

  new internal static IntPtr GetStaticType() {
    IntPtr ret = NoesisGUI_PINVOKE.EasingThicknessKeyFrame_GetStaticType();
    if (NoesisGUI_PINVOKE.SWIGPendingException.Pending) throw NoesisGUI_PINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

}

}

