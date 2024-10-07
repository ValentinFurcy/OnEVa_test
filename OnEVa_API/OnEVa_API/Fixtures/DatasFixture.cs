using DTOs.VehiclePropertiesDTOs.CreateInput;
using Entities;
using Entities.CONST;
using Repositories;
using System.Security.AccessControl;
using static System.Net.WebRequestMethods;

namespace OnEVa_API.Fixtures
{
    public class DatasFixture
    {
        private readonly APIDbContext _context;
        public DatasFixture(APIDbContext context)
        {
            _context = context;
        }

        public async Task<string> GenerateDatabase()
        {
            using (var transaction = _context.Database.BeginTransaction())
            {
                try
                {
                    Brand peugeot = new Brand { Label = "Peugeot" };
                    Brand tesla = new Brand { Label = "Tesla" };
                    Brand fiat = new Brand { Label = "Fiat" };

                    await _context.Brands.AddRangeAsync(peugeot, tesla, fiat);


                    Category citadine = new Category { Label = "Citadine" };
                    Category berline = new Category { Label = "Berline" };
                    Category familiale = new Category { Label = "Familiale" };

                    await _context.Categories.AddRangeAsync(citadine, berline, familiale);


                    Engine essence = new Engine { Label = "Essence" };
                    Engine gazole = new Engine { Label = "Gazole" };
                    Engine elec = new Engine { Label = "Electrique" };

                    await _context.Engines.AddRangeAsync(essence, gazole, elec);


                    Model peugeot1 = new Model { Label = "208" };
                    Model peugeot2 = new Model { Label = "308" };
                    Model peugeot3 = new Model { Label = "3008" };
                    Model teslaS = new Model { Label = "tesla S" };
                    Model fiat500 = new Model { Label = "500" };

                    await _context.Models.AddRangeAsync(peugeot1, peugeot2, peugeot3, teslaS, fiat500);


                    Picture picture208 = new Picture { Title = "208", Url = "https://www.caradisiac.com/modele--peugeot-208-2e-generation/photo/", Color = Colors.Rouge };
                    Picture picture308 = new Picture { Title = "308", Url = "https://www.caradisiac.com/modele--peugeot-308-3e-generation/photo/" , Color = Colors.Bleu};
                    Picture picture3008 = new Picture { Title = "3008", Url = "https://www.caradisiac.com/modele--peugeot-3008-2e-generation/photo/", Color = Colors.Gris };

                    Picture pictureTeslaS = new Picture { Title = "Tesla S", Url = "https://www.caradisiac.com/modele--tesla-model-s/photo_2/", Color = Colors.Blanc };

                    Picture pictureFiat500 = new Picture { Title = "Fiat 500", Url = "https://www.caradisiac.com/modele--fiat-500-3e-generation/photo/", Color = Colors.Rouge };

                    await _context.Pictures.AddRangeAsync(picture208, picture308, picture3008, pictureTeslaS, pictureFiat500);

                    await _context.SaveChangesAsync();

                    Vehicle vehicle1 = new Vehicle
                    {
                        VehicleNumber = 1,
                        Registration = "AA-123-BB",
                        BrandId = peugeot.Id,
                        ModelId = peugeot1.Id,
                        CategoryId = berline.Id,
                        Pictures = new List<Picture> { picture208 },
                        EngineId = essence.Id,
                        Co2Emission = 200,
                        MaxSeatNb = 5,
                        Status = Entities.Status.InService
                    };

                    Vehicle vehicle2 = new Vehicle
                    {
                        VehicleNumber = 2,
                        Registration = "AA-124-BB",
                        BrandId = tesla.Id,
                        ModelId = teslaS.Id,
                        CategoryId = berline.Id,
                        Pictures = new List<Picture> { pictureTeslaS },
                        EngineId = elec.Id,
                        Co2Emission = 0,
                        MaxSeatNb = 5,
                        Status = Entities.Status.InService
                    };

                    Vehicle vehicle3 = new Vehicle
                    {
                        VehicleNumber = 3,
                        Registration = "AA-125-BB",
                        BrandId = fiat.Id,
                        ModelId = fiat500.Id,
                        CategoryId = citadine.Id,
                        Pictures = new List<Picture> { pictureFiat500 },
                        EngineId = essence.Id,
                        Co2Emission = 200,
                        MaxSeatNb = 4,
                        Status = Entities.Status.InService
                    };

                    Vehicle vehicle4 = new Vehicle
                    {
                        VehicleNumber = 4,
                        Registration = "AA-126-BB",
                        BrandId = peugeot.Id,
                        ModelId = peugeot2.Id,
                        CategoryId = berline.Id,
                        Pictures = new List<Picture> { picture308 },
                        EngineId = essence.Id,
                        Co2Emission = 200,
                        MaxSeatNb = 5,
                        Status = Entities.Status.UnderRepair
                    };

                    Vehicle vehicle5 = new Vehicle
                    {
                        VehicleNumber = 5,
                        Registration = "AA-127-BB",
                        BrandId = peugeot.Id,
                        ModelId = peugeot3.Id,
                        CategoryId = familiale.Id,
                        Pictures = new List<Picture> { picture3008 },
                        EngineId = gazole.Id,
                        Co2Emission = 300,
                        MaxSeatNb = 5,
                        Status = Entities.Status.OutOfService
                    };

                    await _context.Vehicles.AddRangeAsync(vehicle1, vehicle2, vehicle3, vehicle4, vehicle5);


                    Address adress1 = new Address { City = "Perols", StreetNb = "40", StreetName = "Rue Louis Lepine", PostCode = "34470" };
                    Address adress2 = new Address { City = "Saint-Herblain", StreetNb = "4", StreetName = "Rue Edith Piaf", PostCode = "44800" };
                    Address adress3 = new Address { City = "Lyon", StreetNb = "27", StreetName = "Rue Maurice Flandin", PostCode = "69003" };

                    await _context.Addresses.AddRangeAsync(adress1, adress2, adress3);

                    await _context.SaveChangesAsync();
                    transaction.Commit();

                    return "Database Generated Successfully";
                }
                catch (Exception e)
                {
                    transaction.Rollback();
                    return $"ERROR : {e.Message}";
                }
            }
        }
    }
}
   
