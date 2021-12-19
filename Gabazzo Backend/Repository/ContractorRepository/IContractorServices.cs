using Gabazzo_Backend.Models.DbModels;
using Gabazzo_Backend.Models.InputModels.ContractorsModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Gabazzo_Backend.Repository.ContractorRepository
{
    public interface IContractorServices
    {
        public Task<RegisteredContractor> GetContractor(string email);
        public Task<string> CreateContractor(ContractorRegistration contractorRegistration);
        public Task<string> CreatePortolio(Portfolio portfolio);
        public Task<string> CreateService(Service service);
        


    }
}
