namespace ChatworkApi.Tester.Domain.UseCases.Responses
{
    using System.Collections.Generic;
    using Models;

    /// <summary>
    /// <see cref="IGetRoomMessagesUseCase"/> の応答結果を保持するインターフェースです。
    /// </summary>
    public interface IGetRoomMessagesResponse
    {
        /// <summary>
        /// 最新のメッセージのシーケンスを取得します。(最大100件）
        /// </summary>
        IEnumerable<ChatMessage> Messages { get; }
    }
}