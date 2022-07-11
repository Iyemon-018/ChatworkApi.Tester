namespace ChatworkApi.Tester.ViewModels
{
    using System;
    using System.Collections.ObjectModel;
    using System.ComponentModel;
    using System.Linq;
    using System.Threading.Tasks;
    using System.Windows.Data;
    using System.Windows.Input;
    using Bus;
    using Domain.Interactors.Requests;
    using Domain.Models;
    using Domain.UseCases;
    using Presentation;
    using Presentation.ComponentModels;
    using Presentation.Components;
    using Presentation.Models;
    using Prism.Commands;

    /// <summary>
    /// 特定のチャットルーム画面の機能を提供する ViewModel クラスです。
    /// </summary>
    public sealed class ChatRoomViewModel : ViewModelBase, IViewLoadedHolder
    {
        private readonly IGetMyRoomsUseCase _myRoomsUseCase;

        private readonly IGetRoomDataUseCase _roomDataUseCase;

        private readonly IGetRoomMembersUseCase _roomMembersUseCase;

        private readonly IGetRoomMessagesUseCase _roomMessagesUseCase;

        private readonly IChatRoomUseCaseBus _useCaseBus;

        /// <summary>
        /// タスク登録用のViewModel
        /// </summary>
        private AddTaskViewModel _addTaskViewModel;

        private CollectionAmountAdd<MyRoom> _collectionAmount;

        /// <summary>
        /// タスク追加 View を表示するかどうか
        /// </summary>
        private bool _isOpenTaskCreateView;

        private CollectionAmountAdd<ChatMessage> _messageAmountAdd;

        /// <summary>
        /// ルームが変更されたかどうか
        /// </summary>
        private bool _roomChanged;

        /// <summary>
        /// ルームの詳細情報
        /// </summary>
        private RoomDataViewModel _roomData;

        /// <summary>
        /// 検索対象のルーム名称
        /// </summary>
        private string _searchingRoomName;

        /// <summary>
        /// 選択されているフィルター条件
        /// </summary>
        private ChatRoomFilterCondition _selectedFilterCondition;

        /// <summary>
        /// 選択されているチャットルーム
        /// </summary>
        private MyRoom _selectedMyRoom;

        /// <summary>
        /// 選択されているチャットルーム名称
        /// </summary>
        private string _selectedRoomName;

        /// <summary>
        /// 現在の状態
        /// </summary>
        private ChatRoomListViewState _viewState;

        /// <summary>
        /// ルーム一覧を表示するかどうか
        /// </summary>
        private bool _visibleRoomList;

        /// <summary>
        /// ルーム検索領域を表示するかどうか
        /// </summary>
        private bool _visibleSearch;

        public ChatRoomViewModel(IChatRoomUseCaseBus useCaseBus)
        {
            _useCaseBus           = useCaseBus;
            LoadedCommand         = new DelegateCommand(ExecuteLoadedCommand);
            _visibleRoomList      = true;
            _isOpenTaskCreateView = false;

            _myRoomsUseCase          = _useCaseBus.GetMyRooms;
            _roomMessagesUseCase     = _useCaseBus.GetRoomMessages;
            _roomMembersUseCase      = _useCaseBus.GetRoomMembers;
            _roomDataUseCase         = _useCaseBus.GetRoomData;
            _selectedFilterCondition = ChatRoomFilterCondition.All;

            Rooms = new ObservableCollection<MyRoom>();
            BindingOperations.EnableCollectionSynchronization(Rooms, new object());

            RoomsView = CollectionViewSource.GetDefaultView(Rooms) as CollectionView;
            if (RoomsView != null)
            {
                // フィルター条件は以下の組み合わせとする。
                // 1.ピン留めされているものを上位に
                // 2.メンションのあるものを上位に
                // 3.未読のあるものを上位に
                // 4.最終更新時間が新しいものを上位に
                // この順序は Chatwork のデフォルトの並び替え順序である。
                RoomsView.Filter = OnRoomsViewFilter;
                RoomsView.SortDescriptions.Add(new SortDescription(nameof(MyRoom.Sticky)
                                                                 , ListSortDirection.Descending));
                RoomsView.SortDescriptions.Add(new SortDescription(nameof(MyRoom.HasMention)
                                                                 , ListSortDirection.Descending));
                RoomsView.SortDescriptions.Add(new SortDescription(nameof(MyRoom.ExistsUnread)
                                                                 , ListSortDirection.Descending));
                RoomsView.SortDescriptions.Add(new SortDescription(nameof(MyRoom.LastUpdate)
                                                                 , ListSortDirection.Descending));
            }

            Messages = new ObservableCollection<ChatMessage>();
            BindingOperations.EnableCollectionSynchronization(Messages, new object());

            FilterConditions = new ObservableCollection<CollectionViewItem<ChatRoomFilterCondition>>
                               {
                                   new CollectionViewItem<ChatRoomFilterCondition>
                                   {
                                       DisplayName = "すべてのチャットルーム", Item = ChatRoomFilterCondition.All
                                   }
                                 , new CollectionViewItem<ChatRoomFilterCondition>
                                   {
                                       DisplayName = "未読のあるチャットルーム", Item = ChatRoomFilterCondition.Unread
                                   }
                                 , new CollectionViewItem<ChatRoomFilterCondition>
                                   {
                                       DisplayName = "タスクのあるチャットルーム", Item = ChatRoomFilterCondition.ContainsMyTasks
                                   }
                               };
            ToggleStateToSearch    = new DelegateCommand(() => ViewState = ChatRoomListViewState.Search);
            ToggleStateToRoomList  = new DelegateCommand(() => ViewState = ChatRoomListViewState.RoomSelect);
            GetPastMessagesCommand = new DelegateCommand(ExecuteGetPastMessagesCommand);

            _addTaskViewModel          =  new AddTaskViewModel(_useCaseBus.AddWorkTask);
            _addTaskViewModel.Register += AddTask_OnRegister;
            _addTaskViewModel.Cancel   += AddTask_OnCancel;

            AddTaskCommand = new DelegateCommand(ExecuteAddTaskCommand);

            ChatMessage               =  new ChatMessageViewModel(useCaseBus.AddMessage);
            ChatMessage.SendCompleted += ChatMessage_OnSendCompleted;

            RoomData = new RoomDataViewModel();

            NotifyMessage = new NotifyMessage();

            NextRoomGetCommand = new DelegateCommand(ExecuteNextRoomCommand);
        }

        /// <summary>
        /// タスク登録用のViewModelを取得します。
        /// </summary>
        public AddTaskViewModel AddTaskViewModel
        {
            get => _addTaskViewModel;
            private set => SetProperty(ref _addTaskViewModel, value);
        }

        /// <summary>
        /// 選択されているチャットルーム名称を取得します。
        /// </summary>
        public string SelectedRoomName
        {
            get => _selectedRoomName;
            private set => SetProperty(ref _selectedRoomName, value);
        }

        /// <summary>
        /// 選択されているチャットルームを設定、または取得します。
        /// </summary>
        public MyRoom SelectedMyRoom
        {
            get => _selectedMyRoom;
            set => SetProperty(ref _selectedMyRoom, value);
        }

        /// <summary>
        /// ルーム一覧を表示するかどうかを設定、または取得します。
        /// </summary>
        public bool VisibleRoomList
        {
            get => _visibleRoomList;
            set => SetProperty(ref _visibleRoomList, value);
        }

        /// <summary>
        /// ルームの詳細情報を取得します。
        /// </summary>
        public RoomDataViewModel RoomData
        {
            get => _roomData;
            private set => SetProperty(ref _roomData, value);
        }

        /// <summary>
        /// フィルター条件に設定可能な項目のコレクションを取得します。
        /// </summary>
        public ObservableCollection<CollectionViewItem<ChatRoomFilterCondition>> FilterConditions { get; }

        /// <summary>
        /// 自分の参加しているチャットルームのコレクションを取得します。
        /// </summary>
        public ObservableCollection<MyRoom> Rooms { get; }

        /// <summary>
        /// 選択中のチャットメッセージのコレクションを取得します。
        /// </summary>
        public ObservableCollection<ChatMessage> Messages { get; }

        /// <summary>
        /// 選択されているフィルター条件を設定、または取得します。
        /// </summary>
        public ChatRoomFilterCondition SelectedFilterCondition
        {
            get => _selectedFilterCondition;
            set => SetProperty(ref _selectedFilterCondition, value);
        }

        /// <summary>
        /// 検索対象のルーム名称を設定、または取得します。
        /// </summary>
        public string SearchingRoomName
        {
            get => _searchingRoomName;
            set => SetProperty(ref _searchingRoomName, value);
        }

        /// <summary>
        /// ルーム検索領域を表示するかどうかを設定、または取得します。
        /// </summary>
        public bool VisibleSearch
        {
            get => _visibleSearch;
            private set => SetProperty(ref _visibleSearch, value);
        }

        /// <summary>
        /// ルーム検索状態に切り替えるためのコマンドを取得します。
        /// </summary>
        public ICommand ToggleStateToSearch { get; }

        /// <summary>
        /// ルーム一覧表示状態に切り替えるためのコマンドを取得します。
        /// </summary>
        public ICommand ToggleStateToRoomList { get; }

        public ICommand GetPastMessagesCommand { get; }

        /// <summary>
        /// タスク追加ボタンを押下したときに呼ばれるコマンドを取得します。
        /// </summary>
        public ICommand AddTaskCommand { get; }

        public ICommand NextRoomGetCommand { get; }

        /// <summary>
        /// タスク追加 View を表示するかどうかを設定、または取得します。
        /// </summary>
        public bool IsOpenTaskCreateView
        {
            get => _isOpenTaskCreateView;
            set => SetProperty(ref _isOpenTaskCreateView, value);
        }

        /// <summary>
        /// 現在の状態を取得します。
        /// </summary>
        public ChatRoomListViewState ViewState
        {
            get => _viewState;
            private set => SetProperty(ref _viewState, value);
        }

        public CollectionView RoomsView { get; }

        /// <summary>
        /// ユーザーへ通知する一過性のメッセージ情報を取得します。
        /// </summary>
        public NotifyMessage NotifyMessage { get; }

        public ChatMessageViewModel ChatMessage { get; }

        /// <summary>
        /// ルームが変更されたかどうかを設定、または取得します。
        /// </summary>
        public bool RoomChanged
        {
            get => _roomChanged;
            private set => SetProperty(ref _roomChanged, value);
        }

        /// <summary>
        /// Loaded イベントの実行時に呼ばれるコマンドを取得します。
        /// </summary>
        public ICommand LoadedCommand { get; }

        private async void ExecuteNextRoomCommand() => await AddRoomsAsync().ConfigureAwait(false);

        private void AddTask_OnRegister(object    sender
                                      , EventArgs e)
        {
            IsOpenTaskCreateView = false;

            NotifyMessage.Notify("タスクを登録しました。");
        }

        private void AddTask_OnCancel(object    sender
                                    , EventArgs e)
        {
            IsOpenTaskCreateView = false;
        }

        private async void ExecuteLoadedCommand()
        {
            var response = await _myRoomsUseCase.Execute(GetMyRoomsRequest.Empty());
            var rooms    = response.Rooms.ToArray();

            // スクロールするたびに10件ずつ増やす。この程度であれば描画速度は人間の目には低下しているようには見えない。
            // 最初だけはスクロールバーが表示できるまでチャットルームを表示するため、２回実行している。
            _collectionAmount = new CollectionAmountAdd<MyRoom>(rooms, Rooms, 20);

            await AddRoomsAsync().ConfigureAwait(false);
        }

        private async Task AddRoomsAsync()
        {
            var rooms = await _collectionAmount.ExecuteAsync().ConfigureAwait(false);
            await BitmapImageMapping.SetRoomIconToImageProperties(rooms
                                                                , x => x.IconImage
                                                                , x => x.Id
                                                                , x => x.IconPath)
                                    .ConfigureAwait(false);
        }

        private bool OnRoomsViewFilter(object item)
        {
            if (item is MyRoom myRoom)
            {
                var accept = true;

                if (SelectedFilterCondition == ChatRoomFilterCondition.ContainsMyTasks)
                {
                    accept = myRoom.AssignedTaskCount > 0;
                }
                else if (SelectedFilterCondition == ChatRoomFilterCondition.Unread)
                {
                    accept = myRoom.UnreadCount > 0;
                }

                if (!string.IsNullOrEmpty(SearchingRoomName)) accept = myRoom.Name.Contains(SearchingRoomName);

                return accept;
            }

            return true;
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
                case nameof(ViewState):
                    OnViewStateChanged(_viewState);
                    break;
                case nameof(SelectedFilterCondition):
                    OnSelectedFilterConditionChanged(SelectedFilterCondition);
                    break;
                case nameof(SearchingRoomName):
                    OnSearchingRoomNameChanged(SearchingRoomName);
                    break;
                case nameof(SelectedMyRoom):
                    OnSelectedMyRoomChanged(SelectedMyRoom);
                    break;
            }
        }

        /// <summary>
        /// <see cref="SelectedMyRoom" /> が変更された際に呼ばれます。
        /// </summary>
        /// <param name="newValue"></param>
        private async void OnSelectedMyRoomChanged(MyRoom newValue)
        {
            if (newValue == null) return;

            var selectedRoomId = newValue.Id;
            AddTaskViewModel.RoomId = selectedRoomId;

            SelectedRoomName = newValue.Name;
            VisibleRoomList  = false;
            RoomChanged      = false;

            await GetRoomMessagesAsync(selectedRoomId).ConfigureAwait(false);
            await GetRoomDataAsync(selectedRoomId).ConfigureAwait(false);

            ChatMessage.RoomId = selectedRoomId;
            RoomChanged        = true;

            // こいつが最も最大処理時間が長くなるため、最後に実行する。
            // 最後に実行しないと次の処理が実行できなくなるので。
            await GetRoomMembersAsync(selectedRoomId).ConfigureAwait(false);
        }

        private async Task GetRoomDataAsync(int roomId)
        {
            var request     = new GetRoomDataRequest(roomId);
            var response    = await _roomDataUseCase.Execute(request);
            var description = response.RoomData.Description;

            RoomData.Update(description);
        }

        private async Task GetRoomMessagesAsync(int roomId)
        {
            Messages.Clear();

            var response = await _roomMessagesUseCase.Execute(new GetRoomMessagesRequest(roomId, true))
                                                     .ConfigureAwait(false);

            var messages = response.Messages.OrderByDescending(x => x.SendDateTime).ToArray();
            _messageAmountAdd = new CollectionAmountAdd<ChatMessage>(messages
                                                                   , Messages
                                                                   , 10
                                                                   , ItemAddOrientation.First);
            await AddMessagesAsync().ConfigureAwait(false);
        }

        private async Task AddMessagesAsync()
        {
            var messages = await _messageAmountAdd.ExecuteAsync().ConfigureAwait(false);
            await BitmapImageMapping.SetAccountIconToImageProperties(messages.Select(x => x.Sender).ToArray()
                                                                   , x => x.AvatarImage
                                                                   , x => x.Id
                                                                   , x => x.AvatarUrl)
                                    .ConfigureAwait(false);
        }

        /// <summary>
        /// メッセージの送信が完了したあとに呼ばれるイベントハンドラです。
        /// </summary>
        /// <param name="sender">イベント送信元オブジェクト</param>
        /// <param name="e">イベント引数オブジェクト</param>
        private async void ChatMessage_OnSendCompleted(object    sender
                                                     , EventArgs e)
        {
            var request  = new GetRoomMessagesRequest(SelectedMyRoom.Id, false);
            var response = await _roomMessagesUseCase.Execute(request).ConfigureAwait(false);

            _messageAmountAdd.AddSource(response.Messages);
            await _messageAmountAdd.ExecuteAsync();
        }

        private async Task GetRoomMembersAsync(int roomId)
        {
            var request  = new GetRoomMembersRequest(roomId);
            var response = await _roomMembersUseCase.Execute(request).ConfigureAwait(false);
            var members  = response.Members.ToArray();

            AddTaskViewModel.SetRoomMembers(members);
            await ChatMessage.SetAccounts(members).ConfigureAwait(false);
            RoomData.SetMembers(members);

            // 5人ずつアイコンを表示する。
            // 表示する人数が100人とかになると劇的に描画速度が落ちる。
            // アイコンのバインドがユーザー分完了するまで描画されないとフリーズしたように見えるので
            // 少しずつ表示していくことでフリーズしていないように見せる。
            // TODO ここはもう少しスッキリ書きたい。
            const int SplitCount = 5;
            for (int i = 0; i < members.Length / SplitCount + 1; i++)
            {
                var items = members.Skip(i * SplitCount).Take(SplitCount).ToArray();
                await BitmapImageMapping.SetRoomIconToImageProperties(items
                                                                    , x => x.AvatarImage
                                                                    , x => x.AccountId
                                                                    , x => x.AvatarUrl)
                                        .ConfigureAwait(false);
            }
        }

        private async void ExecuteGetPastMessagesCommand()
        {
            if (!Messages.Any()) return;

            await AddMessagesAsync().ConfigureAwait(false);
        }

        private void ExecuteAddTaskCommand()
        {
            AddTaskViewModel.Cleanup();
            IsOpenTaskCreateView = true;
        }

        private void OnSearchingRoomNameChanged(string newValue)
        {
            RoomsView.Refresh();
        }

        private void OnSelectedFilterConditionChanged(ChatRoomFilterCondition newValue)
        {
            RoomsView.Refresh();
        }

        private void OnViewStateChanged(ChatRoomListViewState viewState)
        {
            VisibleSearch = viewState == ChatRoomListViewState.Search;

            if (viewState != ChatRoomListViewState.Search) SearchingRoomName = string.Empty;
        }
    }
}