using System;
using System.Collections.Generic;

#nullable disable

namespace Gabazzo_Backend.Models.DbModels
{
    public partial class RegisteredUser
    {
        public string UserId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Role { get; set; }
    }
}
