namespace ChatworkApi.Tester.Domain.UseCases
{
    using System.Threading.Tasks;
    using Requests;
    using Responses;

    /// <summary>
    /// 自分自身に割り当てられているタスク一覧を取得するためのユースケース インターフェースです。
    /// </summary>
    public interface IGetMyTasksUseCase
    {
        Task<IGetMyTasksResponse> Execute(IGetMyTasksRequest request);
    }
}