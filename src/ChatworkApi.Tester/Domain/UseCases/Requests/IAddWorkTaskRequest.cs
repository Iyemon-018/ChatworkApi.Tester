namespace ChatworkApi.Tester.Domain.UseCases.Requests
{
    using System;

    public interface IAddWorkTaskRequest
    {
        int RoomId { get; }

        string Body { get; }
        
        int[] AssignedAccountIds { get; }

        DateTime? Limit { get; }

        TaskLimitType LimitType { get; }
    }
}