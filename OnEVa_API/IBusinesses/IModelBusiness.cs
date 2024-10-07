using DTOs.VehiclePropertiesDTOs.CreateInput;
using DTOs.VehiclePropertiesDTOs.Output;
using DTOs.VehiclePropertiesDTOs.UpdateInput;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IBusinesses
{
    public interface IModelBusiness
    {
        /// <summary>
        /// Add a new model.
        /// </summary>
        /// <param name="modelCreateInputDTO"></param>
        /// <returns>A model has the following format : ModelOutputDTO.</returns>
        public Task<ModelOutputDTO> CreateModelAsync(ModelCreateInputDTO modelCreateInputDTO);

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
        /// Get the model corresponding to label.
        /// </summary>
        /// <param name="label"></param>
        /// <returns>A model has the following format : ModelOutputDTO.</returns>
        public Task<ModelOutputDTO> GetModelByLabelAsync(string label);

        /// <summary>
        /// Update an existing model.
        /// </summary>
        /// <param name="modelUpdateInputDTO"></param>
        /// <returns> The updated model has the following format : ModelOutputDTO.</returns>
        public Task<ModelOutputDTO> UpdateModelAsync(ModelUpdateInputDTO modelUpdateInputDTO);

        /// <summary>
        /// Delete model if exists.
        /// </summary>
        /// <param name="modelId"></param>
        /// <returns>True if delete succed of False if unsucced.</returns>
        public Task<bool> DeleteModelAsync(int modelId);
    }
}
