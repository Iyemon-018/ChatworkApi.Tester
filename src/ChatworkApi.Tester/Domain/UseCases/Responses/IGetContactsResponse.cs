namespace ChatworkApi.Tester.Domain.UseCases.Responses
{
    using System.Collections.Generic;
    using Models;

    /// <summary>
    /// <see cref="IGetContactsUseCase"/> の応答情報を保持するインターフェースです。
    /// </summary>
    public interface IGetContactsResponse
    {
        IEnumerable<Contact> Contacts { get; }
    }
}