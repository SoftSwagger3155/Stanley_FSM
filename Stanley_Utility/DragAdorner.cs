using System.Windows;
using System.Windows.Documents;
using System.Windows.Media;
using System.Windows.Shapes;


namespace Stanley_Utility
{
    public class DragAdorner : Adorner
    {
        public DragAdorner(UIElement adornedElement, Size size, Brush brush) : base(adornedElement)
        {
            this.child = new Rectangle
            {
                Fill = brush,
                Width = size.Width,
                Height = size.Height,
                IsHitTestVisible = false
            };
        }

        public override GeneralTransform GetDesiredTransform(GeneralTransform transform)
        {
            return new GeneralTransformGroup
            {
                Children =
                {
                    base.GetDesiredTransform(transform),
                    new TranslateTransform(this.offsetLeft, this.offsetTop)
                }
            };
        }

        public double OffsetLeft
        {
            get
            {
                return this.offsetLeft;
            }
            set
            {
                this.offsetLeft = value;
                this.UpdateLocation();
            }
        }

        public void SetOffsets(double left, double top)
        {
            this.offsetLeft = left;
            this.offsetTop = top;
            this.UpdateLocation();
        }

        public double OffsetTop
        {
            get
            {
                return this.offsetTop;
            }
            set
            {
                this.offsetTop = value;
                this.UpdateLocation();
            }
        }

        protected override Size MeasureOverride(Size constraint)
        {
            this.child.Measure(constraint);
            return this.child.DesiredSize;
        }

        protected override Size ArrangeOverride(Size finalSize)
        {
            this.child.Arrange(new Rect(finalSize));
            return finalSize;
        }

        protected override Visual GetVisualChild(int index)
        {
            return this.child;
        }

        protected override int VisualChildrenCount
        {
            get
            {
                return 1;
            }
        }

        private void UpdateLocation()
        {
            AdornerLayer adornerLayer = base.Parent as AdornerLayer;
            if (adornerLayer != null)
            {
                adornerLayer.Update(base.AdornedElement);
            }
        }

        private Rectangle child = null;

        private double offsetLeft = 0.0;

        private double offsetTop = 0.0;
    }
}
