using Gabazzo_Backend.Models.DbModels;
using Gabazzo_Backend.Models.InputModels.UserModels;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Gabazzo_Backend.Repository.UserRepository
{
    public class UserServices : IUserServices
    {
        GabazzoDBContext Db;
        public UserServices(GabazzoDBContext _Db)
        {
            Db = _Db;
        }
        public async Task<string> CreateUser(UserRegistration userRegistration)
        {
            if (Db != null)
            {
                var user = await Db.RegisteredUsers.FirstOrDefaultAsync(u => u.Email == userRegistration.Email);
                if (user == null)
                {
                    RegisteredUser userDB = new RegisteredUser
                    {
                        UserId = userRegistration.UserId.ToString(),
                        FirstName = userRegistration.UserFirstName.ToString().ToLower(),
                        LastName = userRegistration.UserLastName.ToString().ToLower(),
                        UserName = userRegistration.UserName.ToLower(),
                        Email = userRegistration.Email.ToLower(),
                        Password = userRegistration.Password,
                        Role = userRegistration.Role.ToString().ToLower()
                    };
                    await Db.RegisteredUsers.AddAsync(userDB);
                    await Db.SaveChangesAsync();
                    return "User Created Successfully";
                }
                else
                {
                    return "User already exist with email " + userRegistration.Email;
                }

            }

            return "Something went wrong try again";
        }

        public Task<string> DeleteUser(string email)
        {
            throw new NotImplementedException();
        }

        public async Task<RegisteredUser> GetUser(string email)
        {
            if (Db != null)
            {
                RegisteredUser user = await Db.RegisteredUsers.FirstOrDefaultAsync(u => u.Email == email);
                return user;
            }

            return null;
        }


        public Task<string> UpdateUser(UserRegistration userRegistration)
        {
            throw new NotImplementedException();
        }
    }
}
