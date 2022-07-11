namespace ChatworkApi.Tester.Domain.Interactors.Requests
{
    using UseCases.Requests;

    public sealed class GetRoomMembersRequest : IGetRoomMembersRequest
    {
        public GetRoomMembersRequest(int roomId)
        {
            RoomId = roomId;
        }

        /// <summary>
        /// 取得対象のルーム ID を取得します。
        /// </summary>
        public int RoomId { get; }
    }
}