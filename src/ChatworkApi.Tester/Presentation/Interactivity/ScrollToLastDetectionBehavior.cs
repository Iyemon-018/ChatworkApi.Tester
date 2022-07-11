namespace ChatworkApi.Tester.Presentation.Interactivity
{
    using System;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Controls.Primitives;
    using System.Windows.Input;
    using Microsoft.Xaml.Behaviors;

    public sealed class ScrollToLastDetectionBehavior : Behavior<ItemsControl>
    {
        public static readonly DependencyProperty ReachedCommandProperty
            = DependencyProperty.Register("ReachedCommand"
                                        , typeof(ICommand)
                                        , typeof(ScrollToLastDetectionBehavior)
                                        , new FrameworkPropertyMetadata(null));

        public static readonly DependencyProperty ReachedCommandParameterProperty
            = DependencyProperty.Register("ReachedCommandParameter"
                                        , typeof(object)
                                        , typeof(ScrollToLastDetectionBehavior)
                                        , new FrameworkPropertyMetadata(null));

        public ICommand ReachedCommand
        {
            get => (ICommand)GetValue(ReachedCommandProperty);
            set => SetValue(ReachedCommandProperty, value);
        }

        public object ReachedCommandParameter
        {
            get => (ICommand)GetValue(ReachedCommandParameterProperty);
            set => SetValue(ReachedCommandParameterProperty, value);
        }

        /// <summary>
        /// Called after the action is attached to an AssociatedObject.
        /// </summary>
        protected override void OnAttached()
        {
            base.OnAttached();

            AssociatedObject.AddHandler(ScrollBar.ValueChangedEvent, (RoutedEventHandler)OnValueChanged);
        }

        private void OnValueChanged(object          sender
                                  , RoutedEventArgs e)
        {
            if (e.OriginalSource is ScrollBar scrollBar)
            {
                if (Math.Abs(scrollBar.Value) < 0.1d)
                {
                    if (ReachedCommand == null) return;
                    if (ReachedCommand.CanExecute(ReachedCommandParameter))
                    {
                        ReachedCommand.Execute(ReachedCommandParameter);
                    }
                }
            }
        }

        /// <summary>
        /// Called when the behavior is being detached from its AssociatedObject, but before it has actually occurred.
        /// </summary>
        /// <remarks>Override this to unhook functionality from the AssociatedObject.</remarks>
        protected override void OnDetaching()
        {
            base.OnDetaching();

            AssociatedObject.RemoveHandler(ScrollBar.ValueChangedEvent, (RoutedEventHandler)OnValueChanged);
        }
    }
}