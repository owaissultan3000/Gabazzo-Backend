using Gabazzo_Backend.Models.DbModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Gabazzo_Backend.Repository.CommonRepository
{
    public interface ICommonService
    {
        public Task<List<ContractorService>> GetService();
        public Task<List<RegisteredContractor>> GetCompanies();
        public Task<List<ContractorService>> SearchService(string query);
        public Task<List<RegisteredContractor>> SearchCompany(string query);
    }
}
