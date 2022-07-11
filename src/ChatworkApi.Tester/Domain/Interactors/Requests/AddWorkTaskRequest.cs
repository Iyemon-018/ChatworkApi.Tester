namespace ChatworkApi.Tester.Domain.Interactors.Requests
{
    using System;
    using UseCases.Requests;

    public sealed class AddWorkTaskRequest : IAddWorkTaskRequest
    {
        public AddWorkTaskRequest(int           roomId
                                , string        body
                                , int[]         assignedAccountIds
                                , DateTime?     limit     = null
                                , TaskLimitType limitType = TaskLimitType.None)
        {
            RoomId             = roomId;
            Body               = body;
            AssignedAccountIds = assignedAccountIds;
            Limit              = limit;
            LimitType          = limitType;
        }

        public int RoomId { get; }

        public string Body { get; }

        public int[] AssignedAccountIds { get; }

        public DateTime? Limit { get; }

        public TaskLimitType LimitType { get; }
    }
}