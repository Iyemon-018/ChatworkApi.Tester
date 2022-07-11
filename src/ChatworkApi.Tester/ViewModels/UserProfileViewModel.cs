namespace ChatworkApi.Tester.ViewModels
{
    using System.Windows.Input;
    using Domain.Interactors.Requests;
    using Domain.Models;
    using Domain.UseCases;
    using Presentation.ComponentModels;
    using Prism.Commands;

    public sealed class UserProfileViewModel : ViewModelBase, IViewLoadedHolder
    {
        private readonly IGetUserProfileUseCase _useCase;

        private UserProfile _profile;

        public UserProfileViewModel()
        {
        }

        public UserProfileViewModel(IGetUserProfileUseCase useCase)
        {
            LoadedCommand = new DelegateCommand(Loaded);
            _useCase      = useCase;
        }

        public UserProfile Profile
        {
            get => _profile;
            set => SetProperty(ref _profile, value);
        }

        public ICommand LoadedCommand { get; }

        public async void Loaded()
        {
            var response = await _useCase.Execute(new GetUserProfileRequest());
            Profile = response.Profile;
        }
    }
}