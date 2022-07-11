namespace ChatworkApi.Tester.Domain.Interactors
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Models;
    using Responses;
    using Services;
    using UseCases;
    using UseCases.Requests;
    using UseCases.Responses;

    public sealed class GetRoomMembersInteractor : IGetRoomMembersUseCase
    {
        private readonly IChatworkApiService _apiService;

        public GetRoomMembersInteractor(IChatworkApiService apiService)
        {
            _apiService = apiService;
        }

        /// <summary>
        /// 実行します。
        /// </summary>
        /// <param name="request">要求情報</param>
        /// <returns>実行結果を返します。</returns>
        public async Task<IGetRoomMembersResponse> Execute(IGetRoomMembersRequest request)
        {
            var members = await _apiService.Rooms.GetRoomMembersAsync(request.RoomId).ConfigureAwait(false);

            return new GetRoomMembersResponse(members.Map<IEnumerable<RoomMember>>());
        }
    }
}