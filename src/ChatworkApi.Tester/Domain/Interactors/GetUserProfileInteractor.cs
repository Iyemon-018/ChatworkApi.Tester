namespace ChatworkApi.Tester.Domain.Interactors
{
    using System.Threading.Tasks;
    using Models;
    using Responses;
    using Services;
    using UseCases;
    using UseCases.Requests;
    using UseCases.Responses;

    /// <summary>
    /// ユーザー情報を取得するための機能を提供するクラスです。
    /// </summary>
    internal sealed class GetUserProfileInteractor : IGetUserProfileUseCase
    {
        private readonly IChatworkApiService _apiService;

        public GetUserProfileInteractor(IChatworkApiService apiService)
        {
            _apiService = apiService;
        }

        public async Task<IGetUserProfileResponse> Execute(IGetUserProfileRequest request)
        {
            var me      = await _apiService.Me.GetAsync();
            var profile = me.Map<UserProfile>();

            return new GetUserProfileResponse(profile);
        }
    }
}