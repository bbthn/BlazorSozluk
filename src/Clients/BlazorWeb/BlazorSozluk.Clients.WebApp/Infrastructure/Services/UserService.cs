using BlazorSozluk.Clients.WebApp.Infrastructure.Services.Interfaces;
using BlazorSozluk.Common.Events.User;
using BlazorSozluk.Common.Infrastructure.Exceptions;
using BlazorSozluk.Common.Infrastructure.Results;
using BlazorSozluk.Common.Models.Queries;
using BlazorSozluk.Common.Models.RequestModels;
using System.Net.Http.Json;
using System.Text.Json;

namespace BlazorSozluk.Clients.WebApp.Infrastructure.Services;

public class UserService : IUserService
{
    private readonly HttpClient httpClient;

    public UserService(HttpClient httpClient)
    {
        this.httpClient = httpClient;
    }
    public async Task<UserDetailViewModel> GetUserDetail(Guid userId)
    {
        var response = await httpClient.GetFromJsonAsync<UserDetailViewModel>($"/api/user/{userId}");
        return response;
    }
    public async Task<UserDetailViewModel> GetUserDetail(string userName)
    {
        var response = await httpClient.GetFromJsonAsync<UserDetailViewModel>($"/api/user/username/{userName}");
        return response;
    }
    public async Task<bool> UpdateUser(UpdateUserCommand command)
    {
        var res = await httpClient.PostAsJsonAsync("/api/user/Update", command);
        return res.IsSuccessStatusCode;
    }
    public async Task<bool> ChangeUserPassword(string oldPassword, string newPassword)
    {
        var command = new ChangeUserPasswordCommand(null, oldPassword, newPassword);
        var httpResponse = await httpClient.PostAsJsonAsync("/api/user/ChangePassword", command);

        if (httpResponse != null && !httpResponse.IsSuccessStatusCode)
        {
            if (httpResponse.StatusCode == System.Net.HttpStatusCode.BadGateway)
            {
                var responseStr = await httpResponse.Content.ReadAsStringAsync();
                var validation = JsonSerializer.Deserialize<ValidationResponseModel>(responseStr);
                responseStr = validation.FlattenError;
                throw new DatabaseValidationException(responseStr);
            }
            return false;
        }
        return httpResponse.IsSuccessStatusCode;

    }

}
