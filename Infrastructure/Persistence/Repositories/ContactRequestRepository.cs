using Application.Abstractions.Repositories;
using Application.CustomerService.Inputs;
using Application.CustomerService.Outputs;
using Infrastructure.Persistence.Contexts;
using Infrastructure.Persistence.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence.Repositories;

public class ContactRequestRepository(PersistenceContext context) : IContactRequestRepository
{
    public async Task<bool> AddAsync(ContactRequestInput model)
    {
        ArgumentNullException.ThrowIfNull(model); //Kan bygga en egen exception som returnerar fel

        var entity = new ContactRequestEntity
        {
            Id = model.Id,
            FirstName = model.FirstName,
            LastName = model.LastName,
            Email = model.Email,
            Phone = model.Phone,
            Message = model.Message,
            CreatedAt = model.CreatedAt
        };

        await context.AddAsync(entity);
        await context.SaveChangesAsync();

        return true;
    }

    public async Task<IReadOnlyList<ContactRequest>> GetAllAsync()
    {
        var entities = await context.ContactRequests
            .AsNoTracking()
            .OrderByDescending(x => x.CreatedAt)
            .ToListAsync();

        return [.. entities.Select(x => new ContactRequest(x.Id, x.FirstName, x.LastName, x.Email, x.Phone, x.Message, x.CreatedAt))];
    }
}
