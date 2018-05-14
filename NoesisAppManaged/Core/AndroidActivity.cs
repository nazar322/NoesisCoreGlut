﻿using System;
using System.Threading;
using System.Collections.Generic;
using System.Collections.Concurrent;
using Android.App;
using Android.Runtime;
using Android.Views;

namespace NoesisApp
{
    public abstract class AndroidActivity : Activity
    {
        internal static AndroidActivity Current { get; private set; }

        internal AndroidView View { get; private set; }

        internal ConcurrentQueue<ActivityEvent> EventQueue { get; private set; }

        protected override void OnCreate(Android.OS.Bundle savedInstanceState)
        {
            Current = this;
            EventQueue = new ConcurrentQueue<ActivityEvent>();

            base.OnCreate(savedInstanceState);
            ForceFullscreen();

            View = new AndroidView(this);
            SetContentView(View);
        }

        protected override void OnDestroy()
        {
            base.OnDestroy();

            Current = null;
        }

        private void ForceFullscreen()
        {
            RequestWindowFeature(WindowFeatures.NoTitle);
            Window.SetFlags(WindowManagerFlags.Fullscreen, WindowManagerFlags.Fullscreen);

            View decorView = Window.DecorView;
            var uiOptions = (int)decorView.SystemUiVisibility;
            var newUiOptions = (int)uiOptions;

            newUiOptions |= (int)SystemUiFlags.LowProfile;
            newUiOptions |= (int)SystemUiFlags.Fullscreen;
            newUiOptions |= (int)SystemUiFlags.HideNavigation;
            newUiOptions |= (int)SystemUiFlags.ImmersiveSticky;

            decorView.SystemUiVisibility = (StatusBarVisibility)newUiOptions;
        }

        public override bool OnKeyDown([GeneratedEnum] Keycode keyCode, KeyEvent e)
        {
            EventQueue.Enqueue(new KeyActivityEvent(keyCode, e));
            return true;
        }

        public override bool OnKeyUp([GeneratedEnum] Keycode keyCode, KeyEvent e)
        {
            EventQueue.Enqueue(new KeyActivityEvent(keyCode, e));
            return true;
        }

        internal void Start()
        {
            MainLoop = new Thread(Main);
            MainLoop.Start();
        }

        private Thread MainLoop { get; set; }

        /// <summary>
        /// Entry point for Android applications in Noesis App Framework
        /// </summary>
        protected abstract void Main();
    }

    public enum ActivityEventType
    {
        WindowCreated,
        WindowDestroyed,
        WindowSizeChanged,
        KeyEvent,
        TouchEvent
    }

    public abstract class ActivityEvent
    {
        public ActivityEventType Type { get; protected set; }
    }

    public class WindowCreatedActivityEvent : ActivityEvent
    {
        public WindowCreatedActivityEvent()
        {
            Type = ActivityEventType.WindowCreated;
        }
    }

    public class WindowDestroyedActivityEvent : ActivityEvent
    {
        public WindowDestroyedActivityEvent()
        {
            Type = ActivityEventType.WindowDestroyed;
        }
    }

    public class WindowSizeChangedActivityEvent : ActivityEvent
    {
        public WindowSizeChangedActivityEvent()
        {
            Type = ActivityEventType.WindowSizeChanged;
        }
    }

    public class KeyActivityEvent : ActivityEvent
    {
        public KeyActivityEvent(Keycode keyCode, KeyEvent e)
        {
            Type = ActivityEventType.KeyEvent;
            Action = e.Action;
            KeyCode = keyCode;
            Char = (uint)e.GetUnicodeChar(e.MetaState);
        }

        public KeyEventActions Action { get; set; }
        public Keycode KeyCode { get; set; }
        public uint Char { get; set; }
    }

    public class TouchActivityEvent : ActivityEvent
    {
        public TouchActivityEvent(MotionEvent e)
        {
            Type = ActivityEventType.TouchEvent;
            Action = e.ActionMasked;

            Pointers = new List<PointerInfo>();
            switch (Action)
            {
                case MotionEventActions.Down:
                case MotionEventActions.PointerDown:
                case MotionEventActions.Up:
                case MotionEventActions.PointerUp:
                {
                    int index = e.ActionIndex;
                    int x = (int)e.GetX(index);
                    int y = (int)e.GetY(index);
                    ulong id = (ulong)e.GetPointerId(index);
                    Pointers.Add(new PointerInfo { X = x, Y = y, Id = id });
                    break;
                }
                case MotionEventActions.Move:
                {
                    // Multiple Move events are bundled, so handle them differently
                    for (int index = 0; index < e.PointerCount; index++)
                    {
                        int x = (int)e.GetX(index);
                        int y = (int)e.GetY(index);
                        ulong id = (ulong)e.GetPointerId(index);
                        Pointers.Add(new PointerInfo { X = x, Y = y, Id = id });
                    }
                    break;
                }
            }
        }

        public MotionEventActions Action { get; set; }

        public struct PointerInfo
        {
            public int X { get; set; }
            public int Y { get; set; }
            public ulong Id { get; set; }
        }

        public List<PointerInfo> Pointers;
    }
}
