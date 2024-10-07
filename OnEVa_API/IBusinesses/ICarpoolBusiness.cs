using DTOs.CarpoolDTOs;
using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IBusinesses
{
    public interface ICarpoolBusiness
    {
        public Task<CarpoolOutputDTO> CreateCarpoolAsync(CarpoolCreateInputDTO carpoolCreateInputDTO);
        public Task<List<CarpoolOutputDTO>> GetAllCarpoolsAsync();
        public Task<List<CarpoolOutputDTO>> GetAllCarpoolsByDatesAsync(DateTime startDate, DateTime? endDate);
        public Task<List<CarpoolOutputDTO>> GetAllCarpoolsByAddressAndByDatesAsync(Address startAddress, Address endAddress, DateTime startDate, DateTime? endDate);
        public Task<List<CarpoolOutputDTO>> GetAllCarpoolsByStateAsync(int stateId);
        public Task<List<CarpoolOutputDTO>> GetAllCarpoolsByPersonAsync(int personId);
        public Task<CarpoolOutputDTO> GetCarpoolByIdAsync(int id);
        public Task<CarpoolOutputDTO> UpdateCarpoolAsync(CarpoolUpdateInputDTO carpoolUpdateInputDTO);
        public Task<CarpoolOutputDTO> UpdateCarpoolAttendeeAsync(int carpoolId, Person person);
        public Task<bool> DeleteCarpoolAsync(int id);
        public Task<bool> DeleteCarpoolAttendeeAsync(int carpoolId, int personId);
    }
}
