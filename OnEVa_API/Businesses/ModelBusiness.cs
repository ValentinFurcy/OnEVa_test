using DTOs.VehiclePropertiesDTOs.CreateInput;
using DTOs.VehiclePropertiesDTOs.Output;
using DTOs.VehiclePropertiesDTOs.UpdateInput;
using Entities;
using IBusinesses;
using IRepositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Businesses
{
    public class ModelBusiness : IModelBusiness
    {
        private readonly IModelRepository _modelRepository;

        public ModelBusiness(IModelRepository modelRepository)
        {
            _modelRepository = modelRepository;
        }

        public async Task<ModelOutputDTO> CreateModelAsync(ModelCreateInputDTO modelCreateInputDTO)
        {
            var isExists = await _modelRepository.GetModelByLabelAsync(modelCreateInputDTO.Label);

            if (string.IsNullOrEmpty(modelCreateInputDTO.Label))
            {
                throw new Exception("Category format is not correct.");
            }
            else if (isExists != null)
            {
                throw new Exception($"This model {modelCreateInputDTO.Label} already exists.");
            }
            else
            {
                Model model = new Model { Label = modelCreateInputDTO.Label };
                return await _modelRepository.CreateModelAsync(model);
            }
        }

        public async Task<List<ModelOutputDTO>> GetAllModelsAsync()
        {
            return await _modelRepository.GetAllModelsAsync();
        }

        public async Task<ModelOutputDTO> GetModelByIdAsync(int id)
        {
            return await _modelRepository.GetModelByIdAsync(id);
        }

        public async Task<ModelOutputDTO> GetModelByLabelAsync(string label)
        {
            if (string.IsNullOrWhiteSpace(label))
            {
                throw new Exception("Model format is not correct.");
            }
            else return await _modelRepository.GetModelByLabelAsync(label);
        }

        public async Task<ModelOutputDTO> UpdateModelAsync(ModelUpdateInputDTO modelUpdateInputDTO)
        {
            if (string.IsNullOrWhiteSpace(modelUpdateInputDTO.Label) || modelUpdateInputDTO.ModelId <= 0)
            {
                throw new Exception("Field Requiered is empty.");
            }
            else
            {
                var isExists = await _modelRepository.GetModelByLabelAsync(modelUpdateInputDTO.Label);
                if (isExists != null && isExists.ModelId == modelUpdateInputDTO.ModelId)
                {
                    Model model = new Model { Id = modelUpdateInputDTO.ModelId, Label = modelUpdateInputDTO.Label };
                    return await _modelRepository.UpdateModelAsync(model);
                }
                else if (isExists == null)
                {
                    Model model = new Model { Id = modelUpdateInputDTO.ModelId, Label = modelUpdateInputDTO.Label };
                    return await _modelRepository.UpdateModelAsync(model);
                }
                else
                {
                    throw new Exception($"A Model with this label ({modelUpdateInputDTO.Label}) already exists.");
                }
            }
        }

        public async Task<bool> DeleteModelAsync(int modelId)
        {
            var model = await _modelRepository.GetModelByIdAsync(modelId);
            if (model == null)
            {
                throw new Exception("Can't delete a model that doesn't exist");
            }
            else { return await _modelRepository.DeleteModelAsync(modelId); }
        }

    }
}
