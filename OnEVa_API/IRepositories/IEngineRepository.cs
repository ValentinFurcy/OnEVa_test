using DTOs.VehiclePropertiesDTOs.Output;
using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IRepositories
{
    public interface IEngineRepository
    {
        public Task<EngineOutputDTO> CreateEngineAsync(Engine engine);
        public Task<List<EngineOutputDTO>> GetAllEnginesAsync();
        public Task<EngineOutputDTO> GetEngineByIdAsync(int id);
		public Task<EngineOutputDTO> GetEngineByLabelAsync(string label);
		public Task<EngineOutputDTO> UpdateEngineAsync(Engine engine);
        public Task<bool> DeleteEngineAsync(int id);
    }
}
