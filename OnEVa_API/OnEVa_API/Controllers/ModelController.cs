using Businesses;
using DTOs.VehiclePropertiesDTOs.CreateInput;
using DTOs.VehiclePropertiesDTOs.Output;
using DTOs.VehiclePropertiesDTOs.UpdateInput;
using IBusinesses;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace OnEVa_API.Controllers;

[Authorize(Roles = "ADMIN")]
[Route("api/[controller]/[action]")]
[ApiController]
public class ModelController : ControllerBase
{
    private readonly IModelBusiness _modelBusiness;

    public ModelController(IModelBusiness modelBusiness)
    {
        _modelBusiness = modelBusiness;
    }

    /// <summary>
    /// Create a new model.
    /// </summary>
    /// <param name="modelCreateInputDTO"></param>
    /// <returns>The ModelOutputDTO created.</returns>
    [HttpPost]
    [ProducesResponseType(200)]
    [ProducesResponseType(204)]
    [ProducesResponseType(400)]
    [ProducesResponseType(500)]

    public async Task<ActionResult> CreateModel(ModelCreateInputDTO modelCreateInputDTO)
    {
        try
        {
            if (modelCreateInputDTO != null)
            {
                var result = await _modelBusiness.CreateModelAsync(modelCreateInputDTO);
                if (result != null)
                {
                    return Ok(result);
                }
                else return NoContent();
            }
            else { return BadRequest("Field requiered empty"); }
        }
        catch (Exception ex) { return Problem(ex.Message); }
    }

    /// <summary>
    /// Get all models.
    /// </summary>
    /// <returns>The List of ModelOutputDTO created.</returns>
    [HttpGet]
    [ProducesResponseType(200)]
    [ProducesResponseType(204)]
    public async Task<ActionResult<List<ModelOutputDTO>>> GetAllModels()
    {
        try
        {
            var models = await _modelBusiness.GetAllModelsAsync();
            if (models.Any())
            {
                return Ok(models);
            }
            else { return NoContent(); }
        }catch (Exception ex) { return Problem(ex.Message); }
    }

    /// <summary>
    /// Get a model by EngineId.
    /// </summary>
    /// <param name="id"></param>
    /// <returns>A ModelOutputDTO.</returns>
    [HttpGet]
    [ProducesResponseType(400)]
    [ProducesResponseType(204)]
    [ProducesResponseType(200)]
    [ProducesResponseType(500)]
    public async Task<ActionResult<ModelOutputDTO>> GetModelById(int id)
    {
        if (id <= 0)
        {
            return BadRequest("The id entered must be greater than 0.");
        }
        else
        {
            try
            {
                var model = await _modelBusiness.GetModelByIdAsync(id);
                if (model == null)
                {
                    return NoContent();
                }
                else { return Ok(model); }
            }
            catch (Exception ex) { return Problem(ex.Message); }
        }
    }

    /// <summary>
    /// Get a model by label
    /// </summary>
    /// <param name="label"></param>
    /// <returns>A ModelOutputDTO.</returns>
    [HttpGet]
    [ProducesResponseType(400)]
    [ProducesResponseType(204)]
    [ProducesResponseType(200)]
    [ProducesResponseType(500)]
    public async Task<ActionResult<ModelOutputDTO>> GetModelByLabel(string label)
    {
        if (label == null)
        {
            return BadRequest("Field requiered is empty.");
        }
        else
        {
            try
            {
                var model = await _modelBusiness.GetModelByLabelAsync(label);
                if (model == null)
                {
                    return NoContent();
                }
                else { return Ok(model); }
            }
            catch (Exception ex) { return BadRequest(ex.Message); }
        }
    }

    /// <summary>
    /// Update a model.
    /// </summary>
    /// <param name="modelUpdateInputDTO"></param>
    /// <returns>A ModelOutputDTO updated. </returns>
    [HttpPut]
    [ProducesResponseType(400)]
    [ProducesResponseType(204)]
    [ProducesResponseType(200)]
    [ProducesResponseType(500)]
    public async Task<ActionResult<ModelOutputDTO>> UpdateModelAsync(ModelUpdateInputDTO modelUpdateInputDTO)
    {
        try
        {
            if (modelUpdateInputDTO != null)
            {
                var brand = await _modelBusiness.UpdateModelAsync(modelUpdateInputDTO);
                return Ok(brand);
            }
            else { return BadRequest("Field required empty"); }
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    /// <summary>
    /// Delete a model.
    /// </summary>
    /// <param name="modelId"></param>
    /// <returns>A boolean answer</returns>
    [HttpDelete]
    [ProducesResponseType(200)]
    [ProducesResponseType(400)]
    [ProducesResponseType(500)]

    public async Task<ActionResult<bool>> DeleteModelAsync(int modelId)
    {
        if (modelId <= 0)
        {
            return BadRequest("The id entered must be greater than 0.");
        }
        else
        {
            try
            {
                return await _modelBusiness.DeleteModelAsync(modelId);
            }
            catch (Exception ex) { return Problem(ex.Message); }
        }
    }
}
