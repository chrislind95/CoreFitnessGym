using Application.CustomerService.Inputs;
using Application.CustomerService.Outputs;

namespace Application.Abstractions.Services;

public interface IContactRequestService
{
    Task<bool> CreateContactRequestAsync(ContactRequestInput input);
    Task<IReadOnlyList<ContactRequest>> GetContactRequestAsync();
}
