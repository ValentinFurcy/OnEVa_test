using DTOs.VehiclePropertiesDTOs.Output;
using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IRepositories
{
    public interface IBrandRepository
    {
        /// <summary>
        /// Add new Brand if not exists.
        /// </summary>
        /// <param name="brand"></param>
        /// <returns>A brand has the following format : BrandOutputDTO.</returns>
        public Task<BrandOutputDTO> CreateBrandAsync(Brand brand);

        /// <summary>
        /// GetAll Brands if they exists.
        /// </summary>
        /// <returns>The list of brand has the following format : BandOutputDTO.</returns>
        public Task<List<BrandOutputDTO>> GetAllBrandsAsync();

        /// <summary>
        /// Get the brand corresponding to id if exists.
        /// </summary>
        /// <param name="brandId"></param>
        /// <returns>A brand has the following format : BrandOutputDTO.</returns>
        public Task<BrandOutputDTO> GetBrandByIdAsync(int brandId);

        /// <summary>
        /// Get the brand corresponding to label if exists
        /// </summary>
        /// <param name="label"></param>
        /// <returns>A brand has the following format : BrandOutputDTO.</returns>
        public Task<BrandOutputDTO> GetBrandByLabelAsync(string label);

        /// <summary>
        /// Update the brand if exists.
        /// </summary>
        /// <param name="brand"></param>
        /// <returns>The updated brand has the following format : BrandOutputDTO.</returns>
        public Task<BrandOutputDTO> UpdateBrandAsync(Brand brand);

        /// <summary>
        /// Delete the Brand if exists
        /// </summary>
        /// <param name="brandId"></param>
        /// <returns>True if delete succed of False if unsucced.</returns>
        public Task<bool> DeleteBrandAsync(int brandId);


	}
}
