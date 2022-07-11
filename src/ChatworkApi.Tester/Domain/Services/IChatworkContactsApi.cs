namespace ChatworkApi.Tester.Domain.Services
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using ChatworkApi.Models;

    /// <summary>
    /// /contacts API を実行するための機能を提供するインターフェースです。
    /// </summary>
    public interface IChatworkContactsApi
    {
        /// <summary>
        /// 自分のコンタクト一覧を取得します。
        /// </summary>
        /// <returns>取得したコンタクト一覧を返します。</returns>
        Task<IEnumerable<Contact>> GetAsync();
    }
}