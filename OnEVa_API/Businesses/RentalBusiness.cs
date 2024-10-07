using AutoMapper;
using DTOs.RentalDTO;
using DTOs.VehiclePropertiesDTOs.CreateInput;
using DTOs.VehiclePropertiesDTOs.UpdateInput;
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
    public class RentalBusiness : IRentalBusiness
    {
        private readonly IRentalRepository _rentalRepository;
        private readonly IPersonRepository _personRepository;
        private readonly IMapper _mapper;
        public RentalBusiness(IRentalRepository rentalRepository, IPersonRepository personRepository, IMapper mapper)
        {
            _rentalRepository = rentalRepository;
            _personRepository = personRepository;
            _mapper = mapper;
        }

        public async Task<RentalOutputDTO> CreateRentalAsync(RentalCreateInputDTO rentalCreateInputDTO, string userId)
        {
            var person = await _personRepository.GetPersonByUserIdAsync(userId);
            var rentalExisting = await _rentalRepository.GetRentalForControl(rentalCreateInputDTO.StartDate, rentalCreateInputDTO.EndDate, person.Id, rentalCreateInputDTO.VehicleId);

            if (rentalExisting == null)
            {
                if (rentalCreateInputDTO.AddressId <= 0 || rentalCreateInputDTO.StartDate == null || rentalCreateInputDTO.EndDate == null || rentalCreateInputDTO.VehicleId <= 0)
                {
                    throw new Exception("Fields required is empty");
                }
                else if (rentalCreateInputDTO.StartDate < DateTime.Now.AddHours(1) || rentalCreateInputDTO.EndDate < DateTime.Now.AddHours(3))
                {
                    throw new Exception("Days is not correct");
                }
                var rental = new Rental
                {
                    AddressId = rentalCreateInputDTO.AddressId,
                    StartDate = rentalCreateInputDTO.StartDate,
                    EndDate = rentalCreateInputDTO.EndDate,
                    PersonId = person.Id,
                    StateId = 1,
                    VehicleId = rentalCreateInputDTO.VehicleId,
                };

                return await _rentalRepository.CreateRentalAsync(rental);
            }
            throw new Exception("You already have a reservation at this time. You cannot create a new reservation.");
        }

        public async Task<RentalOutputDTO> UpdateRentalAsync(RentalUpdateInputDTO rentalUpdateInputDTO, string UserId)
        {
            var person = await _personRepository.GetPersonByUserIdAsync(UserId);
            var rentalToUpdate = await _rentalRepository.GetRentalByIdAsync(rentalUpdateInputDTO.Id);

            if (rentalUpdateInputDTO.AddressId <= 0 || rentalUpdateInputDTO.StartDate == null || rentalUpdateInputDTO.EndDate == null || rentalUpdateInputDTO.VehicleId <= 0)
            {
                throw new Exception("Field required is empty.");
            }
            else if (rentalUpdateInputDTO.StartDate < DateTime.Now.AddHours(1) || rentalUpdateInputDTO.EndDate < DateTime.Now.AddHours(3))
            {
                throw new Exception("Days is not correct");
            }
            else if (rentalToUpdate != null && rentalToUpdate.PersonId == person.Id)
            {
                var rentalExisting = await _rentalRepository.GetRentalForControl(rentalUpdateInputDTO.StartDate, rentalUpdateInputDTO.EndDate, person.Id, rentalUpdateInputDTO.VehicleId);
                if (rentalExisting == null)
                {
                    var rental = new Rental
                    {
                        StartDate = rentalUpdateInputDTO.StartDate,
                        EndDate = rentalUpdateInputDTO.EndDate,
                        PersonId = person.Id,
                        VehicleId = rentalUpdateInputDTO.VehicleId,
                        AddressId = rentalUpdateInputDTO.AddressId,
                    };

                    return await _rentalRepository.UpdateRentalAsync(rental);
                }
                else throw new Exception("You Cannot update a rental, the vehicle is not available");
            }
            else throw new Exception("you cannot update a rental that is not yours.");
        }

        public async Task<List<RentalOutputDTO>> GetAllRentalsAsync()
        {
            return await _rentalRepository.GetAllRentalsAsync();
        }

        public async Task<List<RentalOutputDTO>> GetAllRentalsByDatesAsync(DateTime startDate, DateTime? endDate)
        {
            return await _rentalRepository.GetAllRentalsByDatesAsync(startDate, endDate);
        }

        public async Task<List<RentalOutputDTO>> GetAllRentalsByPersonAsync(int personId)
        {
            return await _rentalRepository.GetAllRentalsByPersonAsync(personId);
        }

        public async Task<List<RentalOutputDTO>> GetAllRentalsByPersonAndByOnGoingStateAsync(int personId)
        {
            return await _rentalRepository.GetAllRentalsByPersonAndByOnGoingStateAsync(personId);
        }

        public async Task<List<RentalOutputDTO>> GetRentalByPersonandByStateOrderedByDate(int personId, int stateId)
        {
            return await _rentalRepository.GetRentalByPersonAndByStateSortedByDates(personId, stateId);
        }

        public async Task<List<RentalOutputDTO>> GetAllRentalsByRegistrationAsync(string registration)
        {
            return await _rentalRepository.GetAllRentalsByRegistrationAsync(registration);
        }

        public async Task<List<RentalOutputDTO>> GetAllRentalsByStateAsync(int stateId)
        {
            return await _rentalRepository.GetAllRentalsByStateAsync(stateId);
        }

        public async Task<RentalOutputDTO> GetRentalByIdAsync(int id)
        {
            return await _rentalRepository.GetRentalByIdAsync(id);
        }



        public async Task<bool> DeleteRentalAsync(int id, string userId)
        {
            var person = await _personRepository.GetPersonByUserIdAsync(userId);
            var rental = await _rentalRepository.GetRentalByIdAsync(id);
            if (rental != null)
            {
                if (rental.PersonId == person.Id)
                {
                    return await _rentalRepository.DeleteRentalAsync(id);
                }
                else throw new Exception("you cannot delete a rental that is not yours.");
            }
            else throw new Exception("Cannot delete a rental that does not exist");
        }
    }
}
