namespace ChatworkApi.Tester.Domain.Interactors.Requests
{
    using UseCases.Requests;

    public sealed class WorkTaskStateChangeRequest : IWorkTaskStateChangeRequest
    {
        public WorkTaskStateChangeRequest(int        roomId
                                        , int        taskId
                                        , TaskStatus status)
        {
            RoomId = roomId;
            TaskId = taskId;
            Status = status;
        }

        public int RoomId { get; }

        public int TaskId { get; }

        public TaskStatus Status { get; }

        public static IWorkTaskStateChangeRequest ToComplete(int roomId, int taskId) => new WorkTaskStateChangeRequest(roomId, taskId, TaskStatus.Done);

        public static IWorkTaskStateChangeRequest ToInProgress(int roomId, int taskId) => new WorkTaskStateChangeRequest(roomId, taskId, TaskStatus.InProgress);
    }
}