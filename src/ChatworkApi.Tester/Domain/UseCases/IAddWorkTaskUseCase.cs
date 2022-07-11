namespace ChatworkApi.Tester.Domain.UseCases
{
    using System.Threading.Tasks;
    using Requests;

    /// <summary>
    /// タスクを登録するためのユースケース インターフェースです。
    /// </summary>
    public interface IAddWorkTaskUseCase
    {
        Task<IAddWorkTaskResponse> Execute(IAddWorkTaskRequest request);
    }
}