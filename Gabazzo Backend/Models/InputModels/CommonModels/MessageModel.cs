using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Gabazzo_Backend.Models.InputModels.CommonModels
{
    public class MessageModel
    {
        [Required(ErrorMessage = "SenderID is required.")]
        public string SenderID { get; set; }

        [Required(ErrorMessage = "ReceiverID is required.")]
        public string ReceiverID { get; set; }

        [Required(ErrorMessage = "Message is required.")]
        public string MessageBody { get; set; }

    }
}
