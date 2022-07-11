namespace ChatworkApi.Tester.Domain.Models
{
    using System.Windows.Media.Imaging;
    using Prism.Mvvm;

    /// <summary>
    /// ルームの情報を保持するクラスです。
    /// </summary>
    public sealed class Room : BindableBase
    {
        /// <summary>
        /// ルームID
        /// </summary>
        private int _id;

        /// <summary>
        /// ルームIDを設定、または取得します。
        /// </summary>
        public int Id
        {
            get => _id;
            set => SetProperty(ref _id, value);
        }

        /// <summary>
        /// グループチャット名称
        /// </summary>
        private string _name;

        /// <summary>
        /// グループチャット名称を設定、または取得します。
        /// </summary>
        public string Name
        {
            get => _name;
            set => SetProperty(ref _name, value);
        }

        /// <summary>
        /// ルームのアイコンURL
        /// </summary>
        private string _iconPath;

        /// <summary>
        /// ルームのアイコンURLを設定、または取得します。
        /// </summary>
        public string IconPath
        {
            get => _iconPath;
            set => SetProperty(ref _iconPath, value);
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
    }
}