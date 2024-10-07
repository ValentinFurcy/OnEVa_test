using Businesses;
using DTOs.RentalDTO;
using DTOs.VehicleDTOs;
using Entities;
using IBusinesses;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using static Businesses.Tools.StringHelper;

namespace OnEVa_API.Controllers
{
	[Authorize]
	[Route("api/[controller]/[action]")]
	[ApiController]
	public class VehicleController : ControllerBase
	{
		private readonly IVehicleBusiness _vehicleBusiness;

		public VehicleController(IVehicleBusiness vehicleBusiness)
		{
			_vehicleBusiness = vehicleBusiness;
		}


		/// <summary>
		/// Create a new vehicle
		/// </summary>
		/// <param name="vehicleCreateInputDTO"></param>
		/// <returns>Return a VehicleOutputDTO</returns>
		[ProducesResponseType(200)]
		[ProducesResponseType(400)]
		[ProducesResponseType(500)]
		[HttpPost]
		public async Task<ActionResult> CreateVehicle(VehicleCreateInputDTO vehicleCreateInputDTO)
		{
			try
			{
				if (vehicleCreateInputDTO != null)
				{
					var result = await _vehicleBusiness.CreateVehicleAsync(vehicleCreateInputDTO);
					return Ok(result);
				}
				else { return BadRequest("Field required empty"); }
			}
			catch (Exception ex) { return Problem(ex.Message); }
		}

		/// </summary>
		/// Update an existing vehicle by a new one entered by user
		/// <param name="VehicleUpdateInputDTO"></param>
		/// <returns>Return a VehicleOutputDTO</returns>
		[ProducesResponseType(200)]
		[ProducesResponseType(400)]
		[ProducesResponseType(500)]
		[HttpPut]
		public async Task<ActionResult> UpdateVehicle(VehicleUpdateInputDTO vehicleUpdateInputDTO)
		{
			try
			{
				if (vehicleUpdateInputDTO != null)
				{
					var vehicle = await _vehicleBusiness.UpdateVehicleAsync(vehicleUpdateInputDTO);
					return Ok(vehicle);
				}
				else { return BadRequest("Field required empty"); }
			}
			catch (Exception ex) { return Problem(ex.Message); }
		}

		/// <summary>
		/// Get all the vehicles
		/// </summary>
		/// <param name=""></param>
		/// <returns>Return a List of VehicleOutputDTO</returns>
		[ProducesResponseType(200)]
		[ProducesResponseType(400)]
		[ProducesResponseType(500)]
		[HttpGet]
		public async Task<ActionResult> GetAllVehicles()
		{
			try
			{
				var vehicles = await _vehicleBusiness.GetAllVehiclesAsync();
				if (vehicles.Any())
				{
					return Ok(vehicles);
				}
				else return NoContent();
			}
			catch (Exception ex) { return Problem(ex.Message); }
		}

		/// <summary>
		/// Get All the vehicles by dates
		/// </summary>
		/// <param name="startDate, endDate"></param>
		/// <returns>Return a List of VehicleOutputDTO</returns>
		[ProducesResponseType(200)]
		[ProducesResponseType(400)]
		[ProducesResponseType(500)]
		[HttpGet]
		public async Task<ActionResult> GetAllVehiclesAvailableByDates(DateTime startDate, DateTime endDate)
		{
			try
			{
				if (startDate != null && endDate != null)
				{
					var vehicles = await _vehicleBusiness.GetAllVehiclesAvailableByDatesAsync(startDate, endDate);
					if (vehicles.Any())
					{
						return Ok(vehicles);
					}
					else
						return NoContent();
				}
				return BadRequest("The startDate or endDate fields cannot be empty.");
			}
			catch (Exception ex) { return Problem(ex.Message); }
		}

		/// <summary>
		/// Get a Vehicle by id
		/// </summary>
		/// <param name="id"></param>
		/// <returns>Return a VehicleOutputDTO</returns>
		[ProducesResponseType(200)]
		[ProducesResponseType(400)]
		[ProducesResponseType(500)]
		[HttpGet]
		public async Task<ActionResult> GetVehicleById(int id)
		{
			try
			{
				if (id <= 0)
				{
					return BadRequest("Enter an id greater than 0.");
				}
				else
				{
					var vehicle = await _vehicleBusiness.GetVehicleByIdAsync(id);
					if (vehicle != null)
					{
						return Ok(vehicle);
					}
					else
						return NoContent();
				}
			}
			catch (Exception ex) { return Problem(ex.Message); }
		}

		/// <summary>
		/// Get All the Vehicles by brand
		/// </summary>
		/// <param name="stateId"></param>
		/// <returns>Return a List of VehicleOutputDTO</returns>
		[ProducesResponseType(200)]
		[ProducesResponseType(400)]
		[ProducesResponseType(500)]
		[HttpGet]
		public async Task<ActionResult> GetAllVehiclesByBrand(string brand)
		{
			try
			{
				if (brand != null)
				{
					var vehicles = await _vehicleBusiness.GetAllVehiclesByBrandAsync(brand);
					if (vehicles.Any())
					{
						return Ok(vehicles);
					}
					else { return NoContent(); }
				}
				else return BadRequest("The required Brand field cannot be empty.");
			}
			catch (Exception ex) { return Problem(ex.Message); }
		}

		/// <summary>
		/// Get All the Vehicles by registration
		/// </summary>
		/// <param name="registration"></param>
		/// <returns>Return a List of VehicleOutputDTO</returns>
		[ProducesResponseType(200)]
		[ProducesResponseType(400)]
		[ProducesResponseType(500)]
		[HttpGet]
		public async Task<ActionResult> GetVehicleByRegistration(string registration)
		{
			try
			{
				if (registration != null)
				{
					var vehicle = await _vehicleBusiness.GetVehicleByRegistrationAsync(registration);
					if (vehicle!=null)
					{
						return Ok(vehicle);
					}
					else { return NoContent(); }
				}
				else return BadRequest("The required registration field cannot be empty.");
			}
			catch (Exception ex) { return Problem(ex.Message); }
		}

        /// <summary>
        /// Get All the Vehicles by registration
        /// </summary>
        /// <param name="registration"></param>
        /// <returns>Return a List of VehicleOutputDTO</returns>
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(500)]
        [HttpGet]
        public async Task<ActionResult> GetVehicleByRegistrationAutoComplete(string registration)
        {
            try
            {
                if (registration != null)
                {
                    var vehicle = await _vehicleBusiness.GetVehicleByRegistrationAutoCompleteAsync(registration);
                    if (vehicle != null)
                    {
                        return Ok(vehicle);
                    }
                    else { return NoContent(); }
                }
                else return BadRequest("The required registration field cannot be empty.");
            }
            catch (Exception ex) { return Problem(ex.Message); }
        }

		[HttpPut]
		public async Task<ActionResult> DesableVehicle(int idVehcile)
		{
			if(idVehcile > 0)
			{
				try
				{
					await _vehicleBusiness.DesableVehicle(idVehcile);
                    return Ok("Vehicle deleted!");
                }
				catch (Exception ex)
				{
					return Problem(ex.Message);
				}
			}
            else return BadRequest("Enter an id greater than 0.");
        }

        /// <summary>
        /// Delete an existing vehicle by an EngineId entered
        /// </summary>
        /// <param name="id"></param>
        /// <returns>boolean</returns>
        [ProducesResponseType(200)]
		[ProducesResponseType(400)]
		[ProducesResponseType(500)]
		[HttpDelete]
		public async Task<ActionResult> DeleteVehicle(int id)
		{
			try
			{
				if (id > 0)
				{
					await _vehicleBusiness.DeleteVehicleAsync(id);
					return Ok("Vehicle deleted!");
				}
				else return BadRequest("Enter an id greater than 0.");
			}
			catch (Exception ex) { return Problem(ex.Message); }
		}
	}
}


