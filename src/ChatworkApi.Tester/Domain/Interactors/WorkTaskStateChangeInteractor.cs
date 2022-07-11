namespace ChatworkApi.Tester.Domain.Interactors
{
    using System.Threading.Tasks;
    using Responses;
    using Services;
    using UseCases;
    using UseCases.Requests;
    using UseCases.Responses;

    public sealed class WorkTaskStateChangeInteractor : IWorkTaskStateChangeUseCase
    {
        private readonly IChatworkApiService _apiService;

        public WorkTaskStateChangeInteractor(IChatworkApiService apiService)
        {
            _apiService = apiService;
        }

        public async Task<IWorkTaskStateChangeResponse> Execute(IWorkTaskStateChangeRequest request)
        {
            var status   = request.Status.Map<ChatworkApi.TaskStatus>();
            var response = await _apiService.Rooms.UpdateTaskStatusAsync(request.RoomId, request.TaskId, status);

            return new WorkTaskStateChangeResponse(response.task_id);
        }
    }
}