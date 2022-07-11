namespace ChatworkApi.Tester.ViewModels.Bus
{
    using Domain.UseCases;

    /// <summary>
    /// <see cref="ChatRoomViewModel"/> で使用するためのユースケースを提供するバス インターフェースです。
    /// </summary>
    public interface IChatRoomUseCaseBus
    {
        /// <summary>
        /// ルーム情報を取得するためのユースケースを取得します。
        /// </summary>
        IGetMyRoomsUseCase GetMyRooms { get; }

        /// <summary>
        /// ルームのメッセージを取得するためのユースケースを取得します。
        /// </summary>
        IGetRoomMessagesUseCase GetRoomMessages { get; }

        /// <summary>
        /// ルームのメンバー情報を取得するためのユースケースを取得します。
        /// </summary>
        IGetRoomMembersUseCase GetRoomMembers { get; }

        /// <summary>
        /// タスク登録するためのユースケースを取得します。
        /// </summary>
        IAddWorkTaskUseCase AddWorkTask { get; }

        /// <summary>
        /// メッセージを追加するためのユースケースを取得します。
        /// </summary>
        IAddMessageUseCase AddMessage { get; }

        /// <summary>
        /// チャットルームの詳細情報を取得するためのユースケースを取得します。
        /// </summary>
        IGetRoomDataUseCase GetRoomData { get; }
    }
}