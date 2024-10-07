using Businesses;
using DTOs.VehiclePropertiesDTOs.CreateInput;
using DTOs.VehiclePropertiesDTOs.UpdateInput;
using Entities;
using IBusinesses;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Repositories;

namespace OnEVa_API.Controllers;

[Authorize(Roles = "ADMIN")]
[Route("api/[controller]/[action]")]
[ApiController]
public class CategoryController : ControllerBase
{
    private readonly ICategoryBusiness _categoryBusiness;
    public CategoryController(ICategoryBusiness categoryBusiness)
    {
        _categoryBusiness = categoryBusiness;
    }


    /// <summary>
    /// Create a new category.
    /// </summary>
    /// <param name="categoryCreateInputDTO"></param>
    /// <returns>The new category created as a CategoryCreateOutputDTO.</returns>
    [ProducesResponseType(200)]
    [ProducesResponseType(204)]
    [ProducesResponseType(400)]
    [ProducesResponseType(500)]
    [HttpPost]
    public async Task<ActionResult> CreateCategory(CategoryCreateInputDTO categoryCreateInputDTO)
    {
        try
        {
            if (categoryCreateInputDTO != null)
            {
                var result = await _categoryBusiness.CreateCategoryAsync(categoryCreateInputDTO);
                if (result != null)
                {
                    return Ok(result);
                }
                else return NoContent();
            }
            else { return BadRequest("Field requiered is empty"); }
        }
        catch (Exception ex) { return Problem(ex.Message); }
    }

    /// <summary>
    /// Update an existing category.
    /// </summary>
    /// <param name="categoryUpdateInputDTO"></param>
    /// <returns>The new category updated as a CategoryCreateOutputDTO.</returns>
    [ProducesResponseType(200)]
    [ProducesResponseType(204)]
    [ProducesResponseType(400)]
    [ProducesResponseType(500)]
    [HttpPut]
    public async Task<ActionResult> UpdateCategory(CategoryUpdateInputDTO categoryUpdateInputDTO)
    {
        try
        {
            if (categoryUpdateInputDTO != null)
            {
                var category = await _categoryBusiness.UpdateCategoryAsync(categoryUpdateInputDTO);
                if (category != null)
                {
                    return Ok(category);
                }
                else return NoContent();
            }
            else { return BadRequest("Field required is empty"); }
        }
        catch (Exception e)
        {
            return Problem(e.Message);
        }
    }

    /// <summary>
    /// Get all the existing categories.
    /// </summary>
    /// <returns>The List of categogies as a list of CategorieOutpuntDTO.</returns>
    [ProducesResponseType(200)]
    [ProducesResponseType(204)]
    [ProducesResponseType(500)]
    [HttpGet]
    public async Task<ActionResult> GetAllCategories()
    {
        try
        {
            var categories = await _categoryBusiness.GetAllCategoriesAsync();
            if (categories.Any())
            {
                return Ok(categories);
            }
            else return NoContent();
        }
        catch (Exception e) { return Problem(e.Message); }
    }

    /// <summary>
    /// Get a category by id.
    /// </summary>
    /// <param name="categoryId"></param>
    /// <returns></returns>
    [ProducesResponseType(200)]
    [ProducesResponseType(204)]
    [ProducesResponseType(400)]
    [ProducesResponseType(500)]
    [HttpGet]
    public async Task<ActionResult> GetCategoryById(int categoryId)
    {
        try
        {
            if (categoryId <= 0)
            {
                return BadRequest("Enter an ID greater than 0");
            }
            else
            {
                var category = await _categoryBusiness.GetCategoryByIdAsync(categoryId);
                if (category != null)
                {
                    return Ok(category);
                }
                else return NoContent();
            }
        }
        catch (Exception e) { return Problem(e.Message); }
    }

    /// <summary>
    /// Get Category by label if exists.
    /// </summary>
    /// <param name="label"></param>
    /// <returns>CategoryOutputDTO.</returns>
    [ProducesResponseType(200)]
    [ProducesResponseType(204)]
    [ProducesResponseType(400)]
    [ProducesResponseType(500)]
    [HttpGet]
    public async Task<ActionResult> GetCategoryByLabel(string label)
    {
        if (label != null)
        {
            try
            {
                var category = await _categoryBusiness.GetCategoryByLabelAsync(label);
                if (category != null)
                {
                    return Ok(category);
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
            return BadRequest("Field required is empty");
        }
    }

    /// <summary>
    /// Delete category if exists.
    /// </summary>
    /// <param name="categoryId"></param>
    /// <returns>True if delete succed of False if unsucced</returns>
    [ProducesResponseType(200)]
    [ProducesResponseType(400)]
    [ProducesResponseType(500)]
    [HttpDelete]
    public async Task<ActionResult<bool>> DeleteCategory(int categoryId)
    {
        if (categoryId > 0)
        {
            try
            {
                return await _categoryBusiness.DeleteCategoryAsync(categoryId);
            }
            catch (Exception e)
            {
                return Problem($"ERROR   : {e.Message} ");
            }
        }
        else return BadRequest("Enter an ID greater than 0 ");
    }
}
