using System;
using System.Collections.Generic;

#nullable disable

namespace Gabazzo_Backend.Models.DbModels
{
    public partial class Offer
    {
        public string OfferId { get; set; }
        public string ConversationId { get; set; }
        public string Sender { get; set; }
        public string Receiver { get; set; }
        public string Service { get; set; }
        public string Type { get; set; }
        public int Discount { get; set; }
        public int SubTotal { get; set; }
        public int TotalCost { get; set; }
        public string Notes { get; set; }
        public int? Revision { get; set; }
        public int? TotalDuration { get; set; }
        public string Status { get; set; }

        public virtual Conversation Conversation { get; set; }
        public virtual RegisteredUser ReceiverNavigation { get; set; }
        public virtual RegisteredContractor SenderNavigation { get; set; }
        public virtual ContractorService ServiceNavigation { get; set; }
    }
}
