using BlazorSozluk.Api.Application.Features.Queries.GetEntries;
using BlazorSozluk.Api.Application.Features.Queries.GetEntryComments;
using BlazorSozluk.Api.Application.Features.Queries.GetEntryDetail;
using BlazorSozluk.Api.Application.Features.Queries.GetMainPageEntries;
using BlazorSozluk.Api.Application.Features.Queries.GetUserEntries;
using BlazorSozluk.Common.Models.Queries;
using BlazorSozluk.Common.Models.RequestModels;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BlazorSozluk.Api.WebApi.Controllers;
[Route("api/[controller]")]
[ApiController]

public class EntryController : BaseController
{
    private readonly IMediator mediator;

    public EntryController(IMediator mediator)
    {
        this.mediator = mediator;
    }
    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(Guid id)
    {
        var entry = await mediator.Send(new GetEntryDetailQuery(id,UserId));
        return Ok(entry);
    }
    [HttpGet("Comments/{id}")]
    public async Task<IActionResult> GetEntryComments(Guid entryId, int page, int pageSize)
    {
        var result = await mediator.Send(new GetEntryCommentsQuery(UserId, entryId, page, pageSize));
        return Ok(result);

    }
    [HttpGet]
    [Route("UserEntries")]
    public async Task<IActionResult> GetUserEntries(Guid userId,string userName, int page, int pageSize)
    {
        if(userId == Guid.Empty && string.IsNullOrEmpty(userName))
        {
            userId = UserId.Value;
        }
        var result = await mediator.Send(new GetUserEntriesDetailQuery(UserId, userName, page, pageSize));
        return Ok(result);

    }


    [HttpGet]
    public async Task<IActionResult> GetEntries([FromQuery] GetEntriesQuery query)
    {
        var entries = await mediator.Send(query);

        return Ok(entries);
    }

    [HttpGet]
    [Route("GetMainPageEntries")]
    public async Task<IActionResult> GetEntriesPaged(int page, int pageSize)
    {
        var entries = await mediator.Send(new GetMainPageEntriesQuery(new Guid(),page, pageSize));
        return Ok(entries);
    }



    [HttpPost]
    [Route("CreateEntry")]
    [Authorize]
    public async Task<IActionResult> CreateEntry([FromBody] CreateEntryCommand command)
    {
        if (!command.CreatedById.HasValue)
            command.CreatedById = UserId;

        var result = await mediator.Send(command);

        return Ok(result);
    }

    [HttpPost]
    [Route("CreateEntryComment")]
    [Authorize]
    public async Task<IActionResult> CreateEntryComment([FromBody] CreateEntryCommentCommand command)
    {
        if (!command.CreatedById.HasValue)
            command.CreatedById = UserId;

        var result = await mediator.Send(command);

        return Ok(result);
    }

    [HttpGet]
    [Route("Search")]
    public async Task<IActionResult> Search([FromQuery] SearchEntryQuery query)
    {
        var result = await mediator.Send(query);
        return Ok(result);
    }


}
