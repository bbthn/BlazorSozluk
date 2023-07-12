using BlazorSozluk.Common.Models.Queries;
using BlazorSozluk.Common.Models.RequestModels;

namespace BlazorSozluk.Clients.WebApp.Infrastructure.Services.Interfaces
{
    public interface IUserService
    {
        Task<bool> ChangeUserPassword(string oldPassword, string newPassword);
        Task<UserDetailViewModel> GetUserDetail(Guid userId);
        Task<UserDetailViewModel> GetUserDetail(string userName);
        Task<bool> UpdateUser(UpdateUserCommand command);
    }
}