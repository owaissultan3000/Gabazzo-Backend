using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Gabazzo_Backend.Models.InputModels.ContractorsModels
{
    public class ContractorRegistration
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public Guid ContractorId { get; set; }
        [Required(ErrorMessage = "First Name Required!")]
        [MaxLength(70, ErrorMessage = "Too Long Name")]
        public string ContractorFirstName { get; set; }

        [MaxLength(70, ErrorMessage = "Too Long Name")]
        public string ContractorLastName { get; set; }

        [Required(ErrorMessage = "User Name is Required!")]
        [MaxLength(70, ErrorMessage = "Too Long Name")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "Email is required.")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Password is required")]
        [StringLength(255, ErrorMessage = "Too Short Password", MinimumLength = 8)]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required(ErrorMessage = "PhoneNumber is required")]
        public string PhoneNumber { get; set; }

        [Required(ErrorMessage = "CompanyAddress is required")]
        public string CompanyAddress { get; set; }
        public string Role { get; set; }
    }
}
