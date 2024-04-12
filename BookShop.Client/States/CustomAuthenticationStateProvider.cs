using Microsoft.AspNetCore.Components.Authorization;
using Blazored.LocalStorage;
using System.Security.Claims;
using System.IdentityModel.Tokens.Jwt;
using BookShop.Shared;
using System.Net.Http;
using System.Net.Http.Headers;

namespace BookShop.Client
{
    public class CustomAuthenticationStateProvider(ILocalStorageService localStorageService, HttpClient _httpClient) : AuthenticationStateProvider
    {
        private readonly ClaimsPrincipal anonymous = new(new ClaimsIdentity());

        public async Task UpdateAuthenticationState(string? JWTtoken = null)
        {
            ClaimsPrincipal userClaimPrincipal = new();

            if (!string.IsNullOrEmpty(JWTtoken))
            {
                _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(ConstSettings.DefaultJWTscheme, JWTtoken);

                List<Claim> userClaims = GetClaims(JWTtoken);
                userClaimPrincipal = SetClaims(userClaims);

                if (userClaimPrincipal is null)
                    return;

                await localStorageService.SetItemAsStringAsync(ConstSettings.CachedJWT, JWTtoken);
            }

            else
            {
                _httpClient.DefaultRequestHeaders.Authorization = null;
                await localStorageService.RemoveItemAsync(ConstSettings.CachedJWT);
            }

            NotifyAuthenticationStateChanged(Task.FromResult(new AuthenticationState(userClaimPrincipal)));
        }

        public override async Task<AuthenticationState> GetAuthenticationStateAsync()
        {
            string? token = null;

            try { token = await localStorageService.GetItemAsStringAsync(ConstSettings.CachedJWT); }
            catch (InvalidOperationException) { }

            if (string.IsNullOrEmpty(token))
                return new(anonymous);

            List<Claim> userClaims = GetClaims(token);

            var userClaimPrincipal = SetClaims(userClaims);

            if (userClaimPrincipal is null)
                return new(anonymous);

            return new(userClaimPrincipal);
        }

        private static ClaimsPrincipal SetClaims(List<Claim> userClaims)
        {
            return new(new ClaimsIdentity(userClaims, ConstSettings.AuthenticationType));
        }

        private static List<Claim> GetClaims(string JWTtoken)
        {
            var tokenHandler = new JwtSecurityTokenHandler();

            var token = tokenHandler.ReadJwtToken(JWTtoken);

            List<Claim> userCliams =
            [
                token.Claims.FirstOrDefault(c => c.Type.Equals(ClaimTypes.Role))!,
                token.Claims.FirstOrDefault(c => c.Type.Equals(ClaimTypes.Name))!,
                token.Claims.FirstOrDefault(c => c.Type.Equals(ClaimTypes.Email))!,
            ];

            return userCliams;
        }
    }
}
