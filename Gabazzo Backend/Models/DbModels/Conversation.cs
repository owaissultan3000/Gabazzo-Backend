using System;
using System.Collections.Generic;

#nullable disable

namespace Gabazzo_Backend.Models.DbModels
{
    public partial class Conversation
    {
        public Conversation()
        {
            Messages = new HashSet<Message>();
            Offers = new HashSet<Offer>();
        }

        public string ConversationId { get; set; }
        public string Sender { get; set; }
        public string Receiver { get; set; }

        public virtual RegisteredUser ReceiverNavigation { get; set; }
        public virtual RegisteredContractor SenderNavigation { get; set; }
        public virtual ICollection<Message> Messages { get; set; }
        public virtual ICollection<Offer> Offers { get; set; }
    }
}
