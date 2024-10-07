using AutoMapper;
using Businesses.Tools;
using DTOs.PersonDTOs;
using DTOs.VehiclePropertiesDTOs.CreateInput;
using Entities;
using Entities.CONST;
using IBusinesses;
using IRepositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Identity.Client;
using OnEVa_API.Tools;
using SQLitePCL;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;


namespace Businesses
{
    public class PersonBusiness : IPersonBusiness
    {
        private readonly IPersonRepository _personRepository;
        private readonly IMapper _mapper;
        private readonly SerializeHelper _serializeHelper;
        private readonly AuthBusiness _authBusiness;
        private readonly PicturesHelpers _picturesHelpers;

        public PersonBusiness(IPersonRepository personRepository, IMapper mapper, SerializeHelper serializeHelper, AuthBusiness authBusiness, PicturesHelpers picturesHelpers)
        {
            _personRepository = personRepository;
            _mapper = mapper;
            _serializeHelper = serializeHelper;
            _authBusiness = authBusiness;
            _picturesHelpers = picturesHelpers;
        }

        public async Task<PersonOutputDTO> CreatePersonAsync(PersonCreateInputDTO? personCreateInputDTO, Person? personByJSON)
        {
            if (personCreateInputDTO != null)
            {
                var isExist = await _personRepository.GetPersonByEmailAsync(personCreateInputDTO.Email);

                if (string.IsNullOrWhiteSpace(personCreateInputDTO.FirstName) || string.IsNullOrWhiteSpace(personCreateInputDTO.LastName) || string.IsNullOrWhiteSpace(personCreateInputDTO.Email))
                {
                    throw new Exception("Person format is not correct.");
                }
                else if (isExist != null)
                {
                    throw new Exception($"An account with email : {personCreateInputDTO.Email} already exists.");
                }
                else
                {
                    var user = await _authBusiness.CreateUser(personCreateInputDTO);

                    var person = _mapper.Map<Person>(personCreateInputDTO);
                    person.AppUserId = user.Id;

                    return await _personRepository.CreatePersonAsync(person);
                }
            }
            else
            {
                return await _personRepository.CreatePersonAsync(personByJSON);
            }

        }

        public async Task<bool> InsertPersonsFileJSONAsync(IFormFile personFileJSON)
        {
            if (personFileJSON != null)
            {
                var listPersonsDeserialize = await _serializeHelper.DeserializeFileAsync<PersonCreateInputDTO>(null, personFileJSON);

                foreach (var person in listPersonsDeserialize)
                {
                    var isExist = await _personRepository.GetPersonByEmailAsync(person.Email);

                    if (string.IsNullOrWhiteSpace(person.FirstName) || string.IsNullOrWhiteSpace(person.LastName) || string.IsNullOrWhiteSpace(person.Email))
                    {
                        throw new Exception("Person format is not correct.");
                    }
                    else if (isExist != null)
                    {
                        throw new Exception($"An account with email : {person.Email} already exists.");
                    }
                    var user = await _authBusiness.CreateUser(person);

                    var personByJSON = _mapper.Map<Person>(person);
                    personByJSON.AppUserId = user.Id;

                    await CreatePersonAsync(null, personByJSON);
                }
                return true;
            }
            else throw new Exception("This file is empty or not correct");
        }

        public async Task<bool> DeletePersonAsync(string email, string appUserId, bool isAdmin)
        {
            var personToDelete = await GetPersonByEmailAsync(email);

            if (personToDelete == null)
            {
                throw new Exception("This email does not correspond to any account");
            }
            else if (isAdmin || personToDelete.AppUserId == appUserId)
            {
                return await _personRepository.DeletePersonAsync(personToDelete.AppUserId);
            }
            else throw new Exception("Unauthorized access");
        }

        public async Task<List<PersonOutputDTO>> GetAllPersonsAsync()
        {
            return await _personRepository.GetAllPersonsAsync();
        }
        public async Task<PersonOutputDTO> GetPersonByEmailAsync(string email)
        {
            var regex = new Regex(@"^[a-zA-Z0-9.!#$%&'*+/=?^_`{|}~-]+@[a-zA-Z0-9-]+(?:\.[a-zA-Z0-9-]+)*$");

            if (!regex.IsMatch(email))
            {
                throw new Exception("Email format is not valid");
            }
            return await _personRepository.GetPersonByEmailAsync(email);
        }

        public async Task<PersonOutputDTO> GetPersonByUserIdAsync(string userId)
        {
            if (userId == null) throw new ArgumentNullException("userId");
            return await _personRepository.GetPersonByUserIdAsync(userId);
        }

        public async Task<PersonOutputDTO> UpdatePersonAsync(PersonUpdateInputDTO personUpdateInputDTO, string appUserId, bool isAdmin)
        {
            if (isAdmin)
            {
                var personInDbForAdmin = await GetPersonByEmailAsync(personUpdateInputDTO.CurrentEmail);

                if (personInDbForAdmin == null)
                {
                    throw new Exception("Email entered does not exist");
                }
                if (!string.IsNullOrWhiteSpace(personUpdateInputDTO.NewEmail) && personUpdateInputDTO.NewEmail != personInDbForAdmin.Email)
                {
                    var emailExisting = await GetPersonByEmailAsync(personUpdateInputDTO.NewEmail);

                    if (emailExisting != null)
                    {
                        throw new Exception("Email already exists");
                    }
                    else return await _personRepository.UpdatePersonAsync(personUpdateInputDTO, personInDbForAdmin.AppUserId);
                }
                else throw new Exception("Email is empty or identical");
            }
            else
            {
                var personInDb = await GetPersonByUserIdAsync(appUserId);

                if (personInDb != null && personInDb.AppUserId == appUserId)
                {
                    if (!string.IsNullOrWhiteSpace(personUpdateInputDTO.NewEmail) && personUpdateInputDTO.NewEmail != personInDb.Email)
                    {
                        var emailExisting = await GetPersonByEmailAsync(personUpdateInputDTO.NewEmail);

                        if (emailExisting != null)
                        {
                            throw new Exception("Email already exists");
                        }
                        else return await _personRepository.UpdatePersonAsync(personUpdateInputDTO, appUserId);
                    }
                    else throw new Exception("Email is empty or identical");
                }
                else throw new Exception("Unauthorized access");
            }
        }

        public async Task<bool> UploadProfilePictureAsync(byte[] pictureData, string appUserId)
        {
            var pictureInDb = await GetPersonByUserIdAsync(appUserId);
            if (pictureInDb.ProfilePicture != null)
            {
                var equals = await _picturesHelpers.ComparePictureByHash(pictureInDb.ProfilePicture, pictureData);

                if (equals)
                {
                    return false;
                }
                return await _personRepository.UploadProfilePictureAsync(pictureData, appUserId);
            }
            else
            {
                return await _personRepository.UploadProfilePictureAsync(pictureData, appUserId);
            }
        }
    }
}
