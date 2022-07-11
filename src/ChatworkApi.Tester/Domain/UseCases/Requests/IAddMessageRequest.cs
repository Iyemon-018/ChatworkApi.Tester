namespace ChatworkApi.Tester.Domain.UseCases.Requests
{
    public interface IAddMessageRequest
    {
        /// <summary>
        /// メッセージを追加するルームID を取得します。
        /// </summary>
        int RoomId { get; }

        /// <summary>
        /// メッセージ本文を取得します。
        /// </summary>
        string Body { get; }

        /// <summary>
        /// 送信したメッセージを既読にするかどうかを取得します。
        /// </summary>
        bool? Unread { get; }
    }
}