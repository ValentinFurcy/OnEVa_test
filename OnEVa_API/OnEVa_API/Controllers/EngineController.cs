using AutoMapper;
using Businesses;
using DTOs.VehiclePropertiesDTOs.CreateInput;
using DTOs.VehiclePropertiesDTOs.Output;
using DTOs.VehiclePropertiesDTOs.UpdateInput;
using IBusinesses;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace OnEVa_API.Controllers
{
    [Authorize(Roles = "ADMIN")]
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class EngineController : ControllerBase
    {

        private readonly IEngineBusiness _engineBusiness;

        public EngineController(IEngineBusiness engineBusiness)
        {
            _engineBusiness = engineBusiness;
        }

        /// <summary>
        /// Create new engine.
        /// </summary>
        /// <param name="engineCreateInputDTO"></param>
        /// <returns>The new engine created as a EngineOutputDTO.</returns>
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(500)]
        [HttpPost]
        public async Task<ActionResult> CreateEngine(EngineCreateInputDTO engineCreateInputDTO)
        {
            try
            {
                if (engineCreateInputDTO != null)
                {
                    var result = await _engineBusiness.CreateEngineAsync(engineCreateInputDTO);
                    if (result != null)
                    {
                        return Ok(result);
                    }
                    else return NoContent();
                }
                else { return BadRequest("Field required empty"); }
            }
            catch (Exception ex) { return Problem(ex.Message); }
        }

        /// <summary>
        /// Update an existing engine.
        /// </summary>
        /// <param name="engineUpdateInputDTO"></param>
        /// <returns>The new engine updated as a EngineOutputDTO.</returns>
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(500)]
        [HttpPut]
        public async Task<ActionResult> UpdateEngine(EngineUpdateInputDTO engineUpdateInputDTO)
        {
            try
            {
                if (engineUpdateInputDTO != null)
                {
                    var result = await _engineBusiness.UpdateEngineAsync(engineUpdateInputDTO);
                    if (result != null)
                    {
                        return Ok(result);
                    }
                    else return NoContent();
                }
                else { return BadRequest("Field required empty"); }
            }
            catch (Exception ex) { return BadRequest(ex.Message); }
        }


        /// <summary>
        /// Get all the existing engines.
        /// </summary>
        /// <returns>The List of engines as a list of EngineOutpuntDTO.</returns>
        [ProducesResponseType(200)]
        [ProducesResponseType(204)]
        [ProducesResponseType(500)]
        [HttpGet]
        public async Task<ActionResult> GetAllEngines()
        {
            try
            {
                var engines = await _engineBusiness.GetAllEnginesAsync();
                if (engines.Any())
                {
                    return Ok(engines);
                }
                else return NoContent();
            }
            catch (Exception ex) { return Problem($"{ex.Message}"); }
        }

        /// <summary>
        /// Get the engine by id if exists.
        /// </summary>
        /// <param name="engineId"></param>
        /// <returns>The engine as a EngineOutpuntDTO.</returns>
        [ProducesResponseType(200)]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(500)]
        [HttpGet]

        public async Task<ActionResult> GetEngineById(int engineId)
        {
            if (engineId <= 0)
            {
                return BadRequest("Enter an ID greater than 0");
            }
            else
            {
                try
                {
                    var engine = await _engineBusiness.GetEngineByIdAsync(engineId);
                    if (engine != null)
                    {
                        return Ok(engine);
                    }
                    else return NoContent();
                }catch (Exception ex) { return Problem(ex.Message); }
            }
        }


        /// <summary>
        /// Get the engine by label if exists.
        /// </summary>
        /// <param name="label"></param>
        /// <returns>The engine as a EngineOutpuntDTO.</returns>
        [ProducesResponseType(200)]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(500)]
        [HttpGet]
        public async Task<ActionResult> GetEngineByLabel(string label)
        {
            if (label != null)
            {
                try
                {
                    var engine = await _engineBusiness.GetEngineByLabelAsync(label);
                    if (engine != null)
                    {
                        return Ok(engine);
                    }
                    else return NoContent();
                }
                catch (Exception e)
                {
                    return Problem($"ERROR : {e.Message}");
                }
            }
            else
            {
                return BadRequest("Field required empty");
            }
        }


        /// <summary>
        /// Delete an engine if exists.
        /// </summary>
        /// <param name="engineId"></param>
        /// <returns>True if delete succed of False if unsucced.</returns>
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(500)]
        [HttpDelete]
        public async Task<ActionResult<bool>> DeleteEngine(int engineId)
        {
            if (engineId > 0)
            {
                try
                {
                    return await _engineBusiness.DeleteEngineAsync(engineId);
                }
                catch (Exception e)
                {
                    return Problem($"ERROR   : {e.Message} ");
                }
            }
            else return BadRequest("Enter an ID greater than 0 ");
        }
    }
}
