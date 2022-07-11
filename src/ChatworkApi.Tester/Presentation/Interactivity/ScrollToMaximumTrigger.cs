namespace ChatworkApi.Tester.Presentation.Interactivity
{
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Controls.Primitives;
    using Microsoft.Xaml.Behaviors;

    /// <summary>
    /// コレクションUIのスクロールが最大値になったことを検知するトリガーです。
    /// </summary>
    public sealed class ScrollToMaximumTrigger : TriggerBase<Selector>
    {
        public static readonly DependencyProperty OrientationProperty =
            DependencyProperty.Register("Orientation", typeof(Orientation)
                                      , typeof(ScrollToMaximumTrigger)
                                      , new PropertyMetadata(Orientation.Vertical));

        public Orientation Orientation
        {
            get => (Orientation) GetValue(OrientationProperty);
            set => SetValue(OrientationProperty, value);
        }

        /// <summary>
        /// Called after the trigger is attached to an AssociatedObject.
        /// </summary>
        protected override void OnAttached()
        {
            base.OnAttached();

            AssociatedObject.AddHandler(ScrollBar.ScrollEvent, (RoutedEventHandler) OnScroll);
            AssociatedObject.AddHandler(ScrollViewer.ScrollChangedEvent, (RoutedEventHandler) OnScrollChanged);
        }

        private void OnScrollChanged(object          sender
                                   , RoutedEventArgs e)
        {
            if (!(e.OriginalSource is ScrollViewer scrollViewer)) return;
            if (!(e is ScrollChangedEventArgs args)) return;

            if (Orientation == Orientation.Vertical)
            {
                OnScrollVertical(scrollViewer, args);
            }
            else if (Orientation == Orientation.Horizontal)
            {
                OnScrollHorizontal(scrollViewer, args);
            }
        }

        private void OnScrollHorizontal(ScrollViewer           scrollViewer
                                      , ScrollChangedEventArgs e)
        {
            if (e.ExtentWidth == 0) return;

            var currentHorizontalOffset = e.HorizontalOffset;
            var current = e.ViewportWidth + currentHorizontalOffset;
            if (current == e.ExtentWidth)
            {
                InvokeActions(null);
                scrollViewer.ScrollToHorizontalOffset(currentHorizontalOffset);
            }
        }

        private void OnScrollVertical(ScrollViewer           scrollViewer
                                    , ScrollChangedEventArgs e)
        {
            if (e.ExtentHeight == 0) return;

            var currentVerticalOffset = e.VerticalOffset;
            var current = e.ViewportHeight + currentVerticalOffset;
            if (current == e.ExtentHeight)
            {
                InvokeActions(null);
                scrollViewer.ScrollToVerticalOffset(currentVerticalOffset);
            }
        }

        private void OnScroll(object          sender
                            , RoutedEventArgs e)
        {
            if (!(e.OriginalSource is ScrollBar scrollBar)) return;

            if (scrollBar.Value == scrollBar.Maximum) InvokeActions(null);
        }

        /// <summary>
        /// Called when the trigger is being detached from its AssociatedObject, but before it has actually occurred.
        /// </summary>
        protected override void OnDetaching()
        {
            AssociatedObject.RemoveHandler(ScrollBar.ScrollEvent, (RoutedEventHandler) OnScroll);
            AssociatedObject.RemoveHandler(ScrollViewer.ScrollChangedEvent, (RoutedEventHandler) OnScrollChanged);

            base.OnDetaching();
        }
    }
}