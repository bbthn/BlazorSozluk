using Blazored.LocalStorage;
using BlazorSozluk.Clients.WebApp.Infrastructure.Extensions;
using BlazorSozluk.Clients.WebApp.Infrastructure.Services.Interfaces;
using BlazorSozluk.Common.Infrastructure.Exceptions;
using BlazorSozluk.Common.Infrastructure.Results;
using BlazorSozluk.Common.Models.Queries;
using BlazorSozluk.Common.Models.RequestModels;
using System.Net.Http.Json;
using System.Text.Json;

namespace BlazorSozluk.Clients.WebApp.Infrastructure.Services
{
    public class IdentityService : IIdentityService
    {
        private readonly HttpClient httpClient;
        private readonly ISyncLocalStorageService syncLocalStorageService;

        public IdentityService(HttpClient httpClient, ISyncLocalStorageService syncLocalStorageService)
        {
            this.httpClient = httpClient;
            this.syncLocalStorageService = syncLocalStorageService;
        }
        public bool IsLoggedIn => !string.IsNullOrEmpty(GetUserToken());

        public string GetUserToken()
        {
            return syncLocalStorageService.GetToken();
        }
        public string GetUserName()
        {
            return syncLocalStorageService.GetToken();
        }
        public Guid GetUserId()
        {
            return syncLocalStorageService.GetUserId();
        }

        public async Task<bool> Login(LoginUserCommand command)
        {
            string responseStr;
            var httpResponse = await httpClient.PostAsJsonAsync("/api/user/login", command);

            if (httpResponse != null && !httpResponse.IsSuccessStatusCode)
            {
                if (httpResponse.StatusCode == System.Net.HttpStatusCode.BadRequest)
                {
                    responseStr = await httpResponse.Content.ReadAsStringAsync();
                    var validation = JsonSerializer.Deserialize<ValidationResponseModel>(responseStr);
                    responseStr = validation.FlattenError;
                    throw new DatabaseValidationException(responseStr);
                }
                return false;
            }


            responseStr = await httpResponse.Content.ReadAsStringAsync();
            var response = JsonSerializer.Deserialize<LoginUserViewModel>(responseStr);
            if (!string.IsNullOrEmpty(response.Token))
            {
                syncLocalStorageService.SetToken(response.Token);
                syncLocalStorageService.SetUserName(response.UserName);
                syncLocalStorageService.SetUserId(response.Id);
            }
            httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("blazorsozluk");
            return true;
        }

        public void Logout()
        {
            syncLocalStorageService.RemoveItem(LocalStorageExtensions.UserId);
            syncLocalStorageService.RemoveItem(LocalStorageExtensions.TokenName);
            syncLocalStorageService.RemoveItem(LocalStorageExtensions.UserName);

            httpClient.DefaultRequestHeaders.Authorization = null;
        }
    }
}
