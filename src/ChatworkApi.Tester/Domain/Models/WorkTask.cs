namespace ChatworkApi.Tester.Domain.Models
{
    using System;
    using Prism.Mvvm;

    /// <summary>
    /// タスクの情報を保持するクラスです。
    /// </summary>
    public sealed class WorkTask : BindableBase
    {
        #region Fields

        /// <summary>
        /// このタスクを登録したユーザーの情報
        /// </summary>
        private Account _assigned;

        /// <summary>
        /// メッセージ本文
        /// </summary>
        private string _body;

        /// <summary>
        /// ID
        /// </summary>
        private int _id;

        /// <summary>
        /// 期限
        /// </summary>
        private DateTime? _limit;

        /// <summary>
        /// 期限の状態
        /// </summary>
        private TaskLimitType _limitType;

        /// <summary>
        /// メッセージID
        /// </summary>
        private string _messageId;

        /// <summary>
        /// このタスクが登録されているルームの情報
        /// </summary>
        private Room _room;

        /// <summary>
        /// 状態
        /// </summary>
        private TaskStatus _status;

        #endregion

        #region Properties

        /// <summary>
        /// IDを設定、または取得します。
        /// </summary>
        public int Id
        {
            get => _id;
            set => SetProperty(ref _id, value);
        }

        /// <summary>
        /// このタスクが登録されているルームの情報を設定、または取得します。
        /// </summary>
        public Room Room
        {
            get => _room;
            set => SetProperty(ref _room, value);
        }

        /// <summary>
        /// このタスクを登録したユーザーの情報を設定、または取得します。
        /// </summary>
        public Account Assigned
        {
            get => _assigned;
            set => SetProperty(ref _assigned, value);
        }

        /// <summary>
        /// メッセージIDを設定、または取得します。
        /// </summary>
        public string MessageId
        {
            get => _messageId;
            set => SetProperty(ref _messageId, value);
        }

        /// <summary>
        /// メッセージ本文を設定、または取得します。
        /// </summary>
        public string Body
        {
            get => _body;
            set => SetProperty(ref _body, value);
        }

        /// <summary>
        /// 期限を設定、または取得します。
        /// </summary>
        public DateTime? Limit
        {
            get => _limit;
            set => SetProperty(ref _limit, value);
        }

        /// <summary>
        /// 状態を設定、または取得します。
        /// </summary>
        public TaskStatus Status
        {
            get => _status;
            set => SetProperty(ref _status, value);
        }

        /// <summary>
        /// 期限の状態を設定、または取得します。
        /// </summary>
        public TaskLimitType LimitType
        {
            get => _limitType;
            set => SetProperty(ref _limitType, value);
        }

        #endregion
    }
}