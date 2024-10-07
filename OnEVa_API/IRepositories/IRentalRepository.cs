using DTOs.RentalDTO;
using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IRepositories
{
    public interface IRentalRepository
    {
        public Task<RentalOutputDTO> CreateRentalAsync(Rental rental);

        /// <summary>
        /// Get all the existing rentals.
        /// </summary>
        /// <returns>The list of rentals as a list of RentalOutputDTO.</returns>
        public Task<List<RentalOutputDTO>> GetAllRentalsAsync();
        public Task<List<RentalOutputDTO>> GetAllRentalsByDatesAsync(DateTime startDate, DateTime? endDate);
		public Task<List<RentalOutputDTO>> GetAllRentalsByStateAsync(int stateId);
		public Task<List<RentalOutputDTO>> GetAllRentalsByPersonAsync(int personId);
        Task<List<RentalOutputDTO>> GetAllRentalsByPersonAndByOnGoingStateAsync(int personId);
        public Task<List<RentalOutputDTO>> GetRentalByPersonAndByStateSortedByDates(int personId, int stateId);
        public Task<List<RentalOutputDTO>> GetAllRentalsByRegistrationAsync(string registration);
		public Task<RentalOutputDTO> GetRentalByIdAsync(int id);
		public Task<RentalOutputDTO> UpdateRentalAsync(Rental rental);
        public Task<bool> DeleteRentalAsync(int id);
        public Task<List<Rental>> GetRentalForControl(DateTime startDate, DateTime endDate, int? personId, int? vehicleId);
    }
}
