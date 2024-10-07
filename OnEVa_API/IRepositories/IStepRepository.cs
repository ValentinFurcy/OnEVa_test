using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IRepositories
{
    public interface IStepRepository
    {
        public Task<Step> CreateStepAsync(Step step);
        public Task<List<Step>> GetAllStepsAsync();
        public Task<Step> GetStepByIdAsync(int id);
        public Task<Step> UpdateStepAsync(Step step);
        public Task<bool> DeleteStepAsync(int id);
    }
}
