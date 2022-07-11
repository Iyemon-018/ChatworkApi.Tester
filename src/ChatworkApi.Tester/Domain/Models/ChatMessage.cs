namespace ChatworkApi.Tester.Domain.Models
{
    using System;
    using Prism.Mvvm;

    public sealed class ChatMessage : BindableBase
    {
        /// <summary>
        /// 本文
        /// </summary>
        private string _body;

        /// <summary>
        /// メッセージの識別子
        /// </summary>
        private string _id;

        /// <summary>
        /// メッセージを送信した日時
        /// </summary>
        private DateTime? _sendDateTime;

        /// <summary>
        /// 送信ユーザーのアカウント情報
        /// </summary>
        private Account _sender;

        /// <summary>
        /// メッセージを更新した日時
        /// </summary>
        private DateTime? _updateDateTime;

        public ChatMessage(string    id
                         , string    body
                         , DateTime? sendDateTime
                         , DateTime? updateDateTime)
        {
            _id             = id;
            _body           = body;
            _sendDateTime   = sendDateTime;
            _updateDateTime = updateDateTime;
            _sender         = new Account();
        }

        /// <summary>
        /// メッセージの識別子を取得します。
        /// </summary>
        public string Id
        {
            get => _id;
            private set => SetProperty(ref _id, value);
        }

        /// <summary>
        /// 本文を取得します。
        /// </summary>
        public string Body
        {
            get => _body;
            private set => SetProperty(ref _body, value);
        }

        /// <summary>
        /// 送信ユーザーのアカウント情報を取得します。
        /// </summary>
        public Account Sender
        {
            get => _sender;
            private set => SetProperty(ref _sender, value);
        }

        /// <summary>
        /// メッセージを送信した日時を取得します。
        /// </summary>
        public DateTime? SendDateTime
        {
            get => _sendDateTime;
            private set => SetProperty(ref _sendDateTime, value);
        }

        /// <summary>
        /// メッセージを更新した日時を取得します。
        /// </summary>
        public DateTime? UpdateDateTime
        {
            get => _updateDateTime;
            private set => SetProperty(ref _updateDateTime, value);
        }
    }
}