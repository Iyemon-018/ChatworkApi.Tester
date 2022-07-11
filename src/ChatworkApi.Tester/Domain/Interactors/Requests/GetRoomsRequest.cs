namespace ChatworkApi.Tester.Domain.Interactors.Requests
{
    using UseCases.Requests;

    /// <summary>
    /// <see cref="GetMyRoomsInteractor"/> の要求情報を保持するクラスです。
    /// </summary>
    public sealed class GetMyRoomsRequest : IGetMyRoomsRequest
    {
        public static IGetMyRoomsRequest Empty() => new GetMyRoomsRequest();
    }
}