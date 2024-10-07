using DTOs.VehicleDTOs;
using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IRepositories
{
    public interface IVehicleRepository
    {
        public Task<VehicleOutputDTO> CreateVehicleAsync(Vehicle vehicle);
        public Task<List<VehicleOutputDTO>> GetAllVehiclesAsync();
        public Task<List<VehicleOutputDTO>> GetAllVehiclesByBrandAsync(string brand);
        public Task<VehicleOutputDTO> GetVehicleByRegistrationAsync(string registration);
        public Task<List<VehicleOutputDTO>> GetVehicleByRegistrationAutoCompleteAsync(string registration);
        public Task<List<VehicleOutputDTO>> GetAllVehiclesAvailableByDatesAsync(DateTime startDate, DateTime endDate);
        public Task<VehicleOutputDTO> GetVehicleByIdAsync(int id);
        public Task<Vehicle> GetVehicleByIdToolsAsync(int id);
        public Task<VehicleOutputDTO> UpdateVehicleAsync(Vehicle vehicle);
        public Task<bool> DeleteVehicleAsync(int id);
        public Task<bool> DesableVehicleAsync(int idVehcile);
    }
}
