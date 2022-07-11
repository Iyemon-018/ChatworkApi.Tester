namespace ChatworkApi.Tester.Domain.Services
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using ChatworkApi.Models;
    using TaskLimitType = ChatworkApi.TaskLimitType;
    using TaskStatus = ChatworkApi.TaskStatus;

    /// <summary>
    /// Chatwork API を使用するための機能を提供するサービス クラスです。
    /// </summary>
    internal sealed partial class ChatworkApiService : IChatworkApiService
    {
        private ClientApi _clientApi;

        /// <summary>
        /// API Token を登録します。
        /// </summary>
        /// <param name="apiToken"></param>
        public void Register(string apiToken)
        {
            _clientApi = new ClientApi(apiToken);
        }

        /// <summary>
        /// /me API を呼び出すためのインターフェースを取得します。
        /// </summary>
        public IChatworkMeApi Me => this;

        /// <summary>
        /// /my API を呼び出すためのインターフェースを取得します。
        /// </summary>
        public IChatworkMyApi My => this;

        /// <summary>
        /// /rooms API を呼び出すためのインターフェースを取得します。
        /// </summary>
        public IChatworkRoomsApi Rooms => this;

        /// <summary>
        /// /contacts API を呼び出すためのインターフェースを取得します。
        /// </summary>
        public IChatworkContactsApi Contacts => this;
    }

    internal partial class ChatworkApiService // /me API
    {
        public Task<Me> GetAsync() => _clientApi.GetMeAsync();
    }

    internal partial class ChatworkApiService // /my API
    {
        Task<IEnumerable<MyTask>> IChatworkMyApi.GetTasksAsync(int?       requesterId
                                                             , TaskStatus status) =>
            _clientApi.GetMyTasksAsync(requesterId, status);
    }

    internal partial class ChatworkApiService // /rooms API
    {
        Task<IEnumerable<MyRoom>> IChatworkRoomsApi.GetMyRoomsAsync() => _clientApi.GetMyRoomsAsync();

        /// <summary>
        /// 指定したルームのメンバー情報を非同期で取得します。
        /// </summary>
        /// <param name="roomId">ルームID</param>
        /// <returns>チャットルームのメンバー情報のシーケンスを返します。</returns>
        Task<IEnumerable<RoomMember>> IChatworkRoomsApi.GetRoomMembersAsync(int roomId)
            => _clientApi.GetRoomMembersAsync(roomId);

        /// <summary>
        /// 指定したルームの最新メッセージを取得します。
        /// </summary>
        /// <param name="roomId">取得対象のグループチャットID</param>
        /// <param name="force">
        /// 最新の 100 件を取得するかどうか。
        /// <c>true</c> の場合、最新の 100 件を取得します。
        /// <c>false</c> もしくは <c>null</c> の場合、前回取得分からの差分を取得します。
        /// </param>
        /// <returns>最大 100 件までのメッセージを取得します。</returns>
        Task<IEnumerable<Message>> IChatworkRoomsApi.GetMessagesAsync(int   roomId
                                                                    , bool? force) =>
            _clientApi.GetMessagesAsync(roomId, force);

        /// <summary>
        /// 指定したタスクの状態を更新します。
        /// </summary>
        /// <param name="roomId">タスクのあるルームID</param>
        /// <param name="taskId">タスクID</param>
        /// <param name="taskStatus">更新するタスクの状態</param>
        /// <returns></returns>
        Task<UpdatedTaskStatus> IChatworkRoomsApi.UpdateTaskStatusAsync(int        roomId
                                                                      , int        taskId
                                                                      , TaskStatus taskStatus) =>
            _clientApi.UpdateTaskStatusAsync(roomId, taskId, taskStatus);

        /// <summary>
        /// チャットに新しいタスクを追加します。
        /// </summary>
        /// <param name="roomId">タスクを追加するルームのID</param>
        /// <param name="body">タスクの内容</param>
        /// <param name="toIds">担当者のIDリスト</param>
        /// <param name="limitType">タスクの期限種別</param>
        /// <param name="limit">タスクの期限日時</param>
        /// <returns></returns>
        Task<AddTask> IChatworkRoomsApi.AddTaskAsync(int            roomId
                                                   , string         body
                                                   , int[]          toIds
                                                   , TaskLimitType? limitType
                                                   , DateTime?      limit)
            => _clientApi.AddTaskAsync(roomId, body, toIds, limitType, limit);


        /// <summary>
        /// メッセージを追加します。
        /// </summary>
        /// <param name="roomId">ルームID</param>
        /// <param name="body">メッセージの本文</param>
        /// <param name="unread">追加したメッセージを既読にするかどうか</param>
        /// <returns></returns>
        Task<AddMessage> IChatworkRoomsApi.AddMessageAsync(int    roomId
                                                         , string body
                                                         , bool?  unread)
            => _clientApi.AddMessageAsync(roomId, body, unread);


        /// <summary>
        /// 指定したルームの詳細情報を取得します。
        /// </summary>
        /// <param name="roomId">ルームID</param>
        /// <returns></returns>
        Task<RoomConfiguration> IChatworkRoomsApi.GetRoomConfigurationAsync(int roomId)
            => _clientApi.GetRoomConfigurationAsync(roomId);
    }

    internal partial class ChatworkApiService // /contacts API
    {
        /// <summary>
        /// 自分のコンタクト一覧を取得します。
        /// </summary>
        /// <returns>取得したコンタクト一覧を返します。</returns>
        Task<IEnumerable<Contact>> IChatworkContactsApi.GetAsync() => _clientApi.GetContactsAsync();
    }
}