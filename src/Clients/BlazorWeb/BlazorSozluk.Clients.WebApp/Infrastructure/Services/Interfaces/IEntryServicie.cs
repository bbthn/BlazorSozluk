using BlazorSozluk.Common.Models.Page;
using BlazorSozluk.Common.Models.Queries;
using BlazorSozluk.Common.Models.RequestModels;

namespace BlazorSozluk.Clients.WebApp.Infrastructure.Services.Interfaces
{
    public interface IEntryServicie
    {
        Task<Guid> CreateEntry(CreateEntryCommand command);
        Task<Guid> CreateEntryComment(CreateEntryCommentCommand command);
        Task<List<GetEntriesViewModel>> GetEntries();
        Task<PagedViewModel<GetEntryCommentsViewModel>> GetEntryComments(Guid entryId, int page, int pageSize);
        Task<GetEntryDetailViewModel> GetEntryDetail(Guid id);
        Task<PagedViewModel<GetEntryDetailViewModel>> GetMainPageEntries(int page, int pageSize);
        Task<PagedViewModel<GetUserEntriesDetailViewModel>> GetUserEntries(string userName, int page, int pageSize);
        Task<List<SearchEntryViewModel>> SearchBySubject(string searchText);
    }
}