using System;
using System.Collections.Generic;

#nullable disable

namespace Gabazzo_Backend.Models.DbModels
{
    public partial class Message
    {
        public int Id { get; set; }
        public string ConversationId { get; set; }
        public string Texts { get; set; }
        public string SenderId { get; set; }
        public string ReceiverId { get; set; }

        public virtual Conversation Conversation { get; set; }
    }
}
