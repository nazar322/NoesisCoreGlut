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

public class KeyboardFocusChangedEventArgs : RoutedEventArgs {
  private HandleRef swigCPtr;

  internal KeyboardFocusChangedEventArgs(IntPtr cPtr, bool cMemoryOwn) : base(cPtr, cMemoryOwn) {
    swigCPtr = new HandleRef(this, cPtr);
  }

  internal static HandleRef getCPtr(KeyboardFocusChangedEventArgs obj) {
    return (obj == null) ? new HandleRef(null, IntPtr.Zero) : obj.swigCPtr;
  }

  ~KeyboardFocusChangedEventArgs() {
    Dispose();
  }

  public override void Dispose() {
    lock(this) {
      if (swigCPtr.Handle != IntPtr.Zero) {
        if (swigCMemOwn) {
          swigCMemOwn = false;
          NoesisGUI_PINVOKE.delete_KeyboardFocusChangedEventArgs(swigCPtr);
        }
        swigCPtr = new HandleRef(null, IntPtr.Zero);
      }
      GC.SuppressFinalize(this);
      base.Dispose();
    }
  }

  public Noesis.UIElement NewFocus {
    get {
      return GetNewFocusHelper();
    }
  }

  public Noesis.UIElement OldFocus {
    get {
      return GetOldFocusHelper();
    }
  }

  public KeyboardFocusChangedEventArgs(object s, RoutedEvent e, UIElement o, UIElement n) : this(NoesisGUI_PINVOKE.new_KeyboardFocusChangedEventArgs(Noesis.Extend.GetInstanceHandle(s), RoutedEvent.getCPtr(e), UIElement.getCPtr(o), UIElement.getCPtr(n)), true) {
    if (NoesisGUI_PINVOKE.SWIGPendingException.Pending) throw NoesisGUI_PINVOKE.SWIGPendingException.Retrieve();
  }

  private UIElement GetNewFocusHelper() {
    IntPtr cPtr = NoesisGUI_PINVOKE.KeyboardFocusChangedEventArgs_GetNewFocusHelper(swigCPtr);
    if (NoesisGUI_PINVOKE.SWIGPendingException.Pending) throw NoesisGUI_PINVOKE.SWIGPendingException.Retrieve();
    return (UIElement)Noesis.Extend.GetProxy(cPtr, false);
  }

  private UIElement GetOldFocusHelper() {
    IntPtr cPtr = NoesisGUI_PINVOKE.KeyboardFocusChangedEventArgs_GetOldFocusHelper(swigCPtr);
    if (NoesisGUI_PINVOKE.SWIGPendingException.Pending) throw NoesisGUI_PINVOKE.SWIGPendingException.Retrieve();
    return (UIElement)Noesis.Extend.GetProxy(cPtr, false);
  }

}

}

