namespace ChatworkApi.Tester.Domain.UseCases.Responses
{
    using System.Collections.Generic;
    using Models;

    /// <summary>
    /// <see cref="IGetMyTasksUseCase"/> の応答結果情報を保持するインターフェースです。
    /// </summary>
    public interface IGetMyTasksResponse
    {
        /// <summary>
        /// 自分のタスク一覧を取得します。
        /// </summary>
        IEnumerable<WorkTask> Tasks { get; }
    }
}