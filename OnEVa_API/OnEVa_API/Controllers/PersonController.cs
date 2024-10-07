using DTOs.PersonDTOs;
using DTOs.VehiclePropertiesDTOs.CreateInput;
using Entities;
using Entities.CONST;
using IBusinesses;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Data;

namespace OnEVa_API.Controllers
{
    [Authorize]
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class PersonController : ControllerBase
    {
        private readonly IPersonBusiness _personBusiness;
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public PersonController(IPersonBusiness personBusiness, UserManager<AppUser> userManager, SignInManager<AppUser> signInManager, RoleManager<IdentityRole> roleManager)
        {
            _personBusiness = personBusiness;
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
        }

        /// <summary>
        /// Create a new person
        /// </summary>
        /// <param name="personCreateInputDTO"></param>
        /// <returns>The new person created as a PersonOutputDTO</returns>
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(500)]
        [HttpPost]
        public async Task<ActionResult> CreatePerson(PersonCreateInputDTO personCreateInputDTO)
        {
            try
            {
                if (personCreateInputDTO != null)
                {
                    var person = await _personBusiness.CreatePersonAsync(personCreateInputDTO, null);
                    return Ok(person);
                }
                else { return BadRequest("Field required empty"); }
            }
            catch (Exception ex) { return Problem(ex.Message); }
        }

        /// <summary>
        /// Create new people by insert json file
        /// </summary>
        /// <param name="personFileJSON"></param>
        /// <returns></returns>
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(500)]
        [HttpPost]
        public async Task<ActionResult> InsertPersonsFileJSON(IFormFile personFileJSON)
        {
            try
            {
                if (personFileJSON != null)
                {
                    var person = await _personBusiness.InsertPersonsFileJSONAsync(personFileJSON);
                    return Ok(person);
                }
                else { return BadRequest("Field required empty"); }
            }
            catch (Exception ex) { return Problem(ex.Message); }
        }

        [ProducesResponseType(200)]
        [ProducesResponseType(204)]
        [ProducesResponseType(500)]
        [HttpGet]
        public async Task<ActionResult> GetAllPersons()
        {
            var persons = await _personBusiness.GetAllPersonsAsync();
            try
            {
                if (persons != null)
                {
                    return Ok(persons);
                }
                else return NoContent();
            }
            catch (Exception ex) { return Problem(ex.Message); }
        }


        [ProducesResponseType(200)]
        [ProducesResponseType(204)]
        [ProducesResponseType(500)]
        [HttpGet]
        public async Task<ActionResult> GetPersonByEmail(string email)
        {
            if (email != null)
            {
                try
                {
                    var person = await _personBusiness.GetPersonByEmailAsync(email);

                    if (person != null)
                        return Ok(person);
                    else return NoContent();
                }
                catch (Exception e)
                {
                    return Problem($"ERROR : {e.Message}");
                }
            }
            else return BadRequest("Field required empty !");
        }

        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(500)]
        [HttpPut]
        public async Task<ActionResult> UpdatePerson(PersonUpdateInputDTO personUpdateInput)
        {
            if (personUpdateInput != null)
            {
                var user = await _userManager.GetUserAsync(User);
                var isAdmin = await _userManager.IsInRoleAsync(user, "ADMIN");

                var appUserId = user.Id;
                try
                {
                    var person = await _personBusiness.UpdatePersonAsync(personUpdateInput, appUserId, isAdmin);
                    return Ok(person);
                }
                catch (Exception e)
                {
                    return Problem($"ERROR :{e.Message}");
                }
            }
            else return BadRequest("Required fields are not entered");
        }

        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(500)]
        [HttpPut]
        public async Task<ActionResult> UploadProfilePicture(IFormFile pictureFile)
        {
            if (pictureFile != null && pictureFile.Length > 0)
            {
                var user = await _userManager.GetUserAsync(User);
                var appUserId = user.Id;

                using (MemoryStream ms = new MemoryStream())
                {
                    await pictureFile.CopyToAsync(ms);
                    byte[] pictureData = ms.ToArray();

                    var uploadedPicture = await _personBusiness.UploadProfilePictureAsync(pictureData, appUserId);

                    if (uploadedPicture)
                    {
                        return Ok(uploadedPicture);
                    }
                    else return Problem("Update failed, try again");
                }
            }
            else return BadRequest("Format is not valid");
        }

        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(500)]
        [HttpDelete]
        public async Task<ActionResult> DeletePerson(string email)
        {
            if (email != null)
            {
                try
                {
                    var user = await _userManager.GetUserAsync(User);
                    var isAdmin = await _userManager.IsInRoleAsync(user, "ADMIN");
                    var appUserId = user.Id;

                    var personDeleted = await _personBusiness.DeletePersonAsync(email, appUserId, isAdmin);

                    return Ok(personDeleted);
                }
                catch (Exception e)
                {
                    return Problem(e.Message);
                }
            }
            else
            {
                return BadRequest("Field required empty");
            }
        }

        [AllowAnonymous]
        [HttpPost]
        public async Task<ActionResult> CreateAdmin(PersonCreateInputDTO personCreateInputDTO)
        {
            try
            {
                if (personCreateInputDTO != null)
                {
                    var user = new AppUser { UserName = personCreateInputDTO.Email, Email = personCreateInputDTO.Email };
                    var userCreated = await _userManager.CreateAsync(user, personCreateInputDTO.Password);
                    if (userCreated.Succeeded)
                    {
                        if (!await _roleManager.RoleExistsAsync(ROLES.ADMIN))
                        {
                            await _roleManager.CreateAsync(new IdentityRole { Name = ROLES.ADMIN, NormalizedName = ROLES.ADMIN });
                        }

                        await _userManager.AddToRoleAsync(user, ROLES.ADMIN);

                        return Ok(user);
                    }
                    else return Problem("ERROR : error when creating user account");
                }
                else { return BadRequest("Field required empty"); }
            }
            catch (Exception ex) { return Problem(ex.Message); }
        }

        [HttpGet]
        public async Task<ActionResult> GetRole()
        {
            var user = await _userManager.GetUserAsync(User);
            var role = await _userManager.GetRolesAsync(user);
            if (role.Contains(ROLES.ADMIN)) { return Ok(role); }
            else if (role.Contains(ROLES.COLLABORATER)) { return Ok(role); }
            else return null;
        }
    }
}
