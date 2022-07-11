namespace ChatworkApi.Tester.ViewModels
{
    using System;
    using System.Windows.Input;
    using Bus;
    using Events;
    using Presentation.ComponentModels;
    using Prism.Commands;

    public sealed class ApiTokenRegisterViewModel : ValidatableViewModelBase
    {
        private readonly IEncryptionBus _encryptionBus;

        /// <summary>
        /// API Token
        /// </summary>
        private string _apiToken;

        [Obsolete]
        public ApiTokenRegisterViewModel()
        {
        }

        public ApiTokenRegisterViewModel(IEncryptionBus encryptionBus)
        {
            _encryptionBus = encryptionBus;

            RegisterCommand = new DelegateCommand(ExecuteRegisterCommand);
            CancelCommand   = new DelegateCommand(ExecuteCancelCommand);
        }

        /// <summary>
        /// API Tokenを設定、または取得します。
        /// </summary>
        public string ApiToken
        {
            get => _apiToken;
            set => SetProperty(ref _apiToken, value);
        }

        public ICommand RegisterCommand { get; }

        public ICommand CancelCommand { get; }

        public event EventHandler<ApiTokenRegisteredEventArgs> Registered;

        private void ExecuteCancelCommand()
        {
        }

        private void ExecuteRegisterCommand()
        {
            Validate();

            if (HasErrors) return;

            var apiTokenAes = _encryptionBus.Encryption.Encrypt(ApiToken);

            OnRegistered(apiTokenAes);
        }

        protected override void OnValidate()
        {
            base.OnValidate();

            if (string.IsNullOrWhiteSpace(ApiToken))
                NotifyError(nameof(ApiToken), "API Tokenを入力してください。");
        }

        private void OnRegistered(string apiToken)
        {
            Registered?.Invoke(this, new ApiTokenRegisteredEventArgs(apiToken));
        }
    }
}