using BookShop.Shared;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using System.Security.Claims;

namespace BookShop.Client;

public partial class Manage
{

    private bool isAdmin = false;

    protected async override Task OnInitializedAsync()
    {
        var context = await AuthenticationStateProvider.GetAuthenticationStateAsync();
        if (context?.User?.Claims != null)
        {
            var roleClaim = context.User.Claims.FirstOrDefault(c => c.Type.Equals(ClaimTypes.Role));
            if (roleClaim != null && roleClaim.Value.Equals(ConstSettings.AdminRole))
                isAdmin = true;
        }
        StateHasChanged();
        await base.OnInitializedAsync();
    }


}