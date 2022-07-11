namespace ChatworkApi.Tester.Domain.UseCases
{
    using System.Threading.Tasks;
    using Requests;
    using Responses;

    /// <summary>
    /// チャットルームのメンバーを取得するためのユースケースです。
    /// </summary>
    public interface IGetRoomMembersUseCase
    {
        /// <summary>
        /// 実行します。
        /// </summary>
        /// <param name="request">要求情報</param>
        /// <returns>実行結果を返します。</returns>
        Task<IGetRoomMembersResponse> Execute(IGetRoomMembersRequest request);
    }
}