namespace ChatworkApi.Tester.Domain.Interactors.Requests
{
    using UseCases.Requests;

    public sealed class AddMessageRequest : IAddMessageRequest
    {
        /// <summary>Initializes a new instance of the <see cref="T:System.Object" /> class.</summary>
        public AddMessageRequest(int    roomId
                               , string body
                               , bool?  unread)
        {
            RoomId = roomId;
            Body   = body;
            Unread = unread;
        }

        /// <summary>
        /// メッセージを追加するルームID を取得します。
        /// </summary>
        public int RoomId { get; }

        /// <summary>
        /// メッセージ本文を取得します。
        /// </summary>
        public string Body { get; }

        /// <summary>
        /// 送信したメッセージを既読にするかどうかを取得します。
        /// </summary>
        public bool? Unread { get; }
    }
}