namespace ChatworkApi.Tester.Domain.Interactors.Responses
{
    using System.Collections.Generic;
    using Models;
    using UseCases.Responses;

    internal sealed class GetRoomMembersResponse : IGetRoomMembersResponse
    {
        public GetRoomMembersResponse(IEnumerable<RoomMember> members)
        {
            Members = members;
        }

        /// <summary>
        /// メンバー情報をシーケンスした結果を返します。
        /// </summary>
        public IEnumerable<RoomMember> Members { get; }
    }
}