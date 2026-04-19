using Application.CustomerService.Inputs;
using Infrastructure.Persistence.Contexts;
using Infrastructure.Persistence.Repositories;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;

namespace Tests.Integration;

public class ContactRequestRepository_Tests
{
    private PersistenceContext CreateDbContext(out SqliteConnection connection)
    {
        connection = new SqliteConnection("DataSource=:memory:");
        connection.Open();

        var options = new DbContextOptionsBuilder<PersistenceContext>()
            .UseSqlite(connection)
            .Options;

        var context = new PersistenceContext(options);

        context.Database.EnsureCreated();

        return context;
    }

    [Fact]
    public async Task AddAsync_ShouldSaveToDatabase()
    {
        var context = CreateDbContext(out var connection);
        var repository = new ContactRequestRepository(context);

        var input = new ContactRequestInput(
          "John",
          "Doe",
          "john@test.com",
          "123456",
          "Hello"
      );

        input.SetId(Guid.NewGuid().ToString());
        input.SetDate(DateTime.UtcNow);

        await repository.AddAsync(input);

        var saved = await context.ContactRequests.FirstOrDefaultAsync();

        Assert.NotNull(saved);
        Assert.Equal("John", saved!.FirstName);

        await connection.DisposeAsync();
    }

    [Fact]
    public async Task GetAllAsync_ShouldReturnEmptyList_WhenNoDataExists()
    {
        var context = CreateDbContext(out var connection);
        var repository = new ContactRequestRepository(context);

        var result = await repository.GetAllAsync();

        Assert.NotNull(result);
        Assert.Empty(result);

        await connection.DisposeAsync();
    }

    [Fact]
    public async Task GetAllAsync_ShouldReturnAllItems()
    {
        var context = CreateDbContext(out var connection);
        var repository = new ContactRequestRepository(context);

        for (int i = 0; i < 5; i++)
        {
            var input = new ContactRequestInput(
                $"First{i}",
                $"Last{i}",
                $"test{i}@mail.com",
                "123",
                "Hello"
            );

            input.SetId(Guid.NewGuid().ToString());
            input.SetDate(DateTime.UtcNow);

            await repository.AddAsync(input);
        }

        var result = await repository.GetAllAsync();

        Assert.Equal(5, result.Count);

        await connection.DisposeAsync();
    }

}
