namespace ChatworkApi.Tester.Domain.Interactors.Responses
{
    using Models;
    using UseCases.Responses;

    internal sealed class GetRoomDataResponse : IGetRoomDataResponse
    {
        public GetRoomDataResponse(RoomData roomData)
        {
            RoomData = roomData;
        }

        public RoomData RoomData { get; }
    }
}