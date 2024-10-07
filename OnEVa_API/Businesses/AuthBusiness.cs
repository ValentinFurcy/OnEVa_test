using DTOs.PersonDTOs;
using Entities;
using Entities.CONST;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Businesses
{
    public class AuthBusiness
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public AuthBusiness(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager, RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
        }

        public async Task<AppUser> CreateUser(PersonCreateInputDTO personCreateInputDTO)
        {
            var user = new AppUser { UserName = personCreateInputDTO.Email, Email = personCreateInputDTO.Email };
            var userCreated = await _userManager.CreateAsync(user, personCreateInputDTO.Password);
            if (userCreated.Succeeded)
            {
                if (!await _roleManager.RoleExistsAsync(ROLES.COLLABORATER))
                {
                    await _roleManager.CreateAsync(new IdentityRole { Name = ROLES.COLLABORATER, NormalizedName = ROLES.COLLABORATER });
                }

                await _userManager.AddToRoleAsync(user, ROLES.COLLABORATER);

                return user;
            }
            else throw new Exception("Error user creation failed");
        }
    }
}
