namespace ChatworkApi.Tester.Domain.Interactors.Responses
{
    using System.Collections.Generic;
    using Models;
    using UseCases.Responses;

    /// <summary>
    /// <see cref="GetMyRoomsInteractor"/> の応答結果を保持するクラスです。
    /// </summary>
    internal sealed class GetMyRoomsResponse : IGetMyRoomsResponse
    {
        public GetMyRoomsResponse(IEnumerable<MyRoom> rooms)
        {
            Rooms = rooms;
        }

        public IEnumerable<MyRoom> Rooms { get; }
    }
}