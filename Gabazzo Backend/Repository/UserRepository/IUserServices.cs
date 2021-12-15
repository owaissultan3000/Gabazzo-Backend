using Gabazzo_Backend.Models.DbModels;
using Gabazzo_Backend.Models.InputModels.UserModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Gabazzo_Backend.Repository.UserRepository
{
    public interface IUserServices
    {
        public Task<RegisteredUser> GetUser(string email);
        public Task<string> CreateUser(UserRegistration userRegistration );
        public Task<string> UpdateUser(UserRegistration userRegistration);
        public Task<string> DeleteUser(string email);
    }
}
