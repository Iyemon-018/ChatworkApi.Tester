namespace ChatworkApi.Tester.Presentation.ComponentModels
{
    using Prism.Mvvm;

    public sealed class ValidationData : BindableBase
    {
        /// <summary>
        /// 入力値が正常かどうか
        /// </summary>
		private bool _isValid;

        /// <summary>
        /// 入力値が正常かどうかを設定、または取得します。
        /// </summary>
        public bool IsValid
        {
            get => _isValid;
            private set => SetProperty(ref _isValid, value);
        }

        /// <summary>
        /// 入力値が異常かどうか
        /// </summary>
		private bool _hasError;

        /// <summary>
        /// を設定、または取得します。
        /// </summary>
        public bool HasError
        {
            get => _hasError;
            private set => SetProperty(ref _hasError, value);
        }

        /// <summary>
        /// エラーメッセージ
        /// </summary>
		private string _errorMessage;

        /// <summary>
        /// エラーメッセージを設定、または取得します。
        /// </summary>
        public string ErrorMessage
        {
            get => _errorMessage;
            private set => SetProperty(ref _errorMessage, value);
        }

        public ValidationData()
        {
            _isValid = true;
            _hasError = false;
            _errorMessage = string.Empty;
        }

        public void Valid()
        {
            IsValid = true;
            HasError = false;
            ErrorMessage = string.Empty;
        }

        public void Invalid(string errorMessage)
        {
            IsValid = false;
            HasError = true;
            ErrorMessage = errorMessage;
        }
    }
}