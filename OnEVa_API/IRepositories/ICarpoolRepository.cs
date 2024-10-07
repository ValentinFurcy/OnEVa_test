using DTOs.CarpoolDTOs;
using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IRepositories
{
    public interface ICarpoolRepository
    {
        public Task<CarpoolOutputDTO> CreateCarpoolAsync(Carpool carpool);
        public Task<List<CarpoolOutputDTO>> GetAllCarpoolsAsync();
        public Task<List<CarpoolOutputDTO>> GetAllCarpoolsByDatesAsync(DateTime startDate, DateTime? endDate);
        public Task<List<CarpoolOutputDTO>> GetAllCarpoolsByAddressAndByDatesAsync(Address startAddress, Address endAddress, DateTime? startDate, DateTime? endDate);
        public Task<List<CarpoolOutputDTO>> GetAllCarpoolsByStateAsync(int stateId);
        public Task<List<CarpoolOutputDTO>> GetAllCarpoolsByPersonAsync(int personId);
        public Task<CarpoolOutputDTO> GetCarpoolByIdAsync(int carpoolId);
        public Task<Carpool> GetCarpoolByIdToolsAsync(int carpoolId);
        public Task<CarpoolOutputDTO> UpdateCarpoolAsync(Carpool carpool);
        //public Task<CarpoolOutputDTO> UpdateCarpoolAddAttendeeAsync(int carpoolId, List<Person> person);
        public Task<CarpoolOutputDTO> UpdateCarpoolAddAttendeeAsync(Carpool carpool);
        public Task<bool> DeleteCarpoolAsync(int carpoolId);
        public Task<bool> DeleteCarpoolAttendeeAsync(int carpoolId, int personId);
        public Task<List<Carpool>> GetCarpoolForMethodExistingAsync(CarpoolCreateInputDTO carpoolCreateInputDTO, int organizerId);

    }
}
