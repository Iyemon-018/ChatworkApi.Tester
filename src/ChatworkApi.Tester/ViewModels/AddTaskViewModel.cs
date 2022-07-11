namespace ChatworkApi.Tester.ViewModels
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;
    using System.Windows.Data;
    using System.Windows.Input;
    using Domain;
    using Domain.Interactors.Requests;
    using Domain.Models;
    using Domain.UseCases;
    using Presentation.ComponentModels;
    using Presentation.Models;
    using Prism.Commands;

    public sealed class AddTaskViewModel : ValidatableViewModelBase
    {
        public ObservableCollection<SelectableAccount> AssignedAccountCollection { get; }

        private readonly IAddWorkTaskUseCase _useCase;

        /// <summary>
        /// タスクの内容
        /// </summary>
        private string _body;

        /// <summary>
        /// アカウント名のフィルタ
        /// </summary>
        private string _filteringUserName;

        /// <summary>
        /// 期限の指定が有効かどうか
        /// </summary>
        private bool _isEnableLimit;

        /// <summary>
        /// アカウント一覧を表示するかどうか
        /// </summary>
        private bool _isOpenAccounts;

        /// <summary>
        /// 期限の日付
        /// </summary>
        private DateTime _limitDate;

        /// <summary>
        /// 期限の日時
        /// </summary>
        private DateTime? _limitTime;

        /// <summary>
        /// 期限の種別
        /// </summary>
        private TaskLimitType _limitType;

        /// <summary>
        /// ルームID
        /// </summary>
        private int _roomId;

        public AddTaskViewModel()
        {
        }

        public AddTaskViewModel(IAddWorkTaskUseCase useCase)
        {
            _useCase = useCase;

            AssignedAccountCollection = new ObservableCollection<SelectableAccount>();
            BindingOperations.EnableCollectionSynchronization(AssignedAccountCollection, new object());

            AssignedAccounts = CollectionViewSource.GetDefaultView(AssignedAccountCollection) as CollectionView;
            if (AssignedAccounts != null) AssignedAccounts.Filter = OnFilterAssignedAccounts;

            AssignedAccountsValidationData = new ValidationData();

            PopupCommand    = new DelegateCommand(ExecutePopupCommand);
            RegisterCommand = new DelegateCommand(ExecuteRegisterCommand);
            CancelCommand   = new DelegateCommand(ExecuteCancelCommand);
        }

        /// <summary>
        /// タスクの内容を設定、または取得します。
        /// </summary>
        public string Body
        {
            get => _body;
            set => SetProperty(ref _body, value);
        }

        public CollectionView AssignedAccounts { get; }

        public ICommand PopupCommand { get; }

        /// <summary>
        /// アカウント一覧を表示するかどうかを設定、または取得します。
        /// </summary>
        public bool IsOpenAccounts
        {
            get => _isOpenAccounts;
            set => SetProperty(ref _isOpenAccounts, value);
        }

        /// <summary>
        /// アカウント名のフィルタを設定、または取得します。
        /// </summary>
        public string FilteringUserName
        {
            get => _filteringUserName;
            set => SetProperty(ref _filteringUserName, value);
        }

        /// <summary>
        /// 期限の日付を設定、または取得します。
        /// </summary>
        public DateTime LimitDate
        {
            get => _limitDate;
            set => SetProperty(ref _limitDate, value);
        }

        /// <summary>
        /// を設定、または取得します。
        /// </summary>
        public bool IsEnableLimit
        {
            get => _isEnableLimit;
            set => SetProperty(ref _isEnableLimit, value);
        }

        /// <summary>
        /// 期限の日時を設定、または取得します。
        /// </summary>
        public DateTime? LimitTime
        {
            get => _limitTime;
            set => SetProperty(ref _limitTime, value);
        }

        public ICommand RegisterCommand { get; }

        public ICommand CancelCommand { get; }

        /// <summary>
        /// ルームIDを設定、または取得します。
        /// </summary>
        public int RoomId
        {
            get => _roomId;
            set => SetProperty(ref _roomId, value);
        }

        /// <summary>
        /// 期限の種別を設定、または取得します。
        /// </summary>
        public TaskLimitType LimitType
        {
            get => _limitType;
            set => SetProperty(ref _limitType, value);
        }

        private void ExecutePopupCommand()
        {
            IsOpenAccounts = true;
        }

        public event EventHandler<EventArgs> Register;

        public event EventHandler<EventArgs> Cancel;

        private async void ExecuteRegisterCommand()
        {
            Validate();

            if (HasErrors) return;

            var selectedIds = AssignedAccountCollection.Where(x => x.Selected).Select(x => x.AccountId).ToArray();

            DateTime? limitDateTime = null;
            if (LimitType == TaskLimitType.Date)
            {
                if (LimitTime.HasValue)
                {
                    LimitType     = TaskLimitType.DateTime;
                    limitDateTime = LimitDate.AddMinutes(LimitTime.Value.TimeOfDay.TotalMinutes);
                }
                else
                {
                    limitDateTime = LimitDate;
                }
            }

            var response = await _useCase.Execute(new AddWorkTaskRequest(RoomId, Body, selectedIds, limitDateTime, LimitType));

            OnRegister();
        }

        protected override void OnValidate()
        {
            base.OnValidate();

            if (string.IsNullOrWhiteSpace(Body))
            {
                NotifyError(() => Body, $"タスク内容は必ず入力してください。");
            }

            if (!AssignedAccountCollection.Any(x => x.Selected))
            {
                // NotifyError を呼び出さないと、この ViewModel 自体がエラーとして認識されない。
                // 苦肉の策なのでもうちょっといい感じにスッキリさせたい。
                AssignedAccountsValidationData.Invalid($"担当者は１人以上設定してください。");
                NotifyError(() => AssignedAccountCollection, AssignedAccountsValidationData.ErrorMessage);
            }
            else
            {
                AssignedAccountsValidationData.Valid();
            }
        }

        /// <summary>
        /// 担当者一覧の検証結果を取得します。
        /// </summary>
        public ValidationData AssignedAccountsValidationData { get; }

        private void ExecuteCancelCommand()
        {
            OnCancel();
        }

        public void SetRoomMembers(IEnumerable<RoomMember> accounts)
        {
            foreach (var selectableAccount in AssignedAccountCollection)
            {
                selectableAccount.SelectionChanged -= SelectableAccount_OnSelectionChanged;
            }

            AssignedAccountCollection.Clear();
            AssignedAccountCollection.AddRange(accounts.Select(x => new SelectableAccount(x.AccountId, x.Name, x.AvatarUrl)).ToArray());

            foreach (var selectableAccount in AssignedAccountCollection)
            {
                selectableAccount.SelectionChanged += SelectableAccount_OnSelectionChanged;
            }
        }

        private void SelectableAccount_OnSelectionChanged(object sender
                                                       , bool   e)
        {
            if (e)
            {
                // 選択されたときにエラー状態を解除したい。
                // これくらいしか方法が思い浮かばなかった
                AssignedAccountsValidationData.Valid();
            }
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

                case nameof(IsEnableLimit):
                    OnIsEnableLimitChanged(IsEnableLimit);
                    break;
            }
        }

        private void OnIsEnableLimitChanged(bool newValue)
        {
            if (newValue)
            {
                // 有効状態に切り替わる操作をした場合は日付指定として扱う。
                LimitTime = null;
                LimitType = TaskLimitType.Date;
            }
            else
            {
                LimitType = TaskLimitType.None;
            }
        }

        private void OnFilteringUserNameChanged(string newValue)
        {
            AssignedAccounts.Refresh();
        }

        private bool OnFilterAssignedAccounts(object item)
        {
            if (!(item is SelectableAccount member)) return false;
            if (string.IsNullOrWhiteSpace(FilteringUserName)) return true;

            return member.Name.Contains(FilteringUserName);
        }

        public void Cleanup()
        {
            Body          = string.Empty;
            LimitDate     = DateTime.Today;
            LimitTime     = null;
            IsEnableLimit = false;

            foreach (var selectableAccount in AssignedAccountCollection) selectableAccount.Selected = false;
        }

        private void OnRegister()
        {
            Register?.Invoke(this, EventArgs.Empty);
        }

        private void OnCancel()
        {
            Cancel?.Invoke(this, EventArgs.Empty);
        }
    }
}