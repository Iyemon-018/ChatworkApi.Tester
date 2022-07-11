namespace ChatworkApi.Tester.Domain.Interactors.Responses
{
    using System.Collections.Generic;
    using Models;
    using UseCases.Responses;

    /// <summary>
    /// <see cref="GetContactsInteractor"/> の応答情報を保持するクラスです。
    /// </summary>
    internal sealed class GetContactsResponse : IGetContactsResponse
    {
        public GetContactsResponse(IEnumerable<Contact> contacts)
        {
            Contacts = contacts;
        }

        public IEnumerable<Contact> Contacts { get; }
    }
}