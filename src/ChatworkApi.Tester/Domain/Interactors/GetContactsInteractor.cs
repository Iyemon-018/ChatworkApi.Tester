namespace ChatworkApi.Tester.Domain.Interactors
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Models;
    using Responses;
    using Services;
    using UseCases;
    using UseCases.Requests;
    using UseCases.Responses;

    public sealed class GetContactsInteractor : IGetContactsUseCase
    {
        private readonly IChatworkApiService _apiService;

        public GetContactsInteractor(IChatworkApiService apiService)
        {
            _apiService = apiService;
        }

        public async Task<IGetContactsResponse> Execute(IGetContactsRequest request)
        {
            var contacts = await _apiService.Contacts.GetAsync();
            return new GetContactsResponse(contacts.Map<IEnumerable<Contact>>());
        }
    }
}