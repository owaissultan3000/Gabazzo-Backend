using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Gabazzo_Backend.Models.InputModels.ContractorsModels
{
    public class Portfolio
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public Guid PortfolioId { get; set; }
        public string ContractorId { get; set; }

        [Required(ErrorMessage = "Title is Required!")]
        public string Title { get; set; }

        [Required(ErrorMessage = "Category is Required!")]
        public string Category { get; set; }

        [Required(ErrorMessage = "Service is Required!")]
        public string Service { get; set; }
        public string Description { get; set; }

        [Required(ErrorMessage = "Budget is Required!")]
        public string Budget { get; set; }
        public string Picture { get; set; }

    }
}
