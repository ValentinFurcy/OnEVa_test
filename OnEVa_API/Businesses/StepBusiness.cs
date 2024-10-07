using Entities;
using IBusinesses;
using IRepositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Businesses
{
    public class StepBusiness : IStepBusiness
    {
        private readonly IStepRepository _stepRepository;
        public StepBusiness(IStepRepository stepRepository)
        {
            _stepRepository = stepRepository;
        }

        public async Task<Step> CreateStepAsync(Step step)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> DeleteStepAsync(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<List<Step>> GetAllStepsAsync()
        {
            throw new NotImplementedException();
        }

        public async Task<Step> GetStepByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<Step> UpdateStepAsync(Step step)
        {
            throw new NotImplementedException();
        }
    }
}
