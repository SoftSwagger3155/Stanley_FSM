using System;
using System.Windows;

namespace LX_Utility
{
    public static class Splasher
    {
        public static Window Splash
        {
            get
            {
                return Splasher.mSplash;
            }
            set
            {
                Splasher.mSplash = value;
            }
        }

        public static void ShowSplash()
        {
            if (Splasher.mSplash != null)
            {
                Splasher.mSplash.Show();
            }
        }

        public static void CloseSplash()
        {
            if (Splasher.mSplash != null)
            {
                Splasher.mSplash.Close();
                if (Splasher.mSplash is IDisposable)
                {
                    (Splasher.mSplash as IDisposable).Dispose();
                }
                Splasher.mSplash = null;
            }
        }

        private static Window mSplash;
    }
}
