using static BookShop.Shared.ServiceResponses;

namespace BookShop.Shared;

public interface IUserAccount
{
    Task<GeneralResponse> CreateAccount(UserDTO userDTO);
    Task<LoginResponse> LoginAccount(LoginDTO loginDTO);
    public Task<GeneralResponse> UpdateUserData(UserDTO updatedUser);
}
