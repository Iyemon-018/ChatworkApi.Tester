namespace ChatworkApi.Tester.ViewModels
{
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.ComponentModel;
    using System.Linq;
    using System.Threading.Tasks;
    using System.Windows.Data;
    using System.Windows.Input;
    using Domain.Interactors.Requests;
    using Domain.Models;
    using Domain.UseCases;
    using Presentation;
    using Presentation.ComponentModels;
    using Presentation.Converters;
    using Prism.Commands;

    public sealed class ContactsViewModel : ViewModelBase, IViewLoadedHolder
    {
        private readonly IGetContactsUseCase _useCase;

        /// <summary>
        /// コンタクトを表示しているかどうか
        /// </summary>
        private bool _viewingContact;

        public ContactsViewModel()
        {
        }

        public ContactsViewModel(IGetContactsUseCase useCase)
        {
            _useCase                       = useCase;
            _viewingContact                = false;
            LoadedCommand                  = new DelegateCommand(ExecuteLoadedCommand);
            ContactSelectionChangedCommand = new DelegateCommand(ExecuteContactSelectionChangedCommand);
            GetNextCommand                 = new DelegateCommand(ExecuteGetNextCommand);

            Contacts = new ObservableCollection<Contact>();
            BindingOperations.EnableCollectionSynchronization(Contacts, new object());

            ContactsView = CollectionViewSource.GetDefaultView(Contacts) as CollectionView;
            if (ContactsView != null)
            {
                // 名前の頭文字でグループ化した外観を作る。
                // 並び替え順序はアプリ側と同じく名前の昇順とする。
                ContactsView.GroupDescriptions.Add(new PropertyGroupDescription(nameof(Contact.Name)
                                                                              , new NameToInitialConverter()));
                ContactsView.SortDescriptions.Add(new SortDescription(nameof(Contact.Name)
                                                                    , ListSortDirection.Ascending));
            }
        }

        private CollectionAmountAdd<Contact> _contactsAmount;

        public ObservableCollection<Contact> Contacts { get; }

        public CollectionView ContactsView { get; }

        /// <summary>
        /// コンタクトを表示しているかどうかを設定、または取得します。
        /// </summary>
        public bool ViewingContact
        {
            get => _viewingContact;
            set => SetProperty(ref _viewingContact, value);
        }

        public ICommand ContactSelectionChangedCommand { get; }

        /// <summary>
        /// Loaded イベントの実行時に呼ばれるコマンドを取得します。
        /// </summary>
        public ICommand LoadedCommand { get; }

        public ICommand GetNextCommand { get; }

        private void ExecuteContactSelectionChangedCommand()
        {
            ViewingContact = true;
        }

        private async void ExecuteLoadedCommand()
        {
            Contacts.Clear();

            var response = await _useCase.Execute(GetContactsRequest.Empty()).ConfigureAwait(false);
            var contacts = response.Contacts.OrderBy(x => x.Name).ToArray();
            _contactsAmount = new CollectionAmountAdd<Contact>(contacts, Contacts, 20);

            await AddContactsAsync();
        }

        private async void ExecuteGetNextCommand() => await AddContactsAsync();

        private async Task AddContactsAsync()
        {
            var items = await _contactsAmount.ExecuteAsync().ConfigureAwait(false);
            await BitmapImageMapping.SetAccountIconToImageProperties(items
                                                                   , x => x.AvatarImage
                                                                   , x => x.AccountId
                                                                   , x => x.AvatarImageUrl);
        }
    }
}