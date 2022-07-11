namespace ChatworkApi.Tester.ViewModels
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Linq;
    using System.Threading.Tasks;
    using System.Windows.Data;
    using System.Windows.Input;
    using Domain.Interactors.Requests;
    using Domain.Models;
    using Domain.UseCases;
    using Messages;
    using Presentation;
    using Presentation.ComponentModels;
    using Presentation.Models;
    using Prism.Commands;

    public sealed class ChatMessageViewModel : ValidatableViewModelBase
    {
        private readonly IAddMessageUseCase _useCase;

        /// <summary>
        /// ユーザー名検索
        /// </summary>
        private string _filteringUserName;

        /// <summary>
        /// 送信メッセージ
        /// </summary>
        private string _message;

        /// <summary>
        /// 現在のルームID
        /// </summary>
        private int _roomId;

        [Obsolete]
        public ChatMessageViewModel()
        {
        }

        public ChatMessageViewModel(IAddMessageUseCase useCase)
        {
            _useCase         = useCase;
            AssignedAccounts = new ObservableCollection<MessageTo>();
            BindingOperations.EnableCollectionSynchronization(AssignedAccounts, new object());

            AssignedAccountsView = CollectionViewSource.GetDefaultView(AssignedAccounts) as CollectionView;
            if (AssignedAccountsView != null) AssignedAccountsView.Filter += AssignedAccountsView_OnFilter;

            SendCommand = new DelegateCommand(ExecuteSendCommand);
        }

        /// <summary>
        /// 現在のルームIDを設定、または取得します。
        /// </summary>
        public int RoomId
        {
            get => _roomId;
            set => SetProperty(ref _roomId, value);
        }

        public CollectionView AssignedAccountsView { get; }

        /// <summary>
        /// ユーザー名検索を設定、または取得します。
        /// </summary>
        public string FilteringUserName
        {
            get => _filteringUserName;
            set => SetProperty(ref _filteringUserName, value);
        }

        public ObservableCollection<MessageTo> AssignedAccounts { get; }

        /// <summary>
        /// 送信メッセージを設定、または取得します。
        /// </summary>
        public string Message
        {
            get => _message;
            set => SetProperty(ref _message, value);
        }

        /// <summary>
        /// メッセージを送信するためのコマンドを取得します。
        /// </summary>
        public ICommand SendCommand { get; }

        private bool AssignedAccountsView_OnFilter(object item)
        {
            if (!(item is MessageTo messageTo)) return false;
            if (string.IsNullOrWhiteSpace(FilteringUserName)) return true;

            return messageTo.DisplayName.Contains(FilteringUserName);
        }

        private async void ExecuteSendCommand()
        {
            Validate();

            if (HasErrors) return;

            var request = new AddMessageRequest(RoomId, Message, false);
            var response = await _useCase.Execute(request);

            Message = string.Empty;

            OnSendCompleted();
        }

        protected override void OnValidate()
        {
            base.OnValidate();

            if (string.IsNullOrWhiteSpace(Message)) NotifyError(nameof(Message), $"メッセージを入力してください。");
        }

        public async Task SetAccounts(IEnumerable<RoomMember> members)
        {
            AssignedAccounts.Clear();

            var messageBuilder = new MessageBuilder();
            var memberToList = members.Select(x => new MessageTo(x.AccountId
                                                               , messageBuilder.To.Add(x.AccountId, x.Name).Build()
                                                               , x.AvatarUrl
                                                               , x.Name))
                                      .ToArray();

            // TODO 以下だと上のユーザーも含まれてしまう。.Build()のバグなので修正する。
            //AssignedAccounts.Add(new MessageTo(messageBuilder.To.All().Build(), null, "すべてのメンバー"));
            AssignedAccounts.Add(new MessageTo(-1,"[toall]", null, "すべてのメンバー"));
            AssignedAccounts.AddRange(memberToList);

            await BitmapImageMapping.SetAccountIconToImageProperties(memberToList
                                                             , x => x.AvatarImage
                                                             , x => x.Id
                                                             , x => x.AvatarUrl)
                                    .ConfigureAwait(false);
        }

        /// <summary>
        /// プロパティが変更された際に呼ばれます。
        /// </summary>
        /// <param name="propertyName">変更されたプロパティの名称</param>
        protected override void OnPropertyChangedCore(string propertyName)
        {
            base.OnPropertyChangedCore(propertyName);

            switch (propertyName)
            {
                case nameof(FilteringUserName):
                    OnFilteringUserNameChanged(FilteringUserName);

                    break;
            }
        }

        private void OnFilteringUserNameChanged(string newValue)
        {
            AssignedAccountsView.Refresh();
        }

        public event EventHandler<EventArgs> SendCompleted;

        private void OnSendCompleted()
        {
            SendCompleted?.Invoke(this, EventArgs.Empty);
        }
    }
}