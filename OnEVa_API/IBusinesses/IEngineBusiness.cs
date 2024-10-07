using DTOs.VehiclePropertiesDTOs.CreateInput;
using DTOs.VehiclePropertiesDTOs.Output;
using DTOs.VehiclePropertiesDTOs.UpdateInput;
using Entities;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IBusinesses
{
    public interface IEngineBusiness
    {
        public Task<EngineOutputDTO> CreateEngineAsync(EngineCreateInputDTO engineCreateInputDTO);
		public Task<List<EngineOutputDTO>> GetAllEnginesAsync();
        public Task<EngineOutputDTO> GetEngineByIdAsync(int id);
        public Task<EngineOutputDTO> GetEngineByLabelAsync(string label);
        public Task<EngineOutputDTO> UpdateEngineAsync(EngineUpdateInputDTO engineUpdateInputDTO);
        public Task<bool> DeleteEngineAsync(int Id);
    }
}
