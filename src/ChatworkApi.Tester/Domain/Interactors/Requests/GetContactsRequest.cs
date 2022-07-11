namespace ChatworkApi.Tester.Domain.Interactors.Requests
{
    using UseCases.Requests;

    /// <summary>
    /// <see cref="GetContactsInteractor"/> の要求情報を保持するクラスです。
    /// </summary>
    public class GetContactsRequest : IGetContactsRequest
    {
        public static IGetContactsRequest Empty() => new GetContactsRequest();
    }
}