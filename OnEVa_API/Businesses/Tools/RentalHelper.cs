using DTOs.RentalDTO;
using IRepositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Businesses.Tools
{
    public class RentalHelper
    {
        private readonly IRentalRepository _rentalRepository;

        public RentalHelper(IRentalRepository rentalRepository)
        {
            _rentalRepository = rentalRepository;
        }

        //public async Task<bool> RentalVerificationIsPossible(RentalCreateInputDTO rentalCreateInputDTO, int personId)
        //{
        //    var rentals = await _rentalRepository.GetRentalForControl(rentalCreateInputDTO.StartDate, rentalCreateInputDTO.EndDate, personId);

        //    if (rentals.Any())
        //    {
        //        foreach (var rental in rentals)
        //        {
        //            if (rental) { } ;
        //        }
        //    }
        //    else return true;
        //}
    }
}
