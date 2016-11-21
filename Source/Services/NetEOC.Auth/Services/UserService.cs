using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using NetEOC.Auth.Models;
using NetEOC.Auth.Data;

namespace NetEOC.Auth.Services
{
    public class UserService
    {
        public UserRepository UserRepository { get; set; }

        public UserService()
        {
            UserRepository = new UserRepository();
        }

        public async Task<User> CreateUser(User user)
        {
            User existing;

            if(user.Id != Guid.Empty)
            {
                existing = await GetUserById(user.Id);

                if(existing == null)
                {
                    return await UserRepository.Create(user);
                }
            }

            existing = await GetUserByAuthId(user.AuthId);

            if (existing == null)
            {
                return await UserRepository.Create(user);
            }

            user.Id = existing.Id;

            return await UpdateUser(user);
        }

        public async Task<bool> DeleteUser(Guid userId)
        {
            return await UserRepository.Delete(userId);
        }

        public async Task<User> GetUserByAuthId(string id)
        {
            return await UserRepository.GetByAuthId(id);
        }

        public async Task<User> GetUserById(Guid id)
        {
            return await UserRepository.Get(id);
        }

        public async Task<User> UpdateUser(User user)
        {
            if(user.Id == Guid.Empty || string.IsNullOrWhiteSpace(user.AuthId))
            {
                return null;
            }

            return await UserRepository.Update(user);
        }

        public async Task<bool> ValidateUser(string authId, Guid id)
        {
            User user = await GetUserById(id);

            if (user == null) return false;

            return user.AuthId == authId;
        }
    }
}
