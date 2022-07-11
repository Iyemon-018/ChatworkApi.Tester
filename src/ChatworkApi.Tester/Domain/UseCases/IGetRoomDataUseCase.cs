namespace ChatworkApi.Tester.Domain.UseCases
{
    using System.Threading.Tasks;
    using Requests;
    using Responses;

    /// <summary>
    /// チャットルームの詳細情報を取得するためのユースケース インターフェースです。
    /// </summary>
    public interface IGetRoomDataUseCase
    {
        Task<IGetRoomDataResponse> Execute(IGetRoomDataRequest request);
    }
}