namespace ChatworkApi.Tester.Domain.UseCases.Requests
{
    /// <summary>
    /// <see cref="IGetRoomMessagesUseCase"/> の要求情報を保持するインターフェースです。
    /// </summary>
    public interface IGetRoomMessagesRequest
    {
        /// <summary>
        /// 取得対象のルームIDを取得します。
        /// </summary>
        int RoomId { get; }

        bool? Force { get; }
    }
}