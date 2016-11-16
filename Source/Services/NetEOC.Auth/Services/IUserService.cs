using NetEOC.Auth.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NetEOC.Auth.Services
{
    public interface IUserService
    {
        Task<User> GetUserById(Guid id);

        Task<User> GetUserByAuthId(string id);

        Task<User> CreateUser(User user);

        Task<User> UpdateUser(User user);

        Task<bool> DeleteUser(Guid userId);

        Task<bool> ValidateUser(string token, Guid id); 
    }
}
