namespace ChatworkApi.Tester.Domain.UseCases
{
    using System.Threading.Tasks;
    using Requests;
    using Responses;

    /// <summary>
    /// ユーザー情報を取得するためのユースケース インターフェースです。
    /// </summary>
    public interface IGetUserProfileUseCase
    {
        Task<IGetUserProfileResponse> Execute(IGetUserProfileRequest request);
    }
}