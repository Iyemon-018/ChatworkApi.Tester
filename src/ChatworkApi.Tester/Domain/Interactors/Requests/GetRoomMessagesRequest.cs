namespace ChatworkApi.Tester.Domain.Interactors.Requests
{
    using UseCases;
    using UseCases.Requests;

    /// <summary>
    /// <see cref="IGetRoomMessagesUseCase"/> の要求情報を保持するクラスです。
    /// </summary>
    public sealed class GetRoomMessagesRequest : IGetRoomMessagesRequest
    {
        public GetRoomMessagesRequest(int roomId
                                     , bool? force)
        {
            RoomId = roomId;
            Force = force;
        }

        public int RoomId { get; }

        public bool? Force { get; }
    }
}