using System;
using System.Collections.Generic;

#nullable disable

namespace Gabazzo_Backend.Models.DbModels
{
    public partial class ContractorService
    {
        public string ServicesId { get; set; }
        public string ContractorId { get; set; }
        public string Title { get; set; }
        public string Category { get; set; }
        public string Service { get; set; }
        public string PriceFrom { get; set; }
        public string PriceTo { get; set; }
        public string EstimatedTime { get; set; }
        public string Description { get; set; }

        public virtual RegisteredContractor Contractor { get; set; }
    }
}
