using DTOs.VehiclePropertiesDTOs.CreateInput;
using DTOs.VehiclePropertiesDTOs.Output;
using DTOs.VehiclePropertiesDTOs.UpdateInput;
using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace IBusinesses
{
	public interface IBrandBusiness
	{
        /// <summary>
        /// Add new Brand if not exists.
        /// </summary>
        /// <param name="brandCreateInputDTO"></param>
        /// <returns>A brand has the following format : BrandOutputDTO.</returns>
        public Task<BrandOutputDTO> CreateBrandAsync(BrandCreateInputDTO brandCreateInputDTO);

        /// <summary>
        /// GetAll Brands if they exists.
        /// </summary>
        /// <returns>The list of brand has the following format : BrandOutputDTO.</returns>
        public Task<List<BrandOutputDTO>> GetAllBrandsAsync();

        /// <summary>
        /// Get the brand corresponding to id if exists.
        /// </summary>
        /// <param name="id"></param>
        /// <returns>A brand has the following format : BrandOutputDTO.</returns>
        public Task<BrandOutputDTO> GetBrandByIdAsync(int id);

        /// <summary>
        /// Get the brand corresponding to label if exists.
        /// </summary>
        /// <param name="label"></param>
        /// <returns>A brand has the following format : BrandOutputDTO.</returns>
        public Task<BrandOutputDTO> GetBrandByLabelAsync(string label);

        /// <summary>
        /// Update the brand if exists.
        /// </summary>
        /// <param name="brandUpdateInputDTO"></param>
        /// <returns>The updated brand has the following format : BrandOutputDTO.</returns>
        public Task<BrandOutputDTO> UpdateBrandAsync(BrandUpdateInputDTO brandUpdateInputDTO);

        /// <summary>
        /// Delete the brand corresponding to id if exists.
        /// </summary>
        /// <param name="brandId"></param>
        /// <returns>True if delete succed of False if unsucced.</returns>
        public Task<bool> DeleteBrandAsync(int brandId);
        
    }
}
