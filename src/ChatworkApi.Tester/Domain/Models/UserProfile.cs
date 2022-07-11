namespace ChatworkApi.Tester.Domain.Models
{
    using Prism.Mvvm;

    public sealed class UserProfile : BindableBase
    {
        #region Fields

        /// <summary>
        /// アカウントID
        /// </summary>
        private int _accountId;

        /// <summary>
        /// アバターアイコンのURL
        /// </summary>
        private string _avatarUrl;

        /// <summary>
        /// Chatwork ID
        /// </summary>
        private string _chatworkId;

        /// <summary>
        /// 部門
        /// </summary>
        private string _department;

        /// <summary>
        /// 電話番号(内線)
        /// </summary>
        private string _extensionTelephoneNumber;

        /// <summary>
        /// Facebook アカウント名
        /// </summary>
        private string _facebookAccount;

        /// <summary>
        /// 自己紹介文
        /// </summary>
        private string _introduction;

        /// <summary>
        /// ログイン メールアドレス
        /// </summary>
        private string _loginMail;

        /// <summary>
        /// メールアドレス
        /// </summary>
        private string _mail;

        /// <summary>
        /// 電話番号(携帯)
        /// </summary>
        private string _mobileTelephoneNumber;

        /// <summary>
        /// ユーザーの登録名
        /// </summary>
        private string _name;

        /// <summary>
        /// 組織ID
        /// </summary>
        private int _organizationId;

        /// <summary>
        /// 組織名
        /// </summary>
        private string _organizationName;

        /// <summary>
        /// 会社の電話番号
        /// </summary>
        private string _organizationTelephoneNumber;

        /// <summary>
        /// 自身のルームID
        /// </summary>
        private int _roomId;

        /// <summary>
        /// Skype アカウント名
        /// </summary>
        private string _skypeAccount;

        /// <summary>
        /// タイトル
        /// </summary>
        private string _title;

        /// <summary>
        /// Twitter アカウント名
        /// </summary>
        private string _twitterAccount;

        /// <summary>
        /// Url
        /// </summary>
        private string _url;

        #endregion

        #region Properties

        /// <summary>
        /// アカウントIDを設定、または取得します。
        /// </summary>
        public int AccountId
        {
            get => _accountId;
            set => SetProperty(ref _accountId, value);
        }

        /// <summary>
        /// 自身のルームIDを設定、または取得します。
        /// </summary>
        public int RoomId
        {
            get => _roomId;
            set => SetProperty(ref _roomId, value);
        }

        /// <summary>
        /// ユーザーの登録名を設定、または取得します。
        /// </summary>
        public string Name
        {
            get => _name;
            set => SetProperty(ref _name, value);
        }

        /// <summary>
        /// Chatwork IDを設定、または取得します。
        /// </summary>
        public string ChatworkId
        {
            get => _chatworkId;
            set => SetProperty(ref _chatworkId, value);
        }

        /// <summary>
        /// 組織IDを設定、または取得します。
        /// </summary>
        public int OrganizationId
        {
            get => _organizationId;
            set => SetProperty(ref _organizationId, value);
        }

        /// <summary>
        /// 組織名を設定、または取得します。
        /// </summary>
        public string OrganizationName
        {
            get => _organizationName;
            set => SetProperty(ref _organizationName, value);
        }

        /// <summary>
        /// 部門を設定、または取得します。
        /// </summary>
        public string Department
        {
            get => _department;
            set => SetProperty(ref _department, value);
        }

        /// <summary>
        /// タイトルを設定、または取得します。
        /// </summary>
        public string Title
        {
            get => _title;
            set => SetProperty(ref _title, value);
        }

        /// <summary>
        /// Urlを設定、または取得します。
        /// </summary>
        public string Url
        {
            get => _url;
            set => SetProperty(ref _url, value);
        }

        /// <summary>
        /// 自己紹介文を設定、または取得します。
        /// </summary>
        public string Introduction
        {
            get => _introduction;
            set => SetProperty(ref _introduction, value);
        }

        /// <summary>
        /// メールアドレスを設定、または取得します。
        /// </summary>
        public string Mail
        {
            get => _mail;
            set => SetProperty(ref _mail, value);
        }

        /// <summary>
        /// 会社の電話番号を設定、または取得します。
        /// </summary>
        public string OrganizationTelephoneNumber
        {
            get => _organizationTelephoneNumber;
            set => SetProperty(ref _organizationTelephoneNumber, value);
        }

        /// <summary>
        /// 電話番号(内線)を設定、または取得します。
        /// </summary>
        public string ExtensionTelephoneNumber
        {
            get => _extensionTelephoneNumber;
            set => SetProperty(ref _extensionTelephoneNumber, value);
        }

        /// <summary>
        /// 電話番号(携帯)を設定、または取得します。
        /// </summary>
        public string MobileTelephoneNumber
        {
            get => _mobileTelephoneNumber;
            set => SetProperty(ref _mobileTelephoneNumber, value);
        }

        /// <summary>
        /// Skype アカウント名を設定、または取得します。
        /// </summary>
        public string SkypeAccount
        {
            get => _skypeAccount;
            set => SetProperty(ref _skypeAccount, value);
        }

        /// <summary>
        /// Facebook アカウント名を設定、または取得します。
        /// </summary>
        public string FacebookAccount
        {
            get => _facebookAccount;
            set => SetProperty(ref _facebookAccount, value);
        }

        /// <summary>
        /// Twitter アカウント名を設定、または取得します。
        /// </summary>
        public string TwitterAccount
        {
            get => _twitterAccount;
            set => SetProperty(ref _twitterAccount, value);
        }

        /// <summary>
        /// アバターアイコンのURLを設定、または取得します。
        /// </summary>
        public string AvatarUrl
        {
            get => _avatarUrl;
            set => SetProperty(ref _avatarUrl, value);
        }

        /// <summary>
        /// ログイン メールアドレスを設定、または取得します。
        /// </summary>
        public string LoginMail
        {
            get => _loginMail;
            set => SetProperty(ref _loginMail, value);
        }

        #endregion
    }
}