namespace ChatworkApi.Tester.Domain.Models
{
    using System.Windows.Media.Imaging;
    using Prism.Mvvm;

    /// <summary>
    /// チャットルームのメンバー情報を保持するクラスです。
    /// </summary>
    public sealed class RoomMember : BindableBase
    {
        /// <summary>
        /// アカウントID
        /// </summary>
        private int _accountId;

        /// <summary>
        /// アイコン
        /// </summary>
        private string _avatarUrl;

        /// <summary>
        /// Chatwork Id
        /// </summary>
        private string _chatworkId;

        /// <summary>
        /// 役職
        /// </summary>
        private string _department;

        /// <summary>
        /// ユーザー名
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
        /// 役割
        /// </summary>
        private string _role;

        public RoomMember(int    accountId
                        , string role
                        , string name
                        , string chatworkId
                        , int    organizationId
                        , string organizationName
                        , string department
                        , string avatarUrl)
        {
            _accountId        = accountId;
            _role             = role;
            _name             = name;
            _chatworkId       = chatworkId;
            _organizationId   = organizationId;
            _organizationName = organizationName;
            _department       = department;
            _avatarUrl        = avatarUrl;
        }

        /// <summary>
        /// アカウントIDを設定、または取得します。
        /// </summary>
        public int AccountId
        {
            get => _accountId;
            private set => SetProperty(ref _accountId, value);
        }

        /// <summary>
        /// 役割を設定、または取得します。
        /// </summary>
        public string Role
        {
            get => _role;
            private set => SetProperty(ref _role, value);
        }

        /// <summary>
        /// ユーザー名を設定、または取得します。
        /// </summary>
        public string Name
        {
            get => _name;
            private set => SetProperty(ref _name, value);
        }

        /// <summary>
        /// Chatwork Idを設定、または取得します。
        /// </summary>
        public string ChatworkId
        {
            get => _chatworkId;
            private set => SetProperty(ref _chatworkId, value);
        }

        /// <summary>
        /// 所属組織IDを設定、または取得します。
        /// </summary>
        public int OrganizationId
        {
            get => _organizationId;
            private set => SetProperty(ref _organizationId, value);
        }

        /// <summary>
        /// 所属組織名称を設定、または取得します。
        /// </summary>
        public string OrganizationName
        {
            get => _organizationName;
            private set => SetProperty(ref _organizationName, value);
        }

        /// <summary>
        /// 役職を設定、または取得します。
        /// </summary>
        public string Department
        {
            get => _department;
            private set => SetProperty(ref _department, value);
        }

        /// <summary>
        /// アイコンを設定、または取得します。
        /// </summary>
        public string AvatarUrl
        {
            get => _avatarUrl;
            private set => SetProperty(ref _avatarUrl, value);
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