﻿using BlazorSozluk.Api.Application.Interfaces.Repositories;
using BlazorSozluk.Common.Infrastructure.Entensions;
using BlazorSozluk.Common.Models.Page;
using BlazorSozluk.Common.Models.Queries;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorSozluk.Api.Application.Features.Queries.GetUserEntries
{
    public class GetUserEntriesDetailQueryHandler : IRequestHandler<GetUserEntriesDetailQuery, PagedViewModel<GetEntryDetailViewModel>>
    {
        private readonly IEntryRepository entryRepository;

        public GetUserEntriesDetailQueryHandler(IEntryRepository entryRepository)
        {
            this.entryRepository = entryRepository;
        }

        public Task<PagedViewModel<GetEntryDetailViewModel>> Handle(GetUserEntriesDetailQuery request, CancellationToken cancellationToken)
        {
            var query = entryRepository.AsQueryable();

            if (request.UserId != null && request.UserId.HasValue && request.UserId != Guid.Empty)
            {
                query = query.Where(i => i.CreatedById == request.UserId);
            }
            else if (!string.IsNullOrEmpty(request.UserName))
            {
                query = query.Where(i => i.CreatedBy.UserName == request.UserName);
            }

            query = query.Include(i => i.EntryFavorites)
                          .Include(i => i.CreatedBy);

            var list = query.Select(i => new GetEntryDetailViewModel()
            {
                Content = i.Content,
                Subject = i.Subject,
                CreatedByUserName = i.CreatedBy.UserName,
                CreatedDate = i.CreateDate,
                FavoritedCount = i.EntryFavorites.Count,
                Id = i.Id,
                IsFavorited = false
            });

            var result = list.GetPaged(request.Page, request.PageSize);
            return result;
        }
    }
}
