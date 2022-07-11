namespace ChatworkApi.Tester.Domain.Models
{
    using System;
    using System.Windows.Media.Imaging;
    using Prism.Mvvm;

    /// <summary>
    /// ユーザーのチャットルーム情報を保持するクラスです。
    /// </summary>
    public sealed class MyRoom : BindableBase
    {
        /// <summary>
        /// 自分に割り当てられたタスク数
        /// </summary>
        private int _assignedTaskCount;

        /// <summary>
        /// ファイル数
        /// </summary>
        private int _fileCount;

        /// <summary>
        /// ルーム アイコンのパス
        /// </summary>
        private string _iconPath;

        /// <summary>
        /// チャットルームを識別するID
        /// </summary>
        private int _id;

        /// <summary>
        /// 最終更新日時
        /// </summary>
        private DateTime? _lastUpdate;

        /// <summary>
        /// 返信数
        /// </summary>
        private int _mentionCount;

        /// <summary>
        /// メッセージ数
        /// </summary>
        private int _messageCount;

        /// <summary>
        /// ルーム名称
        /// </summary>
        private string _name;

        /// <summary>
        /// 役割
        /// </summary>
        private string _role;

        /// <summary>
        /// ピン留めしているかどうか
        /// </summary>
        private bool _sticky;

        /// <summary>
        /// すべてのタスクの数
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

        /// <summary>
        /// 自分に対する言及があるかどうか
        /// </summary>
        private bool _hasMention;

        /// <summary>
        /// 未読メッセージがあるかどうか
        /// </summary>
        private bool _existsUnread;

        public MyRoom(int       id
                    , string    name
                    , string    type
                    , string    role
                    , bool      sticky
                    , int       unreadCount
                    , int       mentionCount
                    , int       assignedTaskCount
                    , int       messageCount
                    , int       fileCount
                    , int       taskCount
                    , string    iconPath
                    , DateTime? lastUpdate)
        {
            _id                = id;
            _name              = name;
            _type              = type;
            _role              = role;
            _sticky            = sticky;
            _unreadCount       = unreadCount;
            _mentionCount      = mentionCount;
            _assignedTaskCount = assignedTaskCount;
            _messageCount      = messageCount;
            _fileCount         = fileCount;
            _taskCount         = taskCount;
            _iconPath          = iconPath;
            _lastUpdate        = lastUpdate;
            _existsUnread      = _unreadCount > 0;
            _hasMention        = _mentionCount > 0;
        }

        /// <summary>
        /// チャットルームを識別するIDを取得します。
        /// </summary>
        public int Id
        {
            get => _id;
            private set => SetProperty(ref _id, value);
        }

        /// <summary>
        /// ルーム名称を取得します。
        /// </summary>
        public string Name
        {
            get => _name;
            private set => SetProperty(ref _name, value);
        }

        /// <summary>
        /// 種別を取得します。
        /// </summary>
        public string Type
        {
            get => _type;
            private set => SetProperty(ref _type, value);
        }

        /// <summary>
        /// 役割を取得します。
        /// </summary>
        public string Role
        {
            get => _role;
            private set => SetProperty(ref _role, value);
        }

        /// <summary>
        /// ピン留めしているかどうかを取得します。
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
        /// 自分に割り当てられたタスク数を取得します。
        /// </summary>
        public int AssignedTaskCount
        {
            get => _assignedTaskCount;
            private set => SetProperty(ref _assignedTaskCount, value);
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
        /// すべてのタスクの数を取得します。
        /// </summary>
        public int TaskCount
        {
            get => _taskCount;
            private set => SetProperty(ref _taskCount, value);
        }

        /// <summary>
        /// ルーム アイコンのパスを取得します。
        /// </summary>
        public string IconPath
        {
            get => _iconPath;
            private set => SetProperty(ref _iconPath, value);
        }

        /// <summary>
        /// アイコン イメージ
        /// </summary>
		private BitmapImage _iconImage;

        /// <summary>
        /// アイコン イメージを設定、または取得します。
        /// </summary>
        public BitmapImage IconImage
        {
            get => _iconImage;
            set => SetProperty(ref _iconImage, value);
        }

        /// <summary>
        /// 最終更新日時を取得します。
        /// </summary>
        public DateTime? LastUpdate
        {
            get => _lastUpdate;
            private set => SetProperty(ref _lastUpdate, value);
        }

        /// <summary>
        /// 未読メッセージがあるかどうかを取得します。
        /// </summary>
        public bool ExistsUnread
        {
            get => _existsUnread;
            private set => SetProperty(ref _existsUnread, value);
        }

        /// <summary>
        /// 自分に対する言及があるかどうか取得します。
        /// </summary>
        public bool HasMention
        {
            get => _hasMention;
            private set => SetProperty(ref _hasMention, value);
        }
    }
}