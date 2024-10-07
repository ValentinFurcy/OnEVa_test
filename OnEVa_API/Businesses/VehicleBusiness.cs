using AutoMapper;
using DTOs.VehicleDTOs;
using DTOs.VehiclePropertiesDTOs.CreateInput;
using DTOs.VehiclePropertiesDTOs.Output;
using DTOs.VehiclePropertiesDTOs.UpdateInput;
using Entities;
using Entities.CONST;
using IBusinesses;
using IRepositories;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Businesses.Tools.StringHelper;

namespace Businesses
{
    public class VehicleBusiness : IVehicleBusiness
    {
        private readonly IVehicleRepository _vehicleRepository;
        private readonly IPictureRepository _pictureRepository;
        private IMapper _mapper;

        public VehicleBusiness(IVehicleRepository vehicleRepository, IPictureRepository pictureRepository, IMapper mapper)
        {
            _vehicleRepository = vehicleRepository;
            _pictureRepository = pictureRepository;
            _mapper = mapper;
        }
        public async Task<VehicleOutputDTO> CreateVehicleAsync(VehicleCreateInputDTO vehicleCreateInputDTO)
        {
            if (vehicleCreateInputDTO.PicturesId.Count() <= 0 && vehicleCreateInputDTO.PicturesToAdd.Count() <= 0)
            {
                throw new Exception("The Pictures required are empty!");
            }
            if (vehicleCreateInputDTO.PicturesId.Any() && !(vehicleCreateInputDTO.PicturesId is IEnumerable<int>))
            {
                throw new ArgumentException("Type is not an array or list");
            }
            if (vehicleCreateInputDTO.PicturesToAdd.Any() && !(vehicleCreateInputDTO.PicturesToAdd is IEnumerable<PictureCreateInputDTO>))
            {
                throw new ArgumentException("Type is not an array or list");
            }
            if (vehicleCreateInputDTO.PicturesToAdd.Any())
            {
                foreach (var pic in vehicleCreateInputDTO.PicturesToAdd)
                {
                    var picture = _mapper.Map<Picture>(pic);
                    var pictureExisting = await _pictureRepository.GetPictureByUrlAsync(picture.Url);
                    if (pictureExisting == null)
                    {
                        var pictureCreated = await _pictureRepository.CreatePictureAsync(picture);
                        vehicleCreateInputDTO.PicturesId.Add(pictureCreated.Id);
                    }
                    else
                        vehicleCreateInputDTO.PicturesId.Add(pictureExisting.Id);
                }
            }

            List<int> ids = vehicleCreateInputDTO.PicturesId.ToList();
            if (vehicleCreateInputDTO.VehicleNumber != null && vehicleCreateInputDTO.BrandId != null
                && vehicleCreateInputDTO.ModelId != null && vehicleCreateInputDTO.CategoryId != null
                && vehicleCreateInputDTO.EngineId != null && vehicleCreateInputDTO.Co2Emission != null
                && vehicleCreateInputDTO.MaxSeatNb != null && vehicleCreateInputDTO.PicturesId.Any()
                && IsValidRegistration(vehicleCreateInputDTO.Registration))
            {
                List<Picture> pictures = await _pictureRepository.GetPicturesByIdsAsync(ids);
                Vehicle vehicle = new Vehicle
                {
                    VehicleNumber = vehicleCreateInputDTO.VehicleNumber,
                    Registration = vehicleCreateInputDTO.Registration,
                    BrandId = vehicleCreateInputDTO.BrandId,
                    ModelId = vehicleCreateInputDTO.ModelId,
                    CategoryId = vehicleCreateInputDTO.CategoryId,
                    Pictures = pictures,
                    EngineId = vehicleCreateInputDTO.EngineId,
                    Co2Emission = vehicleCreateInputDTO.Co2Emission,
                    MaxSeatNb = vehicleCreateInputDTO.MaxSeatNb,
                    Status = Entities.Status.InService
                };

                return await _vehicleRepository.CreateVehicleAsync(vehicle);
            }
            else
            {
                throw new Exception("The Vehicle was not created !");
            }
        }
        public async Task<VehicleOutputDTO> UpdateVehicleAsync(VehicleUpdateInputDTO vehicleUpdateInputDTO)
        {
            if (vehicleUpdateInputDTO.PicturesToAdd.Any() && (vehicleUpdateInputDTO.PicturesToAdd is IEnumerable<PictureCreateInputDTO>))
            {
                foreach (var pic in vehicleUpdateInputDTO.PicturesToAdd)
                {
                    var picture = _mapper.Map<Picture>(pic);
                    var pictureExisting = await _pictureRepository.GetPictureByUrlAsync(picture.Url);
                    if (pictureExisting == null)
                    {
                        var pictureCreated = await _pictureRepository.CreatePictureAsync(picture);
                        vehicleUpdateInputDTO.PicturesId.Add(pictureCreated.Id);

                    }
                    else
                    {
                        vehicleUpdateInputDTO.PicturesId.Add(pictureExisting.Id);
                    }
                }
            }
            else { throw new ArgumentException("Type is not an array or list"); }

            List<Picture> pictures = new List<Picture>();

            if (vehicleUpdateInputDTO.PicturesId.Any() && vehicleUpdateInputDTO.PicturesId is IEnumerable<int>)
            {
                List<int> ids = vehicleUpdateInputDTO.PicturesId.ToList();
                //recuperer liste picture et comparer avec la nouvelle
                pictures = await _pictureRepository.GetPicturesByIdsAsync(ids);
                var vehiclePicturesInDb = await _vehicleRepository.GetVehicleByIdToolsAsync(vehicleUpdateInputDTO.VehicleId);

                foreach (var picture in vehiclePicturesInDb.Pictures)
                {
                    if (!pictures.Contains(picture))
                    {
                        pictures.Add(picture);
                    }
                }
            }
            else { throw new ArgumentException("Type is not an array or list"); }

            if (vehicleUpdateInputDTO.VehicleNumber != null && vehicleUpdateInputDTO.BrandId != null
                && vehicleUpdateInputDTO.ModelId != null && vehicleUpdateInputDTO.CategoryId != null
                && vehicleUpdateInputDTO.EngineId != null && vehicleUpdateInputDTO.Co2Emission != null
                && vehicleUpdateInputDTO.MaxSeatNb != null && IsValidRegistration(vehicleUpdateInputDTO.Registration))
            {
                Vehicle vehicleUpdated = new Vehicle
                {
                    Id = vehicleUpdateInputDTO.VehicleId,
                    VehicleNumber = vehicleUpdateInputDTO.VehicleNumber,
                    Registration = vehicleUpdateInputDTO.Registration,
                    BrandId = vehicleUpdateInputDTO.BrandId,
                    ModelId = vehicleUpdateInputDTO.ModelId,
                    CategoryId = vehicleUpdateInputDTO.CategoryId,
                    Pictures = pictures,
                    EngineId = vehicleUpdateInputDTO.EngineId,
                    Co2Emission = vehicleUpdateInputDTO.Co2Emission,
                    MaxSeatNb = vehicleUpdateInputDTO.MaxSeatNb,
                    Status = vehicleUpdateInputDTO.Status
                };
                return await _vehicleRepository.UpdateVehicleAsync(vehicleUpdated);
            }
            else throw new ArgumentException("Field required empty");
        }
        public async Task<List<VehicleOutputDTO>> GetAllVehiclesAsync()
        {
            return await _vehicleRepository.GetAllVehiclesAsync();
        }

        public async Task<List<VehicleOutputDTO>> GetAllVehiclesAvailableByDatesAsync(DateTime startDate, DateTime endDate)
        {
            if (startDate <= endDate)
            {
                return await _vehicleRepository.GetAllVehiclesAvailableByDatesAsync(startDate, endDate);
            }
            else throw new Exception("The startDate must be prior to the endDate.");
        }

        public async Task<List<VehicleOutputDTO>> GetAllVehiclesByBrandAsync(string brand)
        {
            if (string.IsNullOrWhiteSpace(brand))
            {
                throw new Exception("The brand format is not correct.");
            }
            else return await _vehicleRepository.GetAllVehiclesByBrandAsync(brand);
        }

        public async Task<VehicleOutputDTO> GetVehicleByRegistrationAsync(string registration)
        {
            if (!IsValidRegistration(registration))
            {
                throw new Exception("The registration format is not correct.");
            }
            else return await _vehicleRepository.GetVehicleByRegistrationAsync(registration);
        }

        public async Task<List<VehicleOutputDTO>> GetVehicleByRegistrationAutoCompleteAsync(string registration)
        {
            return await _vehicleRepository.GetVehicleByRegistrationAutoCompleteAsync(registration);
        }

        public async Task<VehicleOutputDTO> GetVehicleByIdAsync(int id)
        {
            var vehicle = await _vehicleRepository.GetVehicleByIdAsync(id);
            if (vehicle != null)
            {
                return vehicle;
            }
            else throw new Exception("This vehicle does not exist.");
        }


        public async Task<bool> DeleteVehicleAsync(int id)
        {
            var vehicle = await _vehicleRepository.GetVehicleByIdAsync(id);
            if (vehicle != null)
            {
                return await _vehicleRepository.DeleteVehicleAsync(id);
            }
            else throw new Exception("This vehicle does not exist and so cannot be deleted.");
        }

        public async Task<bool> DesableVehicle(int idVehcile)
        {
            var vehicle = await _vehicleRepository.GetVehicleByIdAsync(idVehcile);
            if (vehicle != null)
            {
                return await _vehicleRepository.DesableVehicleAsync(idVehcile);
            }
            else throw new Exception("This vehicle does not exist and so cannot be deleted.");
        }
    }
}
