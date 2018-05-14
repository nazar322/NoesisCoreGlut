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

public class Grid : Panel {
  internal new static Grid CreateProxy(IntPtr cPtr, bool cMemoryOwn) {
    return new Grid(cPtr, cMemoryOwn);
  }

  internal Grid(IntPtr cPtr, bool cMemoryOwn) : base(cPtr, cMemoryOwn) {
  }

  internal static HandleRef getCPtr(Grid obj) {
    return (obj == null) ? new HandleRef(null, IntPtr.Zero) : obj.swigCPtr;
  }

  public Grid() {
  }

  protected override IntPtr CreateCPtr(Type type, out bool registerExtend) {
    if ((object)type.TypeHandle == typeof(Grid).TypeHandle) {
      registerExtend = false;
      return NoesisGUI_PINVOKE.new_Grid();
    }
    else {
      return base.CreateExtendCPtr(type, out registerExtend);
    }
  }

  public static uint GetColumn(DependencyObject element) {
    uint ret = NoesisGUI_PINVOKE.Grid_GetColumn(DependencyObject.getCPtr(element));
    if (NoesisGUI_PINVOKE.SWIGPendingException.Pending) throw NoesisGUI_PINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  public static void SetColumn(DependencyObject element, uint column) {
    NoesisGUI_PINVOKE.Grid_SetColumn(DependencyObject.getCPtr(element), column);
    if (NoesisGUI_PINVOKE.SWIGPendingException.Pending) throw NoesisGUI_PINVOKE.SWIGPendingException.Retrieve();
  }

  public static uint GetColumnSpan(DependencyObject element) {
    uint ret = NoesisGUI_PINVOKE.Grid_GetColumnSpan(DependencyObject.getCPtr(element));
    if (NoesisGUI_PINVOKE.SWIGPendingException.Pending) throw NoesisGUI_PINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  public static void SetColumnSpan(DependencyObject element, uint columnSpan) {
    NoesisGUI_PINVOKE.Grid_SetColumnSpan(DependencyObject.getCPtr(element), columnSpan);
    if (NoesisGUI_PINVOKE.SWIGPendingException.Pending) throw NoesisGUI_PINVOKE.SWIGPendingException.Retrieve();
  }

  public static bool GetIsSharedSizeScope(DependencyObject element) {
    bool ret = NoesisGUI_PINVOKE.Grid_GetIsSharedSizeScope(DependencyObject.getCPtr(element));
    if (NoesisGUI_PINVOKE.SWIGPendingException.Pending) throw NoesisGUI_PINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  public static void SetIsSharedSizeScope(DependencyObject element, bool value) {
    NoesisGUI_PINVOKE.Grid_SetIsSharedSizeScope(DependencyObject.getCPtr(element), value);
    if (NoesisGUI_PINVOKE.SWIGPendingException.Pending) throw NoesisGUI_PINVOKE.SWIGPendingException.Retrieve();
  }

  public static uint GetRow(DependencyObject element) {
    uint ret = NoesisGUI_PINVOKE.Grid_GetRow(DependencyObject.getCPtr(element));
    if (NoesisGUI_PINVOKE.SWIGPendingException.Pending) throw NoesisGUI_PINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  public static void SetRow(DependencyObject element, uint row) {
    NoesisGUI_PINVOKE.Grid_SetRow(DependencyObject.getCPtr(element), row);
    if (NoesisGUI_PINVOKE.SWIGPendingException.Pending) throw NoesisGUI_PINVOKE.SWIGPendingException.Retrieve();
  }

  public static uint GetRowSpan(DependencyObject element) {
    uint ret = NoesisGUI_PINVOKE.Grid_GetRowSpan(DependencyObject.getCPtr(element));
    if (NoesisGUI_PINVOKE.SWIGPendingException.Pending) throw NoesisGUI_PINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  public static void SetRowSpan(DependencyObject element, uint rowSpan) {
    NoesisGUI_PINVOKE.Grid_SetRowSpan(DependencyObject.getCPtr(element), rowSpan);
    if (NoesisGUI_PINVOKE.SWIGPendingException.Pending) throw NoesisGUI_PINVOKE.SWIGPendingException.Retrieve();
  }

  public static DependencyProperty ColumnProperty {
    get {
      IntPtr cPtr = NoesisGUI_PINVOKE.Grid_ColumnProperty_get();
      if (NoesisGUI_PINVOKE.SWIGPendingException.Pending) throw NoesisGUI_PINVOKE.SWIGPendingException.Retrieve();
      return (DependencyProperty)Noesis.Extend.GetProxy(cPtr, false);
    }
  }

  public static DependencyProperty ColumnSpanProperty {
    get {
      IntPtr cPtr = NoesisGUI_PINVOKE.Grid_ColumnSpanProperty_get();
      if (NoesisGUI_PINVOKE.SWIGPendingException.Pending) throw NoesisGUI_PINVOKE.SWIGPendingException.Retrieve();
      return (DependencyProperty)Noesis.Extend.GetProxy(cPtr, false);
    }
  }

  public static DependencyProperty IsSharedSizeScopeProperty {
    get {
      IntPtr cPtr = NoesisGUI_PINVOKE.Grid_IsSharedSizeScopeProperty_get();
      if (NoesisGUI_PINVOKE.SWIGPendingException.Pending) throw NoesisGUI_PINVOKE.SWIGPendingException.Retrieve();
      return (DependencyProperty)Noesis.Extend.GetProxy(cPtr, false);
    }
  }

  public static DependencyProperty RowProperty {
    get {
      IntPtr cPtr = NoesisGUI_PINVOKE.Grid_RowProperty_get();
      if (NoesisGUI_PINVOKE.SWIGPendingException.Pending) throw NoesisGUI_PINVOKE.SWIGPendingException.Retrieve();
      return (DependencyProperty)Noesis.Extend.GetProxy(cPtr, false);
    }
  }

  public static DependencyProperty RowSpanProperty {
    get {
      IntPtr cPtr = NoesisGUI_PINVOKE.Grid_RowSpanProperty_get();
      if (NoesisGUI_PINVOKE.SWIGPendingException.Pending) throw NoesisGUI_PINVOKE.SWIGPendingException.Retrieve();
      return (DependencyProperty)Noesis.Extend.GetProxy(cPtr, false);
    }
  }

  public ColumnDefinitionCollection ColumnDefinitions {
    get {
      IntPtr cPtr = NoesisGUI_PINVOKE.Grid_ColumnDefinitions_get(swigCPtr);
      if (NoesisGUI_PINVOKE.SWIGPendingException.Pending) throw NoesisGUI_PINVOKE.SWIGPendingException.Retrieve();
      return (ColumnDefinitionCollection)Noesis.Extend.GetProxy(cPtr, false);
    }
  }

  public RowDefinitionCollection RowDefinitions {
    get {
      IntPtr cPtr = NoesisGUI_PINVOKE.Grid_RowDefinitions_get(swigCPtr);
      if (NoesisGUI_PINVOKE.SWIGPendingException.Pending) throw NoesisGUI_PINVOKE.SWIGPendingException.Retrieve();
      return (RowDefinitionCollection)Noesis.Extend.GetProxy(cPtr, false);
    }
  }

  new internal static IntPtr GetStaticType() {
    IntPtr ret = NoesisGUI_PINVOKE.Grid_GetStaticType();
    if (NoesisGUI_PINVOKE.SWIGPendingException.Pending) throw NoesisGUI_PINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }


  internal new static IntPtr Extend(string typeName) {
    IntPtr nativeType = NoesisGUI_PINVOKE.Extend_Grid(Marshal.StringToHGlobalAnsi(typeName));
    if (NoesisGUI_PINVOKE.SWIGPendingException.Pending) throw NoesisGUI_PINVOKE.SWIGPendingException.Retrieve();
    return nativeType;
  }
}

}

