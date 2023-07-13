using Blazored.LocalStorage;
using BlazorSozluk.Clients.WebApp;
using BlazorSozluk.Clients.WebApp.Infrastructure.Auth;
using BlazorSozluk.Clients.WebApp.Infrastructure.Services;
using BlazorSozluk.Clients.WebApp.Infrastructure.Services.Interfaces;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using System.Security.Principal;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");


builder.Services.AddHttpClient("WebClient", client =>
{
    client.BaseAddress = new Uri("https://localhost:5001");
});//TODO Auth


builder.Services.AddScoped(sp =>
{
    var clientFactory = sp.GetRequiredService<IHttpClientFactory>();
    return clientFactory.CreateClient("WebClient");
});

builder.Services.AddTransient<IVoteService,VoteService>();
builder.Services.AddTransient<IIdentityService, IdentityService>();
builder.Services.AddTransient<IFavoriteService, FavoriteService>();
builder.Services.AddTransient<IEntryService, EntryService>();
builder.Services.AddTransient<IUserService, UserService>();


builder.Services.AddScoped<AuthenticationStateProvider, AuthStateProvider>();
builder.Services.AddBlazoredLocalStorage();


//builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });

builder.Services.AddAuthorizationCore();

await builder.Build().RunAsync();
