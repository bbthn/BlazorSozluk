using System.Net.Http.Json;
using BlazorSozluk.Clients.WebApp.Infrastructure.Services.Interfaces;

namespace BlazorSozluk.Clients.WebApp.Infrastructure.Services;

public class FavoriteService : IFavoriteService
{
    private readonly HttpClient httpClient;

    public FavoriteService(HttpClient httpClient)
    {
        this.httpClient = httpClient;
    }

    public async Task CreateEntryFav(Guid entryId)
    {
        await httpClient.PostAsync($"/api/favorite/entry/{entryId}", null);
    }
    public async Task CreateEntryCommentFav(Guid entryCommentId)
    {
        await httpClient.PostAsync($"/api/favorite/entryComment/{entryCommentId}", null);
    }
    public async Task DeleteEntryFav(Guid entryId)
    {
        await httpClient.PostAsync($"/api/favorite/deleteentryfav/{entryId}", null);
    }
    public async Task DeleteEntryCommentFav(Guid entryCommentId)
    {
        await httpClient.PostAsync($"/api/favorite/deleteentrycommentfav/{entryCommentId}", null);
    }
}
