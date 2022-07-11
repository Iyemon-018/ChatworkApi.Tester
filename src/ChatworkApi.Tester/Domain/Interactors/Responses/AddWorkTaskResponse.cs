namespace ChatworkApi.Tester.Domain.Interactors.Responses
{
    using System.Collections.Generic;
    using UseCases;

    internal sealed class AddWorkTaskResponse : IAddWorkTaskResponse
    {
        public AddWorkTaskResponse(IEnumerable<int> workTaskIds)
        {
            WorkTaskIds = workTaskIds;
        }

        public IEnumerable<int> WorkTaskIds { get; }
    }
}