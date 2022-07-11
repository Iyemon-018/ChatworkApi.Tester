namespace ChatworkApi.Tester.Domain.UseCases.Responses
{
    public interface IAddMessageResponse
    {
        /// <summary>
        /// 追加したメッセージのIDを取得します。
        /// </summary>
        string Id { get; }
    }
}