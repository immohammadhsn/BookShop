using BookShop.Shared;
using System.Net.Http.Json;
using static BookShop.Shared.ServiceResponses;

namespace BookShop.Client.Services;

public class AccountService(HttpClient _httpClient) : IUserAccount
{
    public async Task<GeneralResponse> CreateAccount(RegisterDTO userDTO)
    {
        HttpResponseMessage? response = await _httpClient.PostAsJsonAsync("api/Account/Register", userDTO);
        GeneralResponse? result;

        result = await response.Content.ReadFromJsonAsync<GeneralResponse>();

        return result!;
    }

    public async Task<LoginResponse> LoginAccount(LoginDTO loginDTO)
    {
        HttpResponseMessage? response = await _httpClient.PostAsJsonAsync("api/Account/Login", loginDTO);
        LoginResponse? result = await response.Content.ReadFromJsonAsync<LoginResponse>();

        return result!;
    }

    public async Task<GeneralResponse> UpdateUserData(RegisterDTO updatedUser)
    {
        HttpResponseMessage? response = await _httpClient.PostAsJsonAsync("api/Account/EditUser", updatedUser);
        GeneralResponse? result = await response.Content.ReadFromJsonAsync<GeneralResponse>();

        return result!;
    }
}
