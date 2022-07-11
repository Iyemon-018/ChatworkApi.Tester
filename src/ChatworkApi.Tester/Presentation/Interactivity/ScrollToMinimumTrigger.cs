namespace ChatworkApi.Tester.Presentation.Interactivity
{
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Controls.Primitives;
    using Microsoft.Xaml.Behaviors;

    /// <summary>
    /// コレクションUIのスクロールが最小値になったことを検知するトリガーです。
    /// </summary>
    public sealed class ScrollToMinimumTrigger : TriggerBase<Selector>
    {
        public static readonly DependencyProperty OrientationProperty =
            DependencyProperty.Register("Orientation", typeof(Orientation)
                                      , typeof(ScrollToMinimumTrigger)
                                      , new PropertyMetadata(Orientation.Vertical));

        public Orientation Orientation
        {
            get => (Orientation)GetValue(OrientationProperty);
            set => SetValue(OrientationProperty, value);
        }

        /// <summary>
        /// Called after the trigger is attached to an AssociatedObject.
        /// </summary>
        protected override void OnAttached()
        {
            base.OnAttached();

            AssociatedObject.AddHandler(ScrollBar.ScrollEvent, (RoutedEventHandler)OnScroll);
            AssociatedObject.AddHandler(ScrollViewer.ScrollChangedEvent, (RoutedEventHandler) OnScrollChanged);
        }

        private void OnScrollChanged(object sender
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

        private void OnScrollHorizontal(ScrollViewer scrollViewer
                                      , ScrollChangedEventArgs e)
        {
            if (e.ExtentWidthChange != 0)
            {
                if (e.HorizontalOffset == 0) scrollViewer.ScrollToHorizontalOffset(e.ExtentWidthChange);

                return;
            }

            var current = e.HorizontalOffset;
            if (current == 0)
            {
                var viewportWidth = e.ViewportWidth;
                InvokeActions(null);
                scrollViewer.ScrollToHorizontalOffset(current + viewportWidth);
            }
        }

        private void OnScrollVertical(ScrollViewer scrollViewer
                                    , ScrollChangedEventArgs e)
        {
            // e.VerticalOffset == 0 はスクロールが先頭まで到達したことを意味している。
            // e.ExtentHeightChange > 0 はScrollViewerの領域が変化していることを意味している。
            // つまり、ScrollViewerの領域が変化した際にスクロールの位置が頂点であれば、それはユーザーがスクロール操作を実施していることになる。
            //　このタイミングでスクロール位置をずらしてやることで、最後に表示した位置まで戻すことができる。
            if (e.ExtentHeightChange != 0)
            {
                if (e.VerticalOffset == 0) scrollViewer.ScrollToVerticalOffset(e.ExtentHeightChange);

                return;
            }

            var current = e.VerticalOffset;
            if (current == 0) InvokeActions(null);
        }

        private void OnScroll(object sender
                            , RoutedEventArgs e)
        {
            if (!(e.OriginalSource is ScrollBar scrollBar)) return;
            
            if (scrollBar.Value == scrollBar.Minimum) InvokeActions(null);
        }

        /// <summary>
        /// Called when the trigger is being detached from its AssociatedObject, but before it has actually occurred.
        /// </summary>
        protected override void OnDetaching()
        {
            AssociatedObject.RemoveHandler(ScrollBar.ScrollEvent, (RoutedEventHandler)OnScroll);
            AssociatedObject.RemoveHandler(ScrollViewer.ScrollChangedEvent, (RoutedEventHandler)OnScrollChanged);

            base.OnDetaching();
        }
    }
}