namespace ChatworkApi.Tester.Presentation
{
    using Prism.Mvvm;

    /// <summary>
    /// 画面を表示するためのメニュー情報を保持するクラスです。
    /// </summary>
    public sealed class ViewMenu : BindableBase
    {
        public ViewMenu(string name, ViewType type)
        {
            _name = name;
            _type = type;
        }

        /// <summary>
        /// 表示名
        /// </summary>
		private string _name;

        /// <summary>
        /// 表示名を設定、または取得します。
        /// </summary>
        public string Name
        {
            get => _name;
            private set => SetProperty(ref _name, value);
        }

        /// <summary>
        /// 画面種別
        /// </summary>
		private ViewType _type;

        /// <summary>
        /// 画面種別を設定、または取得します。
        /// </summary>
        public ViewType Type
        {
            get => _type;
            private set => SetProperty(ref _type, value);
        }
    }
}