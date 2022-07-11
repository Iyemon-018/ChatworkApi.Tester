namespace ChatworkApi.Tester.Domain.UseCases.Requests
{
    public interface IGetRoomMembersRequest
    {
        /// <summary>
        /// 取得対象のルーム ID を取得します。
        /// </summary>
        int RoomId { get; }
    }
}