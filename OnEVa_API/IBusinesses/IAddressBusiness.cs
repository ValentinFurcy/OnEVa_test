using DTOs.Address;
using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IBusinesses
{
    public interface IAddressBusiness
    {
        public Task<AddressOutputDTO> CreateAddressAsync(AddressCreateInputDTO addressCreateInputDTO);
        public Task<List<AddressOutputDTO>> GetAllAddressesAsync();
        public Task<AddressOutputDTO> GetAddressByIdAsync(int id);
        public Task<AddressOutputDTO> UpdateAddressAsync(AddressUpdateInputDTO addressUpdateInputDTO);
        public Task<bool> DeleteAddressAsync(int adressId);

    }
}
