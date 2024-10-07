using Businesses;
using DTOs.VehiclePropertiesDTOs.CreateInput;
using DTOs.VehiclePropertiesDTOs.UpdateInput;
using Entities;
using Entities.CONST;
using IBusinesses;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace OnEVa_API.Controllers;

[Authorize (Roles = "ADMIN")]
[Route("api/[controller]/[action]")]
[ApiController]
public class PictureController : ControllerBase
{
    private readonly IPictureBusiness _pictureBusiness;
    public PictureController(IPictureBusiness pictureBusiness)
    {
        _pictureBusiness = pictureBusiness;
    }

    /// <summary>
    /// Add a new picture.
    /// </summary>
    /// <param name="pictureCreateInputDTO"></param>
    /// <returns>A picture has the following format : PictureOutputDTO.</returns>
    [HttpPost]
    [ProducesResponseType(200)]
    [ProducesResponseType(400)]
    [ProducesResponseType(500)]
    public async Task<ActionResult> CreatePicture(PictureCreateInputDTO pictureCreateInputDTO)
    {
        try
        {
            if (pictureCreateInputDTO != null)
            {
                var createdPicture = await _pictureBusiness.CreatePictureAsync(pictureCreateInputDTO);
                return Ok(createdPicture);
            }
            else { return BadRequest("Field required empty"); }
        }
        catch (Exception ex) { return Problem(ex.Message); }
    }

    /// <summary>
    /// Get all pictures.
    /// </summary>
    /// <returns>The list of pictures has the following format : PictureOutputDTO.</returns>
    [HttpGet]
    [ProducesResponseType(200)]
    [ProducesResponseType(204)]
    [ProducesResponseType(500)]
    public async Task<ActionResult> GetAllPictures()
    {
        try
        {
            var pictures = await _pictureBusiness.GetAllPicturesAsync();
            if (pictures.Any())
            {
                return Ok(pictures);
            }
            else return NoContent();
        }
        catch (Exception ex) { return Problem(ex.Message); }
    }


    /// <summary>
    /// Get a picture corresponding to id.
    /// </summary>
    /// <param name="pictureId"></param>
    /// <returns>A picture has the following format : PictureOutputDTO.</returns>
    [HttpGet]
    [ProducesResponseType(200)]
    [ProducesResponseType(204)]
    [ProducesResponseType(400)]
    [ProducesResponseType(500)]
    public async Task<ActionResult> GetPictureById(int pictureId)
    {
        try
        {
            if (pictureId <= 0)
            {
                return BadRequest("Enter an ID greater than 0");
            }
            else
            {
                var picture = await _pictureBusiness.GetPictureByIdAsync(pictureId);
                if (picture != null)
                {
                    return Ok(picture);
                }
                else return NoContent();
            }
        }
        catch (Exception ex) { return Problem(ex.Message); }
    }

    /// <summary>
    /// Get a picture corresponding to title.
    /// </summary>
    /// <param name="title"></param>
    /// <returns>A picture has the following format : PictureOutputDTO.</returns>
    [ProducesResponseType(200)]
    [ProducesResponseType(204)]
    [ProducesResponseType(400)]
    [ProducesResponseType(500)]
    [HttpGet]
    public async Task<ActionResult> GetPicturesByTitle(string title)
    {
        if (title != null)
        {
            try
            {
                var picture = await _pictureBusiness.GetPicturesByTitleAsync(title);
                if (picture != null)
                {
                    return Ok(picture);
                }
                else return NoContent();
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }
        else
        {
            return BadRequest("Field required empty");
        }
    }


    /// <summary>
    /// Get a picture corresponding to title.
    /// </summary>
    /// <param name="title"></param>
    /// <param name="idColor"></param>
    /// <returns>A picture has the following format : PictureOutputDTO.</returns>
    [ProducesResponseType(200)]
    [ProducesResponseType(204)]
    [ProducesResponseType(400)]
    [ProducesResponseType(500)]
    [HttpGet]
    public async Task<ActionResult> GetPicturesByTitleAndColor(string title, Colors idColor)
    {
        if (title != null)
        {
            try
            {
                var picture = await _pictureBusiness.GetPicturesByTitleAndColorAsync(title, idColor);
                if (picture.Any())
                {
                    return Ok(picture);
                }
                else return NoContent();
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }
        else
        {
            return BadRequest("Field required empty");
        }
    }

    /// <summary>
    /// Update an existing picture.
    /// </summary>
    /// <param name="pictureUpdateInputDTO"></param>
    /// <returns>The updated picture has the following format : PictureOutputDTO.</returns>
    [ProducesResponseType(200)]
    [ProducesResponseType(400)]
    [ProducesResponseType(500)]
    [HttpPut]
    public async Task<ActionResult> UpdatePicture(PictureUpdateInputDTO pictureUpdateInputDTO)
    {
        try
        {
            if (pictureUpdateInputDTO != null)
            {
                var picture = await _pictureBusiness.UpdatePictureAsync(pictureUpdateInputDTO);
                return Ok(picture);
            }
            else { return BadRequest("Field required empty"); }
        }
        catch (Exception e)
        {
            return Problem(e.Message);
        }
    }

    /// <summary>
    /// Delete brand if exists.
    /// </summary>
    /// <param name="pictureId"></param>
    /// <returns>True if delete succed of False if unsucced.</returns>
    [ProducesResponseType(200)]
    [ProducesResponseType(400)]
    [ProducesResponseType(500)]
    [HttpDelete]
    public async Task<ActionResult<bool>> DeletePicture(int pictureId)
    {
        if (pictureId > 0)
        {
            try
            {
                return await _pictureBusiness.DeletePictureAsync(pictureId);
            }
            catch (Exception e)
            {
                return Problem($"ERROR   : {e.Message} ");
            }
        }
        else return BadRequest("Enter an ID greater than 0 ");
    }
}
