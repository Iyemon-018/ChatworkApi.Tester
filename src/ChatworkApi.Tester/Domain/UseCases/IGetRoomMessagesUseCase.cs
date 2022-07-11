namespace ChatworkApi.Tester.Domain.UseCases
{
    using System.Threading.Tasks;
    using Requests;
    using Responses;

    /// <summary>
    /// チャットルームのメッセージを取得するためのユースケース インターフェースです。
    /// </summary>
    public interface IGetRoomMessagesUseCase
    {
        Task<IGetRoomMessagesResponse> Execute(IGetRoomMessagesRequest request);
    }
}