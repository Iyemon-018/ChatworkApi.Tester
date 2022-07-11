namespace ChatworkApi.Tester.Domain.Interactors
{
    using System.Threading.Tasks;
    using Services;
    using UseCases;
    using UseCases.Requests;
    using UseCases.Responses;

    public sealed class AddMessageInteractor : IAddMessageUseCase
    {
        private readonly IChatworkApiService _apiService;

        public AddMessageInteractor(IChatworkApiService apiService)
        {
            _apiService = apiService;
        }

        public async Task<IAddMessageResponse> Execute(IAddMessageRequest request)
        {
            var result = await _apiService.Rooms.AddMessageAsync(request.RoomId, request.Body, request.Unread);

            return new AddMessageResponse(result.message_id);
        }
    }
}