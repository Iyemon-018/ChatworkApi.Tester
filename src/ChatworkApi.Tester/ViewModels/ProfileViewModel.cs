namespace ChatworkApi.Tester.ViewModels
{
    using System.Windows.Input;
    using Domain;
    using Domain.Interactors.Requests;
    using Domain.Models;
    using Domain.UseCases;
    using Presentation.ComponentModels;
    using Prism.Commands;

    public sealed class ProfileViewModel : ViewModelBase, IViewLoadedHolder
    {
        private readonly IGetUserProfileUseCase _useCase;

        private UserProfile _profile;

        public ProfileViewModel(IGetUserProfileUseCase useCase)
        {
            _useCase = useCase;
            _profile = new UserProfile();
            LoadedCommand = new DelegateCommand(ExecuteLoadedCommand);
        }

        private async void ExecuteLoadedCommand()
        {
            var response = await _useCase.Execute(new GetUserProfileRequest());
            Profile =  response.Profile.Map<UserProfile>();
        }

        /// <summary>
        /// Loaded イベントの実行時に呼ばれるコマンドを取得します。
        /// </summary>
        public ICommand LoadedCommand { get; }

        public UserProfile Profile
        {
            get => _profile;
            set => SetProperty(ref _profile, value);
        }
    }
}