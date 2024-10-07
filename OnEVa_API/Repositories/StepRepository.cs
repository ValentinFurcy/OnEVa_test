using Entities;
using IRepositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories
{
    public class StepRepository : IStepRepository
    {
        public Task<Step> CreateStepAsync(Step step)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeleteStepAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<List<Step>> GetAllStepsAsync()
        {
            throw new NotImplementedException();
        }

        public Task<Step> GetStepByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<Step> UpdateStepAsync(Step step)
        {
            throw new NotImplementedException();
        }
    }
}
