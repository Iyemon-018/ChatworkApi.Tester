namespace ChatworkApi.Tester.Domain.UseCases
{
    using System.Threading.Tasks;
    using Requests;
    using Responses;

    /// <summary>
    /// 自分の参加しているチャットルーム一覧を取得するためのユースケース インタフェースです。
    /// </summary>
    public interface IGetMyRoomsUseCase
    {
        Task<IGetMyRoomsResponse> Execute(IGetMyRoomsRequest request);
    }
}