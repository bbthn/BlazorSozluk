using BlazorSozluk.Common.Events.Entry;
using Dapper;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorSozluk.Projections.FavoriteService.Services;

public class FavoriteService
{
    private readonly string connectionStr;

    public FavoriteService(string connectionStr)
    {
        this.connectionStr = connectionStr;
    }

    public async Task CreateEntryFavEvent(CreateEntryFavEvent @event)
    {
        using var connection = new SqlConnection(connectionStr);

        await connection.ExecuteAsync
            ("INSERT INTO EntryFavorite (Id, EntryId, CreatedById, CreateDate) VALUES(@Id, @EntryId, @CreatedById, GETDATE())",
            new
            {
                Id=Guid.NewGuid(),
                EntryId = @event.EntryId,
                CreatedById = @event.CreatedBy,
            });

    }
}
