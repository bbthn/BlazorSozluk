using BlazorSozluk.Common.Events.Entry;
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
        using var connection = new SqlConnection(configuration.GetConnectionString("SqlServer"));

        await DeleteEntryVote(vote.EntryId,vote.CreatedBy);

        await connection.ExecuteAsync("INSERT INTO EntryVote (Id EntryId VoteType CreatedById CreateDate) VALUES(@Id,@EntryId,@VoteType,@CreatedById,@CreateDate, GETDATE())"
            , new
            {
                Id = new Guid(),
                EntryId= vote.EntryId,
                VoteType = Convert.ToInt16(vote.VoteType),
                CreatedById = vote.CreatedBy,
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
}
