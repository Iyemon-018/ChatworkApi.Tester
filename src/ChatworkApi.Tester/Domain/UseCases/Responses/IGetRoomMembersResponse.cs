namespace ChatworkApi.Tester.Domain.UseCases.Responses
{
    using System.Collections.Generic;
    using Models;

    public interface IGetRoomMembersResponse
    {
        /// <summary>
        /// メンバー情報をシーケンスした結果を返します。
        /// </summary>
        IEnumerable<RoomMember> Members { get; }
    }
}