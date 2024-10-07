using AutoMapper;
using DTOs.VehiclePropertiesDTOs.Output;
using Entities;
using IRepositories;
using Microsoft.EntityFrameworkCore;

namespace Repositories
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly APIDbContext _context;
        private readonly IMapper _mapper;
        public CategoryRepository(APIDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }


        public async Task<CategoryOutputDTO> CreateCategoryAsync(Category category)
        {
            try
            {
                await _context.Categories.AddAsync(category);
                await _context.SaveChangesAsync();

                return await GetCategoryByIdAsync(category.Id);
            }
            catch (Exception)
            {
                throw new Exception("Error ! The new brand was not created");
            }

        }


        public async Task<List<CategoryOutputDTO>> GetAllCategoriesAsync()
        {
            try
            {
                var category = await _context.Categories.ToListAsync();
                var categoriesOutputDTO = _mapper.Map<List<CategoryOutputDTO>>(category);


                if (categoriesOutputDTO.Any())
                {
                    return categoriesOutputDTO;
                }
                else 
                { 
                    return null; 
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<CategoryOutputDTO> GetCategoryByIdAsync(int id)
        {
            try
            {
                var category = await _context.Categories.FindAsync(id);
                if (category == null)
                {
                    return null;
                }
                else
                {
                    CategoryOutputDTO categoryOutputDTO = _mapper.Map<CategoryOutputDTO>(category);
                    return categoryOutputDTO;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<CategoryOutputDTO> GetCategoryByLabelAsync(string label)
        {
            try
            {
                Category category = await _context.Categories.FirstOrDefaultAsync(c => c.Label == label);
                if (category == null)
                {
                    return null;
                }
                else
                {
                    return _mapper.Map< CategoryOutputDTO>(category);
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<CategoryOutputDTO> UpdateCategoryAsync(Category category)
        {
            try
            {
                var nbrRows = await _context.Categories
                    .Where(c => c.Id == category.Id).
                    ExecuteUpdateAsync(updates => updates.SetProperty(c => c.Label, category.Label));

                if (nbrRows > 0)
                {
                    return await GetCategoryByIdAsync(category.Id);
                }
                else
                {
                    throw new Exception("The update failed");
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<bool> DeleteCategoryAsync(int id)
        {
            try
            {
                var categoryToDeleted = await _context.Categories.FindAsync(id);
                _context.Categories.Remove(categoryToDeleted);
                await _context.SaveChangesAsync();
                if (categoryToDeleted != null)
                {
                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

    }
}
