using DTOs.VehicleDTOs;
using DTOs.VehiclePropertiesDTOs.Output;
using Entities;
using IRepositories;
using Microsoft.EntityFrameworkCore;

namespace Repositories
{
    public class VehicleRepository : IVehicleRepository
	{
		private readonly APIDbContext _context;
		public VehicleRepository(APIDbContext context)
		{
			_context = context;
		}
		public async Task<VehicleOutputDTO> CreateVehicleAsync(Vehicle vehicle)
		{
			try
			{
				await _context.Vehicles.AddAsync(vehicle);
				await _context.SaveChangesAsync();
				return await GetVehicleByIdAsync(vehicle.Id);
			}
			catch (Exception ex) { throw new Exception(ex.Message); }
		}

		public async Task<VehicleOutputDTO> UpdateVehicleAsync(Vehicle vehicle)
		{
			
			using(var transaction = _context.Database.BeginTransaction())
			{
                try
                {
                    var nbRows = await _context.Vehicles.Where(v => v.Id == vehicle.Id)
                        .ExecuteUpdateAsync(updates => updates
                        .SetProperty(v => v.Registration, vehicle.Registration)
                        .SetProperty(v => v.MaxSeatNb, vehicle.MaxSeatNb)
                        .SetProperty(v => v.Co2Emission, vehicle.Co2Emission)
                        .SetProperty(v => v.IsEnabled, vehicle.IsEnabled)
                        .SetProperty(v => v.Status, vehicle.Status)
                        .SetProperty(v => v.BrandId, vehicle.BrandId)
                        .SetProperty(v => v.CategoryId, vehicle.CategoryId)
                        .SetProperty(v => v.EngineId, vehicle.EngineId)
                        .SetProperty(v => v.ModelId, vehicle.ModelId)
                        .SetProperty(v => v.VehicleNumber, vehicle.VehicleNumber));

					if (vehicle.Pictures.Any())
					{
                        var vehicleUpdated = await _context.Vehicles.FindAsync(vehicle.Id);
                        vehicleUpdated.Pictures = vehicle.Pictures;

                        await _context.SaveChangesAsync();
                    }

					transaction.Commit();

                    if (nbRows > 0)
                    {
                        var vehicleOutputDTO = await _context.Vehicles
                                            .Include(v => v.Brand)
                                            .Include(v => v.Category)
                                            .Include(v => v.Engine)
                                            .Include(v => v.Model)
                                            .Include(v => v.Pictures)
                                            .Where(v => v.Id == vehicle.Id)
                                            .Select(v => new VehicleOutputDTO
                                            {
                                                Id = v.Id,
                                                VehicleNumber = v.VehicleNumber,
                                                Registration = v.Registration,
                                                BrandLabel = v.Brand.Label,
                                                ModelLabel = v.Model.Label,
                                                CategoryLabel = v.Category.Label,
                                                PicturesOutputDTO = v.Pictures.Select(p => new PictureOutputDTO { Id = p.Id, Title = p.Title, Url = p.Url }),
                                                EngineLabel = v.Engine.Label,
                                                Co2Emission = v.Co2Emission,
                                                MaxSeatNb = v.MaxSeatNb,
                                                Status = v.Status
                                            })
                                            .FirstOrDefaultAsync();
                        return vehicleOutputDTO;

                    }
                    throw new Exception("The update failed");
                }
                catch (Exception ex) { throw new Exception(ex.Message); }
            }
		
		}

		#region GetMethods
		public async Task<List<VehicleOutputDTO>> GetAllVehiclesAsync()
		{
			try
			{
				var vehiclesOutputDTO = await _context.Vehicles
					.Include(v => v.Brand)
					.Include(v => v.Category)
					.Include(v => v.Engine)
					.Include(v => v.Model)
					.Include(v => v.Pictures)
					.Where(v => v.IsEnabled == false)
					.Select(v => new VehicleOutputDTO
					{
						Id = v.Id,
						VehicleNumber = v.VehicleNumber,
						Registration = v.Registration,
						BrandLabel = v.Brand.Label,
						ModelLabel = v.Model.Label,
						CategoryLabel = v.Category.Label,
						PicturesOutputDTO = v.Pictures.Select(p => new PictureOutputDTO { Id = p.Id, Title = p.Title, Url = p.Url }),
						EngineLabel = v.Engine.Label,
						Co2Emission = v.Co2Emission,
						MaxSeatNb = v.MaxSeatNb,
						Status = v.Status
					})
					.ToListAsync();
				if (!vehiclesOutputDTO.Any())
				{
					return null;
				}
				else return vehiclesOutputDTO;
			}
			catch (Exception ex) { throw new Exception(ex.Message); }
		}

		public async Task<List<VehicleOutputDTO>> GetAllVehiclesAvailableByDatesAsync(DateTime startDate, DateTime endDate)
		{
			try
			{
				var vehiclesOutputDTO = await _context.Vehicles
					.Include(v => v.Brand)
					.Include(v => v.Category)
					.Include(v => v.Engine)
					.Include(v => v.Model)
					.Include(v => v.Pictures)
					.Include(v => v.Rentals)
					.Where(v => v.Rentals.All(r => r.StartDate > endDate || r.EndDate < startDate))
					.Select(v => new VehicleOutputDTO
					{
						Id = v.Id,
						VehicleNumber = v.VehicleNumber,
						Registration = v.Registration,
						BrandLabel = v.Brand.Label,
						ModelLabel = v.Model.Label,
						CategoryLabel = v.Category.Label,
						PicturesOutputDTO = v.Pictures.Select(p => new PictureOutputDTO { Id = p.Id, Title = p.Title, Url = p.Url }),
						EngineLabel = v.Engine.Label,
						Co2Emission = v.Co2Emission,
						MaxSeatNb = v.MaxSeatNb,
						Status = v.Status
					})
					.ToListAsync();

				if (!vehiclesOutputDTO.Any())
				{
					return null;
				}
				else return vehiclesOutputDTO;
			}
			catch (Exception ex) { throw new Exception(ex.Message); }
		}

		public async Task<List<VehicleOutputDTO>> GetAllVehiclesByBrandAsync(string brand)
		{
			try
			{
				var vehiclesOutputDTO = await _context.Vehicles
					.Include(v => v.Category)
					.Include(v => v.Engine)
					.Include(v => v.Model)
					.Include(v => v.Pictures)
					.Where(v => v.Brand.Label == brand && v.IsEnabled == false)
					.Select(v => new VehicleOutputDTO
					{
						Id = v.Id,
						VehicleNumber = v.VehicleNumber,
						Registration = v.Registration,
						BrandLabel = v.Brand.Label,
						ModelLabel = v.Model.Label,
						CategoryLabel = v.Category.Label,
						PicturesOutputDTO = v.Pictures.Select(p => new PictureOutputDTO { Id = p.Id, Title = p.Title, Url = p.Url }),
						EngineLabel = v.Engine.Label,
						Co2Emission = v.Co2Emission,
						MaxSeatNb = v.MaxSeatNb,
						Status = v.Status
					})
					.ToListAsync();

				if (!vehiclesOutputDTO.Any())
				{
					return null;
				}
				else return vehiclesOutputDTO;
			}
			catch (Exception ex)
			{
				throw new Exception(ex.Message);
			}
		}

		public async Task<VehicleOutputDTO> GetVehicleByRegistrationAsync(string registration)
		{
			try
			{
				var vehicleOutputDTO = await _context.Vehicles
					.Include(v => v.Brand)
					.Include(v => v.Category)
					.Include(v => v.Engine)
					.Include(v => v.Model)
					.Include(v => v.Pictures)
                    .Where(v => v.IsEnabled == false)
                    .Select(v => new VehicleOutputDTO
					{
						Id = v.Id,
						VehicleNumber = v.VehicleNumber,
						Registration = v.Registration,
						BrandLabel = v.Brand.Label,
						ModelLabel = v.Model.Label,
						CategoryLabel = v.Category.Label,
						PicturesOutputDTO = v.Pictures.Select(p => new PictureOutputDTO { Id = p.Id, Title = p.Title, Url = p.Url }),
						EngineLabel = v.Engine.Label,
						Co2Emission = v.Co2Emission,
						MaxSeatNb = v.MaxSeatNb,
						Status = v.Status
					})
					.FirstOrDefaultAsync(v => v.Registration == registration);
				if (vehicleOutputDTO == null)
				{
					return null;
				}
				else
				{
					return vehicleOutputDTO;
				}
			}
			catch (Exception ex)
			{
				throw new Exception(ex.Message);
			}
		}

        public async Task<List<VehicleOutputDTO>> GetVehicleByRegistrationAutoCompleteAsync(string registration)
        {
            try
            {
				var vehicleOutputDTO = await _context.Vehicles
					.Include(v => v.Brand)
					.Include(v => v.Category)
					.Include(v => v.Engine)
					.Include(v => v.Model)
					.Include(v => v.Pictures)
					.Where(v => v.Registration.Contains(registration) && v.IsEnabled == false)
                    .Select(v => new VehicleOutputDTO
					{
						Id = v.Id,
						VehicleNumber = v.VehicleNumber,
						Registration = v.Registration,
						BrandLabel = v.Brand.Label,
						ModelLabel = v.Model.Label,
						CategoryLabel = v.Category.Label,
						PicturesOutputDTO = v.Pictures.Select(p => new PictureOutputDTO { Id = p.Id, Title = p.Title, Url = p.Url }),
						EngineLabel = v.Engine.Label,
						Co2Emission = v.Co2Emission,
						MaxSeatNb = v.MaxSeatNb,
						Status = v.Status
					})
					.ToListAsync();
                if (vehicleOutputDTO == null)
                {
                    return null;
                }
                else
                {
                    return vehicleOutputDTO;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<VehicleOutputDTO> GetVehicleByIdAsync(int id)
		{
			try
			{
				var vehicleOutputDTO = await _context.Vehicles
					.Include(v => v.Brand)
					.Include(v => v.Category)
					.Include(v => v.Engine)
					.Include(v => v.Model)
					.Include(v => v.Pictures)
                    .Where(v => v.IsEnabled == false)
                    .Select(v => new VehicleOutputDTO
					{
						Id = v.Id,
						VehicleNumber = v.VehicleNumber,
						Registration = v.Registration,
						BrandLabel = v.Brand.Label,
						ModelLabel = v.Model.Label,
						CategoryLabel = v.Category.Label,
						PicturesOutputDTO = v.Pictures.Select(p => new PictureOutputDTO { Id = p.Id, Title = p.Title, Url = p.Url }),
						EngineLabel = v.Engine.Label,
						Co2Emission = v.Co2Emission,
						MaxSeatNb = v.MaxSeatNb,
						Status = v.Status
					})
					.FirstOrDefaultAsync(v => v.Id == id);
				if (vehicleOutputDTO == null)
				{
					return null;
				}
				else
				{
					return vehicleOutputDTO;
				}
			}
			catch (Exception ex) { throw new Exception(ex.Message); }
		}

        public async Task<Vehicle> GetVehicleByIdToolsAsync(int id)
        {
            try
            {
                var vehicleOutputDTO = await _context.Vehicles
                    .Include(v => v.Brand)
                    .Include(v => v.Category)
                    .Include(v => v.Engine)
                    .Include(v => v.Model)
                    .Include(v => v.Pictures)
                    .Where(v => v.IsEnabled == false)
                    .FirstOrDefaultAsync(v => v.Id == id);
                if (vehicleOutputDTO == null)
                {
                    return null;
                }
                else
                {
                    return vehicleOutputDTO;
                }
            }
            catch (Exception ex) { throw new Exception(ex.Message); }
        }
        #endregion
        public async Task<bool> DeleteVehicleAsync(int id)
		{
			try
			{
				var vehicle = await _context.Vehicles.FindAsync(id);
				if (vehicle != null)
				{
					await _context.Vehicles.Where(v => v.Id == id).ExecuteDeleteAsync();
					return true;
				}
				else return false;
			}
			catch (Exception ex) { throw new Exception(ex.Message); }
		}

        public async Task<bool> DesableVehicleAsync(int idVehicle)
        {
            try
            {
                var vehicle = await _context.Vehicles.FindAsync(idVehicle);
                if (vehicle != null)
                {
					await _context.Vehicles.Where(v => v.Id == idVehicle).ExecuteUpdateAsync(updates => updates
						.SetProperty(v => v.IsEnabled, true));
                    return true;
                }
                else return false;
            }
            catch (Exception ex) { throw new Exception(ex.Message); }
        }
    }
}
