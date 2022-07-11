namespace ChatworkApi.Tester.ViewModels
{
    using System;
    using System.Collections.ObjectModel;
    using System.IO;
    using System.Linq;
    using System.Windows.Input;
    using Bus;
    using Configurations;
    using Domain.Repositories;
    using Domain.Services;
    using Events;
    using Presentation;
    using Presentation.ComponentModels;
    using Prism.Commands;

    public sealed class ShellViewModel : ViewModelBase
    {
        private readonly IConfigurationRepository _configurationRepository;

        private readonly IEncryptionBus _encryptionBus;

        private readonly IChatworkApiTokenRegister _register;

        /// <summary>
        /// API Token登録用
        /// </summary>
        private ApiTokenRegisterViewModel _apiTokenRegister;

        /// <summary>
        /// API Tokenを入力中かどうか
        /// </summary>
        private bool _inputApiToken;

        /// <summary>
        /// 選択中のメニュー
        /// </summary>
        private ViewMenu _selectedMenu;

        public ShellViewModel(IChatworkApiTokenRegister register
                            , IEncryptionBus            encryptionBus
                            , IConfigurationRepository  configurationRepository)
        {
            _register                = register;
            _encryptionBus           = encryptionBus;
            _configurationRepository = configurationRepository;
            Menus = new ObservableCollection<ViewMenu>(new[]
                                                       {
                                                           new ViewMenu("チャット", ViewType.Chat)
                                                         , new ViewMenu("タスク", ViewType.TaskList)
                                                         , new ViewMenu("コンタクト", ViewType.Contact)
                                                         , new ViewMenu("アカウント", ViewType.Account)
                                                       });
            SelectedMenuCommand = new DelegateCommand<ViewMenu>(ExecuteSelectedMenuCommand);

            _apiTokenRegister            =  new ApiTokenRegisterViewModel(encryptionBus);
            _apiTokenRegister.Registered += ApiTokenRegister_OnRegistered;

            var registeredApiToken = false;
            ApplicationConfiguration appConfig = null;

            try
            {
                appConfig = configurationRepository.Load(Constants.ConfigurationFileName);
                registeredApiToken = !string.IsNullOrWhiteSpace(appConfig.api_token);
            }
            catch (Exception)
            {
                registeredApiToken = false;
            }

            _inputApiToken     = !registeredApiToken;

            // API Token が入力されている場合。
            // このケースはAPI Tokenの入力操作を省くために構成情報ファイルを利用している。
            if (registeredApiToken)
            {
                RegisterApiToken(appConfig.api_token);
                SelectedMenu = Menus.FirstOrDefault(x => x.Type == ViewType.Chat);
            }
        }

        /// <summary>
        /// 選択中のメニューを設定、または取得します。
        /// </summary>
        public ViewMenu SelectedMenu
        {
            get => _selectedMenu;
            set => SetProperty(ref _selectedMenu, value);
        }

        /// <summary>
        /// メニュー選択コマンドを取得します。
        /// </summary>
        public ICommand SelectedMenuCommand { get; }

        /// <summary>
        /// API Token登録用を設定、または取得します。
        /// </summary>
        public ApiTokenRegisterViewModel ApiTokenRegister
        {
            get => _apiTokenRegister;
            private set => SetProperty(ref _apiTokenRegister, value);
        }

        /// <summary>
        /// 選択可能なメニューリストを取得します。
        /// </summary>
        public ObservableCollection<ViewMenu> Menus { get; }

        /// <summary>
        /// API Tokenを入力中かどうかを設定、または取得します。
        /// </summary>
        public bool InputApiToken
        {
            get => _inputApiToken;
            private set => SetProperty(ref _inputApiToken, value);
        }

        private void RegisterApiToken(string apiToken)
        {
            var decryptApiToken = _encryptionBus.Decryption.Decrypt(apiToken);
            _register.Register(decryptApiToken);
        }

        /// <summary>
        /// API Tokenの登録が完了した際に呼ばれるイベントハンドラです。
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ApiTokenRegister_OnRegistered(object                      sender
                                                 , ApiTokenRegisteredEventArgs e)
        {
            _configurationRepository.Save(Constants.ConfigurationFileName
                                        , new ApplicationConfiguration
                                          {
                                              api_token = e.ApiToken
                                          });
            RegisterApiToken(e.ApiToken);

            InputApiToken = false;
            SelectedMenu  = Menus.FirstOrDefault(x => x.Type == ViewType.Chat);
        }

        private void ExecuteSelectedMenuCommand(ViewMenu parameter)
        {
            SelectedMenu = parameter;
        }
    }
}