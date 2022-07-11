namespace ChatworkApi.Tester.Presentation.Models
{
    using System.Windows.Media.Imaging;
    using Prism.Mvvm;

    public sealed class MessageTo : BindableBase
    {
        /// <summary>
        /// アイコン イメージ
        /// </summary>
        private BitmapImage _avatarImage;

        /// <summary>
        /// アイコン
        /// </summary>
        private string _avatarUrl;

        /// <summary>
        /// 表示名称
        /// </summary>
        private string _displayName;

        /// <summary>
        /// アカウントID
        /// </summary>
        private int _id;

        /// <summary>
        /// 送信先メッセージの文字列
        /// </summary>
        private string _toContent;

        public MessageTo(int    id
                       , string content
                       , string avatarUrl
                       , string displayName)
        {
            _id          = id;
            _toContent   = content;
            _avatarUrl   = avatarUrl;
            _displayName = displayName;
        }

        /// <summary>
        /// アカウントIDを取得します。
        /// </summary>
        public int Id
        {
            get => _id;
            private set => SetProperty(ref _id, value);
        }

        /// <summary>
        /// 送信先メッセージの文字列を設定、または取得します。
        /// </summary>
        public string ToContent
        {
            get => _toContent;
            private set => SetProperty(ref _toContent, value);
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
        /// アイコン イメージを設定、または取得します。
        /// </summary>
        public BitmapImage AvatarImage
        {
            get => _avatarImage;
            set => SetProperty(ref _avatarImage, value);
        }

        /// <summary>
        /// 表示名称を設定、または取得します。
        /// </summary>
        public string DisplayName
        {
            get => _displayName;
            private set => SetProperty(ref _displayName, value);
        }
    }
}