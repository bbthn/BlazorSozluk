using Blazored.LocalStorage;
using BlazorSozluk.Clients.WebApp.Infrastructure.Extensions;

namespace BlazorSozluk.Clients.WebApp.Infrastructure.Auth
{
    public class AuthTokenHandler : DelegatingHandler
    {
        private readonly ISyncLocalStorageService syncLocalStorageService;

        public AuthTokenHandler(ISyncLocalStorageService syncLocalStorageService)
        {
            this.syncLocalStorageService = syncLocalStorageService;
        }

        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            var token = syncLocalStorageService.GetToken();
            if (!string.IsNullOrEmpty(token))
                request.Headers.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("bearer", token);

            return await base.SendAsync(request, cancellationToken);
        }
    }
}
