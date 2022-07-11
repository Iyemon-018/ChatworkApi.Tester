namespace ChatworkApi.Tester.Domain.Models
{
    using System;
    using Prism.Mvvm;

    public sealed class RoomData : BindableBase
    {
        /// <summary>
        /// 概要
        /// </summary>
        private string _description;

        /// <summary>
        /// ファイル数
        /// </summary>
        private int _fileCount;

        /// <summary>
        /// ルームのアイコンパス
        /// </summary>
        private string _iconPath;

        /// <summary>
        /// ルームID
        /// </summary>
        private int _id;

        /// <summary>
        /// 最後に更新した日時
        /// </summary>
        private DateTime? _lastUpdated;

        /// <summary>
        /// 返信数
        /// </summary>
        private int _mentionCount;

        /// <summary>
        /// メッセージ数
        /// </summary>
        private int _messageCount;

        /// <summary>
        /// 自分に割り当てられているタスク数
        /// </summary>
        private int _myTaskCount;

        /// <summary>
        /// 名称
        /// </summary>
        private string _name;

        /// <summary>
        /// このルームでの自分の役割
        /// </summary>
        private string _role;

        /// <summary>
        /// ピン留めされているかどうか
        /// </summary>
        private bool _sticky;

        /// <summary>
        /// タスク数
        /// </summary>
        private int _taskCount;

        /// <summary>
        /// 種別
        /// </summary>
        private string _type;

        /// <summary>
        /// 未読数
        /// </summary>
        private int _unreadCount;

        public RoomData(int       id
                      , string    name
                      , string    type
                      , string    role
                      , bool      sticky
                      , int       unreadCount
                      , int       mentionCount
                      , int       myTaskCount
                      , int       messageCount
                      , int       fileCount
                      , int       taskCount
                      , string    iconPath
                      , DateTime? lastUpdated
                      , string    description)
        {
            _id           = id;
            _name         = name;
            _type         = type;
            _role         = role;
            _sticky       = sticky;
            _unreadCount  = unreadCount;
            _mentionCount = mentionCount;
            _myTaskCount  = myTaskCount;
            _messageCount = messageCount;
            _fileCount    = fileCount;
            _taskCount    = taskCount;
            _iconPath     = iconPath;
            _lastUpdated  = lastUpdated;
            _description  = description;
        }

        /// <summary>
        /// ルームIDを取得します。
        /// </summary>
        public int Id
        {
            get => _id;
            private set => SetProperty(ref _id, value);
        }

        /// <summary>
        /// 名称を取得します。
        /// </summary>
        public string Name
        {
            get => _name;
            private set => SetProperty(ref _name, value);
        }

        /// <summary>
        /// を取得します。
        /// </summary>
        public string Type
        {
            get => _type;
            private set => SetProperty(ref _type, value);
        }

        /// <summary>
        /// このルームでの自分の役割を取得します。
        /// </summary>
        public string Role
        {
            get => _role;
            private set => SetProperty(ref _role, value);
        }

        /// <summary>
        /// ピン留めされているかどうかを取得します。
        /// </summary>
        public bool Sticky
        {
            get => _sticky;
            private set => SetProperty(ref _sticky, value);
        }

        /// <summary>
        /// 未読数を取得します。
        /// </summary>
        public int UnreadCount
        {
            get => _unreadCount;
            private set => SetProperty(ref _unreadCount, value);
        }

        /// <summary>
        /// 返信数を取得します。
        /// </summary>
        public int MentionCount
        {
            get => _mentionCount;
            private set => SetProperty(ref _mentionCount, value);
        }

        /// <summary>
        /// 自分に割り当てられているタスク数を取得します。
        /// </summary>
        public int MyTackCount
        {
            get => _myTaskCount;
            private set => SetProperty(ref _myTaskCount, value);
        }

        /// <summary>
        /// メッセージ数を取得します。
        /// </summary>
        public int MessageCount
        {
            get => _messageCount;
            private set => SetProperty(ref _messageCount, value);
        }

        /// <summary>
        /// ファイル数を取得します。
        /// </summary>
        public int FileCount
        {
            get => _fileCount;
            private set => SetProperty(ref _fileCount, value);
        }

        /// <summary>
        /// タスク数を取得します。
        /// </summary>
        public int TaskCount
        {
            get => _taskCount;
            private set => SetProperty(ref _taskCount, value);
        }

        /// <summary>
        /// ルームのアイコンパスを取得します。
        /// </summary>
        public string IconPath
        {
            get => _iconPath;
            private set => SetProperty(ref _iconPath, value);
        }

        /// <summary>
        /// 最後に更新した日時を取得します。
        /// </summary>
        public DateTime? LastUpdated
        {
            get => _lastUpdated;
            private set => SetProperty(ref _lastUpdated, value);
        }

        /// <summary>
        /// 概要を取得します。
        /// </summary>
        public string Description
        {
            get => _description;
            private set => SetProperty(ref _description, value);
        }
    }
}