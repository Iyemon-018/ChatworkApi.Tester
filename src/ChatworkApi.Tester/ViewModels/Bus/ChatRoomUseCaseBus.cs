namespace ChatworkApi.Tester.ViewModels.Bus
{
    using Domain.UseCases;

    internal sealed class ChatRoomUseCaseBus : IChatRoomUseCaseBus
    {
        public ChatRoomUseCaseBus(IGetMyRoomsUseCase      myRooms
                                , IGetRoomMessagesUseCase roomMessages
                                , IGetRoomMembersUseCase  roomMembers
                                , IAddWorkTaskUseCase     addWorkTask
                                , IAddMessageUseCase      addMessage
                                 , IGetRoomDataUseCase getRoomData)
        {
            GetMyRooms      = myRooms;
            GetRoomMessages = roomMessages;
            GetRoomMembers  = roomMembers;
            AddWorkTask     = addWorkTask;
            AddMessage      = addMessage;
            GetRoomData = getRoomData;
        }

        /// <summary>
        /// ルーム情報を取得するためのユースケースを取得します。
        /// </summary>
        public IGetMyRoomsUseCase GetMyRooms { get; }

        /// <summary>
        /// ルームのメッセージを取得するためのユースケースを取得します。
        /// </summary>
        public IGetRoomMessagesUseCase GetRoomMessages { get; }

        /// <summary>
        /// ルームのメンバー情報を取得するためのユースケースを取得します。
        /// </summary>
        public IGetRoomMembersUseCase GetRoomMembers { get; }

        /// <summary>
        /// タスク登録するためのユースケースを取得します。
        /// </summary>
        public IAddWorkTaskUseCase AddWorkTask { get; }

        /// <summary>
        /// メッセージを追加するためのユースケースを取得します。
        /// </summary>
        public IAddMessageUseCase AddMessage { get; }

        /// <summary>
        /// チャットルームの詳細情報を取得するためのユースケースを取得します。
        /// </summary>
        public IGetRoomDataUseCase GetRoomData { get; }
    }
}