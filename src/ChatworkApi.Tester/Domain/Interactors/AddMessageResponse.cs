namespace ChatworkApi.Tester.Domain.Interactors
{
    using UseCases.Responses;

    internal sealed class AddMessageResponse : IAddMessageResponse
    {
        public AddMessageResponse(string id)
        {
            Id = id;
        }

        /// <summary>
        /// 追加したメッセージのIDを取得します。
        /// </summary>
        public string Id { get; }
    }
}