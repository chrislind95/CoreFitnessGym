namespace Application.CustomerService.Outputs;

public sealed record ContactRequest
(
    string Id,
    string FirstName,
    string LastName,
    string Email,
    string? Phone,
    string Message,
    DateTime CreatedAt
);
