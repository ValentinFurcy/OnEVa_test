using DTOs.Address;
using DTOs.VehiclePropertiesDTOs.CreateInput;
using DTOs.VehiclePropertiesDTOs.UpdateInput;
using IBusinesses;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace OnEVa_API.Controllers
{
    [Authorize]
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class AddressController : ControllerBase
    {
        private readonly IAddressBusiness _addressBusiness;
        public AddressController(IAddressBusiness addressBusiness)
        {
            _addressBusiness = addressBusiness;
        }
        /// <summary>
        /// Create new address
        /// </summary>
        /// <param name="addressCreateInputDTO"></param>
        /// <returns>Return a AddressOutputDTO</returns>
        [ProducesResponseType(200)]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(500)]
        [HttpPost]
        public async Task<ActionResult> CreateAddress(AddressCreateInputDTO addressCreateInputDTO)
        {
            try
            {
                if (addressCreateInputDTO != null)
                {
                    var address = await _addressBusiness.CreateAddressAsync(addressCreateInputDTO);
                    if (address != null)
                    {
                        return Ok(address);
                    }
                    else return NoContent();
                }
                else { return BadRequest("Field required empty"); }
            }
            catch (Exception ex) { return Problem(ex.Message); }
        }

        /// <summary>
        /// Update Address existing
        /// </summary>
        /// <param name="addressUpdateInputDTO"></param>
        /// <returns>The ddress updated => AddressOutputDTO</returns>
		[ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(500)]
        [HttpPut]
        public async Task<ActionResult> UpdateAddress(AddressUpdateInputDTO addressUpdateInputDTO)
        {
            try
            {
                if (addressUpdateInputDTO != null)
                {
                    var address = await _addressBusiness.UpdateAddressAsync(addressUpdateInputDTO);
                    if (address != null)
                    {
                        return Ok(address);
                    }
                    else return NoContent();
                }
                else { return BadRequest("Field required empty"); }
            }
            catch (Exception e)
            {
                return Problem(e.Message);
            }
        }

        /// <summary>
        /// GetAll Addresses if they exists
        /// </summary>
        /// <returns>List AddressOutputDTO </returns>
        [ProducesResponseType(200)]
        [ProducesResponseType(204)]
        [HttpGet]
        public async Task<ActionResult> GetAllAddresses()
        {
            try
            {
                var addresses = await _addressBusiness.GetAllAddressesAsync();
                if (addresses != null)
                {
                    return Ok(addresses);
                }
                else return NoContent();
            }
            catch (Exception ex) { return Problem(ex.Message); }
        }


        /// <summary>
        /// Delete address if exists
        /// </summary>
        /// <param name="addressId"></param>
        /// <returns>True if delete succed of False if unsucced</returns>
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(500)]
        [HttpDelete]
        public async Task<ActionResult<bool>> DeleteAddress(int addressId)
        {
            if (addressId > 0)
            {
                try
                {
                    return await _addressBusiness.DeleteAddressAsync(addressId);
                }
                catch (Exception e)
                {
                    return BadRequest($"ERROR   : {e.Message} ");
                }
            }
            else return BadRequest("Enter an ID greater than 0 ");
        }
    }
}
