using System;
using System.Collections.Generic;

#nullable disable

namespace Gabazzo_Backend.Models.DbModels
{
    public partial class RegisteredContractor
    {
        public RegisteredContractor()
        {
            ContractorPortfolios = new HashSet<ContractorPortfolio>();
            ContractorServices = new HashSet<ContractorService>();
            Conversations = new HashSet<Conversation>();
            Offers = new HashSet<Offer>();
        }

        public string ContractorId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string Password { get; set; }
        public string CompanyAddress { get; set; }
        public string CompanyName { get; set; }
        public string Logo { get; set; }
        public string Description { get; set; }
        public string Role { get; set; }

        public virtual ICollection<ContractorPortfolio> ContractorPortfolios { get; set; }
        public virtual ICollection<ContractorService> ContractorServices { get; set; }
        public virtual ICollection<Conversation> Conversations { get; set; }
        public virtual ICollection<Offer> Offers { get; set; }
    }
}
