using DTOs.StateDTOs;
using DTOs.VehiclePropertiesDTOs.Output;
using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IRepositories
{
    public interface IStateRepository
    {
        public Task<StateOutputDTO> CreateStateAsync(State state);
        public Task<List<StateOutputDTO>> GetAllStatesAsync();
        public Task<StateOutputDTO> GetStateByIdAsync(int id);
		public Task<StateOutputDTO> GetStateByLabelAsync(string label);
		public Task<StateOutputDTO> UpdateStateAsync(State state);
        public Task<bool> DeleteStateAsync(int id);
    }
}
