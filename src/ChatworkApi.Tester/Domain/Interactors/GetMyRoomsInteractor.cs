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

    /// <summary>
    /// 自分の参加しているチャットルーム一覧を取得するためのクラスです。
    /// </summary>
    public sealed class GetMyRoomsInteractor : IGetMyRoomsUseCase
    {
        private readonly IChatworkApiService _apiService;

        public GetMyRoomsInteractor(IChatworkApiService apiService)
        {
            _apiService = apiService;
        }

        public async Task<IGetMyRoomsResponse> Execute(IGetMyRoomsRequest request)
        {
             var myRooms = await _apiService.Rooms.GetMyRoomsAsync();
             return new GetMyRoomsResponse(myRooms.Map<IEnumerable<MyRoom>>());
        }
    }
}