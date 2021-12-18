using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Gabazzo_Backend.Models.InputModels.ContractorsModels
{
    public class Service
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public Guid ServicesId { get; set; }

        [Required(ErrorMessage = "ContractorId is Required!")]
        public string ContractorId { get; set; }

        [Required(ErrorMessage = "Title is Required!")]
        public string Title { get; set; }

        [Required(ErrorMessage = "Category is Required!")]
        public string Category { get; set; }

        [Required(ErrorMessage = "Services is Required!")]
        public string Services { get; set; }

        [Required(ErrorMessage = "PriceFrom is Required!")]
        public string PriceFrom { get; set; }

        [Required(ErrorMessage = "PriceTo is Required!")]
        public string PriceTo { get; set; }

        [Required(ErrorMessage = "EstimatedTime is Required!")]
        public string EstimatedTime { get; set; }
        public string Description { get; set; }
    }
}
