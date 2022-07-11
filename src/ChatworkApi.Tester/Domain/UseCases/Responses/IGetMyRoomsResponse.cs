namespace ChatworkApi.Tester.Domain.UseCases.Responses
{
    using System.Collections.Generic;
    using Models;

    /// <summary>
    /// <see cref="IGetMyRoomsUseCase"/> の応答結果を保持するインターフェースです。
    /// </summary>
    public interface IGetMyRoomsResponse
    {
        IEnumerable<MyRoom> Rooms { get; }
    }
}