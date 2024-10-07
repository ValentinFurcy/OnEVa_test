using DTOs.StateDTOs;
using IBusinesses;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace OnEVa_API.Controllers
{
    [Authorize]
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class StateController : ControllerBase
    {
        private readonly IStateBusiness _stateBusiness;

        public StateController(IStateBusiness stateBusiness)
        {
            _stateBusiness = stateBusiness;
        }

        /// <summary>
        /// Create new state
        /// </summary>
        /// <param name="stateInputDTO"></param>
        /// <returns>Return an StateOutputDTO</returns>
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(500)]
        [HttpPost]
        public async Task<ActionResult> CreateState(StateCreateInputDTO stateInputDTO)
        {
            try
            {
                if (stateInputDTO != null)
                {
                    var result = await _stateBusiness.CreateStateAsync(stateInputDTO);
                    return Ok(result);
                }
                else { return BadRequest("Field required empty"); }
            }
            catch (Exception ex) { return Problem(ex.Message); }
        }

        /// <summary>
        /// Get all the existing states
        /// </summary>
        /// < returns > Return an StateOutputDTO</returns>
        [ProducesResponseType(200)]
        [ProducesResponseType(204)]
        [ProducesResponseType(500)]
        [HttpGet]
        public async Task<ActionResult> GetAllStates()
        {
            try
            {
                var states = await _stateBusiness.GetAllStatesAsync();
                if (states != null)
                {
                    return Ok(await _stateBusiness.GetAllStatesAsync());
                }
                else
                {
                    return NoContent();
                }
            }
            catch (Exception ex) { return Problem(ex.Message); }
        }

        /// <summary>
        /// Get the existing state by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Return an StateOutputDTO</returns>
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(500)]
        [HttpGet]

        public async Task<ActionResult> GetStateById(int id)
        {
            if (id <= 0)
            {
                return BadRequest("Enter an ID greater than 0");
            }
            else
            {
                try
                {
                    var state = await _stateBusiness.GetStateByIdAsync(id);
                    if (state != null)
                    {
                        return Ok(state);
                    }
                    else return NoContent();
                }
                catch (Exception ex) { return Problem(ex.Message); }
            }
        }

        /// <summary>
        /// Update an existing statee by a new one entered by user
        /// </summary>
        /// <param name="stateInputDTO"></param>
        /// <returns>Return an EngineOutputDTO</returns>
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(500)]
        [HttpPut]
        public async Task<ActionResult> UpdateState(StateUpdateInputDTO stateInputDTO)
        {
            if (stateInputDTO == null)
            {
                return BadRequest("Field required empty");
            }
            else
            {
                try
                {       
                    var state = await _stateBusiness.UpdateStateAsync(stateInputDTO);
                    return Ok(state);
                }
                catch (Exception ex) { return Problem(ex.Message); }
            }
        }

        /// <summary>
        /// Delete an existing state by an EngineId entered
        /// </summary>
        /// <param name="id"></param>
        /// <returns>boolean</returns>
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(500)]
        [HttpDelete]
        public async Task<ActionResult> DeleteState(int id)
        {
            try
            {
                if (id > 0)
                {
                    var deleted = await _stateBusiness.DeleteStateAsync(id);
                    if (deleted)
                    {
                        return Ok("State deleted!");
                    }
                    else return Problem();
                }
                else return BadRequest("Enter an ID greater than 0 ");
            }
            catch (Exception ex) { return Problem(ex.Message); }
        }
    }
}
