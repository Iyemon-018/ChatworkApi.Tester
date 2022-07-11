namespace ChatworkApi.Tester.Domain.Interactors
{
    using System.Threading.Tasks;
    using Models;
    using Responses;
    using Services;
    using UseCases;
    using UseCases.Requests;
    using UseCases.Responses;

    public sealed class GetRoomDataInteractor : IGetRoomDataUseCase
    {
        private readonly IChatworkApiService _apiService;

        public GetRoomDataInteractor(IChatworkApiService apiService)
        {
            _apiService = apiService;
        }

        public async Task<IGetRoomDataResponse> Execute(IGetRoomDataRequest request)
        {
            var roomConfiguration = await _apiService.Rooms.GetRoomConfigurationAsync(request.RoomId);

            return new GetRoomDataResponse(roomConfiguration.Map<RoomData>());
        }
    }
}