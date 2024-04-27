using static BookShop.Shared.ServiceResponses;

namespace BookShop.Shared;

public interface IAccountRepository
{
    Task<GeneralResponse> CreateAccount(RegisterDTO userDTO);
    Task<LoginResponse> LoginAccount(LoginDTO loginDTO);
    Task<GeneralResponse> UpdateUserData(RegisterDTO updatedUser);
}

public interface IAccountService
{
    Task<GeneralResponse> CreateAccount(RegisterDTO userDTO);
    Task<LoginResponse> LoginAccount(LoginDTO loginDTO);
    Task<GeneralResponse> UpdateUserData(RegisterDTO updatedUser);
    Task LogOut();

}
