namespace ChatworkApi.Tester.Domain.UseCases
{
    using System.Collections.Generic;

    public interface IAddWorkTaskResponse
    {
        IEnumerable<int> WorkTaskIds { get; }
    }
}