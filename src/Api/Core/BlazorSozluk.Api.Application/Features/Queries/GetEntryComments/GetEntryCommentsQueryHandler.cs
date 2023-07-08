using BlazorSozluk.Api.Application.Interfaces.Repositories;
using BlazorSozluk.Common.Infrastructure.Entensions;
using BlazorSozluk.Common.Models.Page;
using BlazorSozluk.Common.Models.Queries;
using BlazorSozluk.Common.ViewModels;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorSozluk.Api.Application.Features.Queries.GetEntryComments
{
    public class GetEntryCommentsQueryHandler :
        IRequestHandler<GetEntryCommentsQuery, PagedViewModel<GetEntryCommentsViewModel>>
    {
        private readonly IEntryCommentRepository entryCommentRepository;

        public GetEntryCommentsQueryHandler(IEntryCommentRepository entryCommentRepository)
        {
            this.entryCommentRepository = entryCommentRepository;
        }

        public async Task<PagedViewModel<GetEntryCommentsViewModel>> Handle(GetEntryCommentsQuery request, CancellationToken cancellationToken)
        {
            var query =  entryCommentRepository.AsQueryable();

            query = query.Include(i => i.EntryCommentVotes).
                          Include(i => i.EntryCommentFavorites).
                          Include(i => i.CreatedBy)
                          .Where(i=>i.EntryId == request.EntryId);

            var list = query.Select(i => new GetEntryCommentsViewModel()
            {
                Content = i.Content,
                CreatedByUserName = i.CreatedBy.UserName,
                CreatedDate = i.CreateDate,
                Id = i.Id,
                FavoritedCount = i.EntryCommentFavorites.Count,
                IsFavorited = request.UserId.HasValue &&
                i.EntryCommentFavorites.Any(j => j.CreatedById == request.UserId),

                VoteType = request.UserId.HasValue && i.EntryCommentVotes.Any(j => j.CreatedById == request.UserId)
                ? i.EntryCommentVotes.FirstOrDefault(z => z.CreatedById == request.UserId).VoteType
                : VoteType.None,

            });

            var entryComments = await list.GetPaged(request.Page, request.PageSize);

            return entryComments;
        }
    }
}
