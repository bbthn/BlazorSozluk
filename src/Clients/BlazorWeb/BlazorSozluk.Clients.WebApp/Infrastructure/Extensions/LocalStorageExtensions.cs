using Blazored.LocalStorage;

namespace BlazorSozluk.Clients.WebApp.Infrastructure.Extensions;

public static class LocalStorageExtensions
{
    public const string TokenName = "token";
    public const string UserName = "username";
    public const string UserId = "userid";

    public static bool IsUserLoggedIn(this ISyncLocalStorageService localStorageService)
    {
        return !string.IsNullOrEmpty(GetToken(localStorageService));

    }
    public static string GetUserName(this ISyncLocalStorageService localStorageService)
    {
        return localStorageService.GetItem<string>(UserName);
    }

    public static async Task<string> GetUserName(this ILocalStorageService localStorageService)
    {
        return await localStorageService.GetItemAsync<string>(UserName);
    }

    public static void SetUserName(this  ISyncLocalStorageService localStorageService, string userName)
    {
        localStorageService.SetUserName(userName);
    }

    public static async Task SetUserName(this ILocalStorageService storageService, string userName)
    {
        await storageService.SetUserName(userName);
    }
    public static Guid GetUserId(this ISyncLocalStorageService localStorageService)
    {
        return localStorageService.GetItem<Guid>(UserId);
    }
    public static async Task<Guid> GetUserId(this ILocalStorageService localStorageService)
    {
        return await localStorageService.GetItemAsync<Guid>(UserId);
    }

    public static void SetUserId(this ISyncLocalStorageService storageService, Guid id)
    {
         storageService.SetItem<Guid>(UserId, id);
    }
    public static async Task SetUserId(this ILocalStorageService storageService, Guid id)
    {
        await storageService.SetItemAsync<Guid>(UserId, id);
    }
    public static string GetToken(this ISyncLocalStorageService localStorageService)
    {
        //TODO
        var token = localStorageService.GetItem<string>(TokenName);
        //if (string.IsNullOrEmpty(token))
        //    token = "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJzdWIiOiIxMjM0NTY3ODkwIiwibmFtZSI6IkpvaG4gRG9lIiwiaWF0IjoxNTE2MjM5MDIyfQ.SflKxwRJSMeKKF2QT4fwpMeJf36POk6yJV_adQssw5c";
        return token;
        
    }
    public static async Task<string> GetToken(this ILocalStorageService localStorage)
    {
        var token = await localStorage.GetItemAsync<string>(TokenName);
        //if (string.IsNullOrEmpty(token))
        //    token = "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJzdWIiOiIxMjM0NTY3ODkwIiwibmFtZSI6IkpvaG4gRG9lIiwiaWF0IjoxNTE2MjM5MDIyfQ.SflKxwRJSMeKKF2QT4fwpMeJf36POk6yJV_adQssw5c";
        return token;
    }
    public static void SetToken(this ISyncLocalStorageService storageService, string token)
    {
        storageService.SetItem<string>(TokenName, token);   
    }
    public static async Task SetToken(this ILocalStorageService storageService, string token)
    {
         await storageService.SetItemAsync(TokenName, token);
    }
}
