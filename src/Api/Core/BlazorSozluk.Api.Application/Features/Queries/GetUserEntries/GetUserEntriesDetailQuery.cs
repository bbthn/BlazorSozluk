using BlazorSozluk.Common.Models.Page;
using BlazorSozluk.Common.Models.Queries;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorSozluk.Api.Application.Features.Queries.GetUserEntries
{
    public class GetUserEntriesDetailQuery : BasePagedQuery, IRequest<PagedViewModel<GetUserEntriesDetailViewModel>>
    {
        public GetUserEntriesDetailQuery(Guid? userId, string userName=null, int page = 1, int pageSize = 10):base(page,pageSize)
        {
            UserId = userId;
            UserName = userName;
        }

        public Guid? UserId { get; set; }
        public string UserName { get; set; }


    }
}
