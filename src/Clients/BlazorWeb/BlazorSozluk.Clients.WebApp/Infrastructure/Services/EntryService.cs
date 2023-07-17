using BlazorSozluk.Clients.WebApp.Infrastructure.Services.Interfaces;
using BlazorSozluk.Common.Models.Page;
using BlazorSozluk.Common.Models.Queries;
using BlazorSozluk.Common.Models.RequestModels;
using System.Net.Http.Json;

namespace BlazorSozluk.Clients.WebApp.Infrastructure.Services;

public class EntryService : IEntryService
{
    private readonly HttpClient httpClient;

    public EntryService(HttpClient httpClient)
    {
        this.httpClient = httpClient;
    }

    public async Task<List<GetEntriesViewModel>> GetEntries()
    {
        var result = await httpClient.GetFromJsonAsync<List<GetEntriesViewModel>>("/api/entry?todaysentries=false&count=30");
        return result;
    }

    public async Task<GetEntryDetailViewModel> GetEntryDetail(Guid id)
    {
        var result = await httpClient.GetFromJsonAsync<GetEntryDetailViewModel>($"/api/entry/{id}");
        return result;
    }
    public async Task<PagedViewModel<GetEntryDetailViewModel>> GetMainPageEntries(int page, int pageSize)
    {
        var result = await httpClient.GetFromJsonAsync<PagedViewModel<GetEntryDetailViewModel>>
            ($"/api/entry/getmainpageentries?page={page}&pagesize={pageSize}");

        return result;
    }

    public async Task<PagedViewModel<GetUserEntriesDetailViewModel>> GetUserEntries(string userName, int page, int pageSize)
    {
        var result = await httpClient.GetFromJsonAsync<PagedViewModel<GetUserEntriesDetailViewModel>>
            ($"/api/entry/userentries?username={userName}&page={page}&pagesize={pageSize}");

        return result;

    }
    public async Task<PagedViewModel<GetEntryCommentsViewModel>> GetEntryComments(Guid entryId, int page, int pageSize)
    {
        var result = await httpClient.GetFromJsonAsync<PagedViewModel<GetEntryCommentsViewModel>>
            ($"/api/entry/comments/{entryId}?&page={page}&pagesize={pageSize}");
        return result;
    }
    public async Task<Guid> CreateEntry(CreateEntryCommand command)
    {
        var result = await httpClient.PostAsJsonAsync("/api/entry/createentry", command);

        if (!result.IsSuccessStatusCode)
            return Guid.Empty;

        var guidStr = await result.Content.ReadAsStringAsync();

        return new Guid(guidStr.Trim('"'));
    }

    public async Task<Guid> CreateEntryComment(CreateEntryCommentCommand command)
    {
        var res = await httpClient.PostAsJsonAsync("/api/Entry/CreateEntryComment", command);

        if (!res.IsSuccessStatusCode)
            return Guid.Empty;

        var guidStr = await res.Content.ReadAsStringAsync();

        return new Guid(guidStr.Trim('"'));
    }

    public async Task<List<SearchEntryViewModel>> SearchBySubject(string searchText)
    {
        var result = await httpClient.GetFromJsonAsync<List<SearchEntryViewModel>>($"api/entry/search?searchtext={searchText}");
        return result;


    }

}
