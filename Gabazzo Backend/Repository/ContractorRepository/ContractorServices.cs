using Gabazzo_Backend.Models.DbModels;
using Gabazzo_Backend.Models.InputModels.ContractorsModels;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Gabazzo_Backend.Repository.ContractorRepository
{
    public class ContractorServices : IContractorServices
    {
        gabazzodbContext Db;
        public ContractorServices(gabazzodbContext _Db)
        {
            Db = _Db;
        }

        public ContractorPortfolio ContractorService { get; private set; }

        public async Task<string> CreateContractor(ContractorRegistration contractorRegistration)
        {
            if (Db != null)
            {
                var user = await Db.RegisteredContractors.FirstOrDefaultAsync(u => u.Email == contractorRegistration.Email);
                if (user == null)
                {
                    RegisteredContractor registeredContractor = new RegisteredContractor
                    {
                        ContractorId = Guid.NewGuid().ToString(),
                        FirstName = contractorRegistration.ContractorFirstName,
                        LastName = contractorRegistration.ContractorLastName,
                        UserName = contractorRegistration.UserName,
                        Email = contractorRegistration.Email,
                        PhoneNumber = contractorRegistration.PhoneNumber,
                        CompanyAddress = contractorRegistration.CompanyAddress,
                        CompanyName = contractorRegistration.CompanyName,
                        Description = contractorRegistration.Description,
                        Logo = contractorRegistration.Logo,
                        Password = contractorRegistration.Password,
                        Role = contractorRegistration.Role

                    };
                    var options = new JsonSerializerOptions()
                    {
                        WriteIndented = true
                    };
                    var json = JsonSerializer.Serialize(registeredContractor, new JsonSerializerOptions()
                    {
                        WriteIndented = true,
                        ReferenceHandler = ReferenceHandler.Preserve
                    });
                    await Db.RegisteredContractors.AddAsync(registeredContractor);
                    await Db.SaveChangesAsync();
                    return "Contractor Created Successfully";
                }
                else
                {
                    return "Contractor already exist with email " + contractorRegistration.Email;
                }

            }

            return "Something went wrong try again";
        }
        public string GetConversationId(string SenderId, string ReceiverId)
        {
            if (Db != null)
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

        public async Task<string> CreateOffer(Offer offer)
        {
            if (Db != null)
            {
                string conversationId = GetConversationId(offer.Sender, offer.Receiver);
                    if (conversationId == null)
                {
                    string conversationid = Guid.NewGuid().ToString();
                    Conversation conversation = new Conversation
                    {
                        ConversationId = conversationid,
                        Sender = offer.Sender,
                        Receiver = offer.Receiver
                    };

                    Message message = new Message
                    {
                        ConversationId = conversationid,
                        Texts = "",
                        SenderId = offer.Sender,
                        ReceiverId = offer.Receiver
                    };

                    await Db.Conversations.AddAsync(conversation);
                    
                    await Db.Messages.AddAsync(message);
                    
                    await Db.Offers.AddAsync(offer);

                    await Db.SaveChangesAsync();

                    return "Offer Successfully Created";

                }
                
            }
            return null;
        }

        public async Task<string> CreatePortolio(Portfolio portfolio)
        {
            if (Db != null)
            {
                var temp = await Db.RegisteredContractors.FirstOrDefaultAsync(u => u.ContractorId == portfolio.ContractorId.ToString());
                if (temp != null)
                {
                    portfolio.PortfolioId = Guid.NewGuid();

                    ContractorPortfolio contractorPortfolio = new ContractorPortfolio

                    {
                        PortfolioId = portfolio.PortfolioId.ToString(),
                        ContractorId = portfolio.ContractorId,
                        Title = portfolio.Title,
                        Category = portfolio.Category,
                        Service = portfolio.Service,
                        Description = portfolio.Description,
                        Budget = portfolio.Budget,
                        Picture = portfolio.Picture

                    };
                    
                    await Db.ContractorPortfolios.AddAsync(contractorPortfolio);
                    await Db.SaveChangesAsync();
                    return "Portfolio Created Successfully";
                }
                else return "Unauthorized Contractor";


            }
            return "Unable to create portfolio try again";
        }

        public async Task<string> CreateService(Service service)
        {
            if (Db != null)
            {
                var temp = await Db.RegisteredContractors.FirstOrDefaultAsync(u => u.ContractorId == service.ContractorId.ToString());
                if (temp != null)
                {
                    service.ServicesId = Guid.NewGuid();

                    ContractorService contractorService  = new ContractorService

                    {
                        ServicesId = service.ServicesId.ToString(),
                        ContractorId = service.ContractorId,
                        Title = service.Title,
                        Category = service.Category,
                        Service = service.Services,
                        PriceFrom = service.PriceFrom,
                        PriceTo = service.PriceTo,
                        EstimatedTime = service.EstimatedTime,
                        Description = service.Description

                    };

                   await Db.ContractorServices.AddAsync(contractorService);
                    await Db.SaveChangesAsync();
                    return "Service Created Successfully";
                }
                else return "Unauthorized Contractor";


            }
            return "Unable to create service try again";
        }

        public async Task<string> DeletePortfolio(string id)
        {
            if (Db != null)
            {
                
                ContractorPortfolio contractorPortfolio = await Db.ContractorPortfolios.FirstOrDefaultAsync(u => u.PortfolioId == id);

                if (contractorPortfolio != null)
                {
                    
                    Db.ContractorPortfolios.Remove(contractorPortfolio);

                    
                     await Db.SaveChangesAsync();
                }
                return  "Portfolio Deleted Successfully";
            }

            return "Unable to delete portfolio try again";
        }

        public async Task<string> DeleteService(string id)
        {
            if (Db != null)
            {

                ContractorService contractorService = await Db.ContractorServices.FirstOrDefaultAsync(u => u.ServicesId == id);

                if (contractorService != null)
                {

                    Db.ContractorServices.Remove(contractorService);


                    await Db.SaveChangesAsync();
                }
                return "Service Deleted Successfully";
            }

            return "Unable to delete service try again";
        }

        public async Task<RegisteredContractor> GetCompanyById(string Id)
        {
            if(Db != null)
            {
                return await Db.RegisteredContractors.FirstOrDefaultAsync(u => u.ContractorId == Id);
            }
            return null;
        }

        public async Task<RegisteredContractor> GetContractor(string email)
        {
            if (Db != null)
            {
                RegisteredContractor user = await Db.RegisteredContractors.FirstOrDefaultAsync(u => u.Email == email);
                return user;
            }

            return null;
        }

        public async Task<List<ContractorPortfolio>> GetPortfolioById(string Id)
        {
            if (Db != null)
            {
                List<ContractorPortfolio> contractorPortfolios = new List<ContractorPortfolio>();
                foreach(ContractorPortfolio contractorPortfolio in Db.ContractorPortfolios)
                {
                    if(contractorPortfolio.ContractorId == Id)
                    {
                        contractorPortfolios.Add(contractorPortfolio);
                    }
                }
                return contractorPortfolios;
            }
            return null;
        }

        public async Task<List<ContractorService>> GetServiceByContractorId(string Id)
        {
            if(Db != null)
            {
                List<ContractorService> contractorServices = new List<ContractorService>();
                foreach (ContractorService service in Db.ContractorServices)
                {
                    if(service.ContractorId == Id)
                    {
                        contractorServices.Add(service);
                    }
                }
                return contractorServices;
            }
            return null;
        }

        public async Task<ContractorService> GetServiceByServiceId(string Id)
        {
            if (Db != null)
            {
               return await Db.ContractorServices.FirstOrDefaultAsync(u => u.ServicesId == Id);
            }
            return null;
        }
    }
}
