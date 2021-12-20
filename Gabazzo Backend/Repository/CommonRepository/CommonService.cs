using Gabazzo_Backend.Models.DbModels;
using Gabazzo_Backend.Models.InputModels.CommonModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Gabazzo_Backend.Repository.CommonRepository
{
    public class CommonService : ICommonService
    {
        gabazzodbContext Db;
        public CommonService(gabazzodbContext _Db)
        {
            Db = _Db;
        }
        public async Task<List<RegisteredContractor>> GetCompanies()
        {
            if (Db != null)
            {
                return await Db.RegisteredContractors.ToListAsync();

            }
            return null;
        }

        public string GetConversationId(string SenderId,string ReceiverId)
        {
            if(Db != null)
            {
                foreach (Conversation conversation in Db.Conversations)
                {
                    if ((conversation.Sender == SenderId && conversation.Receiver == ReceiverId) || (conversation.Sender == ReceiverId && conversation.Receiver == SenderId))
                    {
                        return conversation.ConversationId;
                    }
                }
                return null;
            }
            return null;
        }

        public async Task<List<Message>> GetMessages(string SenderId, string ReceiverId)
        {
            if(Db != null)
            {
                List<Message> messages = new List<Message>();
                string ConversationID = GetConversationId(SenderId, ReceiverId);

                foreach(Message message in Db.Messages)
                {
                    if(message.ConversationId == ConversationID)
                    {
                        messages.Add(message);
                    }
                }
                return messages;

            }
            return null;
        }

        public async Task<List<ContractorService>> GetService()
        {
            if (Db != null)
            {
                return await Db.ContractorServices.ToListAsync();
            }
            return null;
        }

        public async Task<List<RegisteredContractor>> SearchCompany(string query)
        {
            if (Db != null)
            {
                List<RegisteredContractor> SearchResult = new List<RegisteredContractor>();

                foreach (RegisteredContractor registeredContractor in Db.RegisteredContractors)
                {
                    if (registeredContractor.CompanyName.Contains(query) || registeredContractor.Description.Contains(query))
                    {
                        SearchResult.Add(registeredContractor);
                    }
                }
                return SearchResult;
            }
            return null;
        }

        public async Task<List<ContractorService>> SearchService(string query)
        {
            if(Db != null)
            {
                List<ContractorService> contractorServices = new List<ContractorService>();
                foreach (ContractorService contractorService in Db.ContractorServices)
                {
                    if(contractorService.Category.Contains(query) || contractorService.Description.Contains(query))
                    {
                        contractorServices.Add(contractorService);
                    }
                }
                return contractorServices;
            }
            return null;
            
        }

        public async Task<string> SendMessage(MessageModel messageModel)
        {
            if (Db != null)
            {
                string ConversationID = GetConversationId(messageModel.SenderID, messageModel.ReceiverID);
                if (ConversationID == null)
                {
                    string conversationid = Guid.NewGuid().ToString();
                    Conversation conversation = new Conversation
                    {
                        ConversationId = conversationid,
                        Sender = messageModel.SenderID,
                        Receiver = messageModel.ReceiverID
                    };

                    Message message = new Message
                    {
                        ConversationId = conversation.ConversationId,
                        Texts = messageModel.MessageBody,
                        SenderId = messageModel.SenderID,
                        ReceiverId = messageModel.ReceiverID,
                        
                    };
                    await Db.Conversations.AddAsync(conversation);
                    await Db.SaveChangesAsync();
                    await Db.Messages.AddAsync(message);
                    await Db.SaveChangesAsync();
                    
                    
                    return "Message Sent";
                }
                else
                {
                    Message message = new Message
                    {
                        ConversationId = ConversationID,
                        SenderId = messageModel.SenderID,
                        ReceiverId = messageModel.ReceiverID,
                        Texts = messageModel.MessageBody
                    };

                    await Db.Messages.AddAsync(message);
                    await Db.SaveChangesAsync();
                    return "Message Sent";

                }
                
            }
            return null;
        }    
        
    }
}
