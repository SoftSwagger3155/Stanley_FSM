using System;
using System.Runtime.InteropServices;
using System.Windows;
using System.Windows.Interop;

namespace Stanley_Utility
{
    public class WindowBehavior
    {
        [AttachedPropertyBrowsableForType(typeof(Window))]
        public static bool GetHideCloseButton(Window obj)
        {
            return (bool)obj.GetValue(WindowBehavior.HideCloseButtonProperty);
        }

        [AttachedPropertyBrowsableForType(typeof(Window))]
        public static void SetHideCloseButton(Window obj, bool value)
        {
            obj.SetValue(WindowBehavior.HideCloseButtonProperty, value);
        }

        private static void HideCloseButtonChangedCallback(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            Window window = d as Window;
            if (window != null)
            {
                bool flag = (bool)e.NewValue;
                if (flag && !WindowBehavior.GetIsHiddenCloseButton(window))
                {
                    if (!window.IsLoaded)
                    {
                        window.Loaded += WindowBehavior.HideWhenLoadedDelegate;
                    }
                    else
                    {
                        WindowBehavior.HideCloseButton(window);
                    }
                    WindowBehavior.SetIsHiddenCloseButton(window, true);
                }
                else if (!flag && WindowBehavior.GetIsHiddenCloseButton(window))
                {
                    if (!window.IsLoaded)
                    {
                        window.Loaded -= WindowBehavior.ShowWhenLoadedDelegate;
                    }
                    else
                    {
                        WindowBehavior.ShowCloseButton(window);
                    }
                    WindowBehavior.SetIsHiddenCloseButton(window, false);
                }
            }
        }

        [DllImport("user32.dll", SetLastError = true)]
        private static extern int GetWindowLong(IntPtr hWnd, int nIndex);

        [DllImport("user32.dll")]
        private static extern int SetWindowLong(IntPtr hWnd, int nIndex, int dwNewLong);

        public static void HideCloseButton(Window w)
        {
            IntPtr handle = new WindowInteropHelper(w).Handle;
            WindowBehavior.SetWindowLong(handle, -16, WindowBehavior.GetWindowLong(handle, -16) & -524289);
        }

        public static void ShowCloseButton(Window w)
        {
            IntPtr handle = new WindowInteropHelper(w).Handle;
            WindowBehavior.SetWindowLong(handle, -16, WindowBehavior.GetWindowLong(handle, -16) | 524288);
        }

        [AttachedPropertyBrowsableForType(typeof(Window))]
        public static bool GetIsHiddenCloseButton(Window obj)
        {
            return (bool)obj.GetValue(WindowBehavior.IsHiddenCloseButtonProperty);
        }

        private static void SetIsHiddenCloseButton(Window obj, bool value)
        {
            obj.SetValue(WindowBehavior.IsHiddenCloseButtonKey, value);
        }

        private const int GWL_STYLE = -16;

        private const int WS_SYSMENU = 524288;

        private static readonly Type OwnerType = typeof(WindowBehavior);

        public static readonly DependencyProperty HideCloseButtonProperty = DependencyProperty.RegisterAttached("HideCloseButton", typeof(bool), WindowBehavior.OwnerType, new FrameworkPropertyMetadata(false, new PropertyChangedCallback(WindowBehavior.HideCloseButtonChangedCallback)));

        private static readonly RoutedEventHandler HideWhenLoadedDelegate = delegate (object sender, RoutedEventArgs args)
        {
            if (sender is Window)
            {
                Window window = (Window)sender;
                WindowBehavior.HideCloseButton(window);
                window.Loaded -= WindowBehavior.HideWhenLoadedDelegate;
            }
        };

        private static readonly RoutedEventHandler ShowWhenLoadedDelegate = delegate (object sender, RoutedEventArgs args)
        {
            if (sender is Window)
            {
                Window window = (Window)sender;
                WindowBehavior.ShowCloseButton(window);
                window.Loaded -= WindowBehavior.ShowWhenLoadedDelegate;
            }
        };

        private static readonly DependencyPropertyKey IsHiddenCloseButtonKey = DependencyProperty.RegisterAttachedReadOnly("IsHiddenCloseButton", typeof(bool), WindowBehavior.OwnerType, new FrameworkPropertyMetadata(false));

        public static readonly DependencyProperty IsHiddenCloseButtonProperty = WindowBehavior.IsHiddenCloseButtonKey.DependencyProperty;
    }
}
