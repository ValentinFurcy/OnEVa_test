using DTOs.VehiclePropertiesDTOs.Output;
using Entities;
using IRepositories;
using Microsoft.EntityFrameworkCore;

namespace Repositories
{
    public class ModelRepository : IModelRepository
    {
        private readonly APIDbContext _context;
        public ModelRepository(APIDbContext context)
        {
            _context = context;
        }

        public async Task<ModelOutputDTO> CreateModelAsync(Model model)
        {
            try
            {
                await _context.Models.AddAsync(model);
                await _context.SaveChangesAsync();

                return await GetModelByIdAsync(model.Id);
            }
            catch (Exception)
            {
                throw new Exception("Error ! The new brand was not created");
            }
        }

        public async Task<List<ModelOutputDTO>> GetAllModelsAsync()
        {
            try
            {
                var modelOutputDTOs = new List<ModelOutputDTO>();

                var models = await _context.Models
                    .Select(m => new ModelOutputDTO { ModelId = m.Id, Label = m.Label })
                    .ToListAsync();

                if (models.Any())
                {
                    return models;
                }
                else { return null; }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<ModelOutputDTO> GetModelByIdAsync(int id)
        {
            try
            {
                var model = await _context.Models.FindAsync(id);
                if (model == null)
                {
                    return null;
                }
                else
                {
                    ModelOutputDTO modelOutputDTO = new ModelOutputDTO { ModelId = model.Id, Label = model.Label };
                    return modelOutputDTO;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<ModelOutputDTO> GetModelByLabelAsync(string label)
        {
            try
            {
                Model model = await _context.Models.FirstOrDefaultAsync(m => m.Label == label);
                if (model == null)
                {
                    return null;
                }
                else
                {
                    ModelOutputDTO modelOutputDTO = new ModelOutputDTO { ModelId = model.Id, Label = model.Label };
                    return modelOutputDTO;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

        }

        public async Task<ModelOutputDTO> UpdateModelAsync(Model model)
        {
            try
            {
                var nbRows = await _context.Models.Where(m => m.Id == model.Id).ExecuteUpdateAsync(
                    updates => updates.SetProperty(b => b.Label, model.Label));
                if (nbRows > 0)
                {
                    return await GetModelByIdAsync(model.Id);
                }
                throw new Exception("The update unsucced");
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }

        }

        public async Task<bool> DeleteModelAsync(int id)
        {
            try
            {
                var modelToDelete = await _context.Models.FindAsync(id);
                _context.Models.Remove(modelToDelete);
                await _context.SaveChangesAsync();
                if (modelToDelete != null)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
