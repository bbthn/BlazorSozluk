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

    public async Task EmailChangedEvent(UserEmailChangedEvent @event)
    {
        using var connection = new SqlConnection(configuration.GetConnectionString("SqlServer"));

        connection.ExecuteAsync
            ("INSERT INTO emailconfirmation (Id, OldEmailAddress,NewEmailAddress,CreateDate) VALUES(@Id,@OldEmailAddress,@NewEmailAddress,GETDATE()",
            new
            {
                Id= Guid.NewGuid(),
                OldEmailAddress = @event.OldEmailAddress,
                NewEmailAddress= @event.NewEmailAddress,
            });




    }

}
