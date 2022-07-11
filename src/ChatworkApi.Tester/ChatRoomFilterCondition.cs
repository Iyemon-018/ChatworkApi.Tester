namespace ChatworkApi.Tester
{
    /// <summary>
    /// チャットルームのフィルター条件を定義します。
    /// </summary>
    public enum ChatRoomFilterCondition
    {
        /// <summary>
        /// すべてのチャットルーム
        /// </summary>
        All,

        /// <summary>
        /// 未読のあるルーム
        /// </summary>
        Unread,

        /// <summary>
        /// 自分のタスクがあるルーム
        /// </summary>
        ContainsMyTasks,
    }
}