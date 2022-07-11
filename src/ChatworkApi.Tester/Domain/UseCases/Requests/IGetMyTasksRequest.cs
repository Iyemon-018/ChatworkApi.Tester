namespace ChatworkApi.Tester.Domain.UseCases.Requests
{
    /// <summary>
    /// <see cref="IGetMyTasksUseCase"/> の要求情報を保持するインターフェースです。
    /// </summary>
    public interface IGetMyTasksRequest
    {
        /// <summary>
        /// タスクを依頼したユーザーのアカウントIDを取得します。
        /// </summary>
        int? RequesterId { get; }

        /// <summary>
        /// 対象のタスク状態を取得します。
        /// </summary>
        TaskStatus Status { get; }
    }
}