using static BookShop.Shared.ServiceResponses;

namespace BookShop.Shared;

public interface IUserAccount
{
    Task<GeneralResponse> CreateAccount(RegisterDTO userDTO);
    Task<LoginResponse> LoginAccount(LoginDTO loginDTO);
    public Task<GeneralResponse> UpdateUserData(RegisterDTO updatedUser);
}
