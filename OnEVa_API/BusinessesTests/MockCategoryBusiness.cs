using DTOs.VehiclePropertiesDTOs.CreateInput;
using DTOs.VehiclePropertiesDTOs.Output;
using DTOs.VehiclePropertiesDTOs.UpdateInput;
using Entities;
using IBusinesses;
using IRepositories;

namespace BusinessesTests;
public class MockCategoryBusiness : ICategoryBusiness
{
    public async Task<CategoryOutputDTO> CreateCategoryAsync(CategoryCreateInputDTO categoryCreateInputDTO)
    {
        Category categoryDb = new Category { Label = "LabelCategory" };

        if (string.IsNullOrWhiteSpace(categoryCreateInputDTO.Label))
        {
            throw new Exception("Brand format is not correct.");
        }
        else if (categoryCreateInputDTO.Label == categoryDb.Label)
        {
            throw new Exception($"This brand : {categoryCreateInputDTO.Label} already exists.");
        }
        else
        {
            CategoryOutputDTO category = new CategoryOutputDTO { Label = categoryCreateInputDTO.Label };
            return category;
        }
    }

    public Task<bool> DeleteCategoryAsync(int categoryId)
    {
        throw new NotImplementedException();
    }

    public Task<List<CategoryOutputDTO>> GetAllCategoriesAsync()
    {
        throw new NotImplementedException();
    }

    public Task<CategoryOutputDTO> GetCategoryByIdAsync(int id)
    {
        throw new NotImplementedException();
    }

    public Task<CategoryOutputDTO> GetCategoryByLabelAsync(string label)
    {
        throw new NotImplementedException();
    }

    public Task<CategoryOutputDTO> UpdateCategoryAsync(CategoryUpdateInputDTO categoryUpdateInputDTO)
    {
        throw new NotImplementedException();
    }
}
