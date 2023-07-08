using BlazorSozluk.Common.Models.Page;
using BlazorSozluk.Common.Models.Queries;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorSozluk.Api.Application.Features.Queries.GetEntryComments
{
    public class GetEntryCommentsQuery : BasePagedQuery , IRequest<PagedViewModel<GetEntryCommentsViewModel>>
    {
        public GetEntryCommentsQuery(Guid? userId,Guid entryId ,int page, int pageSize) : base(page, pageSize)
        {
            this.UserId = userId;
            this.EntryId = entryId;
        }

        public Guid EntryId { get; set; }
        public Guid? UserId { get; set; }
    }
}
