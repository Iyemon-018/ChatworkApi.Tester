namespace ChatworkApi.Tester.Domain.UseCases
{
    using System.Threading.Tasks;
    using Requests;
    using Responses;

    public interface IWorkTaskStateChangeUseCase
    {
        Task<IWorkTaskStateChangeResponse> Execute(IWorkTaskStateChangeRequest request);
    }
}