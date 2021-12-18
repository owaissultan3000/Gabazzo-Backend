using System;
using System.Collections.Generic;

#nullable disable

namespace Gabazzo_Backend.Models.DbModels
{
    public partial class ContractorPortfolio
    {
        public string PortfolioId { get; set; }
        public string ContractorId { get; set; }
        public string Title { get; set; }
        public string Category { get; set; }
        public string Service { get; set; }
        public string Description { get; set; }
        public string Budget { get; set; }
        public string Picture { get; set; }

        public virtual RegisteredContractor Contractor { get; set; }
    }
}
