using DTOs.VehiclePropertiesDTOs.CreateInput;
using DTOs.VehiclePropertiesDTOs.Output;
using DTOs.VehiclePropertiesDTOs.UpdateInput;
using Entities.CONST;
using IBusinesses;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace OnEVa_API.Controllers
{
    [Authorize(Roles = "ADMIN")]
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class BrandController : ControllerBase
    {
        private readonly IBrandBusiness _brandBusiness;
        public BrandController(IBrandBusiness brandBusiness)
        {
            _brandBusiness = brandBusiness;
        }

        /// <summary>
        /// Create new brand.
        /// </summary>
        /// <param name="brandCreateInputDTO"></param>
        /// <returns>The new brand created as a BrandOutputDTO.</returns>
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(500)]
        [HttpPost]
        public async Task<ActionResult> CreateBrand(BrandCreateInputDTO brandCreateInputDTO)
        {
            try
            {
                if (brandCreateInputDTO != null)
                {
                    var brand = await _brandBusiness.CreateBrandAsync(brandCreateInputDTO);
                    return Ok(brand);
                }
                else { return BadRequest("Field required empty"); }
            }
            catch (Exception ex) { return Problem(ex.Message); }
        }

        /// <summary>
        /// Update an existing brand.
        /// </summary>
        /// <param name="brandUpdateInputDTO"></param>
        /// <returns>The new brand updated as a BrandOutputDTO.</returns>
		[ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(500)]
        [HttpPut]
        public async Task<ActionResult> UpdateBrand(BrandUpdateInputDTO brandUpdateInputDTO)
        {
            try
            {
                if (brandUpdateInputDTO != null)
                {
                    var brand = await _brandBusiness.UpdateBrandAsync(brandUpdateInputDTO);
                    return Ok(brand);
                }
                else { return BadRequest("Field required empty"); }
            }
            catch (Exception e)
            {
                return Problem(e.Message);
            }
        }

        /// <summary>
        /// Get all the existing brands.
        /// </summary>
        /// <returns>The List of brands as a list of BrandOutpuntDTO.</returns>
        [ProducesResponseType(200)]
        [ProducesResponseType(204)]
        [ProducesResponseType(500)]
        [HttpGet]
        public async Task<ActionResult> GetAllBrands()

        {
            var brands = await _brandBusiness.GetAllBrandsAsync();
            if (brands != null)
            {
                return Ok(brands);
            }
            else return NoContent();
        }

        /// <summary>
        /// Get the brand by id if exists.
        /// </summary>
        /// <param name="brandId"></param>
        /// <returns>The brand as a BrandOutpuntDTO.</returns>
        [ProducesResponseType(200)]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(500)]
        [HttpGet]
        public async Task<ActionResult> GetBrandById(int brandId)
        {

            if (brandId <= 0)
            {
                return BadRequest("Enter an ID greater than 0");
            }
            else
            {
                try
                {
                    var brand = await _brandBusiness.GetBrandByIdAsync(brandId);
                    if (brand != null)
                    {
                        return Ok(brand);
                    }
                    else return NoContent();
                }
                catch (Exception e) { return Problem(e.Message); }
            }
        }

        /// <summary>
        /// Get the brand by label if exists.
        /// </summary>
        /// <param name="label"></param>
        /// <returns>The brand as a BrandOutpuntDTO.</returns>
        [ProducesResponseType(200)]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(500)]
        [HttpGet]
        public async Task<ActionResult> GetBrandByLabel(string label)
        {
            if (label != null)
            {
                try
                {
                    var brand = await _brandBusiness.GetBrandByLabelAsync(label);
                    if (brand != null)
                    {
                        return Ok(brand);
                    }
                    else return NoContent();
                }
                catch (Exception e)
                {
                    return Problem($"ERROR : {e.Message}");
                }
            }
            else
            {
                return BadRequest("Field required empty");
            }
        }

        /// <summary>
        /// Delete a brand if exists.
        /// </summary>
        /// <param name="brandId"></param>
        /// <returns>True if delete succed of False if unsucced.</returns>
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(500)]
        [HttpDelete]
        public async Task<ActionResult<bool>> DeleteBrand(int brandId)
        {
            if (brandId > 0)
            {
                try
                {
                    return await _brandBusiness.DeleteBrandAsync(brandId);
                }
                catch (Exception e)
                {
                    return Problem($"ERROR   : {e.Message} ");
                }
            }
            else return BadRequest("Enter an ID greater than 0 ");
        }
    }
}
