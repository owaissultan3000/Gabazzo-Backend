using Gabazzo_Backend.Models.DbModels;
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
                    if ((conversation.MemberId == SenderId && conversation.ContractorId == ReceiverId) || (conversation.MemberId == ReceiverId && conversation.ContractorId == SenderId))
                    {
                        return conversation.ContractorId;
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
    }
}
