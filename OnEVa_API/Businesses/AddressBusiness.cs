using AutoMapper;
using DTOs.Address;
using DTOs.VehiclePropertiesDTOs.CreateInput;
using Entities;
using IBusinesses;
using IRepositories;
using SQLitePCL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Businesses
{
    public class AddressBusiness : IAddressBusiness
    {
        private readonly IAddressRepository _addressRepository;
        private readonly IMapper _mapper;
        public AddressBusiness(IAddressRepository addressRepository, IMapper mapper)
        {
            _addressRepository = addressRepository;
            _mapper = mapper;
        }

        public async Task<AddressOutputDTO> CreateAddressAsync(AddressCreateInputDTO addressCreateInputDTO)
        {
            var address = _mapper.Map<Address>(addressCreateInputDTO);

            var isExist = await _addressRepository.GetAddressByFullAddressAsync(address);

            if (string.IsNullOrWhiteSpace(address.City) || string.IsNullOrWhiteSpace(address.PostCode) || string.IsNullOrWhiteSpace(address.StreetName))
            {
                throw new Exception("Address format is not correct.");
            }
            else if (isExist != null)
            {
                throw new Exception($"This adress already exists.");
            }
            else
            {            
                return await _addressRepository.CreateAddressAsync(address);
            }
        }

        public async Task<bool> DeleteAddressAsync(int adressId)
        {

            var address = await _addressRepository.GetAddressByIdAsync(adressId);

            if (address == null)
            {
                throw new Exception("Cannot delete a address that does not exist");
            }
            else return await _addressRepository.DeleteAddressAsync(adressId);
        }

        public async Task<AddressOutputDTO> GetAddressByIdAsync(int addressId)
        {
           return await _addressRepository.GetAddressByIdAsync(addressId);
        }

        public async Task<List<AddressOutputDTO>> GetAllAddressesAsync()
        {
            return await _addressRepository.GetAllAddressesAsync();
        }

        public async Task<AddressOutputDTO> UpdateAddressAsync(AddressUpdateInputDTO addressUpdateInputDTO)
        {
           
            if(string.IsNullOrWhiteSpace(addressUpdateInputDTO.City) || string.IsNullOrWhiteSpace(addressUpdateInputDTO.PostCode) || string.IsNullOrWhiteSpace(addressUpdateInputDTO.StreetName)) 
            {
                throw new Exception("Address format is not correct.");
            }
            else
            {
                var address = _mapper.Map<Address>(addressUpdateInputDTO);

                return await _addressRepository.UpdateAddressAsync(address);
            }
        }
    }
}
