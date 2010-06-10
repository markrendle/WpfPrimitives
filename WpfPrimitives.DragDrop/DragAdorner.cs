using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Documents;
using System.Windows;
using System.Windows.Media;
using System.Windows.Shapes;
using System.Windows.Media.Effects;
using System.Windows.Input;
using System.Diagnostics;
using System.Windows.Controls;

namespace WpfPrimitives.DragAndDrop
{
    /// <summary>
    /// Provides a visual adorner for Drag'n'drop operations
    /// </summary>
    public class DragAdorner : Adorner
    {
        private readonly Point _startPoint;
        private Point _currentPoint;
        private readonly UIElement _element;
        private readonly FrameworkElement _container;
        private readonly UIElement _visual;

        /// <summary>
        /// Initializes a new instance of the <see cref="DragAdorner"/> class.
        /// </summary>
        /// <param name="element">The element from which the drag originated.</param>
        /// <param name="startPoint">The dragging start point.</param>
        public DragAdorner(FrameworkElement element, Point startPoint) : base(element)
        {
            _element = element;
            _container = (FrameworkElement) element.GetAllAncestors().OfType<ContentControl>().Last().Content;
            _startPoint = startPoint;

            _visual = CreateVisual(element);

            _container.AllowDrop = true;
            _container.DragOver += HandleDragOver;
            _container.Drop += HandleDrop;
        }

        private static UIElement CreateVisual(FrameworkElement element)
        {
            return new Rectangle
                        {
                            Fill = new VisualBrush(element),
                            Width = element.RenderSize.Width,
                            Height = element.RenderSize.Height,
                            IsHitTestVisible = false,
                            RenderTransform = new ScaleTransform(1.1, 1.1),
                            Effect = new DropShadowEffect { Opacity = 0.5 },
                        };
        }

        protected override Size ArrangeOverride(Size finalSize)
        {
            _visual.Arrange(new Rect(finalSize));
            return _visual.DesiredSize;
        }

        protected override Size MeasureOverride(Size constraint)
        {
            _visual.Arrange(new Rect(constraint));
            return _visual.DesiredSize;
        }

        protected override Visual GetVisualChild(int index)
        {
            return _visual;
        }

        protected override int VisualChildrenCount
        {
            get { return 1; }
        }

        public override GeneralTransform GetDesiredTransform(GeneralTransform transform)
        {
            var transformGroup = new GeneralTransformGroup();
            transformGroup.Children.Add(base.GetDesiredTransform(transform));
            transformGroup.Children.Add(new TranslateTransform(_currentPoint.X - _startPoint.X, _currentPoint.Y - _startPoint.Y));
            return transformGroup;
        }

        internal void HandleDragOver(object sender, DragEventArgs e)
        {
            if (Parent == null) return;

            var layer = this.Parent as AdornerLayer;

            if (layer != null)
            {
                _currentPoint = e.GetPosition(_container);
                Trace.WriteLine(_currentPoint);
                layer.Update(_element);
            }
        }

        void HandleDrop(object sender, DragEventArgs e)
        {
            if (Parent != null)
                ((AdornerLayer)Parent).Remove(this);
        }
    }
}
