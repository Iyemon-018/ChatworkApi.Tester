namespace ChatworkApi.Tester.Domain.UseCases
{
    using System.Threading.Tasks;
    using Requests;
    using Responses;

    /// <summary>
    /// メッセージを追加するためのユースケース インターフェースです。
    /// </summary>
    public interface IAddMessageUseCase
    {
        Task<IAddMessageResponse> Execute(IAddMessageRequest request);
    }
}