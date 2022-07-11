namespace ChatworkApi.Tester.Domain
{
    using System.Collections.Concurrent;
    using System.Collections.Generic;
    using AutoMapper;
    using ChatworkApi.Models;
    using Models;
    using Presentation.Models;
    using Account = Models.Account;
    using Contact = Models.Contact;
    using MyRoom = Models.MyRoom;
    using Room = Models.Room;
    using RoomMember = ChatworkApi.Models.RoomMember;
    using WorkTask = Models.WorkTask;

    public sealed class AutoMapperProfile : Profile
    {
        private static readonly ConcurrentDictionary<string, TaskStatus> ApiTaskStatusTextToTaskStatus
            = new ConcurrentDictionary<string, TaskStatus>(new List<KeyValuePair<string, TaskStatus>>
                                                               {
                                                                   new KeyValuePair<string, TaskStatus>("done", TaskStatus.Done)
                                                                 , new KeyValuePair<string, TaskStatus>("open", TaskStatus.InProgress)
                                                               });

        private static readonly ConcurrentDictionary<ChatworkApi.TaskStatus, TaskStatus> ApiTaskStatusToTaskStatus
            = new ConcurrentDictionary<ChatworkApi.TaskStatus, TaskStatus>(new List<KeyValuePair<ChatworkApi.TaskStatus, TaskStatus>>
                                                           {
                                                               new KeyValuePair<ChatworkApi.TaskStatus, TaskStatus>(ChatworkApi.TaskStatus.Done, TaskStatus.Done)
                                                             , new KeyValuePair<ChatworkApi.TaskStatus, TaskStatus>(ChatworkApi.TaskStatus.Open, TaskStatus.InProgress)
                                                           });

        private static readonly ConcurrentDictionary<TaskStatus, ChatworkApi.TaskStatus> TaskStatusToApiTaskStatus
            = new ConcurrentDictionary<TaskStatus, ChatworkApi.TaskStatus>(new List<KeyValuePair<TaskStatus, ChatworkApi.TaskStatus>>
                                                                           {
                                                                               new KeyValuePair<TaskStatus, ChatworkApi.TaskStatus>(TaskStatus.InProgress, ChatworkApi.TaskStatus.Open)
                                                                             , new KeyValuePair<TaskStatus, ChatworkApi.TaskStatus>(TaskStatus.Done, ChatworkApi.TaskStatus.Done)
                                                                           });

        private static readonly ConcurrentDictionary<string, TaskLimitType> ApiTaskLimitTypeTextToTaskLimitType
            = new ConcurrentDictionary<string, TaskLimitType>(new List<KeyValuePair<string, TaskLimitType>>
                                                              {
                                                                  new KeyValuePair<string, TaskLimitType>("date", TaskLimitType.Date)
                                                                , new KeyValuePair<string, TaskLimitType>("time", TaskLimitType.DateTime)
                                                                , new KeyValuePair<string, TaskLimitType>("none", TaskLimitType.None)
                                                              });

        private static readonly ConcurrentDictionary<ChatworkApi.TaskLimitType, TaskLimitType> ApiTaskLimitTypeToTaskLimitType
            = new ConcurrentDictionary<ChatworkApi.TaskLimitType, TaskLimitType>(new List<KeyValuePair<ChatworkApi.TaskLimitType, TaskLimitType>>
                                                              {
                                                                  new KeyValuePair<ChatworkApi.TaskLimitType, TaskLimitType>(ChatworkApi.TaskLimitType.Date, TaskLimitType.Date)
                                                                , new KeyValuePair<ChatworkApi.TaskLimitType, TaskLimitType>(ChatworkApi.TaskLimitType.Time, TaskLimitType.DateTime)
                                                                , new KeyValuePair<ChatworkApi.TaskLimitType, TaskLimitType>(ChatworkApi.TaskLimitType.None, TaskLimitType.None)
                                                              });

        private static readonly ConcurrentDictionary<TaskLimitType, ChatworkApi.TaskLimitType> TaskLimitTypeToApiTaskLimitType
                = new ConcurrentDictionary<TaskLimitType, ChatworkApi.TaskLimitType>(new List<KeyValuePair<TaskLimitType, ChatworkApi.TaskLimitType>>
                                                                                     {
                                                                                         new KeyValuePair<TaskLimitType,ChatworkApi.TaskLimitType>(TaskLimitType.Date, ChatworkApi.TaskLimitType.Date)
                                                                                       , new KeyValuePair<TaskLimitType,ChatworkApi.TaskLimitType>(TaskLimitType.DateTime, ChatworkApi.TaskLimitType.Time)
                                                                                       , new KeyValuePair<TaskLimitType,ChatworkApi.TaskLimitType>(TaskLimitType.None, ChatworkApi.TaskLimitType.None)
                                                                                     });

        public AutoMapperProfile()
        {
            CreateMap<TaskStatus, ChatworkApi.TaskStatus>().ConvertUsing(s => TaskStatusToApiTaskStatus[s]);
            CreateMap<ChatworkApi.TaskStatus, TaskStatus>().ConvertUsing(s => ApiTaskStatusToTaskStatus[s]);
            CreateMap<ChatworkApi.TaskLimitType, TaskLimitType>().ConvertUsing(s => ApiTaskLimitTypeToTaskLimitType[s]);
            CreateMap<TaskLimitType, ChatworkApi.TaskLimitType>().ConvertUsing(s => TaskLimitTypeToApiTaskLimitType[s]);

            CreateMap<ChatworkApi.Models.Me, UserProfile>()
                .ForMember(d => d.AccountId, opt => opt.MapFrom(s => s.account_id))
                .ForMember(d => d.ChatworkId, opt => opt.MapFrom(s => s.chatwork_id))
                .ForMember(d => d.Name, opt => opt.MapFrom(s => s.name))
                .ForMember(d => d.OrganizationId, opt => opt.MapFrom(s => s.organization_id))
                .ForMember(d => d.OrganizationName, opt => opt.MapFrom(x => x.organization_name))
                .ForMember(d => d.RoomId, opt => opt.MapFrom(s => s.room_id))
                .ForMember(d => d.Department, opt => opt.MapFrom(s => s.department))
                .ForMember(d => d.Title, opt => opt.MapFrom(s => s.title))
                .ForMember(d => d.Url, opt => opt.MapFrom(s => s.url))
                .ForMember(d => d.Introduction, opt => opt.MapFrom(s => s.introduction))
                .ForMember(d => d.Mail, opt => opt.MapFrom(s => s.mail))
                .ForMember(d => d.OrganizationTelephoneNumber, opt => opt.MapFrom(s => s.tel_organization))
                .ForMember(d => d.ExtensionTelephoneNumber, opt => opt.MapFrom(s => s.tel_extension))
                .ForMember(d => d.MobileTelephoneNumber, opt => opt.MapFrom(s => s.tel_mobile))
                .ForMember(d => d.SkypeAccount, opt => opt.MapFrom(s => s.skype))
                .ForMember(d => d.FacebookAccount, opt => opt.MapFrom(s => s.facebook))
                .ForMember(d => d.TwitterAccount, opt => opt.MapFrom(s => s.twitter))
                .ForMember(d => d.LoginMail, opt => opt.MapFrom(s => s.login_mail))
                .ForMember(d => d.AvatarUrl, opt => opt.MapFrom(s => s.avatar_image_url));

            CreateMap<ChatworkApi.Models.Room, Room>()
                .ForMember(d => d.Id, opt => opt.MapFrom(s => s.room_id))
                .ForMember(d => d.Name, opt => opt.MapFrom(s => s.name))
                .ForMember(d => d.IconPath, opt => opt.MapFrom(s => s.icon_path))
                .ForMember(d => d.IconImage, opt => opt.Ignore());

            CreateMap<ChatworkApi.Models.Account, Account>()
                .ForMember(d => d.Id, opt => opt.MapFrom(s => s.account_id))
                .ForMember(d => d.Name, opt => opt.MapFrom(s => s.name))
                .ForMember(d => d.AvatarUrl, opt => opt.MapFrom(s => s.avatar_image_url))
                .ForMember(d => d.AvatarImage, opt => opt.Ignore());

            CreateMap<ChatworkApi.Models.MyTask, WorkTask>()
                .ForMember(d => d.Id, opt => opt.MapFrom(s => s.task_id))
                .ForMember(d => d.Room, opt => opt.MapFrom(s => s.room))
                .ForMember(d => d.Assigned, opt => opt.MapFrom(s => s.assigned_by_account))
                .ForMember(d => d.MessageId, opt => opt.MapFrom(s => s.message_id))
                .ForMember(d => d.Body, opt => opt.MapFrom(s => s.body))
                .ForMember(d => d.Limit, opt => opt.MapFrom(s => s.limit_time))
                .ForMember(d => d.Status, opt => opt.MapFrom(s => ApiTaskStatusTextToTaskStatus[s.status]))
                .ForMember(d => d.LimitType
                         , opt => opt.MapFrom(s => ApiTaskLimitTypeTextToTaskLimitType[s.limit_type]));

            CreateMap<ChatworkApi.Models.MyRoom, MyRoom>()
                .ConstructUsing(s => new MyRoom(s.room_id
                                              , s.name
                                              , s.type
                                              , s.role
                                              , s.sticky
                                              , s.unread_num
                                              , s.mention_num
                                              , s.mytask_num
                                              , s.message_num
                                              , s.file_num
                                              , s.task_num
                                              , s.icon_path
                                              , s.last_update_time))
                .ForMember(d => d.IconImage, opt => opt.Ignore())
                .IgnoreAllPropertiesWithAnInaccessibleSetter();

            CreateMap<ChatworkApi.Models.Message, ChatMessage>()
                .ConstructUsing(s => new ChatMessage(s.message_id, s.body, s.send_time, s.update_time))
                .ForMember(d => d.Sender, opt => opt.MapFrom(s => s.account))
                .ForMember(d => d.Id, opt => opt.Ignore())
                .ForMember(d => d.Body, opt => opt.Ignore())
                .ForMember(d => d.SendDateTime, opt => opt.Ignore())
                .ForMember(d => d.UpdateDateTime, opt => opt.Ignore());

            CreateMap<ChatworkApi.Models.Contact, Contact>()
                .ConstructUsing(s => new Contact(s.account_id
                                               , s.room_id
                                               , s.name
                                               , s.chatwork_id
                                               , s.organization_id
                                               , s.organization_name
                                               , s.department
                                               , s.avatar_image_url))
                .ForMember(d => d.AvatarImage, opt => opt.Ignore())
                .IgnoreAllPropertiesWithAnInaccessibleSetter();

            CreateMap<Account, SelectableAccount>()
                .ConstructUsing(s => new SelectableAccount(s.Id, s.Name, s.AvatarUrl))
                .ForMember(d => d.Selected, opt => opt.Ignore())
                .IgnoreAllPropertiesWithAnInaccessibleSetter();

            CreateMap<RoomMember, Models.RoomMember>()
                .ConstructUsing(s => new Models.RoomMember(s.account_id
                                                         , s.role
                                                         , s.name
                                                         , s.chatwork_id
                                                         , s.organization_id
                                                         , s.organization_name
                                                         , s.department
                                                         , s.avatar_image_url))
                .ForMember(d => d.AvatarImage, opt => opt.Ignore())
                .IgnoreAllPropertiesWithAnInaccessibleSetter();

            CreateMap<RoomConfiguration, Models.RoomData>()
                .ConstructUsing(s => new Models.RoomData(s.room_id
                                                       , s.name
                                                       , s.type
                                                       , s.role
                                                       , s.sticky
                                                       , s.unread_num
                                                       , s.mention_num
                                                       , s.mytask_num
                                                       , s.message_num
                                                       , s.file_num
                                                       , s.task_num
                                                       , s.icon_path
                                                       , s.last_update_time
                                                       , s.description))
                .IgnoreAllPropertiesWithAnInaccessibleSetter();
        }
    }
}