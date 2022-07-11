namespace ChatworkApi.Tester.Domain.Models
{
    using System.Windows.Media.Imaging;
    using Prism.Mvvm;

    /// <summary>
    /// アカウント情報を保持するクラスです。
    /// </summary>
    public sealed class Account : BindableBase
    {
        /// <summary>
        /// ID
        /// </summary>
        private int _id;

        /// <summary>
        /// ID を設定、または取得します。
        /// </summary>
        public int Id
        {
            get => _id;
            set => SetProperty(ref _id, value);
        }

        /// <summary>
        /// ユーザー名
        /// </summary>
        private string _name;

        /// <summary>
        /// ユーザー名を設定、または取得します。
        /// </summary>
        public string Name
        {
            get => _name;
            set => SetProperty(ref _name, value);
        }

        /// <summary>
        /// ユーザー アイコンURL
        /// </summary>
        private string _avatarUrl;

        /// <summary>
        /// ユーザー アイコンURLを設定、または取得します。
        /// </summary>
        public string AvatarUrl
        {
            get => _avatarUrl;
            set => SetProperty(ref _avatarUrl, value);
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