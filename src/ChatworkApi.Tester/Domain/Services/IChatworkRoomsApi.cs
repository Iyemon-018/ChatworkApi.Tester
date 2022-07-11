namespace ChatworkApi.Tester.Domain.Services
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using ChatworkApi.Models;
    using TaskLimitType = ChatworkApi.TaskLimitType;
    using TaskStatus = ChatworkApi.TaskStatus;

    /// <summary>
    /// /rooms API を実行するための機能を提供するインターフェースです。
    /// </summary>
    public interface IChatworkRoomsApi
    {
        /// <summary>
        /// 自分の参加しているチャットルーム一覧を非同期で取得します。
        /// </summary>
        /// <returns>自分の参加しているチャットルームのシーケンスを返します。</returns>
        Task<IEnumerable<MyRoom>> GetMyRoomsAsync();

        /// <summary>
        /// 指定したルームのメンバー情報を非同期で取得します。
        /// </summary>
        /// <param name="roomId">ルームID</param>
        /// <returns>チャットルームのメンバー情報のシーケンスを返します。</returns>
        Task<IEnumerable<RoomMember>> GetRoomMembersAsync(int roomId);

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
        Task<IEnumerable<Message>> GetMessagesAsync(int roomId, bool? force);

        /// <summary>
        /// 指定したタスクの状態を更新します。
        /// </summary>
        /// <param name="roomId">タスクのあるルームID</param>
        /// <param name="taskId">タスクID</param>
        /// <param name="taskStatus">更新するタスクの状態</param>
        /// <returns></returns>
        Task<UpdatedTaskStatus> UpdateTaskStatusAsync(int        roomId
                                                    , int        taskId
                                                    , TaskStatus taskStatus);

        /// <summary>
        /// チャットに新しいタスクを追加します。
        /// </summary>
        /// <param name="roomId">タスクを追加するルームのID</param>
        /// <param name="body">タスクの内容</param>
        /// <param name="toIds">担当者のIDリスト</param>
        /// <param name="limitType">タスクの期限種別</param>
        /// <param name="limit">タスクの期限日時</param>
        /// <returns></returns>
        Task<AddTask> AddTaskAsync(int            roomId
                               , string         body
                               , int[]          toIds
                               , TaskLimitType? limitType
                               , DateTime?      limit);

        /// <summary>
        /// メッセージを追加します。
        /// </summary>
        /// <param name="roomId">ルームID</param>
        /// <param name="body">メッセージの本文</param>
        /// <param name="unread">追加したメッセージを既読にするかどうか</param>
        /// <returns></returns>
        Task<AddMessage> AddMessageAsync(int    roomId
                                       , string body
                                       , bool?  unread);

        /// <summary>
        /// 指定したルームの詳細情報を取得します。
        /// </summary>
        /// <param name="roomId">ルームID</param>
        /// <returns></returns>
        Task<RoomConfiguration> GetRoomConfigurationAsync(int roomId);
    }
}