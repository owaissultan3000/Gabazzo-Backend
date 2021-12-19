using System;
using System.Collections.Generic;

#nullable disable

namespace Gabazzo_Backend.Models.DbModels
{
    public partial class Conversation
    {
        public string ConversationId { get; set; }
        public string ContractorId { get; set; }
        public string MemberId { get; set; }

        public virtual RegisteredContractor Contractor { get; set; }
        public virtual RegisteredUser Member { get; set; }
    }
}
