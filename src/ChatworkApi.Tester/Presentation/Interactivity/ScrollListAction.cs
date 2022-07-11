namespace ChatworkApi.Tester.Presentation.Interactivity
{
    using System;
    using System.Diagnostics;
    using System.Windows;
    using System.Windows.Controls;
    using Microsoft.Xaml.Behaviors;

    /// <summary>
    /// <see cref="ListBox"/> を最上段、もしくは最下段にスクロースるるためのアクション トリガーです。
    /// </summary>
    public sealed class ScrollListAction : TriggerAction<ListBox>
    {
        public static readonly DependencyProperty DirectionProperty
            = DependencyProperty.Register("Direction"
                                        , typeof(ScrollToDirection)
                                        , typeof(ScrollListAction)
                                        , new FrameworkPropertyMetadata(ScrollToDirection.Top));

        public ScrollToDirection Direction
        {
            get => (ScrollToDirection) GetValue(DirectionProperty);
            set => SetValue(DirectionProperty, value);
        }

        /// <summary>Invokes the action.</summary>
        /// <param name="parameter">The parameter to the action. If the action does not require a parameter, the parameter may be set to a null reference.</param>
        protected override void Invoke(object parameter)
        {
            object destinationScrollItem;

            if (Direction == ScrollToDirection.Top)
            {
                destinationScrollItem = AssociatedObject.Items.GetItemAt(0);
            }
            else if (Direction == ScrollToDirection.Bottom)
            {
                var lastIndex = AssociatedObject.Items.Count - 1;
                if (lastIndex < 0) return;

                destinationScrollItem = AssociatedObject.Items.GetItemAt(lastIndex);
                Debug.WriteLine("ScrollListAction.Invoke");
            }
            else
            {
                return;
            }

            AssociatedObject.ScrollIntoView(destinationScrollItem);
        }
    }
}