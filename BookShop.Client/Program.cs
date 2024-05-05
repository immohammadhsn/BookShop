using Blazored.LocalStorage;
using BookShop.Client;
using BookShop.Client.Services;
using BookShop.Shared;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using System.Net.Sockets;
using System.Net;
using Microsoft.AspNetCore.Http;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
builder.Services.AddScoped<FileUploadService>();

builder.Services.AddScoped<HttpClient>(sp => new() { BaseAddress = new("https://localhost:7278") });

builder.Services.AddBlazoredLocalStorage();
builder.Services.AddScoped<CustomAuthenticationStateProvider>();
builder.Services.AddScoped<AuthorService>();
builder.Services.AddScoped<BookService>();
builder.Services.AddScoped<AuthenticationStateProvider>(provider => provider.GetRequiredService<CustomAuthenticationStateProvider>());

builder.Services.AddScoped<IAccountService, AccountService>();

builder.Services.AddCascadingAuthenticationState();
builder.Services.AddAuthorizationCore();


await builder.Build().RunAsync();
