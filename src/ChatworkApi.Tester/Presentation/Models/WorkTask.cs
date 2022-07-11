namespace ChatworkApi.Tester.Presentation.Models
{
    using System;
    using System.Collections.ObjectModel;
    using Domain;
    using Prism.Mvvm;

    /// <summary>
    /// 画面入力用のタスク情報を保持するクラスです。
    /// </summary>
    public sealed class WorkTask : BindableBase
    {
        /// <summary>
        /// 担当者のアカウントID
        /// </summary>
        private ObservableCollection<int> _assignedAccountIdList;

        /// <summary>
        /// タスクの内容
        /// </summary>
        private string _body;

        /// <summary>
        /// 期限の日付
        /// </summary>
        private DateTime _limitDate;

        /// <summary>
        /// 期限の時刻
        /// </summary>
        private DateTime? _limitTime;

        /// <summary>
        /// 期限の種別
        /// </summary>
        private TaskLimitType _limitType;

        /// <summary>
        /// タスクを登録するルームID
        /// </summary>
        private int _roomId;

        public WorkTask()
        {
            _assignedAccountIdList = new ObservableCollection<int>();
            _limitDate             = DateTime.Today;
            _limitType             = TaskLimitType.Date;
        }

        /// <summary>
        /// タスクの内容を設定、または取得します。
        /// </summary>
        public string Body
        {
            get => _body;
            set => SetProperty(ref _body, value);
        }

        /// <summary>
        /// タスクを登録するルームIDを設定、または取得します。
        /// </summary>
        public int RoomId
        {
            get => _roomId;
            set => SetProperty(ref _roomId, value);
        }

        /// <summary>
        /// 担当者のアカウントIDを設定、または取得します。
        /// </summary>
        public ObservableCollection<int> AssignedAccountIdList
        {
            get => _assignedAccountIdList;
            set => SetProperty(ref _assignedAccountIdList, value);
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
        /// 期限の時刻を設定、または取得します。
        /// </summary>
        public DateTime? LimitTime
        {
            get => _limitTime;
            set => SetProperty(ref _limitTime, value);
        }

        /// <summary>
        /// 期限の種別を設定、または取得します。
        /// </summary>
        public TaskLimitType LimitType
        {
            get => _limitType;
            set => SetProperty(ref _limitType, value);
        }
    }
}