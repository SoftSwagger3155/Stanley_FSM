using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace Stanley_Utility
{
    public static class WPFUIHelper
    {
        public static IEnumerable<DependencyObject> GetChildObjects(this DependencyObject parent)
        {
            if (parent != null)
            {
                if (parent is ContentElement || parent is FrameworkElement)
                {
                    foreach (object obj in LogicalTreeHelper.GetChildren(parent))
                    {
                        DependencyObject depObj = obj as DependencyObject;
                        if (depObj != null)
                        {
                            yield return (DependencyObject)obj;
                        }
                    }
                }
                else
                {
                    int count = VisualTreeHelper.GetChildrenCount(parent);
                    for (int i = 0; i < count; i++)
                    {
                        yield return VisualTreeHelper.GetChild(parent, i);
                    }
                }
            }
            yield break;
        }

        public static IEnumerable<T> FindChildren<T>(this DependencyObject source) where T : DependencyObject
        {
            if (source != null)
            {
                IEnumerable<DependencyObject> childs = source.GetChildObjects();
                foreach (DependencyObject child in childs)
                {
                    if (child != null && child is T)
                    {
                        yield return (T)((object)child);
                    }
                    foreach (T descendant in child.FindChildren<T>())
                    {
                        yield return descendant;
                    }
                }
            }
            yield break;
        }

        public static List<T> GetLogicalChildCollection<T>(object parent) where T : DependencyObject
        {
            List<T> list = new List<T>();
            WPFUIHelper.GetLogicalChildCollection<T>(parent as DependencyObject, list);
            return list;
        }

        private static void GetLogicalChildCollection<T>(DependencyObject parent, List<T> logicalCollection) where T : DependencyObject
        {
            IEnumerable children = LogicalTreeHelper.GetChildren(parent);
            foreach (object obj in children)
            {
                if (obj is DependencyObject)
                {
                    DependencyObject parent2 = obj as DependencyObject;
                    if (obj is T)
                    {
                        logicalCollection.Add(obj as T);
                    }
                    WPFUIHelper.GetLogicalChildCollection<T>(parent2, logicalCollection);
                }
            }
        }

        public static IList<Control> GetControls(Visual parent)
        {
            if (parent is FrameworkElement)
            {
                ((FrameworkElement)parent).ApplyTemplate();
            }
            List<Control> list = new List<Control>();
            for (int i = 0; i < VisualTreeHelper.GetChildrenCount(parent); i++)
            {
                Visual visual = (Visual)VisualTreeHelper.GetChild(parent, i);
                Control control = visual as Control;
                if (null != control)
                {
                    list.Add(control);
                }
                list.AddRange(WPFUIHelper.GetControls(visual));
            }
            return list;
        }

        public static DateTime GetLinkerTime(this Assembly assembly, TimeZoneInfo target = null)
        {
            string location = assembly.Location;
            byte[] array = new byte[2048];
            using (FileStream fileStream = new FileStream(location, FileMode.Open, FileAccess.Read))
            {
                fileStream.Read(array, 0, 2048);
            }
            int num = BitConverter.ToInt32(array, 60);
            int num2 = BitConverter.ToInt32(array, num + 8);
            DateTime dateTime = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
            DateTime dateTime2 = dateTime.AddSeconds((double)num2);
            TimeZoneInfo destinationTimeZone = target ?? TimeZoneInfo.Local;
            return TimeZoneInfo.ConvertTimeFromUtc(dateTime2, destinationTimeZone);
        }
    }
}
