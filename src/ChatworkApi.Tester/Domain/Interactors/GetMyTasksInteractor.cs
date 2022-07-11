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
    using TaskStatus = ChatworkApi.TaskStatus;

    /// <summary>
    /// 自分自身のタスクを取得するための機能を提供するクラスです。
    /// </summary>
    internal sealed class GetMyTasksInteractor : IGetMyTasksUseCase
    {
        private readonly IChatworkApiService _apiService;

        public GetMyTasksInteractor(IChatworkApiService apiService)
        {
            _apiService = apiService;
        }

        public async Task<IGetMyTasksResponse> Execute(IGetMyTasksRequest request)
        {
            var taskStatus = request.Status.Map<ChatworkApi.TaskStatus>();
            var tasks = await _apiService.My.GetTasksAsync(request.RequesterId, taskStatus);
            return new GetMyTasksResponse(tasks.Map<IEnumerable<WorkTask>>());
        }
    }
}