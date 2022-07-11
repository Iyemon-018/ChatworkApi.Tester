namespace ChatworkApi.Tester
{
    using ViewModels;

    /// <summary>
    /// <see cref="ChatRoomListViewModel"/> の状態を定義します。
    /// </summary>
    public enum ChatRoomListViewState
    {
        /// <summary>
        /// ルーム一覧を表示します。
        /// </summary>
        RoomSelect,

        /// <summary>
        /// ルームを検索します。
        /// </summary>
        Search,
    }
}