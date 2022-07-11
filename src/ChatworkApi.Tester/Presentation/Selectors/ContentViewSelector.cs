namespace ChatworkApi.Tester.Presentation.Selectors
{
    using System;
    using System.Collections.Concurrent;
    using System.Collections.Generic;
    using System.Windows;
    using System.Windows.Controls;

    public sealed class ContentViewSelector : DataTemplateSelector
    {
        private readonly ConcurrentDictionary<ViewType, string> ViewTypeToTemplateKeyMap
            = new ConcurrentDictionary<ViewType, string>(new List<KeyValuePair<ViewType, string>>
                                                         {
                                                             new KeyValuePair<ViewType, string>(ViewType.Chat, "View.ChatRoomListView.Template")
                                                             , new KeyValuePair<ViewType, string>(ViewType.TaskList, "View.TaskListView.Template")
                                                             , new KeyValuePair<ViewType, string>(ViewType.Contact, "View.ContactsView.Template")
                                                             , new KeyValuePair<ViewType, string>(ViewType.Account, "View.ProfileView.Template")
                                                         });

        public ContentViewSelector()
        {
            TemplateCache<ViewType>.Set(x => ViewTypeToTemplateKeyMap.ContainsKey(x)
                                                 ? Application.Current.TryFindResource(ViewTypeToTemplateKeyMap[x]) as DataTemplate
                                                 : null);
        }

        /// <summary>When overridden in a derived class, returns a <see cref="T:System.Windows.DataTemplate" /> based on custom logic.</summary>
        /// <param name="item">The data object for which to select the template.</param>
        /// <param name="container">The data-bound object.</param>
        /// <returns>Returns a <see cref="T:System.Windows.DataTemplate" /> or <see langword="null" />. The default value is <see langword="null" />.</returns>
        public override DataTemplate SelectTemplate(object           item
                                                  , DependencyObject container)
        {
            if (!(item is ViewMenu viewMenu)) return base.SelectTemplate(item, container);
            
            return TemplateCache<ViewType>.Get(viewMenu.Type);
        }
    }
}