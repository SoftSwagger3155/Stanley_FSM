using System;
using System.Security.Permissions;
using System.Windows.Threading;

namespace LX_Utility
{
    public static class DispatcherHelper
    {
        private static object obj = new object();

        [SecurityPermission(SecurityAction.Demand, Flags = SecurityPermissionFlag.UnmanagedCode)]
        public static void DoEvents()
        {
            lock (DispatcherHelper.obj)
            {
                DispatcherFrame dispatcherFrame = new DispatcherFrame();
                Dispatcher.CurrentDispatcher.BeginInvoke(DispatcherPriority.Background, new DispatcherOperationCallback(DispatcherHelper.ExitFrames), dispatcherFrame);
                try
                {
                    Dispatcher.PushFrame(dispatcherFrame);
                }
                catch (InvalidOperationException)
                {
                }
            }
        }

        private static object ExitFrames(object frame)
        {
            ((DispatcherFrame)frame).Continue = false;
            return null;
        }
    }
}
