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
using System.Collections.Generic;

namespace Noesis
{

public class Expander : HeaderedContentControl {
  internal new static Expander CreateProxy(IntPtr cPtr, bool cMemoryOwn) {
    return new Expander(cPtr, cMemoryOwn);
  }

  internal Expander(IntPtr cPtr, bool cMemoryOwn) : base(cPtr, cMemoryOwn) {
  }

  internal static HandleRef getCPtr(Expander obj) {
    return (obj == null) ? new HandleRef(null, IntPtr.Zero) : obj.swigCPtr;
  }

  #region Events
  #region Collapsed
  public delegate void CollapsedHandler(object sender, RoutedEventArgs e);
  public event CollapsedHandler Collapsed {
    add {
      if (!_Collapsed.ContainsKey(swigCPtr.Handle)) {
        _Collapsed.Add(swigCPtr.Handle, null);

        NoesisGUI_PINVOKE.BindEvent_Expander_Collapsed(_raiseCollapsed, swigCPtr.Handle);
        if (NoesisGUI_PINVOKE.SWIGPendingException.Pending) throw NoesisGUI_PINVOKE.SWIGPendingException.Retrieve();
      }

      _Collapsed[swigCPtr.Handle] += value;
    }
    remove {
      if (_Collapsed.ContainsKey(swigCPtr.Handle)) {

        _Collapsed[swigCPtr.Handle] -= value;

        if (_Collapsed[swigCPtr.Handle] == null) {
          NoesisGUI_PINVOKE.UnbindEvent_Expander_Collapsed(_raiseCollapsed, swigCPtr.Handle);
          if (NoesisGUI_PINVOKE.SWIGPendingException.Pending) throw NoesisGUI_PINVOKE.SWIGPendingException.Retrieve();

          _Collapsed.Remove(swigCPtr.Handle);
        }
      }
    }
  }

  internal delegate void RaiseCollapsedCallback(IntPtr cPtr, IntPtr sender, IntPtr e);
  private static RaiseCollapsedCallback _raiseCollapsed = RaiseCollapsed;

  [MonoPInvokeCallback(typeof(RaiseCollapsedCallback))]
  private static void RaiseCollapsed(IntPtr cPtr, IntPtr sender, IntPtr e) {
    try {
      if (!_Collapsed.ContainsKey(cPtr)) {
        throw new InvalidOperationException("Delegate not registered for Collapsed event");
      }
      if (sender == IntPtr.Zero && e == IntPtr.Zero) {
        _Collapsed.Remove(cPtr);
        return;
      }
      if (Noesis.Extend.Initialized) {
        CollapsedHandler handler = _Collapsed[cPtr];
        if (handler != null) {
          handler(Noesis.Extend.GetProxy(sender, false), new RoutedEventArgs(e, false));
        }
      }
    }
    catch (Exception exception) {
      Noesis.Error.SetNativePendingError(exception);
    }
  }

  static Dictionary<IntPtr, CollapsedHandler> _Collapsed =
      new Dictionary<IntPtr, CollapsedHandler>();
  #endregion

  #region Expanded
  public delegate void ExpandedHandler(object sender, RoutedEventArgs e);
  public event ExpandedHandler Expanded {
    add {
      if (!_Expanded.ContainsKey(swigCPtr.Handle)) {
        _Expanded.Add(swigCPtr.Handle, null);

        NoesisGUI_PINVOKE.BindEvent_Expander_Expanded(_raiseExpanded, swigCPtr.Handle);
        if (NoesisGUI_PINVOKE.SWIGPendingException.Pending) throw NoesisGUI_PINVOKE.SWIGPendingException.Retrieve();
      }

      _Expanded[swigCPtr.Handle] += value;
    }
    remove {
      if (_Expanded.ContainsKey(swigCPtr.Handle)) {

        _Expanded[swigCPtr.Handle] -= value;

        if (_Expanded[swigCPtr.Handle] == null) {
          NoesisGUI_PINVOKE.UnbindEvent_Expander_Expanded(_raiseExpanded, swigCPtr.Handle);
          if (NoesisGUI_PINVOKE.SWIGPendingException.Pending) throw NoesisGUI_PINVOKE.SWIGPendingException.Retrieve();

          _Expanded.Remove(swigCPtr.Handle);
        }
      }
    }
  }

  internal delegate void RaiseExpandedCallback(IntPtr cPtr, IntPtr sender, IntPtr e);
  private static RaiseExpandedCallback _raiseExpanded = RaiseExpanded;

  [MonoPInvokeCallback(typeof(RaiseExpandedCallback))]
  private static void RaiseExpanded(IntPtr cPtr, IntPtr sender, IntPtr e) {
    try {
      if (!_Expanded.ContainsKey(cPtr)) {
        throw new InvalidOperationException("Delegate not registered for Expanded event");
      }
      if (sender == IntPtr.Zero && e == IntPtr.Zero) {
        _Expanded.Remove(cPtr);
        return;
      }
      if (Noesis.Extend.Initialized) {
        ExpandedHandler handler = _Expanded[cPtr];
        if (handler != null) {
          handler(Noesis.Extend.GetProxy(sender, false), new RoutedEventArgs(e, false));
        }
      }
    }
    catch (Exception exception) {
      Noesis.Error.SetNativePendingError(exception);
    }
  }

  static Dictionary<IntPtr, ExpandedHandler> _Expanded =
      new Dictionary<IntPtr, ExpandedHandler>();
  #endregion

  #endregion

  public Expander() {
  }

  protected override IntPtr CreateCPtr(Type type, out bool registerExtend) {
    if ((object)type.TypeHandle == typeof(Expander).TypeHandle) {
      registerExtend = false;
      return NoesisGUI_PINVOKE.new_Expander();
    }
    else {
      return base.CreateExtendCPtr(type, out registerExtend);
    }
  }

  public static DependencyProperty ExpandDirectionProperty {
    get {
      IntPtr cPtr = NoesisGUI_PINVOKE.Expander_ExpandDirectionProperty_get();
      if (NoesisGUI_PINVOKE.SWIGPendingException.Pending) throw NoesisGUI_PINVOKE.SWIGPendingException.Retrieve();
      return (DependencyProperty)Noesis.Extend.GetProxy(cPtr, false);
    }
  }

  public static DependencyProperty IsExpandedProperty {
    get {
      IntPtr cPtr = NoesisGUI_PINVOKE.Expander_IsExpandedProperty_get();
      if (NoesisGUI_PINVOKE.SWIGPendingException.Pending) throw NoesisGUI_PINVOKE.SWIGPendingException.Retrieve();
      return (DependencyProperty)Noesis.Extend.GetProxy(cPtr, false);
    }
  }

  public ExpandDirection ExpandDirection {
    set {
      NoesisGUI_PINVOKE.Expander_ExpandDirection_set(swigCPtr, (int)value);
      if (NoesisGUI_PINVOKE.SWIGPendingException.Pending) throw NoesisGUI_PINVOKE.SWIGPendingException.Retrieve();
    } 
    get {
      ExpandDirection ret = (ExpandDirection)NoesisGUI_PINVOKE.Expander_ExpandDirection_get(swigCPtr);
      if (NoesisGUI_PINVOKE.SWIGPendingException.Pending) throw NoesisGUI_PINVOKE.SWIGPendingException.Retrieve();
      return ret;
    } 
  }

  public bool IsExpanded {
    set {
      NoesisGUI_PINVOKE.Expander_IsExpanded_set(swigCPtr, value);
      if (NoesisGUI_PINVOKE.SWIGPendingException.Pending) throw NoesisGUI_PINVOKE.SWIGPendingException.Retrieve();
    } 
    get {
      bool ret = NoesisGUI_PINVOKE.Expander_IsExpanded_get(swigCPtr);
      if (NoesisGUI_PINVOKE.SWIGPendingException.Pending) throw NoesisGUI_PINVOKE.SWIGPendingException.Retrieve();
      return ret;
    } 
  }

  new internal static IntPtr GetStaticType() {
    IntPtr ret = NoesisGUI_PINVOKE.Expander_GetStaticType();
    if (NoesisGUI_PINVOKE.SWIGPendingException.Pending) throw NoesisGUI_PINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }


  internal new static IntPtr Extend(string typeName) {
    IntPtr nativeType = NoesisGUI_PINVOKE.Extend_Expander(Marshal.StringToHGlobalAnsi(typeName));
    if (NoesisGUI_PINVOKE.SWIGPendingException.Pending) throw NoesisGUI_PINVOKE.SWIGPendingException.Retrieve();
    return nativeType;
  }
}

}

