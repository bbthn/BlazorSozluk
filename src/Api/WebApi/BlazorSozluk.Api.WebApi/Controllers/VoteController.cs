using BlazorSozluk.Api.Application.Features.Commands.Entry.DeleteVote;
using BlazorSozluk.Api.Application.Features.Commands.EntryComment.DeleteVote;
using BlazorSozluk.Api.Domain.Models;
using BlazorSozluk.Common.Models.RequestModels;
using BlazorSozluk.Common.ViewModels;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BlazorSozluk.Api.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VoteController : BaseController
    {
        private readonly IMediator mediator;

        public VoteController(IMediator mediator)
        {
            this.mediator = mediator;
            
        }

        [HttpPost]
        [Route("Entry/{entryId}")]
        public async Task<IActionResult> CreateEntryVote(Guid entryId, VoteType voteType = VoteType.UpVote)
        {
            var result = await mediator.Send(new CreateEntryVoteCommand(entryId, voteType,UserId.Value));
            return Ok(result);
        }
        [HttpPost]
        [Route("EntryComment/{entryCommentId}")]
        public async Task<IActionResult> CreateEntryCommentVote(Guid entryId, VoteType voteType = VoteType.UpVote)
        {
            var result = await mediator.Send(new CreateEntryCommentVoteCommand(entryId, voteType, UserId.Value));
            return Ok(result);
        }
        [HttpPost]
        [Route("DeleteEntryVote/{entryId}")]
        public async Task<IActionResult> DeleteEntryVote(Guid entryId)
        {
            var result = await mediator.Send(new DeleteEntryVoteCommand(entryId,UserId.Value));
            return Ok(result);
        }
        [HttpPost]
        [Route("DeleteEntryCommentVote/{entryCommentId}")]
        public async Task<IActionResult> DeleteEntryCommentVote(Guid entryId)
        {
            var result = await mediator.Send(new DeleteEntryCommentVoteCommand(entryId, UserId.Value));
            return Ok(result);
        }


    }
}
