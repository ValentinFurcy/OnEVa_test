using DTOs.RentalDTO;
using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IBusinesses
{
    public interface IRentalBusiness
    {
        public Task<RentalOutputDTO> CreateRentalAsync(RentalCreateInputDTO rentalCreateInputDTO, string userId);
        
        /// <summary>
        /// Get all the existing rentals.
        /// </summary>
        /// <returns>The list of rentals as a list of RentalOutputDTO.</returns>
        public Task<List<RentalOutputDTO>> GetAllRentalsAsync();

        public Task<List<RentalOutputDTO>> GetAllRentalsByDatesAsync(DateTime startDate, DateTime? endDate);
        public Task<List<RentalOutputDTO>> GetAllRentalsByStateAsync(int stateId);

        /// <summary>
        /// Get all the existing rentals by personId.
        /// </summary>
        /// <param name="personId"></param>
        /// <returns>The list of rentals as a list of RentalOutputDTO.</returns>
        public Task<List<RentalOutputDTO>> GetAllRentalsByPersonAsync(int personId);

        public Task<List<RentalOutputDTO>> GetAllRentalsByPersonAndByOnGoingStateAsync(int personId);
        public Task<List<RentalOutputDTO>> GetRentalByPersonandByStateOrderedByDate(int personId, int stateId);
        public Task<List<RentalOutputDTO>> GetAllRentalsByRegistrationAsync(string registration);

        /// <summary>
        /// Get the existing rental by id.
        /// </summary>
        /// <param name="id"></param>
        /// <returns>The rental as a RentalOutputDTO.</returns>
        public Task<RentalOutputDTO> GetRentalByIdAsync(int id);
        public Task<RentalOutputDTO> UpdateRentalAsync(RentalUpdateInputDTO rentalUpdateInputDTO, string UserId);
        public Task<bool> DeleteRentalAsync(int id, string userId);

    }
}
