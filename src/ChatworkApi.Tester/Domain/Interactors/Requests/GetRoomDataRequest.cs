namespace ChatworkApi.Tester.Domain.Interactors.Requests
{
    using UseCases.Requests;

    public sealed class GetRoomDataRequest : IGetRoomDataRequest
    {
        public GetRoomDataRequest(int roomId)
        {
            RoomId = roomId;
        }

        public int RoomId { get; }
    }
}