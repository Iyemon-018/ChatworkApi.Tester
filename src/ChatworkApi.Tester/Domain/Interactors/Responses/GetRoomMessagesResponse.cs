namespace ChatworkApi.Tester.Domain.Interactors.Responses
{
    using System.Collections.Generic;
    using Models;
    using UseCases;
    using UseCases.Responses;

    /// <summary>
    /// <see cref="IGetRoomMessagesUseCase"/> の応答結果を保持するクラスです。
    /// </summary>
    internal sealed class GetRoomMessagesResponse : IGetRoomMessagesResponse
    {
        public GetRoomMessagesResponse(IEnumerable<ChatMessage> messages)
        {
            Messages = messages;
        }

        /// <summary>
        /// 最新のメッセージのシーケンスを取得します。(最大100件）
        /// </summary>
        public IEnumerable<ChatMessage> Messages { get; }
    }
}