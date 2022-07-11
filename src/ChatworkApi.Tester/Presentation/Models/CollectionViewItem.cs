namespace ChatworkApi.Tester.Presentation.Models
{
    using Prism.Mvvm;

    /// <summary>
    /// コレクションを表す View オブジェクトの１項目にバインドするための情報を保持するクラスです。
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class CollectionViewItem<T> : BindableBase
    {
        /// <summary>
        /// 表示名
        /// </summary>
        private string _displayName;

        /// <summary>
        /// 表示名を設定、または取得します。
        /// </summary>
        public string DisplayName
        {
            get => _displayName;
            set => SetProperty(ref _displayName, value);
        }

        /// <summary>
        /// この項目の持つデータ
        /// </summary>
        private T _item;

        /// <summary>
        /// この項目の持つデータを設定、または取得します。
        /// </summary>
        public T Item
        {
            get => _item;
            set => SetProperty(ref _item, value);
        }
    }
}