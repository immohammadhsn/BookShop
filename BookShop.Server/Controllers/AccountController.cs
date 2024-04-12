using Azure;
using BookShop.Server.Data;
using BookShop.Shared;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SharedClassLibrary.Contracts;

namespace BookShop.Server.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AccountController(IUserAccount userAccount,UserManager<ApplicationUser> userManager) : ControllerBase
{
    [HttpPost("Register")]
    public async Task<IActionResult> Register(UserDTO userDTO)
    {
        var response = await userAccount.CreateAccount(userDTO);

        return response.Succeeded ? Ok(response) : BadRequest(response);
    }

    [HttpPost("Login")]
    public async Task<IActionResult> Login(LoginDTO loginDTO)
    {
        var response = await userAccount.LoginAccount(loginDTO);

        return response.Succeeded ? Ok(response) : BadRequest(response);
    }

    [HttpPut("EditUser")]
    public async Task<IActionResult> EditUser(UserDTO editedUser)
    {
        var response = await userAccount.UpdateUserData(editedUser);

        return response.Succeeded ? Ok(response) : BadRequest(response);
    }
}