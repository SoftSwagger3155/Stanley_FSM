using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;

namespace Stanley_Utility
{
    public enum ScrollSyncType
    {
        Both,
        Horizontal,
        Vertical,
        None
    }

    public sealed class ScrollSynchronizer
    {
        public static void SetVerticalScrollGroup(DependencyObject obj, string verticalScrollGroup)
        {
            obj.SetValue(ScrollSynchronizer.VerticalScrollGroupProperty, verticalScrollGroup);
        }

        public static string GetVerticalScrollGroup(DependencyObject obj)
        {
            return (string)obj.GetValue(ScrollSynchronizer.VerticalScrollGroupProperty);
        }

        public static void SetHorizontalScrollGroup(DependencyObject obj, string horizontalScrollGroup)
        {
            obj.SetValue(ScrollSynchronizer.HorizontalScrollGroupProperty, horizontalScrollGroup);
        }

        public static string GetHorizontalScrollGroup(DependencyObject obj)
        {
            return (string)obj.GetValue(ScrollSynchronizer.HorizontalScrollGroupProperty);
        }

        public static void SetScrollSyncType(DependencyObject obj, ScrollSyncType scrollSyncType)
        {
            obj.SetValue(ScrollSynchronizer.ScrollSyncTypeProperty, scrollSyncType);
        }

        public static ScrollSyncType GetScrollSyncType(DependencyObject obj)
        {
            return (ScrollSyncType)obj.GetValue(ScrollSynchronizer.ScrollSyncTypeProperty);
        }

        private static void OnVerticalScrollGroupChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ScrollViewer scrollViewer = d as ScrollViewer;
            if (scrollViewer != null)
            {
                string verticalGroupName = (e.NewValue == DependencyProperty.UnsetValue) ? string.Empty : ((string)e.NewValue);
                string verticalGroupName2 = (e.NewValue == DependencyProperty.UnsetValue) ? string.Empty : ((string)e.OldValue);
                ScrollSynchronizer.removeFromVerticalScrollGroup(verticalGroupName2, scrollViewer);
                ScrollSynchronizer.addToVerticalScrollGroup(verticalGroupName, scrollViewer);
                ScrollSyncType scrollSyncType = ScrollSynchronizer.readSyncTypeDPValue(d, ScrollSynchronizer.ScrollSyncTypeProperty);
                if (scrollSyncType == ScrollSyncType.None)
                {
                    d.SetValue(ScrollSynchronizer.ScrollSyncTypeProperty, ScrollSyncType.Vertical);
                }
                else if (scrollSyncType == ScrollSyncType.Horizontal)
                {
                    d.SetValue(ScrollSynchronizer.ScrollSyncTypeProperty, ScrollSyncType.Vertical);
                }
            }
        }

        private static void OnHorizontalScrollGroupChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ScrollViewer scrollViewer = d as ScrollViewer;
            if (scrollViewer != null)
            {
                string horizontalGroupName = (e.NewValue == DependencyProperty.UnsetValue) ? string.Empty : ((string)e.NewValue);
                string horizontalGroupName2 = (e.NewValue == DependencyProperty.UnsetValue) ? string.Empty : ((string)e.OldValue);
                ScrollSynchronizer.removeFromHorizontalScrollGroup(horizontalGroupName2, scrollViewer);
                ScrollSynchronizer.addToHorizontalScrollGroup(horizontalGroupName, scrollViewer);
                ScrollSyncType scrollSyncType = ScrollSynchronizer.readSyncTypeDPValue(d, ScrollSynchronizer.ScrollSyncTypeProperty);
                if (scrollSyncType == ScrollSyncType.None)
                {
                    d.SetValue(ScrollSynchronizer.ScrollSyncTypeProperty, ScrollSyncType.Horizontal);
                }
                else if (scrollSyncType == ScrollSyncType.Vertical)
                {
                    d.SetValue(ScrollSynchronizer.ScrollSyncTypeProperty, ScrollSyncType.Both);
                }
            }
        }

        private static void OnScrollSyncTypeChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ScrollViewer scrollViewer = d as ScrollViewer;
            if (scrollViewer != null)
            {
                string verticalGroupName = ScrollSynchronizer.readStringDPValue(d, ScrollSynchronizer.VerticalScrollGroupProperty);
                string horizontalGroupName = ScrollSynchronizer.readStringDPValue(d, ScrollSynchronizer.HorizontalScrollGroupProperty);
                ScrollSyncType scrollSyncType = ScrollSyncType.None;
                try
                {
                    scrollSyncType = (ScrollSyncType)e.NewValue;
                }
                catch
                {
                }
                switch (scrollSyncType)
                {
                    case ScrollSyncType.Both:
                        if (ScrollSynchronizer.registeredScrollViewers.ContainsKey(scrollViewer))
                        {
                            if (ScrollSynchronizer.registeredScrollViewers[scrollViewer] == ScrollSyncType.Horizontal)
                            {
                                ScrollSynchronizer.addToVerticalScrollGroup(verticalGroupName, scrollViewer);
                            }
                            else if (ScrollSynchronizer.registeredScrollViewers[scrollViewer] == ScrollSyncType.Vertical)
                            {
                                ScrollSynchronizer.addToHorizontalScrollGroup(horizontalGroupName, scrollViewer);
                            }
                            ScrollSynchronizer.registeredScrollViewers[scrollViewer] = ScrollSyncType.Both;
                        }
                        else
                        {
                            ScrollSynchronizer.addToHorizontalScrollGroup(horizontalGroupName, scrollViewer);
                            ScrollSynchronizer.addToVerticalScrollGroup(verticalGroupName, scrollViewer);
                            ScrollSynchronizer.registeredScrollViewers.Add(scrollViewer, ScrollSyncType.Both);
                        }
                        break;
                    case ScrollSyncType.Horizontal:
                        ScrollSynchronizer.removeFromVerticalScrollGroup(verticalGroupName, scrollViewer);
                        ScrollSynchronizer.addToHorizontalScrollGroup(horizontalGroupName, scrollViewer);
                        if (ScrollSynchronizer.registeredScrollViewers.ContainsKey(scrollViewer))
                        {
                            ScrollSynchronizer.registeredScrollViewers[scrollViewer] = ScrollSyncType.Horizontal;
                        }
                        else
                        {
                            ScrollSynchronizer.registeredScrollViewers.Add(scrollViewer, ScrollSyncType.Horizontal);
                        }
                        break;
                    case ScrollSyncType.Vertical:
                        ScrollSynchronizer.removeFromHorizontalScrollGroup(horizontalGroupName, scrollViewer);
                        ScrollSynchronizer.addToVerticalScrollGroup(verticalGroupName, scrollViewer);
                        if (ScrollSynchronizer.registeredScrollViewers.ContainsKey(scrollViewer))
                        {
                            ScrollSynchronizer.registeredScrollViewers[scrollViewer] = ScrollSyncType.Vertical;
                        }
                        else
                        {
                            ScrollSynchronizer.registeredScrollViewers.Add(scrollViewer, ScrollSyncType.Vertical);
                        }
                        break;
                    case ScrollSyncType.None:
                        if (ScrollSynchronizer.registeredScrollViewers.ContainsKey(scrollViewer))
                        {
                            ScrollSynchronizer.removeFromVerticalScrollGroup(verticalGroupName, scrollViewer);
                            ScrollSynchronizer.removeFromHorizontalScrollGroup(horizontalGroupName, scrollViewer);
                            ScrollSynchronizer.registeredScrollViewers.Remove(scrollViewer);
                        }
                        break;
                }
            }
        }

        private static void removeFromVerticalScrollGroup(string verticalGroupName, ScrollViewer scrollViewer)
        {
            if (ScrollSynchronizer.verticalScrollGroups.ContainsKey(verticalGroupName))
            {
                ScrollSynchronizer.verticalScrollGroups[verticalGroupName].ScrollViewers.Remove(scrollViewer);
                if (ScrollSynchronizer.verticalScrollGroups[verticalGroupName].ScrollViewers.Count == 0)
                {
                    ScrollSynchronizer.verticalScrollGroups.Remove(verticalGroupName);
                }
            }
            scrollViewer.ScrollChanged -= ScrollSynchronizer.ScrollViewer_VerticalScrollChanged;
        }

        private static void addToVerticalScrollGroup(string verticalGroupName, ScrollViewer scrollViewer)
        {
            if (ScrollSynchronizer.verticalScrollGroups.ContainsKey(verticalGroupName))
            {
                scrollViewer.ScrollToVerticalOffset(ScrollSynchronizer.verticalScrollGroups[verticalGroupName].Offset);
                ScrollSynchronizer.verticalScrollGroups[verticalGroupName].ScrollViewers.Add(scrollViewer);
            }
            else
            {
                ScrollSynchronizer.verticalScrollGroups.Add(verticalGroupName, new ScrollSynchronizer.OffSetContainer
                {
                    ScrollViewers = new List<ScrollViewer>
                    {
                        scrollViewer
                    },
                    Offset = scrollViewer.VerticalOffset
                });
            }
            scrollViewer.ScrollChanged += ScrollSynchronizer.ScrollViewer_VerticalScrollChanged;
        }

        private static void removeFromHorizontalScrollGroup(string horizontalGroupName, ScrollViewer scrollViewer)
        {
            if (ScrollSynchronizer.horizontalScrollGroups.ContainsKey(horizontalGroupName))
            {
                ScrollSynchronizer.horizontalScrollGroups[horizontalGroupName].ScrollViewers.Remove(scrollViewer);
                if (ScrollSynchronizer.horizontalScrollGroups[horizontalGroupName].ScrollViewers.Count == 0)
                {
                    ScrollSynchronizer.horizontalScrollGroups.Remove(horizontalGroupName);
                }
            }
            scrollViewer.ScrollChanged -= ScrollSynchronizer.ScrollViewer_HorizontalScrollChanged;
        }

        private static void addToHorizontalScrollGroup(string horizontalGroupName, ScrollViewer scrollViewer)
        {
            if (ScrollSynchronizer.horizontalScrollGroups.ContainsKey(horizontalGroupName))
            {
                scrollViewer.ScrollToHorizontalOffset(ScrollSynchronizer.horizontalScrollGroups[horizontalGroupName].Offset);
                ScrollSynchronizer.horizontalScrollGroups[horizontalGroupName].ScrollViewers.Add(scrollViewer);
            }
            else
            {
                ScrollSynchronizer.horizontalScrollGroups.Add(horizontalGroupName, new ScrollSynchronizer.OffSetContainer
                {
                    ScrollViewers = new List<ScrollViewer>
                    {
                        scrollViewer
                    },
                    Offset = scrollViewer.HorizontalOffset
                });
            }
            scrollViewer.ScrollChanged += ScrollSynchronizer.ScrollViewer_HorizontalScrollChanged;
        }

        private static string readStringDPValue(DependencyObject d, DependencyProperty dp)
        {
            object obj = d.ReadLocalValue(dp);
            return (obj == DependencyProperty.UnsetValue) ? string.Empty : obj.ToString();
        }

        private static ScrollSyncType readSyncTypeDPValue(DependencyObject d, DependencyProperty dp)
        {
            object obj = d.ReadLocalValue(dp);
            return (obj == DependencyProperty.UnsetValue) ? ScrollSyncType.None : ((ScrollSyncType)obj);
        }

        private static void ScrollViewer_VerticalScrollChanged(object sender, ScrollChangedEventArgs e)
        {
            ScrollViewer scrollViewer = sender as ScrollViewer;
            if (scrollViewer != null)
            {
                if (e.VerticalChange != 0.0)
                {
                    string key = ScrollSynchronizer.readStringDPValue(sender as DependencyObject, ScrollSynchronizer.VerticalScrollGroupProperty);
                    if (ScrollSynchronizer.verticalScrollGroups.ContainsKey(key))
                    {
                        ScrollSynchronizer.verticalScrollGroups[key].Offset = scrollViewer.VerticalOffset;
                        foreach (ScrollViewer scrollViewer2 in ScrollSynchronizer.verticalScrollGroups[key].ScrollViewers)
                        {
                            if (scrollViewer2.VerticalOffset != scrollViewer.VerticalOffset)
                            {
                                scrollViewer2.ScrollToVerticalOffset(scrollViewer.VerticalOffset);
                            }
                        }
                    }
                }
            }
        }

        private static void ScrollViewer_HorizontalScrollChanged(object sender, ScrollChangedEventArgs e)
        {
            ScrollViewer scrollViewer = sender as ScrollViewer;
            if (scrollViewer != null)
            {
                if (e.HorizontalChange != 0.0)
                {
                    string key = ScrollSynchronizer.readStringDPValue(sender as DependencyObject, ScrollSynchronizer.HorizontalScrollGroupProperty);
                    if (ScrollSynchronizer.horizontalScrollGroups.ContainsKey(key))
                    {
                        ScrollSynchronizer.horizontalScrollGroups[key].Offset = scrollViewer.HorizontalOffset;
                        foreach (ScrollViewer scrollViewer2 in ScrollSynchronizer.horizontalScrollGroups[key].ScrollViewers)
                        {
                            if (scrollViewer2.HorizontalOffset != scrollViewer.HorizontalOffset)
                            {
                                scrollViewer2.ScrollToHorizontalOffset(scrollViewer.HorizontalOffset);
                            }
                        }
                    }
                }
            }
        }

        private const string VerticalScrollGroupPropertyName = "VerticalScrollGroup";

        private const string HorizontalScrollGroupPropertyName = "HorizontalScrollGroup";

        private const string ScrollSyncTypePropertyName = "ScrollSyncType";

        public static readonly DependencyProperty HorizontalScrollGroupProperty = DependencyProperty.RegisterAttached("HorizontalScrollGroup", typeof(string), typeof(ScrollSynchronizer), new PropertyMetadata(string.Empty, new PropertyChangedCallback(ScrollSynchronizer.OnHorizontalScrollGroupChanged)));

        public static readonly DependencyProperty VerticalScrollGroupProperty = DependencyProperty.RegisterAttached("VerticalScrollGroup", typeof(string), typeof(ScrollSynchronizer), new PropertyMetadata(string.Empty, new PropertyChangedCallback(ScrollSynchronizer.OnVerticalScrollGroupChanged)));

        public static readonly DependencyProperty ScrollSyncTypeProperty = DependencyProperty.RegisterAttached("ScrollSyncType", typeof(ScrollSyncType), typeof(ScrollSynchronizer), new PropertyMetadata(ScrollSyncType.None, new PropertyChangedCallback(ScrollSynchronizer.OnScrollSyncTypeChanged)));

        private static readonly Dictionary<string, ScrollSynchronizer.OffSetContainer> verticalScrollGroups = new Dictionary<string, ScrollSynchronizer.OffSetContainer>();

        private static readonly Dictionary<string, ScrollSynchronizer.OffSetContainer> horizontalScrollGroups = new Dictionary<string, ScrollSynchronizer.OffSetContainer>();

        private static readonly Dictionary<ScrollViewer, ScrollSyncType> registeredScrollViewers = new Dictionary<ScrollViewer, ScrollSyncType>();

        private class OffSetContainer
        {
            public double Offset { get; set; }

            public List<ScrollViewer> ScrollViewers { get; set; }
        }
    }
}
