namespace ChatworkApi.Tester.Domain.Interactors.Requests
{
    using UseCases.Requests;

    /// <summary>
    /// <see cref="GetMyTasksInteractor"/> の要求情報クラスです。
    /// </summary>
    public sealed class GetMyTasksRequest : IGetMyTasksRequest
    {
        public int? RequesterId { get; private set; }

        public TaskStatus Status { get; private set; }

        public static IGetMyTasksRequest InProgressAll() => new GetMyTasksRequest
                                                            {
                                                                RequesterId = null
                                                              , Status      = TaskStatus.InProgress
                                                            };

        public static IGetMyTasksRequest DoneAll() => new GetMyTasksRequest
                                                      {
                                                          RequesterId = null
                                                        , Status      = TaskStatus.Done
                                                      };
    }
}