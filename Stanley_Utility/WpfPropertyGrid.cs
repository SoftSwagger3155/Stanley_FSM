using System;
using System.Activities.Presentation;
using System.Activities.Presentation.Model;
using System.Activities.Presentation.View;
using System.ComponentModel;
using System.Reflection;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;

namespace Stanley_Utility
{
    public enum PropertySort
    {
        NoSort,
        Alphabetical,
        Categorized,
        CategorizedAlphabetical
    }

    public class WpfPropertyGrid : Grid
    {
        public object SelectedObject
        {
            get
            {
                return base.GetValue(WpfPropertyGrid.SelectedObjectProperty);
            }
            set
            {
                base.SetValue(WpfPropertyGrid.SelectedObjectProperty, value);
            }
        }

        public object[] SelectedObjects
        {
            get
            {
                return base.GetValue(WpfPropertyGrid.SelectedObjectsProperty) as object[];
            }
            set
            {
                base.SetValue(WpfPropertyGrid.SelectedObjectsProperty, value);
            }
        }

        public bool HelpVisible
        {
            get
            {
                return (bool)base.GetValue(WpfPropertyGrid.HelpVisibleProperty);
            }
            set
            {
                base.SetValue(WpfPropertyGrid.HelpVisibleProperty, value);
            }
        }

        public bool ToolbarVisible
        {
            get
            {
                return (bool)base.GetValue(WpfPropertyGrid.ToolbarVisibleProperty);
            }
            set
            {
                base.SetValue(WpfPropertyGrid.ToolbarVisibleProperty, value);
            }
        }

        public PropertySort PropertySort
        {
            get
            {
                return (PropertySort)base.GetValue(WpfPropertyGrid.PropertySortProperty);
            }
            set
            {
                base.SetValue(WpfPropertyGrid.PropertySortProperty, value);
            }
        }

        private static object CoerceSelectedObject(DependencyObject d, object value)
        {
            WpfPropertyGrid wpfPropertyGrid = d as WpfPropertyGrid;
            object[] array = wpfPropertyGrid.GetValue(WpfPropertyGrid.SelectedObjectsProperty) as object[];
            return (array.Length == 0) ? null : value;
        }

        private static object CoerceSelectedObjects(DependencyObject d, object value)
        {
            WpfPropertyGrid wpfPropertyGrid = d as WpfPropertyGrid;
            object value2 = wpfPropertyGrid.GetValue(WpfPropertyGrid.SelectedObjectsProperty);
            return (value2 == null) ? new object[0] : value;
        }

        private static void SelectedObjectPropertyChanged(DependencyObject source, DependencyPropertyChangedEventArgs e)
        {
            WpfPropertyGrid wpfPropertyGrid = source as WpfPropertyGrid;
            wpfPropertyGrid.CoerceValue(WpfPropertyGrid.SelectedObjectsProperty);
            if (e.NewValue == null)
            {
                MethodBase onSelectionChangedMethod = wpfPropertyGrid.OnSelectionChangedMethod;
                object propertyInspectorView = wpfPropertyGrid.Designer.PropertyInspectorView;
                object[] parameters = new object[1];
                onSelectionChangedMethod.Invoke(propertyInspectorView, parameters);
                wpfPropertyGrid.SelectionTypeLabel.Text = string.Empty;
            }
            else
            {
                EditingContext context = new EditingContext();
                ModelTreeManager modelTreeManager = new ModelTreeManager(context);
                modelTreeManager.Load(e.NewValue);
                Selection selection = Selection.Select(context, modelTreeManager.Root);
                wpfPropertyGrid.OnSelectionChangedMethod.Invoke(wpfPropertyGrid.Designer.PropertyInspectorView, new object[]
                {
                    selection
                });
                wpfPropertyGrid.SelectionTypeLabel.Text = e.NewValue.GetType().Name;
            }
            wpfPropertyGrid.ChangeHelpText(string.Empty, string.Empty);
        }

        private static void SelectedObjectsPropertyChanged(DependencyObject source, DependencyPropertyChangedEventArgs e)
        {
            WpfPropertyGrid wpfPropertyGrid = source as WpfPropertyGrid;
            wpfPropertyGrid.CoerceValue(WpfPropertyGrid.SelectedObjectsProperty);
            object[] array = e.NewValue as object[];
            if (array.Length == 0)
            {
                MethodBase onSelectionChangedMethod = wpfPropertyGrid.OnSelectionChangedMethod;
                object propertyInspectorView = wpfPropertyGrid.Designer.PropertyInspectorView;
                object[] parameters = new object[1];
                onSelectionChangedMethod.Invoke(propertyInspectorView, parameters);
                wpfPropertyGrid.SelectionTypeLabel.Text = string.Empty;
            }
            else
            {
                bool flag = true;
                Type type = null;
                EditingContext context = new EditingContext();
                ModelTreeManager modelTreeManager = new ModelTreeManager(context);
                Selection selection = null;
                for (int i = 0; i < array.Length; i++)
                {
                    modelTreeManager.Load(array[i]);
                    if (i == 0)
                    {
                        selection = Selection.Select(context, modelTreeManager.Root);
                        type = array[0].GetType();
                    }
                    else
                    {
                        selection = Selection.Union(context, modelTreeManager.Root);
                        if (!array[i].GetType().Equals(type))
                        {
                            flag = false;
                        }
                    }
                }
                wpfPropertyGrid.OnSelectionChangedMethod.Invoke(wpfPropertyGrid.Designer.PropertyInspectorView, new object[]
                {
                    selection
                });
                wpfPropertyGrid.SelectionTypeLabel.Text = (flag ? (type.Name + " <multiple>") : "Object <multiple>");
            }
            wpfPropertyGrid.ChangeHelpText(string.Empty, string.Empty);
        }

        private static void HelpVisiblePropertyChanged(DependencyObject source, DependencyPropertyChangedEventArgs e)
        {
            WpfPropertyGrid wpfPropertyGrid = source as WpfPropertyGrid;
            if (e.NewValue != e.OldValue)
            {
                if (e.NewValue.Equals(true))
                {
                    wpfPropertyGrid.RowDefinitions[1].Height = new GridLength(5.0);
                    wpfPropertyGrid.RowDefinitions[2].Height = new GridLength(wpfPropertyGrid.HelpTextHeight);
                }
                else
                {
                    wpfPropertyGrid.HelpTextHeight = wpfPropertyGrid.RowDefinitions[2].Height.Value;
                    wpfPropertyGrid.RowDefinitions[1].Height = new GridLength(0.0);
                    wpfPropertyGrid.RowDefinitions[2].Height = new GridLength(0.0);
                }
            }
        }

        private static void ToolbarVisiblePropertyChanged(DependencyObject source, DependencyPropertyChangedEventArgs e)
        {
            WpfPropertyGrid wpfPropertyGrid = source as WpfPropertyGrid;
            wpfPropertyGrid.PropertyToolBar.Visibility = (e.NewValue.Equals(true) ? Visibility.Visible : Visibility.Collapsed);
        }

        private static void PropertySortPropertyChanged(DependencyObject source, DependencyPropertyChangedEventArgs e)
        {
            WpfPropertyGrid wpfPropertyGrid = source as WpfPropertyGrid;
            PropertySort propertySort = (PropertySort)e.NewValue;
            bool flag = propertySort == PropertySort.Alphabetical || propertySort == PropertySort.NoSort;
            wpfPropertyGrid.IsInAlphaViewMethod.Invoke(wpfPropertyGrid.Designer.PropertyInspectorView, new object[]
            {
                flag
            });
        }

        public WpfPropertyGrid()
        {
            base.ColumnDefinitions.Add(new ColumnDefinition());
            base.RowDefinitions.Add(new RowDefinition
            {
                Height = new GridLength(1.0, GridUnitType.Star)
            });
            base.RowDefinitions.Add(new RowDefinition
            {
                Height = new GridLength(0.0)
            });
            base.RowDefinitions.Add(new RowDefinition
            {
                Height = new GridLength(0.0)
            });
            this.Designer = new WorkflowDesigner();
            TextBlock textBlock = new TextBlock
            {
                Visibility = Visibility.Visible,
                TextWrapping = TextWrapping.NoWrap,
                TextTrimming = TextTrimming.CharacterEllipsis,
                FontWeight = FontWeights.Bold
            };
            TextBlock textBlock2 = new TextBlock
            {
                Visibility = Visibility.Visible,
                TextWrapping = TextWrapping.Wrap,
                TextTrimming = TextTrimming.CharacterEllipsis
            };
            DockPanel dockPanel = new DockPanel
            {
                Visibility = Visibility.Visible,
                LastChildFill = true,
                Margin = new Thickness(3.0, 0.0, 3.0, 0.0)
            };
            textBlock.SetValue(DockPanel.DockProperty, Dock.Top);
            dockPanel.Children.Add(textBlock);
            dockPanel.Children.Add(textBlock2);
            this.HelpText = new Border
            {
                Visibility = Visibility.Visible,
                BorderBrush = SystemColors.ActiveBorderBrush,
                Background = SystemColors.ControlBrush,
                BorderThickness = new Thickness(1.0),
                Child = dockPanel
            };
            this.Splitter = new GridSplitter
            {
                Visibility = Visibility.Visible,
                ResizeDirection = GridResizeDirection.Rows,
                Height = 5.0,
                HorizontalAlignment = HorizontalAlignment.Stretch
            };
            UIElement propertyInspectorView = this.Designer.PropertyInspectorView;
            propertyInspectorView.Visibility = Visibility.Visible;
            propertyInspectorView.SetValue(FrameworkElement.VerticalAlignmentProperty, VerticalAlignment.Stretch);
            this.Splitter.SetValue(Grid.RowProperty, 1);
            this.Splitter.SetValue(Grid.ColumnProperty, 0);
            this.HelpText.SetValue(Grid.RowProperty, 2);
            this.HelpText.SetValue(Grid.ColumnProperty, 0);
            Binding binding = new Binding("Parent.Background");
            textBlock.SetBinding(Panel.BackgroundProperty, binding);
            textBlock2.SetBinding(Panel.BackgroundProperty, binding);
            base.Children.Add(propertyInspectorView);
            base.Children.Add(this.Splitter);
            base.Children.Add(this.HelpText);
            Type type = propertyInspectorView.GetType();
            PropertyInfo[] properties = type.GetProperties(BindingFlags.DeclaredOnly | BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);
            MethodInfo[] methods = type.GetMethods(BindingFlags.DeclaredOnly | BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);
            this.RefreshMethod = type.GetMethod("RefreshPropertyList", BindingFlags.DeclaredOnly | BindingFlags.Instance | BindingFlags.NonPublic);
            this.IsInAlphaViewMethod = type.GetMethod("set_IsInAlphaView", BindingFlags.DeclaredOnly | BindingFlags.Instance | BindingFlags.Public);
            this.OnSelectionChangedMethod = type.GetMethod("OnSelectionChanged", BindingFlags.DeclaredOnly | BindingFlags.Instance | BindingFlags.Public);
            this.SelectionTypeLabel = (type.GetMethod("get_SelectionTypeLabel", BindingFlags.DeclaredOnly | BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic).Invoke(propertyInspectorView, new object[0]) as TextBlock);
            this.PropertyToolBar = (type.GetMethod("get_PropertyToolBar", BindingFlags.DeclaredOnly | BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic).Invoke(propertyInspectorView, new object[0]) as Control);
            type.GetEvent("GotFocus").AddEventHandler(this, Delegate.CreateDelegate(typeof(RoutedEventHandler), this, "GotFocusHandler", false));
            this.SelectionTypeLabel.Text = string.Empty;
        }

        public void RefreshPropertyList()
        {
            this.RefreshMethod.Invoke(this.Designer.PropertyInspectorView, new object[]
            {
                false
            });
        }

        private void GotFocusHandler(object sender, RoutedEventArgs args)
        {
            string title = string.Empty;
            string descrip = string.Empty;
            object[] array = base.GetValue(WpfPropertyGrid.SelectedObjectsProperty) as object[];
            if (array != null && array.Length > 0)
            {
                Type type = array[0].GetType();
                for (int i = 1; i < array.Length; i++)
                {
                    if (!array[i].GetType().Equals(type))
                    {
                        this.ChangeHelpText(title, descrip);
                        return;
                    }
                }
                object dataContext = (args.OriginalSource as FrameworkElement).DataContext;
                PropertyInfo property = dataContext.GetType().GetProperty("PropertyEntry");
                if (property == null)
                {
                    property = dataContext.GetType().GetProperty("ParentProperty");
                }
                if (property != null)
                {
                    object value = property.GetValue(dataContext, null);
                    string name = value.GetType().GetProperty("PropertyName").GetValue(value, null) as string;
                    title = (value.GetType().GetProperty("DisplayName").GetValue(value, null) as string);
                    PropertyInfo property2 = array[0].GetType().GetProperty(name);
                    object[] customAttributes = property2.GetCustomAttributes(typeof(DescriptionAttribute), true);
                    if (customAttributes != null && customAttributes.Length > 0)
                    {
                        descrip = (customAttributes[0] as DescriptionAttribute).Description;
                    }
                }
                this.ChangeHelpText(title, descrip);
            }
        }

        private void ChangeHelpText(string title, string descrip)
        {
            DockPanel dockPanel = this.HelpText.Child as DockPanel;
            (dockPanel.Children[0] as TextBlock).Text = title;
            (dockPanel.Children[1] as TextBlock).Text = descrip;
        }

        private WorkflowDesigner Designer;
        private MethodInfo RefreshMethod;
        private MethodInfo OnSelectionChangedMethod;
        private MethodInfo IsInAlphaViewMethod;
        private TextBlock SelectionTypeLabel;
        private Control PropertyToolBar;
        private Border HelpText;
        private GridSplitter Splitter;
        private double HelpTextHeight = 60.0;

        public static readonly DependencyProperty SelectedObjectProperty = DependencyProperty.Register("SelectedObject", typeof(object), typeof(WpfPropertyGrid), new FrameworkPropertyMetadata(null, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault, new PropertyChangedCallback(WpfPropertyGrid.SelectedObjectPropertyChanged)));
        public static readonly DependencyProperty SelectedObjectsProperty = DependencyProperty.Register("SelectedObjects", typeof(object[]), typeof(WpfPropertyGrid), new FrameworkPropertyMetadata(new object[0], FrameworkPropertyMetadataOptions.BindsTwoWayByDefault, new PropertyChangedCallback(WpfPropertyGrid.SelectedObjectsPropertyChanged), new CoerceValueCallback(WpfPropertyGrid.CoerceSelectedObjects)));
        public static readonly DependencyProperty HelpVisibleProperty = DependencyProperty.Register("HelpVisible", typeof(bool), typeof(WpfPropertyGrid), new FrameworkPropertyMetadata(false, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault, new PropertyChangedCallback(WpfPropertyGrid.HelpVisiblePropertyChanged)));
        public static readonly DependencyProperty ToolbarVisibleProperty = DependencyProperty.Register("ToolbarVisible", typeof(bool), typeof(WpfPropertyGrid), new FrameworkPropertyMetadata(true, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault, new PropertyChangedCallback(WpfPropertyGrid.ToolbarVisiblePropertyChanged)));
        public static readonly DependencyProperty PropertySortProperty = DependencyProperty.Register("PropertySort", typeof(PropertySort), typeof(WpfPropertyGrid), new FrameworkPropertyMetadata(PropertySort.CategorizedAlphabetical, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault, new PropertyChangedCallback(WpfPropertyGrid.PropertySortPropertyChanged)));
    }
}
