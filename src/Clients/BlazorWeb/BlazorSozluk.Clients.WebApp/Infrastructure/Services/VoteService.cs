using BlazorSozluk.Clients.WebApp.Infrastructure.Services.Interfaces;
using BlazorSozluk.Common.ViewModels;

namespace BlazorSozluk.Clients.WebApp.Infrastructure.Services
{
    public class VoteService : IVoteService
    {
        private readonly HttpClient httpClient;
        public VoteService(HttpClient httpClient)
        {

            this.httpClient = httpClient;

        }
        public async Task DeleteEntryVote(Guid entryId)
        {
            var response = await httpClient.PostAsync($"/api/vote/DeleteEntryVote/{entryId}", null);

            if (!response.IsSuccessStatusCode)
                throw new Exception("DeleteEntryVote Error");

        }

        public async Task DeleteEntryCommentVote(Guid entryCommentId)
        {
            var response = await httpClient.PostAsync($"/api/vote/DeleteEntryCommentVote/{entryCommentId}", null);

            if (!response.IsSuccessStatusCode)
                throw new Exception("DeleteEntryCommentVote Error");

        }
        public async Task CreateEntryUpVote(Guid entryId)
        {
            await CreateEntryVote(entryId, VoteType.UpVote);
        }
        public async Task CreateEntryDownVote(Guid entryId)
        {
            await CreateEntryVote(entryId, VoteType.DownVote);
        }

        public async Task CreateEntryCommentUpVote(Guid entryCommentId)
        {
            await CreateEntryCommentVote(entryCommentId, VoteType.UpVote);
        }
        public async Task CreateEntryCommentDownVote(Guid entryCommentId)
        {
            await CreateEntryCommentVote(entryCommentId, VoteType.DownVote);
        }

        private async Task<HttpResponseMessage> CreateEntryCommentVote(Guid entryCommentId, VoteType voteType = VoteType.UpVote)
        {
            var result = await httpClient.PostAsync($"api/vote/entrycomment/{entryCommentId}?votetype={voteType}", null);
            return result;
        }

        private async Task<HttpResponseMessage> CreateEntryVote(Guid entryId, VoteType voteType = VoteType.UpVote)
        {
            var result = await httpClient.PostAsync($"api/vote/entry/{entryId}?votetype={voteType}", null);
            return result;
        }
    }
}
