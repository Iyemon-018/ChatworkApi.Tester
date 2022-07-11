namespace ChatworkApi.Tester.Presentation.Selectors
{
    using System;
    using System.Windows;
    using System.Windows.Controls;

    public sealed class AddTaskTemplateSelector : DataTemplateSelector
    {
        private static readonly DataTemplate AddTaskTemplate;

        static AddTaskTemplateSelector()
        {
            AddTaskTemplate = Application.Current.TryFindResource("View.AddTaskView.Template") as DataTemplate;
        }
        /// <summary>When overridden in a derived class, returns a <see cref="T:System.Windows.DataTemplate" /> based on custom logic.</summary>
        /// <param name="item">The data object for which to select the template.</param>
        /// <param name="container">The data-bound object.</param>
        /// <returns>Returns a <see cref="T:System.Windows.DataTemplate" /> or <see langword="null" />. The default value is <see langword="null" />.</returns>
        public override DataTemplate SelectTemplate(object           item
                                                  , DependencyObject container)
        {
            return item != null ? AddTaskTemplate : throw new NullReferenceException();
        }
    }
}