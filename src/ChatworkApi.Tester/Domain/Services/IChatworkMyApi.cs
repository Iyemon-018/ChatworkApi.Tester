namespace ChatworkApi.Tester.Domain.Services
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using ChatworkApi.Models;
    using TaskStatus = ChatworkApi.TaskStatus;

    /// <summary>
    /// /my API を実行するための機能を提供するインターフェースです。
    /// </summary>
    public interface IChatworkMyApi
    {
        Task<IEnumerable<MyTask>> GetTasksAsync(int?       requesterId
                                              , TaskStatus status);
    }
}