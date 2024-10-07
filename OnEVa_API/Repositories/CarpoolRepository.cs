using AutoMapper;
using DTOs.CarpoolDTOs;
using DTOs.PersonDTOs;
using Entities;
using IRepositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories
{
    public class CarpoolRepository : ICarpoolRepository
    {
        private readonly APIDbContext _context;
        private readonly IMapper _mapper;

        public CarpoolRepository(APIDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<CarpoolOutputDTO> CreateCarpoolAsync(Carpool carpool)
        {
            await _context.Carpools.AddAsync(carpool);
            await _context.SaveChangesAsync();
            var carpoolOutputDTO = _mapper.Map<CarpoolOutputDTO>(carpool);
            return carpoolOutputDTO;
        }

        public async Task<bool> DeleteCarpoolAsync(int carpoolId)
        {
            try
            {
                var carpoolDeleted = await _context.Carpools.Where(c => c.Id == carpoolId).ExecuteDeleteAsync();
                if (carpoolDeleted > 0)
                {
                    return true;
                }
                else { return false; }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public async Task<bool> DeleteCarpoolAttendeeAsync(int carpoolId, int personId)
        {
            var carpool = await _context.Carpools.FindAsync(carpoolId);
            var person = await _context.Persons.FindAsync(personId);
            carpool.Attendees.Remove(person);

            await _context.SaveChangesAsync();

            var attendeeDeleted = await _context.Carpools.FindAsync(carpoolId);
            if (attendeeDeleted.Attendees.Contains(person))
            {
                return false;
            }
            return true;
        }

        public async Task<List<CarpoolOutputDTO>> GetAllCarpoolsAsync()
        {
            try
            {
                var carpools = await _context.Carpools.Include(c => c.Organizer)
                    .Include(c => c.Attendees).Include(c => c.StartAddress)
                    .Include(c => c.EndAddress).Include(c => c.State)
                    .Include(c => c.Vehicle).ToListAsync();
                if (carpools.Any())
                {
                    var carpoolOutputDTO = _mapper.Map<List<CarpoolOutputDTO>>(carpools);

                    return carpoolOutputDTO;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public async Task<List<CarpoolOutputDTO>> GetAllCarpoolsByAddressAndByDatesAsync(Address startAddress, Address endAddress, DateTime? startDate, DateTime? endDate)
        {
            try
            {
                var carpools = await _context.Carpools.Include(c => c.Organizer)
                    .Include(c => c.Attendees).Include(c => c.StartAddress)
                    .Include(c => c.EndAddress).Include(c => c.State)
                    .Include(c => c.Vehicle).Where(c => c.StartAddress == startAddress && c.EndAddress == endAddress && c.StartDate == startDate && c.EndDate == endDate).ToListAsync();
                if (carpools.Any())
                {
                    var carpoolOutputDTO = _mapper.Map<List<CarpoolOutputDTO>>(carpools);

                    return carpoolOutputDTO;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public async Task<List<CarpoolOutputDTO>> GetAllCarpoolsByDatesAsync(DateTime startDate, DateTime? endDate)
        {
            try
            {
                var carpools = await _context.Carpools.Include(c => c.Organizer)
                    .Include(c => c.Attendees).Include(c => c.StartAddress)
                    .Include(c => c.EndAddress).Include(c => c.State)
                    .Include(c => c.Vehicle).Where(c => c.StartDate == startDate && c.EndDate == endDate).ToListAsync();
                if (carpools.Any())
                {
                    var carpoolOutputDTO = _mapper.Map<List<CarpoolOutputDTO>>(carpools);

                    return carpoolOutputDTO;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public async Task<List<CarpoolOutputDTO>> GetAllCarpoolsByPersonAsync(int personId)
        {
            try
            {
                var carpools = await _context.Carpools.Include(c => c.Organizer)
                    .Include(c => c.Attendees).Include(c => c.StartAddress)
                    .Include(c => c.EndAddress).Include(c => c.State)
                    .Include(c => c.Vehicle).Where(c => c.OrganizerId == personId).ToListAsync();
                if (carpools != null)
                {
                    var carpoolOutputDTO = _mapper.Map<List<CarpoolOutputDTO>>(carpools);
                    return carpoolOutputDTO;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public async Task<List<CarpoolOutputDTO>> GetAllCarpoolsByStateAsync(int stateId)
        {
            try
            {
                var carpools = await _context.Carpools.Include(c => c.Organizer)
                    .Include(c => c.Attendees).Include(c => c.StartAddress)
                    .Include(c => c.EndAddress).Include(c => c.State)
                    .Include(c => c.Vehicle).Where(c => c.StateId == stateId).ToListAsync();
                if (carpools != null)
                {
                    var carpoolOutputDTO = _mapper.Map<List<CarpoolOutputDTO>>(carpools);
                    return carpoolOutputDTO;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public async Task<CarpoolOutputDTO> GetCarpoolByIdAsync(int carpoolId)
        {
            try
            {
                var carpool = await _context.Carpools.Include(c => c.Organizer)
                  .Include(c => c.Attendees).Include(c => c.StartAddress)
                  .Include(c => c.EndAddress).Include(c => c.State)
                  .Include(c => c.Vehicle).FirstOrDefaultAsync(c => c.Id == carpoolId);

                if (carpool != null)
                {
                    var carpoolOutputDTO = _mapper.Map<CarpoolOutputDTO>(carpool);
                    return carpoolOutputDTO;
                }
                else return null;
            }
            catch (Exception e)
            {

                throw new Exception(e.Message);
            }

        }

        public async Task<Carpool> GetCarpoolByIdToolsAsync(int carpoolId)
        {
            try
            {
                var carpool = await _context.Carpools.Include(c => c.Organizer)
                  .Include(c => c.Attendees).Include(c => c.StartAddress)
                  .Include(c => c.EndAddress).Include(c => c.State)
                  .Include(c => c.Vehicle).FirstOrDefaultAsync(c => c.Id == carpoolId);

                if (carpool != null)
                {
                    return carpool;
                }
                else return null;
            }
            catch (Exception e)
            {

                throw new Exception(e.Message);
            }

        }
        //var carpoolExisting = await _context.Carpools.Include(c => c.StartAddress).Include(c => c.EndAddress).
        //    Where(c => c.State.Label == "Ongoing" && c.OrganizerId == organizerId && (carpoolCreateInputDTO.StartDate.Hour == c.StartDate.Hour && carpoolCreateInputDTO.StartDate.Minute == c.StartDate.Minute) 
        //    || (carpoolCreateInputDTO.StartDate.Day == c.EndDate.Day )).FirstOrDefaultAsync();

        public async Task<List<Carpool>> GetCarpoolForMethodExistingAsync(CarpoolCreateInputDTO carpoolCreateInputDTO, int organizerId)
        {
            try
            {
                var carpoolExisting = await _context.Carpools.Include(c => c.StartAddress).Include(c => c.EndAddress).
              Where(c => c.State.Label == "OnGoing" && c.OrganizerId == organizerId && c.StartDate.Day == carpoolCreateInputDTO.StartDate.Day).ToListAsync();

                if (carpoolExisting != null)
                {
                    return carpoolExisting;
                }
                else return null;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public async Task<CarpoolOutputDTO> UpdateCarpoolAsync(Carpool carpool)
        {
            try
            {
                var nbRows = await _context.Carpools.Where(c => c.Id == carpool.Id).ExecuteUpdateAsync(
                      updates => updates.SetProperty(c => c.StartDate, carpool.StartDate)
                      .SetProperty(c => c.CarSeatNb, carpool.CarSeatNb)
                      .SetProperty(c => c.VehicleId, carpool.VehicleId)
                      .SetProperty(c => c.StartAddressId, carpool.StartAddressId)
                      .SetProperty(c => c.EndAddressId, carpool.EndAddressId));
                if (nbRows > 0)
                {
                    return await GetCarpoolByIdAsync(carpool.Id);
                }
                else throw new Exception("Update failed");
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public async Task<CarpoolOutputDTO> UpdateCarpoolAddAttendeeAsync(Carpool carpool)
        {
            try
            {
                var carpoolToUpdate = await _context.Carpools.FindAsync(carpool.Id);

                if (carpoolToUpdate == null)
                {
                    throw new Exception("Carpool not found");
                }

                carpoolToUpdate.Attendees = carpool.Attendees;

                await _context.SaveChangesAsync();

                var carpoolOutputDTO = _mapper.Map<CarpoolOutputDTO>(carpoolToUpdate);
                return carpoolOutputDTO;

            }
            catch (Exception)
            {
                throw new Exception("Failed to update carpool");
            }
        }
    }
}
