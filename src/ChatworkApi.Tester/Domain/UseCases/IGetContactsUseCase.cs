namespace ChatworkApi.Tester.Domain.UseCases
{
    using System.Threading.Tasks;
    using Requests;
    using Responses;

    /// <summary>
    /// コンタクト一覧を取得するためのユースケース インターフェースです。
    /// </summary>
    public interface IGetContactsUseCase
    {
        Task<IGetContactsResponse> Execute(IGetContactsRequest request);
    }
}