using BlazorSozluk.Common.Events.Entry;
using BlazorSozluk.Common.Events.EntryComment;
using BlazorSozluk.Common.ViewModels;
using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorSozluk.Projections.VoteService.Services;

public class VoteService
{
    private readonly IConfiguration configuration;
    public VoteService(IConfiguration configuration)
    {
        this.configuration = configuration;
    }

    public async Task CreateEntryVote(CreateEntryVoteEvent vote)
    {
        await DeleteEntryVote(vote.EntryId, vote.CreatedBy);

        using var connection = new SqlConnection(configuration.GetConnectionString("SqlServer"));

        await connection.ExecuteAsync("INSERT INTO ENTRYVOTE (Id,EntryId, VoteType, CreatedById, CreateDate) VALUES(@Id,@EntryId,@VoteType,@CreatedById,@CreateDate)"
            , new
            {
                Id = Guid.NewGuid(),
                EntryId= vote.EntryId,
                VoteType = Convert.ToInt16(vote.VoteType),
                CreatedById = vote.CreatedBy,
                CreateDate = DateTime.Now,
            });
    }
    public  async Task DeleteEntryVote(Guid EntryId, Guid UserId)
    {
        using var connection = new SqlConnection(configuration.GetConnectionString("SqlServer"));

        await connection.ExecuteAsync
           ("DELETE FROM EntryVote WHERE EntryId = @EntryId AND CREATEDBYID = @UserId",
           new
           {
               EntryId = EntryId,
               UserId=UserId
           });
    }
    public async Task CreateEntryCommentVote(CreateEntryCommentVoteEvent vote)
    {
        using var connection = new SqlConnection(configuration.GetConnectionString("SqlServer"));

        await connection.ExecuteAsync
            ("INSERT INTO ENTRYCOMMENTVOTE (ID,ENTRYCOMMENTID, VOTETYPE, CREATEDBYID,CREATEDATE) VALUES(@Id, @EntryCommentId,@VoteType,@CreatedById,GETDATE())",
            new
            {
                Id = Guid.NewGuid(),
                EntryCommentId = vote.EntryCommentId,
                VoteType = vote.VoteType,
                CreatedById = vote.CreatedBy,
            });
    }
    public async Task DeleteEntryCommentVote(DeleteEntryCommentVoteEvent vote)
    {
        using var connection = new SqlConnection(configuration.GetConnectionString("SqlServer"));

        await connection.ExecuteAsync("DELETE FROM ENTRYCOMMENTVOTE WHERE ENTRYCOMMENTID=@EntryCommentId AND CREATEDBYID=@CreatedById",
            new
            {
                EntryCommentId = vote.EntryCommentId,
                CreatedById = vote.CreatedBy,
            });
    }
}
