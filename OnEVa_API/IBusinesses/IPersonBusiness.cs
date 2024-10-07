using DTOs.PersonDTOs;
using Entities;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IBusinesses
{
    public interface IPersonBusiness
    {
        public Task<PersonOutputDTO> CreatePersonAsync(PersonCreateInputDTO? personCreateInputDTO, Person? personByJSON);
        public Task<bool> InsertPersonsFileJSONAsync(IFormFile personFileJSON);
        public Task<List<PersonOutputDTO>> GetAllPersonsAsync();
        public Task<PersonOutputDTO> GetPersonByEmailAsync(string email);
        public Task<PersonOutputDTO> GetPersonByUserIdAsync(string userId);
        public Task<PersonOutputDTO> UpdatePersonAsync(PersonUpdateInputDTO personUpdateInputDTO, string appUserId, bool isAdmin);
        public Task<bool> UploadProfilePictureAsync(byte[] pictureData, string appUserId);
        public Task<bool> DeletePersonAsync(string email, string appUserId, bool isAdmin);

    }
}
