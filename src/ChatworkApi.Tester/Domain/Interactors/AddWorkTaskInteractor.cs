namespace ChatworkApi.Tester.Domain.Interactors
{
    using System.Threading.Tasks;
    using Responses;
    using Services;
    using UseCases;
    using UseCases.Requests;

    public sealed class AddWorkTaskInteractor : IAddWorkTaskUseCase
    {
        private readonly IChatworkApiService _apiService;

        public AddWorkTaskInteractor(IChatworkApiService apiService)
        {
            _apiService = apiService;
        }

        public async Task<IAddWorkTaskResponse> Execute(IAddWorkTaskRequest request)
        {
            var response = await _apiService.Rooms.AddTaskAsync(request.RoomId
                                                              , request.Body
                                                              , request.AssignedAccountIds
                                                              , request.LimitType.Map<ChatworkApi.TaskLimitType>()
                                                              , request.Limit);

            return new AddWorkTaskResponse(response.task_ids);
        }
    }
}