using DTOs.VehiclePropertiesDTOs.Output;
using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IRepositories
{
    public interface IModelRepository
    {
        /// <summary>
        /// Add a new model.
        /// </summary>
        /// <param name="model"></param>
        /// <returns>A model has the following format : ModelOutputDTO.</returns>
        public Task<ModelOutputDTO> CreateModelAsync(Model model);

        /// <summary>
        /// Get all models.
        /// </summary>
        /// <returns>The list of models has the following format : ModelOutputDTO.</returns>
        public Task<List<ModelOutputDTO>> GetAllModelsAsync();

        /// <summary>
        /// Get the model corresponding to id.
        /// </summary>
        /// <param name="id"></param>
        /// <returns>A model has the following format : ModelOutputDTO.</returns>
        public Task<ModelOutputDTO> GetModelByIdAsync(int id);

        /// <summary>
        /// Get the model corresponding to Label.
        /// </summary>
        /// <param name="label"></param>
        /// <returns>A model has the following format : ModelOutputDTO.</returns>
        public Task<ModelOutputDTO> GetModelByLabelAsync(string label);

        /// <summary>
        /// Update an existing model.
        /// </summary>
        /// <param name="model"></param>
        /// <returns>The updated model has the following format : ModelOutputDTO.</returns>
        public Task<ModelOutputDTO> UpdateModelAsync(Model model);

        /// <summary>
        /// Delete model if exists.
        /// </summary>
        /// <param name="modelId"></param>
        /// <returns>True if delete succed of False if unsucced.</returns>
        public Task<bool> DeleteModelAsync(int modelId);
    }
}
