namespace ChatworkApi.Tester.Domain.Interactors.Responses
{
    using System.Collections.Generic;
    using Models;
    using UseCases.Responses;

    /// <summary>
    /// <see cref="GetMyTasksInteractor"/> の応答結果を保持するクラスです。
    /// </summary>
    internal sealed class GetMyTasksResponse : IGetMyTasksResponse
    {
        public GetMyTasksResponse(IEnumerable<WorkTask> tasks)
        {
            Tasks = tasks;
        }

        public IEnumerable<WorkTask> Tasks { get; }
    }
}