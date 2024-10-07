using AutoMapper;
using DTOs.VehiclePropertiesDTOs.CreateInput;
using DTOs.VehiclePropertiesDTOs.Output;
using DTOs.VehiclePropertiesDTOs.UpdateInput;
using Entities;
using IBusinesses;
using IRepositories;
using Microsoft.EntityFrameworkCore.Query.SqlExpressions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Businesses
{
    public class CategoryBusiness : ICategoryBusiness
    {
        private readonly ICategoryRepository _categoryRepository;
        private readonly IMapper _mapper;
        public CategoryBusiness(ICategoryRepository categoryRepository, IMapper mapper) 
        {
            _categoryRepository = categoryRepository;
            _mapper = mapper;
        }

        public async Task<CategoryOutputDTO> CreateCategoryAsync(CategoryCreateInputDTO categoryCreateInputDTO)
        {
            var isExists= await _categoryRepository.GetCategoryByLabelAsync(categoryCreateInputDTO.Label);

            if (string.IsNullOrEmpty(categoryCreateInputDTO.Label)) 
            {
                throw new Exception("Category format is not correct.");
            }
            else if (isExists!=null) 
            {
                throw new Exception($"This category {categoryCreateInputDTO.Label} already exists.");
            }
            else 
            {
                Category category = _mapper.Map<Category>(categoryCreateInputDTO);
                return await _categoryRepository.CreateCategoryAsync(category);
            }
        }

        public async Task<List<CategoryOutputDTO>> GetAllCategoriesAsync()
        {
           return await _categoryRepository.GetAllCategoriesAsync();
        }

        public async Task<CategoryOutputDTO> GetCategoryByIdAsync(int id)
        {
            return await _categoryRepository.GetCategoryByIdAsync(id);
        }

        public async Task<CategoryOutputDTO> GetCategoryByLabelAsync(string label)
        {
            if (string.IsNullOrWhiteSpace(label))
            {
                throw new Exception("Category format is not correct.");
            }
            else return await _categoryRepository.GetCategoryByLabelAsync(label);
        }

        public async Task<CategoryOutputDTO> UpdateCategoryAsync(CategoryUpdateInputDTO categoryUpdateInputDTO)
        {
            if (string.IsNullOrWhiteSpace(categoryUpdateInputDTO.Label) || categoryUpdateInputDTO.CategoryId <= 0)
            {
                throw new Exception("Field required empty.");
            }
            else
            {
                var isExists = await _categoryRepository.GetCategoryByLabelAsync(categoryUpdateInputDTO.Label);
                if (isExists != null && isExists.CategoryId == categoryUpdateInputDTO.CategoryId)
                {
                    Category category = new Category { Id = categoryUpdateInputDTO.CategoryId, Label = categoryUpdateInputDTO.Label };
                    return await _categoryRepository.UpdateCategoryAsync(category);
                }
                else if (isExists == null)
                {
                    Category category = _mapper.Map<Category>(categoryUpdateInputDTO);
                    return await _categoryRepository.UpdateCategoryAsync(category);
                }
                else 
                {
                    throw new Exception($"A Category with this label ({categoryUpdateInputDTO.Label}) already exists.");
                }
            }
        }

        public async Task<bool> DeleteCategoryAsync(int categoryId)
        {
            var brand = await _categoryRepository.GetCategoryByIdAsync(categoryId);
            if (brand == null)
            {
                throw new Exception("Can't delete a category that doesn't exist");
            }
            return await _categoryRepository.DeleteCategoryAsync(categoryId);
        }
    }
}
