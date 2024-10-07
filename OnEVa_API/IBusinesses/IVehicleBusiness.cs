using DTOs.VehicleDTOs;
using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IBusinesses
{
    public interface IVehicleBusiness
    {
        public Task<VehicleOutputDTO> CreateVehicleAsync(VehicleCreateInputDTO vehicleCreateInputDTO);
        public Task<List<VehicleOutputDTO>> GetAllVehiclesAsync();
        public Task<List<VehicleOutputDTO>> GetAllVehiclesByBrandAsync(string brand);
        public Task<VehicleOutputDTO> GetVehicleByRegistrationAsync(string registration);
        public Task<List<VehicleOutputDTO>> GetVehicleByRegistrationAutoCompleteAsync(string registration);
        public Task<List<VehicleOutputDTO>> GetAllVehiclesAvailableByDatesAsync(DateTime startDate, DateTime endDate);
        public Task<VehicleOutputDTO> GetVehicleByIdAsync(int id);
        public Task<VehicleOutputDTO> UpdateVehicleAsync(VehicleUpdateInputDTO vehicleUpdateInputDTO);
        public Task<bool> DeleteVehicleAsync(int id);
        public Task<bool> DesableVehicle(int idVehcile);
    }
}
