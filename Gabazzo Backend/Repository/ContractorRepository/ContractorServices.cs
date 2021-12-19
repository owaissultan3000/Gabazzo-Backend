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

        public async Task<List<RegisteredContractor>> GetCompanies()
        {
            if(Db != null)
            {
                return await Db.RegisteredContractors.ToListAsync();

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

        public async Task<List<ContractorService>> GetService()
        {
            if(Db != null)
            {
              return await Db.ContractorServices.ToListAsync();
            }
            return null;
        }
    }
}
