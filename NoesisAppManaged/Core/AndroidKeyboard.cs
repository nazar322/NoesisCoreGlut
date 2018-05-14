using Android.Views;
using Android.Views.InputMethods;
using Android.Content;
using Android.App;

namespace NoesisApp
{
    public class AndroidKeyboard : Noesis.SoftwareKeyboard
    {
        public AndroidKeyboard(View view)
        {
            Activity activity = (Activity)view.Context;
            View = activity.Window.DecorView;
        }

        public override bool Show(Noesis.UIElement focusedElement)
        {
            InputManager?.ShowSoftInput(View, ShowFlags.Implicit);
            return false;
        }

        public override void Hide()
        {
            InputManager?.HideSoftInputFromWindow(View.WindowToken, HideSoftInputFlags.None);
        }

        private View View { get; set; }

        private InputMethodManager InputManager
        {
            get { return (InputMethodManager)View.Context.GetSystemService(Context.InputMethodService); }
        }
    }
}