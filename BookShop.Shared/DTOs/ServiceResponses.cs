using Microsoft.AspNetCore.Identity;

namespace BookShop.Shared;

public class ServiceResponses
{
    public record class GeneralResponse(bool Succeeded, string Message, IEnumerable<IdentityError>? Errors = null, object? Data = null);
    public record class LoginResponse(bool Succeeded, string Token, string Message);
}
