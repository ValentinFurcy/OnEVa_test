using DTOs.RentalDTO;
using Entities;
using IBusinesses;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace OnEVa_API.Controllers
{
    [Authorize]
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class RentalController : ControllerBase
    {
        private readonly IRentalBusiness _rentalBusiness;
        private readonly UserManager<AppUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        public RentalController(IRentalBusiness rentalBusiness, UserManager<AppUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            _rentalBusiness = rentalBusiness;
            _userManager = userManager;
            _roleManager = roleManager;
        }

        /// <summary>
        /// Create a new rental.
        /// </summary>
        /// <param name="rentalCreateInputDTO"></param>
        /// <returns>The new rental created as a RentalOutputDTO.</returns>
        [ProducesResponseType(200)]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(500)]
        [Authorize]
        [HttpPost]
        public async Task<ActionResult> CreateRental(RentalCreateInputDTO rentalCreateInputDTO)
        {
            try
            {
                var user = await _userManager.GetUserAsync(User);
                var userId = user.Id;

                if (rentalCreateInputDTO != null)
                {
                    var result = await _rentalBusiness.CreateRentalAsync(rentalCreateInputDTO, userId);
                    if (result != null)
                    {
                        return Ok(result);
                    }
                    else return NoContent();
                }
                else { return BadRequest("Field required empty"); }
            }
            catch (Exception ex) { return Problem(ex.Message); }
        }

        /// <summary>
        /// Update an existing rental by a new one entered by user.
        /// </summary>
        /// <param name="rentalUpdateInputDTO"></param>
        /// <returns>The rentals as a RentalOutputDTO.</returns>
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(500)]
        [Authorize]
        [HttpPut]
        public async Task<ActionResult> UpdateRental(RentalUpdateInputDTO rentalUpdateInputDTO)
        {
            try
            {
                var user = await _userManager.GetUserAsync(User);
                var userId = user.Id;

                if (rentalUpdateInputDTO != null)
                {
                    var rentalUpdated = await _rentalBusiness.UpdateRentalAsync(rentalUpdateInputDTO, userId);
                    return Ok(rentalUpdated);
                }
                else { return BadRequest("Field required empty"); }
            }
            catch (Exception ex) { return Problem(ex.Message); }
        }


        /// <summary>
        /// Get all the existing rentals.
        /// </summary>
        /// <returns>The list of rentals as a list of RentalOutputDTO. </returns>
        [ProducesResponseType(200)]
        [ProducesResponseType(204)]
        [ProducesResponseType(500)]
        [HttpGet]
        public async Task<ActionResult> GetAllRentals()
        {
            try
            {
                var rentals = await _rentalBusiness.GetAllRentalsAsync();
                if (rentals != null)
                {
                    return Ok(rentals);
                }
                else return NoContent();
            }
            catch (Exception ex) { return Problem(ex.Message); }
        }


        /// <summary>
        /// Get all the existing rentals dates.
        /// </summary>
        /// <param name="startDate, endDate"></param>
        /// <returns>The list of rentals as a list of RentalOutputDTO.</returns>
        [ProducesResponseType(200)]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(500)]
        [HttpGet]

        public async Task<ActionResult> GetAllRentalsByDates(DateTime startDate, DateTime? endDate)
        {
            try
            {
                if (startDate != null)
                {
                    var result = await _rentalBusiness.GetAllRentalsByDatesAsync(startDate, endDate);
                    if (result != null)
                        return Ok(result);
                    else return NoContent();
                }
                else return BadRequest("startDate required");
            }
            catch (Exception ex) { return Problem(ex.Message); }
        }

        /// <summary>
        /// Get all the existing rentals by state.
        /// </summary>
        /// <param name="stateId"></param>
        /// <returns>Return a List of RentalOutputDTO</returns>
        [ProducesResponseType(200)]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(500)]
        [HttpGet]

        public async Task<ActionResult> GetAllRentalsByState(int stateId)
        {
            if (stateId <= 0)
                return BadRequest();
            else
            {
                try
                {
                    var result = await _rentalBusiness.GetAllRentalsByStateAsync(stateId);
                    if (result != null) return Ok(result);
                    else
                        return NoContent();
                }
                catch (Exception ex) { return Problem(ex.Message); }
            }
        }

        /// <summary>
        /// Get all the existing rentals by person.
        /// </summary>
        /// <param name="personId"></param>
        /// <returns>The list of rentals as a list of RentalOutputDTO.</returns>
        [ProducesResponseType(200)]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(500)]
        [HttpGet]
        public async Task<ActionResult> GetAllRentalsByPerson(int personId)
        {
            if (personId <= 0)
            {
                return BadRequest("Enter an ID greater than 0");
            }
            else
            {
                try
                {
                    var rental = await _rentalBusiness.GetAllRentalsByPersonAsync(personId);
                    if (rental != null)
                    {
                        return Ok(rental);
                    }
                    else
                    {
                        return NoContent();
                    }
                }
                catch (Exception ex) { return Problem(ex.Message); }
            }
        }


        /// <summary>
        /// Get all the existing rentals with InGoing status and by person.
        /// </summary>
        /// <param name="personId"></param>
        /// <returns>The list of rentals with InGoing status as a list of RentalOutputDTO.</returns>
        [ProducesResponseType(200)]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(500)]
        [HttpGet]
        public async Task<ActionResult> GetAllRentalsByPersonAndByStatusInGoingAsync(int personId)
        {
            if (personId <= 0)
            {
                return BadRequest("Enter an ID greater than 0");
            }
            else
            {
                try
                {
                    var rental = await _rentalBusiness.GetAllRentalsByPersonAndByOnGoingStateAsync(personId);
                    if (rental != null)
                    {
                        return Ok(rental);
                    }
                    else
                    {
                        return NoContent();
                    }
                }
                catch (Exception ex) { return Problem(ex.Message); }
            }
        }


        /// <summary>
        /// Get all the existing rentals by person ordered by state then by StartDate.
        /// </summary>
        /// <param name="personId"></param>
        /// <returns>The list of rentals as a list of RentalOutputDTO ordered by state then by StartDate.</returns>
        [ProducesResponseType(200)]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(500)]
        [HttpGet]
        public async Task<ActionResult> GetRentalByPersonandByStateOrderedByDate(int personId, int stateId)
        {
            if (personId <= 0)
            {
                return BadRequest("Enter an ID greater than 0");
            }
            else
            {
                try
                {
                    var rental = await _rentalBusiness.GetRentalByPersonandByStateOrderedByDate(personId, stateId);
                    if (rental != null)
                    {
                        return Ok(rental);
                    }
                    else
                    {
                        return NoContent();
                    }
                }
                catch (Exception ex) { return Problem(ex.Message); }
            }
        }


        /// <summary>
        /// Get all the existing rentals by registration.
        /// </summary>
        /// <param name="registration"></param>
        /// <returns>The list of rentals as a list of RentalOutputDTO.</returns>
        [ProducesResponseType(200)]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(500)]
        [HttpGet]

        public async Task<ActionResult> GetAllRentalsByRegistration(string registration)
        {
            if (registration != null)
            {
                try
                {
                    var result = await _rentalBusiness.GetAllRentalsByRegistrationAsync(registration);
                    if (result != null)
                    {
                        return Ok(result);
                    }
                    else
                    {
                        return NoContent();
                    }
                }
                catch (Exception ex) { return Problem(ex.Message); }
            }
            else return BadRequest("The registration field is required.");
        }

        //SARAH
        /// <summary>
        /// Get the existing rental by id.
        /// </summary>
        /// <param name="id"></param>
        /// <returns>The rental as a RentalOutputDTO.</returns>
        [ProducesResponseType(200)]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(500)]
        [HttpGet]

        public async Task<ActionResult> GetRentalById(int id)
        {
            if (id <= 0)
            {
                return BadRequest("Enter an ID greater than 0");
            }
            else
            {
                try
                {
                    var rental = await _rentalBusiness.GetRentalByIdAsync(id);
                    if (rental != null)
                    {
                        return Ok(rental);
                    }
                    else return NoContent();
                }
                catch (Exception ex) { return Problem(ex.Message); }
            }
        }


        /// <summary>
        /// Delete rental if exists.
        /// </summary>
        /// <param name="id"></param>
        /// <returns>True if delete succed of False if unsucced</returns>
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(500)]
        [HttpDelete]
        [Authorize]
        public async Task<ActionResult<bool>> DeleteRental(int id)
        {
            if (id > 0)
            {
                try
                {
                    var user = await _userManager.GetUserAsync(User);
                    var userId = user.Id;

                    return await _rentalBusiness.DeleteRentalAsync(id, userId);
                }
                catch (Exception ex)
                {
                    return Problem(ex.Message);
                }
            }
            else return BadRequest("Enter an ID gretaer than 0");
        }
    }
}