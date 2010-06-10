using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Interactivity;
using System.Windows;
using System.Windows.Input;
using System.Windows.Data;
using System.ComponentModel;
using System.Windows.Documents;
using System.Diagnostics;

namespace WpfPrimitives.DragAndDrop
{
    public class DragSourceBehavior : Behavior<FrameworkElement>
    {
        private Point _mouseDownPoint;
        private bool _dragging;
        private DragAdorner _adorner;

        public DragSourceBehavior()
        {
        }

        /// <summary>
        /// Gets or sets the data to drag. If not set, the <see cref="FrameworkElement"/> containing the behavior will be used.
        /// </summary>
        /// <value>The data to drag.</value>
        public object DataToDrag
        {
            get { return (object)GetValue(DataToDragProperty); }
            set { SetValue(DataToDragProperty, value); }
        }

        public static readonly DependencyProperty DataToDragProperty =
            DependencyProperty.Register("DataToDrag", typeof(object), typeof(DragSourceBehavior), new UIPropertyMetadata(null));

        protected override void OnAttached()
        {
            base.OnAttached();
            AssociatedObject.PreviewMouseLeftButtonDown += AssociatedObject_PreviewMouseLeftButtonDown;
            AssociatedObject.PreviewMouseMove += AssociatedObject_PreviewMouseMove;
        }

        void AssociatedObject_PreviewMouseMove(object sender, MouseEventArgs e)
        {
            if (_dragging) return;
            if (!e.LeftButton.HasFlag(MouseButtonState.Pressed)) return;

            if (IsUserTryingToDrag(e.GetPosition(AssociatedObject)))
            {
                StartDragOperation(e);
            }
        }

        private void StartDragOperation(MouseEventArgs e)
        {
            _dragging = true;

            var window = AssociatedObject.GetAllAncestors().OfType<Window>().FirstOrDefault();

            if (window != null)
            {
                var container = window.Content as FrameworkElement;
                _adorner = new DragAdorner(AssociatedObject, e.GetPosition(container));
                AdornerLayer.GetAdornerLayer(AssociatedObject).Add(_adorner);
            }

            DragDrop.DoDragDrop(AssociatedObject, DataToDrag ?? AssociatedObject, DragDropEffects.All);
        }


        private void CreateAdorner(Point startPoint)
        {
        }

        private bool IsUserTryingToDrag(Point currentPoint)
        {
            return (Math.Abs(currentPoint.X - _mouseDownPoint.X) >= SystemParameters.MinimumHorizontalDragDistance ||
                Math.Abs(currentPoint.Y - _mouseDownPoint.Y) >= SystemParameters.MinimumVerticalDragDistance);
        }

        void AssociatedObject_PreviewMouseLeftButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            _dragging = false;
            _mouseDownPoint = e.GetPosition(AssociatedObject);
        }

        protected override void OnDetaching()
        {
            base.OnDetaching();
        }
    }
}
