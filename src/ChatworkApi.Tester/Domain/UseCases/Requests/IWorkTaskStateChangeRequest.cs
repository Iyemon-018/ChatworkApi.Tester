namespace ChatworkApi.Tester.Domain.UseCases.Requests
{
    public interface IWorkTaskStateChangeRequest
    {
        int RoomId { get; }

        int TaskId { get; }

        TaskStatus Status { get; }
    }
}