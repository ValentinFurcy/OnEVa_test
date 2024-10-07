using AutoMapper;
using DTOs.PersonDTOs;
using DTOs.VehiclePropertiesDTOs.Output;
using Entities;
using IRepositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace Repositories
{
    public class PersonRepository : IPersonRepository
    {
        private readonly APIDbContext _context;
        private readonly IMapper _mapper;

        public PersonRepository(APIDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<PersonOutputDTO> CreatePersonAsync(Person person)
        {
            try
            {
                await _context.Persons.AddAsync(person);
                await _context.SaveChangesAsync();
                return await GetPersonByUserIdAsync(person.AppUserId);
            }
            catch (Exception)
            {
                throw new Exception("Error ! The new person was not created");
            }
        }

        public async Task<bool> DeletePersonAsync(string userId)
        {
            try
            {
                using (var transaction = _context.Database.BeginTransaction())
                {
                    try
                    {
                        var personDeleted = await _context.Persons.Where(p => p.AppUserId == userId).ExecuteDeleteAsync();

                        var userDeleted = await _context.Users.Where(u => u.Id == userId).ExecuteDeleteAsync();
                    
                        transaction.CommitAsync();                        

                        return true;
                    }
                    catch (Exception e)
                    {
                        transaction.Rollback();

                        throw new Exception(e.Message);
                    }
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public async Task<List<PersonOutputDTO>> GetAllPersonsAsync()
        {
            try
            {
                var persons = await _context.Persons.Include(p => p.AppUser).ToListAsync();
                if (persons.Any())
                {
                    var personsOutputDTO = _mapper.Map<List<PersonOutputDTO>>(persons);

                    return personsOutputDTO;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }                 
        }

        public async Task<PersonOutputDTO> GetPersonByEmailAsync(string email)
        {
            try
            {
                var person = await _context.Persons.Include(p => p.AppUser).FirstOrDefaultAsync(p => p.AppUser.Email == email);
                if (person != null)
                {
                    var personOutputDTO = _mapper.Map<PersonOutputDTO>(person);
                    return personOutputDTO;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public async Task<PersonOutputDTO> GetPersonByUserIdAsync(string userId)
        {
            try
            {
                var person = await _context.Persons.Include(p => p.AppUser).FirstOrDefaultAsync(p => p.AppUserId == userId);
                if (person != null)
                {
                    var personOutputDTO = _mapper.Map<PersonOutputDTO>(person);
                    return personOutputDTO;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public async Task<PersonOutputDTO> UpdatePersonAsync(PersonUpdateInputDTO personUpdateInputDTO, string appUserId)
        {
            //TODO Add vehicle
            try
            {
                var user = await _context.Users.Where(u => u.Id == appUserId).ExecuteUpdateAsync(
                      updates => updates.SetProperty(u => u.Email, personUpdateInputDTO.NewEmail)
                      .SetProperty(u => u.UserName, personUpdateInputDTO.NewEmail));
                if (user > 0)
                {
                    return await GetPersonByUserIdAsync(appUserId);
                }
                else throw new Exception("Update failed");            
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public async Task<bool> UploadProfilePictureAsync(byte[] pictureData, string appUserId) 
        {
            try
            {
                var person = await _context.Persons.Where(p => p.AppUserId == appUserId).ExecuteUpdateAsync(
                    updates => updates .SetProperty(p => p.ProfilePicture, pictureData));

                if (person > 0)
                {
                    return true;
                }
                else return false;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
    }
}
