namespace ChatworkApi.Tester.Domain.Services
{
    /// <summary>
    /// Chatwork API を使用するためのインターフェースです。
    /// </summary>
    public interface IChatworkApiService : IChatworkMeApi, IChatworkMyApi, IChatworkRoomsApi, IChatworkContactsApi
    {
        /// <summary>
        /// /me API を呼び出すためのインターフェースを取得します。
        /// </summary>
        IChatworkMeApi Me { get; }

        /// <summary>
        /// /my API を呼び出すためのインターフェースを取得します。
        /// </summary>
        IChatworkMyApi My { get; }

        /// <summary>
        /// /rooms API を呼び出すためのインターフェースを取得します。
        /// </summary>
        IChatworkRoomsApi Rooms { get; }

        /// <summary>
        /// /contacts API を呼び出すためのインターフェースを取得します。
        /// </summary>
        IChatworkContactsApi Contacts { get; }

        /// <summary>
        /// API Token を登録します。
        /// </summary>
        /// <param name="apiToken"></param>
        void Register(string apiToken);
    }
}