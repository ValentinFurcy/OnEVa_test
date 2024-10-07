
using AutoMapper;
using DTOs.Address;
using DTOs.CarpoolDTOs;
using Entities;
using IRepositories;
using SQLitePCL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Businesses.Tools
{
    public class ToolsCarpool
    {
        private readonly ICarpoolRepository _carpoolRepository;
        private readonly IAddressRepository _addressRepository;
        private readonly IMapper _mapper;

        public ToolsCarpool(ICarpoolRepository carpoolRepository, IAddressRepository addressRepository, IMapper mapper)
        {
            _carpoolRepository = carpoolRepository;
            _addressRepository = addressRepository;
            _mapper = mapper;

        }
        public async Task<bool> CarpoolIsExisting(CarpoolCreateInputDTO carpoolCreateInputDTO, int organizerId)
        {
            var carpoolInDbList = await _carpoolRepository.GetCarpoolForMethodExistingAsync(carpoolCreateInputDTO, organizerId);
            foreach (var carpoolInDb in carpoolInDbList)
            {
                
                if (carpoolCreateInputDTO.StartAddress.Equals(carpoolInDb.StartAddress))
                {
                    TimeSpan durationCarpoolExisting = carpoolInDb.EndDate - carpoolInDb.StartDate;
                    if (carpoolCreateInputDTO.StartDate + durationCarpoolExisting >= carpoolCreateInputDTO.EndDate)
                    {
                        return false;
                    }
                }
                else
                {
                    if (!(carpoolCreateInputDTO.StartDate > carpoolInDb.EndDate || carpoolCreateInputDTO.EndDate < carpoolInDb.StartDate))
                    {
                        return false;
                    }
                }
            }
            return true;
        }

        /// <summary>
        /// If address exists return id , or create if not exists and return id 
        /// </summary>
        /// <param name="addressDTO"></param>
        /// <returns>int => EngineId address</returns>
        public async Task<int> AdressExistsOrCreate(AddressCreateInputDTO addressDTO)
        {
            var address = _mapper.Map<Address>(addressDTO);

            var addressExisting = await _addressRepository.GetAddressByFullAddressAsync(address);

            if (addressExisting != null)
            {
                return addressExisting.Id;
            }
            else 
            { 
                var adressCreated = await _addressRepository.CreateAddressAsync(address);
                return adressCreated.Id;
            }
        }
    }
}
