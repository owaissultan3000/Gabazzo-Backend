using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Gabazzo_Backend.Models.InputModels.UserModels
{
    public class UserRegistration
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public Guid UserId { get; set; }

        [Required(ErrorMessage = "First Name Required!")]
        [MaxLength(70, ErrorMessage = "Too Long Name")]
        public string UserFirstName { get; set; }

        [MaxLength(70, ErrorMessage = "Too Long Name")]
        public string UserLastName { get; set; }

        [Required(ErrorMessage = "User Name Required!")]
        [MaxLength(70, ErrorMessage = "Too Long Name")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "Email is required.")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Password is required")]
        [StringLength(255, ErrorMessage = "Too Short Password", MinimumLength = 8)]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required(ErrorMessage = "Confirm Password is required")]
        [StringLength(255, ErrorMessage = "Too Short Password", MinimumLength = 8)]
        [DataType(DataType.Password)]
        [Compare("Password")]
        public string ConfirmPassword { get; set; }

        public string Role { get; set; }
    }
}
