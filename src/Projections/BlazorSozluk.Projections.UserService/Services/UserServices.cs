using BlazorSozluk.Common.Events.User;
using Dapper;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorSozluk.Projections.UserService.Services;

public class UserServices
{
    private readonly IConfiguration configuration;
    public UserServices(IConfiguration configuration)
    {
        this.configuration = configuration;
    }

    public async Task<Guid> CreateEmailConfirmation(UserEmailChangedEvent @event)
    {

        var guid = Guid.NewGuid();
        using var connection = new SqlConnection(configuration.GetConnectionString("SqlServer"));

        await connection.ExecuteAsync("INSERT INTO emailconfirmation (Id, NewEmailAddress, OldEmailAddress, CreateDate) VALUES (@Id,@NewEmailAddress,@OldEmailAddress,GETDATE())",
              new
              {
                  Id = guid,
                  OldEmailAddress = @event.OldEmailAddress,
                  NewEmailAddress = @event.NewEmailAddress
              });
        return guid;

    }

}
