namespace ChatworkApi.Tester.Presentation.Models
{
    using System;
    using ComponentModels;
    using Prism.Mvvm;

    /// <summary>
    /// 選択可能なユーザー情報を保持するクラスです。
    /// </summary>
    public sealed class SelectableAccount : ViewModelBase
    {
        /// <summary>
        /// アカウントを識別するためのID
        /// </summary>
        private int _accountId;

        /// <summary>
        /// ユーザー アイコンURL
        /// </summary>
        private string _avatarUrl;

        /// <summary>
        /// ユーザー名
        /// </summary>
        private string _name;

        /// <summary>
        /// 選択状態
        /// </summary>
        private bool _selected;

        public SelectableAccount(int    accountId
                               , string name
                               , string avatarUrl)
        {
            _accountId = accountId;
            _name      = name;
            _avatarUrl = avatarUrl;
        }

        /// <summary>
        /// 選択状態を設定、または取得します。
        /// </summary>
        public bool Selected
        {
            get => _selected;
            set => SetProperty(ref _selected, value);
        }

        /// <summary>
        /// アカウントを識別するためのIDを設定、または取得します。
        /// </summary>
        public int AccountId
        {
            get => _accountId;
            private set => SetProperty(ref _accountId, value);
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
        /// ユーザー アイコンURLを設定、または取得します。
        /// </summary>
        public string AvatarUrl
        {
            get => _avatarUrl;
            private set => SetProperty(ref _avatarUrl, value);
        }

        public event EventHandler<bool> SelectionChanged;

        /// <summary>
        /// プロパティが変更された際に呼ばれます。
        /// </summary>
        /// <param name="propertyName">変更されたプロパティの名称</param>
        protected override void OnPropertyChangedCore(string propertyName)
        {
            base.OnPropertyChangedCore(propertyName);

            switch (propertyName)
            {
                case nameof(Selected):
                    OnSelectedChanged(Selected);
                    break;
            }
        }

        private void OnSelectedChanged(bool newValue)
        {
            OnSelectionChanged(newValue);
        }

        private void OnSelectionChanged(bool selected)
        {
            SelectionChanged?.Invoke(this, selected);
        }
    }
}