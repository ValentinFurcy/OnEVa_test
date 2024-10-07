using DTOs.CarpoolDTOs;
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
    public class CarpoolBusiness : ICarpoolBusiness
    {
        private readonly ICarpoolRepository _carpoolRepository;
        public CarpoolBusiness(ICarpoolRepository carpoolRepository) 
        {
            _carpoolRepository = carpoolRepository;
        }

        public async Task<CarpoolOutputDTO> CreateCarpoolAsync(CarpoolCreateInputDTO carpoolCreateInputDTO)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> DeleteCarpoolAsync(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> DeleteCarpoolAttendeeAsync(int carpoolId, int personId)
        {
            throw new NotImplementedException();
        }

        public async Task<List<CarpoolOutputDTO>> GetAllCarpoolsAsync()
        {
            throw new NotImplementedException();
        }

        public async Task<List<CarpoolOutputDTO>> GetAllCarpoolsByAddressAndByDatesAsync(Address startAddress, Address endAddress, DateTime startDate, DateTime? endDate)
        {
            throw new NotImplementedException();
        }

        public async Task<List<CarpoolOutputDTO>> GetAllCarpoolsByDatesAsync(DateTime startDate, DateTime? endDate)
        {
            throw new NotImplementedException();
        }

        public async Task<List<CarpoolOutputDTO>> GetAllCarpoolsByPersonAsync(int personId)
        {
            throw new NotImplementedException();
        }

        public async Task<List<CarpoolOutputDTO>> GetAllCarpoolsByStateAsync(int stateId)
        {
            throw new NotImplementedException();
        }

        public async Task<CarpoolOutputDTO> GetCarpoolByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<CarpoolOutputDTO> UpdateCarpoolAsync(CarpoolUpdateInputDTO carpoolUpdateInputDTO)
        {
            throw new NotImplementedException();
        }

        public async Task<CarpoolOutputDTO> UpdateCarpoolAttendeeAsync(int carpoolId, Person person)
        {
            throw new NotImplementedException();
        }
    }
}
