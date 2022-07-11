namespace ChatworkApi.Tester.Domain.Models
{
    using System.Windows.Media.Imaging;
    using Prism.Mvvm;

    public sealed class Contact : BindableBase
    {
        /// <summary>
        /// アカウントの識別子
        /// </summary>
        private int _accountId;

        /// <summary>
        /// アイコンURL
        /// </summary>
        private string _avatarImageUrl;

        /// <summary>
        /// Chatwork ID
        /// </summary>
        private string _chatworkId;

        /// <summary>
        /// 役職
        /// </summary>
        private string _department;

        /// <summary>
        /// ユーザーの名前
        /// </summary>
        private string _name;

        /// <summary>
        /// 所属組織ID
        /// </summary>
        private int _organizationId;

        /// <summary>
        /// 所属組織名称
        /// </summary>
        private string _organizationName;

        /// <summary>
        /// ユーザールームのID
        /// </summary>
        private int _roomId;

        public Contact(int    accountId
                     , int    roomId
                     , string name
                     , string chatworkId
                     , int    organizationId
                     , string organizationName
                     , string department
                     , string avatarImageUrl)
        {
            _accountId        = accountId;
            _roomId           = roomId;
            _name             = name;
            _chatworkId       = chatworkId;
            _organizationId   = organizationId;
            _organizationName = organizationName;
            _department       = department;
            _avatarImageUrl   = avatarImageUrl;
        }

        /// <summary>
        /// アカウントの識別子を取得します。
        /// </summary>
        public int AccountId
        {
            get => _accountId;
            private set => SetProperty(ref _accountId, value);
        }

        /// <summary>
        /// ユーザールームのIDを取得します。
        /// </summary>
        public int RoomId
        {
            get => _roomId;
            private set => SetProperty(ref _roomId, value);
        }

        /// <summary>
        /// ユーザーの名前を取得します。
        /// </summary>
        public string Name
        {
            get => _name;
            private set => SetProperty(ref _name, value);
        }

        /// <summary>
        /// Chatwork IDを取得します。
        /// </summary>
        public string ChatworkId
        {
            get => _chatworkId;
            private set => SetProperty(ref _chatworkId, value);
        }

        /// <summary>
        /// 所属組織IDを取得します。
        /// </summary>
        public int OrganizationId
        {
            get => _organizationId;
            private set => SetProperty(ref _organizationId, value);
        }

        /// <summary>
        /// 所属組織名称を取得します。
        /// </summary>
        public string OrganizationName
        {
            get => _organizationName;
            private set => SetProperty(ref _organizationName, value);
        }

        /// <summary>
        /// 役職を取得します。
        /// </summary>
        public string Department
        {
            get => _department;
            private set => SetProperty(ref _department, value);
        }

        /// <summary>
        /// アイコンURLを取得します。
        /// </summary>
        public string AvatarImageUrl
        {
            get => _avatarImageUrl;
            private set => SetProperty(ref _avatarImageUrl, value);
        }

        /// <summary>
        /// アイコン イメージ
        /// </summary>
		private BitmapImage _avatarImage;

        /// <summary>
        /// アイコン イメージを設定、または取得します。
        /// </summary>
        public BitmapImage AvatarImage
        {
            get => _avatarImage;
            set => SetProperty(ref _avatarImage, value);
        }
    }
}