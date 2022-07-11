namespace ChatworkApi.Tester.ViewModels.Events
{
    using System;

    public sealed class ApiTokenRegisteredEventArgs : EventArgs
    {
        public ApiTokenRegisteredEventArgs(string apiToken)
        {
            ApiToken = apiToken;
        }

        /// <summary>
        /// 暗号化されているAPI Tokenを取得します。
        /// </summary>
        public string ApiToken { get; }
    }
}