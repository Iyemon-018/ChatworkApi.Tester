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
    /// チャットルームのメッセージを取得するためのユースケース クラスです。
    /// </summary>
    public sealed class GetRoomMessagesInteractor : IGetRoomMessagesUseCase
    {
        private readonly IChatworkApiService _apiService;

        public GetRoomMessagesInteractor(IChatworkApiService apiService)
        {
            _apiService = apiService;
        }

        public async Task<IGetRoomMessagesResponse> Execute(IGetRoomMessagesRequest request)
        {
            var messages = await _apiService.Rooms.GetMessagesAsync(request.RoomId, request.Force);
            return new GetRoomMessagesResponse(messages.Map<IEnumerable<ChatMessage>>());
        }
    }
}