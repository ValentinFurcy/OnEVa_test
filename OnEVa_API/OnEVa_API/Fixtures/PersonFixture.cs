using AutoMapper;
using Bogus;
using Bogus.DataSets;
using Businesses;
using DTOs.PersonDTOs;
using Entities;
using IBusinesses;
using IRepositories;
using Microsoft.EntityFrameworkCore;
using OnEVa_API.Tools;
using Repositories;
using System.Collections;
using System.Data;
using System.Text;
using System.Text.Json;
using System.Xml;



namespace OnEVa_API.Fixtures
{
    public class PersonFixture
    {

        private readonly IPersonBusiness _personBusiness;
        private readonly SerializeHelper _serializeHelper;
        public PersonFixture(IPersonBusiness personBusiness, SerializeHelper serializeHelper)
        {
            _personBusiness = personBusiness;
            _serializeHelper = serializeHelper;
        }

        public async Task<string> GenerateJsonFileForCreatePersonsFixtures(int nbPerson)
        {
            var faker = new Faker("fr");

            var personsList = new List<PersonCreateInputDTO>();

            for (var i = 0; i < nbPerson; i++)
            {
                var firstName = faker.Name.FirstName();
                var lastName = faker.Name.LastName();
                var email = faker.Internet.Email();
                var password = "Azerty1+";

                var person = new PersonCreateInputDTO
                {
                    FirstName = firstName,
                    LastName = lastName,
                    Email = email,
                    Password = password
                };

                personsList.Add(person);
            }

            if (personsList.Any())
            {
                var fileName = "CreatePersonsFixtures.json";
                var jsonString = JsonSerializer.Serialize(personsList);
                File.WriteAllText(fileName, jsonString); 
            }
            if (File.Exists("CreatePersonsFixtures.json"))
            {
                return "CreatePersonsFixtures.json";
            }
            else return "File does not created"; 
        }

        public async Task<bool> InsertPersonsFixtures() 
        {
            try
            {
                string filePath = "CreatePersonsFixtures.json";
                IFormFile formFile = ConvertToIFormFile(filePath);
                await _personBusiness.InsertPersonsFileJSONAsync(formFile);
                return true;
            }
            catch (Exception e)
            { 
                throw new Exception("Error inserting persons: " + e.Message);
            }
        }
        public IFormFile ConvertToIFormFile(string filePath)
        {
            var fileStream = new FileStream(filePath, FileMode.Open, FileAccess.Read);
            return new FormFile(fileStream, 0, fileStream.Length, null, Path.GetFileName(filePath))
            {
                Headers = new HeaderDictionary(),
                ContentType = "application/json"
            };
        }
    }
}
