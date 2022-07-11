namespace ChatworkApi.Tester.Domain.Interactors.Responses
{
    using Models;
    using UseCases;
    using UseCases.Responses;

    /// <summary>
    /// <see cref="GetUserProfileInteractor"/> の応答結果を保持するクラスです。
    /// </summary>
    internal sealed class GetUserProfileResponse : IGetUserProfileResponse
    {
        public GetUserProfileResponse(UserProfile profile)
        {
            Profile = profile;
        }

        public UserProfile Profile { get; }
    }
}