using DTOs.VehiclePropertiesDTOs.Output;
using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IRepositories
{
    public interface ICategoryRepository
    {
        /// <summary>
        /// Add a new category.
        /// </summary>
        /// <param name="category"></param>
        /// <returns>A category has the following format : CategoryOutputDTO.</returns>
        public Task<CategoryOutputDTO> CreateCategoryAsync(Category category);

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
        /// <param name="id"></param>
        /// <returns>A category has the following format : CategoryOutputDTO.</returns>
        public Task<CategoryOutputDTO> GetCategoryByLabelAsync(string label);

        /// <summary>
        /// Update an existing category.
        /// </summary>
        /// <param name="category"></param>
        /// <returns>The updated category has the following format : CategoryOutputDTO.</returns>
        public Task<CategoryOutputDTO> UpdateCategoryAsync(Category category);

        /// <summary>
        /// Deleted category if existes.
        /// </summary>
        /// <param name="categoryId"></param>
        /// <returns>True if delete succed of False if unsucced.</returns>
        public Task<bool> DeleteCategoryAsync(int categoryId);

    }
}
