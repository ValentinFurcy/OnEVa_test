using AutoMapper;
using DTOs.Address;
using DTOs.RentalDTO;
using DTOs.VehicleDTOs;
using DTOs.VehiclePropertiesDTOs.Output;
using Entities;
using IRepositories;
using Microsoft.EntityFrameworkCore;


namespace Repositories
{
    public class RentalRepository : IRentalRepository
    {
        private readonly APIDbContext _context;

        public RentalRepository(APIDbContext context, IMapper mapper)
        {
            _context = context;
        }
        public async Task<RentalOutputDTO> CreateRentalAsync(Rental rental)
        {
            try
            {
                await _context.Rentals.AddAsync(rental);
                await _context.SaveChangesAsync();
                var result = await _context.Rentals
                    .Include(r => r.State)
                    .Include(r => r.Person)
                    .Include(r => r.Vehicle)
                    .Select(r => new RentalOutputDTO
                    {
                        Id = r.Id,
                        FirstNamePerson = r.Person.FirstName,
                        LastNamePerson = r.Person.LastName,
                        PersonId = r.PersonId,
                        StartDate = r.StartDate,
                        EndDate = r.EndDate,
                        StateLabel = r.State.Label,
                        VehicleOutput = new VehicleOutputDTO
                        {
                            Id = r.Vehicle.Id,
                            VehicleNumber = r.Vehicle.VehicleNumber,
                            Registration = r.Vehicle.Registration,
                            BrandLabel = r.Vehicle.Brand.Label,
                            ModelLabel = r.Vehicle.Model.Label,
                            CategoryLabel = r.Vehicle.Category.Label,
                            PicturesOutputDTO = r.Vehicle.Pictures.Select(p => new PictureOutputDTO { Id = p.Id, Title = p.Title, Url = p.Url, Color = p.Color }),
                            EngineLabel = r.Vehicle.Engine.Label,
                            Co2Emission = r.Vehicle.Co2Emission,
                            MaxSeatNb = r.Vehicle.MaxSeatNb,
                            Status = r.Vehicle.Status,
                        },
                        AddressOutput = new AddressOutputDTO
                        {
                            City = r.Address.City,
                        }
                    }).FirstOrDefaultAsync(r => r.Id == rental.Id);
                if (result != null)
                {
                    return result;
                }
                else { return null; }
            }
            catch (Exception ex) { throw new Exception(ex.Message); }
        }

        public async Task<List<RentalOutputDTO>> GetAllRentalsAsync()
        {
            try
            {
                var results = await _context.Rentals
                    .Include(r => r.State)
                    .Include(r => r.Person)
                    .Include(r => r.Vehicle)
                    .Select(r => new RentalOutputDTO
                    {
                        Id = r.Id,
                        FirstNamePerson = r.Person.FirstName,
                        LastNamePerson = r.Person.LastName,
                        PersonId = r.PersonId,
                        StartDate = r.StartDate,
                        EndDate = r.EndDate,
                        StateLabel = r.State.Label,
                        VehicleOutput = new VehicleOutputDTO
                        {
                            Id = r.Vehicle.Id,
                            VehicleNumber = r.Vehicle.VehicleNumber,
                            Registration = r.Vehicle.Registration,
                            BrandLabel = r.Vehicle.Brand.Label,
                            ModelLabel = r.Vehicle.Model.Label,
                            CategoryLabel = r.Vehicle.Category.Label,
                            PicturesOutputDTO = r.Vehicle.Pictures.Select(p => new PictureOutputDTO { Id = p.Id, Title = p.Title, Url = p.Url, Color = p.Color }),
                            EngineLabel = r.Vehicle.Engine.Label,
                            Co2Emission = r.Vehicle.Co2Emission,
                            MaxSeatNb = r.Vehicle.MaxSeatNb,
                            Status = r.Vehicle.Status,
                        },
                        AddressOutput = new AddressOutputDTO
                        {
                            City = r.Address.City,
                        }
                    }).ToListAsync();
                if (results.Any())
                {
                    return results;
                }
                else return null;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<List<RentalOutputDTO>> GetAllRentalsByDatesAsync
             (DateTime startDate, DateTime? endDate)
        {
            try
            {
                var results = await _context.Rentals
                    .Include(r => r.State)
                    .Include(r => r.Person)
                    .Include(r => r.Vehicle)
                    .Where(r => (r.EndDate < endDate && r.EndDate > startDate) || (r.StartDate < endDate && r.EndDate > endDate) && (r.Vehicle.Status == Entities.Status.InService))
                    .Select(r => new RentalOutputDTO
                    {
                        Id = r.Id,
                        FirstNamePerson = r.Person.FirstName,
                        LastNamePerson = r.Person.LastName,
                        PersonId = r.PersonId,
                        StartDate = r.StartDate,
                        EndDate = r.EndDate,
                        StateLabel = r.State.Label,
                        VehicleOutput = new VehicleOutputDTO
                        {
                            Id = r.Vehicle.Id,
                            VehicleNumber = r.Vehicle.VehicleNumber,
                            Registration = r.Vehicle.Registration,
                            BrandLabel = r.Vehicle.Brand.Label,
                            ModelLabel = r.Vehicle.Model.Label,
                            CategoryLabel = r.Vehicle.Category.Label,
                            PicturesOutputDTO = r.Vehicle.Pictures.Select(p => new PictureOutputDTO { Id = p.Id, Title = p.Title, Url = p.Url, Color = p.Color }),
                            EngineLabel = r.Vehicle.Engine.Label,
                            Co2Emission = r.Vehicle.Co2Emission,
                            MaxSeatNb = r.Vehicle.MaxSeatNb,
                            Status = r.Vehicle.Status,
                        },
                        AddressOutput = new AddressOutputDTO
                        {
                            City = r.Address.City,
                        }
                    })
                    .OrderBy(r => r.StartDate)
                    .ToListAsync();
                if (results.Any())
                {
                    return results;
                }
                else { return null; }
            }
            catch (Exception ex) { throw new Exception(ex.Message); }
        }

        public async Task<List<RentalOutputDTO>> GetAllRentalsByPersonAsync(int personId)
        {
            try
            {
                var results = await _context.Rentals
                    .Include(r => r.State)
                    .Include(r => r.Person)
                    .Include(r => r.Vehicle)
                    .Where(r => r.PersonId == personId)
                    .Select(r => new RentalOutputDTO
                    {
                        Id = r.Id,
                        FirstNamePerson = r.Person.FirstName,
                        LastNamePerson = r.Person.LastName,
                        PersonId = r.PersonId,
                        StartDate = r.StartDate,
                        EndDate = r.EndDate,
                        StateLabel = r.State.Label,
                        VehicleOutput = new VehicleOutputDTO
                        {
                            Id = r.Vehicle.Id,
                            VehicleNumber = r.Vehicle.VehicleNumber,
                            Registration = r.Vehicle.Registration,
                            BrandLabel = r.Vehicle.Brand.Label,
                            ModelLabel = r.Vehicle.Model.Label,
                            CategoryLabel = r.Vehicle.Category.Label,
                            PicturesOutputDTO = r.Vehicle.Pictures.Select(p => new PictureOutputDTO { Id = p.Id, Title = p.Title, Url = p.Url, Color = p.Color }),
                            EngineLabel = r.Vehicle.Engine.Label,
                            Co2Emission = r.Vehicle.Co2Emission,
                            MaxSeatNb = r.Vehicle.MaxSeatNb,
                            Status = r.Vehicle.Status,
                        },
                        AddressOutput = new AddressOutputDTO
                        {
                            City = r.Address.City,
                        }
                    }).ToListAsync();

                return results;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<List<RentalOutputDTO>> GetAllRentalsByPersonAndByOnGoingStateAsync(int personId)
        {
            try
            {
                var results = await _context.Rentals
                    .Include(r => r.State)
                    .Include(r => r.Person)
                    .Include(r => r.Vehicle)
                    .Where(r => r.PersonId == personId && r.StateId == 1)
                    .Select(r => new RentalOutputDTO
                    {
                        Id = r.Id,
                        FirstNamePerson = r.Person.FirstName,
                        LastNamePerson = r.Person.LastName,
                        PersonId = r.PersonId,
                        StartDate = r.StartDate,
                        EndDate = r.EndDate,
                        StateLabel = r.State.Label,
                        VehicleOutput = new VehicleOutputDTO
                        {
                            Id = r.Vehicle.Id,
                            VehicleNumber = r.Vehicle.VehicleNumber,
                            Registration = r.Vehicle.Registration,
                            BrandLabel = r.Vehicle.Brand.Label,
                            ModelLabel = r.Vehicle.Model.Label,
                            CategoryLabel = r.Vehicle.Category.Label,
                            PicturesOutputDTO = r.Vehicle.Pictures.Select(p => new PictureOutputDTO { Id = p.Id, Title = p.Title, Url = p.Url , Color = p.Color }),
                            EngineLabel = r.Vehicle.Engine.Label,
                            Co2Emission = r.Vehicle.Co2Emission,
                            MaxSeatNb = r.Vehicle.MaxSeatNb,
                            Status = r.Vehicle.Status,
                        },
                        AddressOutput = new AddressOutputDTO
                        {
                            City = r.Address.City,
                        }
                    }).ToListAsync();

                return results;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }


        public async Task<List<RentalOutputDTO>> GetRentalByPersonAndByStateSortedByDates(int personId, int stateId)
        {
            try
            {
                var results = await _context.Rentals
                    .Include(r => r.State)
                    .Include(r => r.Person)
                    .Include(r => r.Vehicle)
                    .Where(r => r.PersonId == personId && r.StateId == stateId)
                    .Select(r => new RentalOutputDTO
                    {
                        Id = r.Id,
                        FirstNamePerson = r.Person.FirstName,
                        LastNamePerson = r.Person.LastName,
                        PersonId = r.PersonId,
                        StartDate = r.StartDate,
                        EndDate = r.EndDate,
                        StateLabel = r.State.Label,
                        VehicleOutput = new VehicleOutputDTO
                        {
                            Id = r.Vehicle.Id,
                            VehicleNumber = r.Vehicle.VehicleNumber,
                            Registration = r.Vehicle.Registration,
                            BrandLabel = r.Vehicle.Brand.Label,
                            ModelLabel = r.Vehicle.Model.Label,
                            CategoryLabel = r.Vehicle.Category.Label,
                            PicturesOutputDTO = r.Vehicle.Pictures.Select(p => new PictureOutputDTO { Id = p.Id, Title = p.Title, Url = p.Url, Color = p.Color }),
                            EngineLabel = r.Vehicle.Engine.Label,
                            Co2Emission = r.Vehicle.Co2Emission,
                            MaxSeatNb = r.Vehicle.MaxSeatNb,
                            Status = r.Vehicle.Status,
                        },
                        AddressOutput = new AddressOutputDTO
                        {
                            City = r.Address.City,
                        }
                    })
                    .OrderBy(r => r.StartDate)
                    .ToListAsync();
                if (results.Any())
                {
                    return results;
                }
                else return null;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<List<RentalOutputDTO>> GetAllRentalsByRegistrationAsync(string registration)
        {
            try
            {
                var results = await _context.Rentals
                    .Include(r => r.State)
                    .Include(r => r.Person)
                    .Include(r => r.Vehicle)
                    .Where(r => r.Vehicle.Registration == registration)
                    .Select(r => new RentalOutputDTO
                    {
                        Id = r.Id,
                        FirstNamePerson = r.Person.FirstName,
                        LastNamePerson = r.Person.LastName,
                        PersonId = r.PersonId,
                        StartDate = r.StartDate,
                        EndDate = r.EndDate,
                        StateLabel = r.State.Label,
                        VehicleOutput = new VehicleOutputDTO
                        {
                            Id = r.Vehicle.Id,
                            VehicleNumber = r.Vehicle.VehicleNumber,
                            Registration = r.Vehicle.Registration,
                            BrandLabel = r.Vehicle.Brand.Label,
                            ModelLabel = r.Vehicle.Model.Label,
                            CategoryLabel = r.Vehicle.Category.Label,
                            PicturesOutputDTO = r.Vehicle.Pictures.Select(p => new PictureOutputDTO { Id = p.Id, Title = p.Title, Url = p.Url , Color = p.Color }),
                            EngineLabel = r.Vehicle.Engine.Label,
                            Co2Emission = r.Vehicle.Co2Emission,
                            MaxSeatNb = r.Vehicle.MaxSeatNb,
                            Status = r.Vehicle.Status
                        },
                        AddressOutput = new AddressOutputDTO
                        {
                            City = r.Address.City,
                        }
                    }).ToListAsync();
                if (results.Any()) return results;
                else return null;
            }
            catch (Exception ex) { throw new Exception(ex.Message); }
        }


        public async Task<List<RentalOutputDTO>> GetAllRentalsByStateAsync(int stateId)
        {
            try
            {
                var results = await _context.Rentals
                    .Include(r => r.State)
                    .Include(r => r.Person)
                    .Include(r => r.Vehicle)
                    .Where(r => r.StateId == stateId)
                    .Select(r => new RentalOutputDTO
                    {
                        Id = r.Id,
                        FirstNamePerson = r.Person.FirstName,
                        LastNamePerson = r.Person.LastName,
                        PersonId = r.PersonId,
                        StartDate = r.StartDate,
                        EndDate = r.EndDate,
                        StateLabel = r.State.Label,
                        VehicleOutput = new VehicleOutputDTO
                        {
                            Id = r.Vehicle.Id,
                            VehicleNumber = r.Vehicle.VehicleNumber,
                            Registration = r.Vehicle.Registration,
                            BrandLabel = r.Vehicle.Brand.Label,
                            ModelLabel = r.Vehicle.Model.Label,
                            CategoryLabel = r.Vehicle.Category.Label,
                            PicturesOutputDTO = r.Vehicle.Pictures.Select(p => new PictureOutputDTO { Id = p.Id, Title = p.Title, Url = p.Url, Color = p.Color }),
                            EngineLabel = r.Vehicle.Engine.Label,
                            Co2Emission = r.Vehicle.Co2Emission,
                            MaxSeatNb = r.Vehicle.MaxSeatNb,
                            Status = r.Vehicle.Status
                        },
                        AddressOutput = new AddressOutputDTO
                        {
                            City = r.Address.City,
                        }
                    }).ToListAsync();
                if (results.Any()) return results;
                else return null;
            }
            catch (Exception ex) { throw new Exception(ex.Message); }
        }


        public async Task<RentalOutputDTO> GetRentalByIdAsync(int id)
        {
            try
            {
                var result = await _context.Rentals
                    .Include(r => r.State)
                    .Include(r => r.Person)
                    .Include(r => r.Vehicle)
                    .Select(r => new RentalOutputDTO
                    {
                        Id = r.Id,
                        FirstNamePerson = r.Person.FirstName,
                        LastNamePerson = r.Person.LastName,
                        PersonId = r.PersonId,
                        StartDate = r.StartDate,
                        EndDate = r.EndDate,
                        StateLabel = r.State.Label,
                        VehicleOutput = new VehicleOutputDTO
                        {
                            Id = r.Vehicle.Id,
                            VehicleNumber = r.Vehicle.VehicleNumber,
                            Registration = r.Vehicle.Registration,
                            BrandLabel = r.Vehicle.Brand.Label,
                            ModelLabel = r.Vehicle.Model.Label,
                            CategoryLabel = r.Vehicle.Category.Label,
                            PicturesOutputDTO = r.Vehicle.Pictures.Select(p => new PictureOutputDTO { Id = p.Id, Title = p.Title, Url = p.Url , Color = p.Color}),
                            EngineLabel = r.Vehicle.Engine.Label,
                            Co2Emission = r.Vehicle.Co2Emission,
                            MaxSeatNb = r.Vehicle.MaxSeatNb,
                            Status = r.Vehicle.Status,
                        },
                        AddressOutput = new AddressOutputDTO
                        {
                            City = r.Address.City,
                        }
                    }).FirstOrDefaultAsync(r => r.Id == id);

                if (result != null) return result;
                else return null;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }


        public async Task<RentalOutputDTO> UpdateRentalAsync(Rental rental)
        {
            try
            {
                var nbRows = await _context.Rentals.Where(r => r.Id == rental.Id).ExecuteUpdateAsync(
                    updates => updates.SetProperty(r => r.AddressId, rental.AddressId)
                    .SetProperty(r => r.StartDate, rental.StartDate)
                    .SetProperty(r => r.EndDate, rental.EndDate)
                    .SetProperty(r => r.VehicleId, rental.VehicleId));
                if (nbRows > 0)
                {
                    return await GetRentalByIdAsync(rental.Id);
                }
                throw new Exception("The update failed");
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public async Task<bool> DeleteRentalAsync(int id)
        {
            try
            {
                var rentalDeleted = await _context.Rentals.Where(r => r.Id == id).ExecuteDeleteAsync();
                if (rentalDeleted > 0)
                {
                    return true;
                }
                else { return false; }
            }
            catch (Exception ex) { throw new Exception(ex.Message); }
        }




        public async Task<List<Rental>> GetRentalForControl(DateTime startDate, DateTime endDate, int? personId, int? vehicleId)
        {
            try
            {
                var existingRentals = await _context.Rentals
                    .Where(r =>
                (r.Person.Id == personId || r.VehicleId == vehicleId)
                && (
                (r.StartDate >= startDate.AddHours(-1) && r.StartDate <= endDate.AddHours(1))
                || (r.EndDate >= startDate.AddHours(-1) && r.EndDate <= endDate.AddHours(1))
                || ((r.StartDate >= startDate && r.EndDate <= endDate))
                )
                ).ToListAsync();
                if (existingRentals.Any())
                {
                    return existingRentals;
                }
                else return null;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
    }
}