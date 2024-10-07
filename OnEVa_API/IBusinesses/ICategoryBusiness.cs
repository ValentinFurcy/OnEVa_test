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
    public interface ICategoryBusiness
    {
        /// <summary>
        /// Add a new category.
        /// </summary>
        /// <param name="categoryCreateInputDTO"></param>
        /// <returns>A category has the following format : CategoryOutputDTO.</returns>
        public Task<CategoryOutputDTO> CreateCategoryAsync(CategoryCreateInputDTO categoryCreateInputDTO);

        /// <summary>
        /// Get all categories.
        /// </summary>
        /// <returns>The list of categories has the following format : CategoryOutputDTO.</returns>
        public Task<List<CategoryOutputDTO>> GetAllCategoriesAsync();

        /// <summary>
        /// Get the category corresponding to the id.
        /// </summary>
        /// <param name="id"></param>
        /// <returns>A category has the following format : CategoryOutputDTO.</returns>
        public Task<CategoryOutputDTO> GetCategoryByIdAsync(int id);

        /// <summary>
        /// Get the category corresponding to the label.
        /// </summary>
        /// <param name="label"></param>
        /// <returns>A category has the following format : CategoryOutputDTO.</returns>
        public Task<CategoryOutputDTO> GetCategoryByLabelAsync(string label);

        /// <summary>
        /// Update an existing category.
        /// </summary>
        /// <param name="categoryUpdateInputDTO"></param>
        /// <returns>The updated category has the following format : CategoryOutputDTO.</returns>
        public Task<CategoryOutputDTO> UpdateCategoryAsync(CategoryUpdateInputDTO categoryUpdateInputDTO);

        /// <summary>
        /// Deleted category if existes.
        /// </summary>
        /// <param name="categoryId"></param>
        /// <returns>True if delete succed of False if unsucced.</returns>
        public Task<bool> DeleteCategoryAsync(int categoryId);
    }
}
