namespace ChatworkApi.Tester.Domain.Interactors.Responses
{
    using UseCases.Responses;

    internal sealed class WorkTaskStateChangeResponse : IWorkTaskStateChangeResponse
    {
        public WorkTaskStateChangeResponse(int stateChangedTaskId)
        {
            StateChangedTaskId = stateChangedTaskId;
        }

        public int StateChangedTaskId { get; }
    }
}