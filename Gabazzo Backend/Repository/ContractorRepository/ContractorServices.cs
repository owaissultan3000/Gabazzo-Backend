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
        GabazzoDBContext Db;
        public ContractorServices(GabazzoDBContext _Db)
        {
            Db = _Db;
        }
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

        public async Task<RegisteredContractor> GetContractor(string email)
        {
            if (Db != null)
            {
                RegisteredContractor user = await Db.RegisteredContractors.FirstOrDefaultAsync(u => u.Email == email);
                return user;
            }

            return null;
        }
    }
}
