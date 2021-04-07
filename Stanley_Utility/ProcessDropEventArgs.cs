using System;
using System.Collections.ObjectModel;
using System.Windows;

namespace Stanley_Utility
{
    public class ProcessDropEventArgs<ItemType> : EventArgs where ItemType : class
    {
        internal ProcessDropEventArgs(ObservableCollection<ItemType> itemsSource, ItemType dataItem, int oldIndex, int newIndex, DragDropEffects allowedEffects)
        {
            this.itemsSource = itemsSource;
            this.dataItem = dataItem;
            this.oldIndex = oldIndex;
            this.newIndex = newIndex;
            this.allowedEffects = allowedEffects;
        }

        public ObservableCollection<ItemType> ItemsSource
        {
            get
            {
                return this.itemsSource;
            }
        }

        public ItemType DataItem
        {
            get
            {
                return this.dataItem;
            }
        }

        public int OldIndex
        {
            get
            {
                return this.oldIndex;
            }
        }

        public int NewIndex
        {
            get
            {
                return this.newIndex;
            }
        }

        public DragDropEffects AllowedEffects
        {
            get
            {
                return this.allowedEffects;
            }
        }

        public DragDropEffects Effects
        {
            get
            {
                return this.effects;
            }
            set
            {
                this.effects = value;
            }
        }

        private ObservableCollection<ItemType> itemsSource;

        private ItemType dataItem;

        private int oldIndex;

        private int newIndex;

        private DragDropEffects allowedEffects = DragDropEffects.None;

        private DragDropEffects effects = DragDropEffects.None;
    }
}
