using Application.Abstractions.Repositories;
using Application.Abstractions.Services;
using Application.CustomerService.Inputs;
using Application.CustomerService.Outputs;

namespace Application.CustomerService;

public sealed class ContactRequestService(IContactRequestRepository repo) : IContactRequestService
{
    public async Task<bool> CreateContactRequestAsync(ContactRequestInput input)
    {
        ArgumentNullException.ThrowIfNull(input);

        input.SetId(Guid.NewGuid().ToString());
        input.SetDate(DateTime.UtcNow);

        var result = await repo.AddAsync(input);
        return result;
    }

    public async Task<IReadOnlyList<ContactRequest>> GetContactRequestAsync() => await repo.GetAllAsync();

}
