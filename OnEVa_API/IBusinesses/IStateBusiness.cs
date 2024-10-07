using DTOs.StateDTOs;
using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IBusinesses
{
    public interface IStateBusiness
    {
        public Task<StateOutputDTO> CreateStateAsync(StateCreateInputDTO stateInputDTO);
        public Task<List<StateOutputDTO>> GetAllStatesAsync();
        public Task<StateOutputDTO> GetStateByIdAsync(int id);
        public Task<StateOutputDTO> UpdateStateAsync(StateUpdateInputDTO stateInputDTO);
        public Task<bool> DeleteStateAsync(int id);
    }
}
