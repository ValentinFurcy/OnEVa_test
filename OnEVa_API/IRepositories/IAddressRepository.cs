using DTOs.Address;
using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IRepositories
{
    public interface IAddressRepository
    {
        public Task<AddressOutputDTO> CreateAddressAsync(Address address);
        public Task<List<AddressOutputDTO>> GetAllAddressesAsync();
        public Task<AddressOutputDTO> GetAddressByIdAsync(int addressId);
        public Task<AddressOutputDTO> UpdateAddressAsync(Address address);
        public Task<bool> DeleteAddressAsync(int addressId);
        public Task <AddressOutputDTO> GetAddressByFullAddressAsync(Address address);
    }
}
