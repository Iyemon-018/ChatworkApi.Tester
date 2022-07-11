namespace ChatworkApi.Tester.Domain.Services
{
    using System.Threading.Tasks;
    using ChatworkApi.Models;

    /// <summary>
    /// /me API を実行するための機能を提供するインターフェースです。
    /// </summary>
    public interface IChatworkMeApi
    {
        Task<Me> GetAsync();
    }
}