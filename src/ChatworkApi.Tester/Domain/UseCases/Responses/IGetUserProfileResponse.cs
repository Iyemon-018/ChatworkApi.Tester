namespace ChatworkApi.Tester.Domain.UseCases.Responses
{
    using Models;

    /// <summary>
    /// <see cref="IGetUserProfileUseCase"/> の応答結果を保持するインターフェースです。
    /// </summary>
    public interface IGetUserProfileResponse
    {
        UserProfile Profile { get; }
    }
}