using DTOs.PersonDTOs;
using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IRepositories
{
    public interface IPersonRepository
    {
        public Task<PersonOutputDTO> CreatePersonAsync(Person person);
        public Task<List<PersonOutputDTO>> GetAllPersonsAsync();
        public Task<PersonOutputDTO> GetPersonByEmailAsync(string email);
        public Task<PersonOutputDTO> GetPersonByUserIdAsync(string userId);
        public Task<PersonOutputDTO> UpdatePersonAsync(PersonUpdateInputDTO personUpdateInputDTO, string appUserId);
        public Task<bool> UploadProfilePictureAsync(byte[] pictureData, string appUserId);
        public Task<bool> DeletePersonAsync(string userId);
        
    }
}
