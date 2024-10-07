using AutoMapper;
using DTOs.Address;
using DTOs.VehiclePropertiesDTOs.Output;
using Entities;
using IRepositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories
{
    public class AddressRepository : IAddressRepository
    {
        private readonly APIDbContext _context;
        private readonly IMapper _mapper;

        public AddressRepository(APIDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<AddressOutputDTO> CreateAddressAsync(Address address)
        {
            try
            {
                await _context.Addresses.AddAsync(address);
                await _context.SaveChangesAsync();

                return await GetAddressByIdAsync(address.Id);
            }
            catch (Exception)
            {
                throw new Exception("Error ! The new adress was not created");
            };
        }

        public async Task<bool> DeleteAddressAsync(int addressId)
        {
            try
            {
                var addressDeleted = await _context.Addresses.Where(a => a.Id == addressId).ExecuteDeleteAsync();
                if (addressDeleted > 0)
                {
                    return true;
                }
                else return false;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public async Task<AddressOutputDTO> GetAddressByFullAddressAsync(Address address)
        {
            try
            {
                var addressExists = await _context.Addresses.FirstOrDefaultAsync(a =>
                        a.StreetNb == address.StreetNb &&
                        a.StreetName == address.StreetName &&
                        a.City == address.City &&
                        a.PostCode == address.PostCode);
                if (addressExists != null)
                {
                    var addressOutputDTO = _mapper.Map<AddressOutputDTO>(address);
                    return addressOutputDTO;
                }
                else return null;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public async Task<AddressOutputDTO> GetAddressByIdAsync(int addressId)
        {
            try
            {
                var address = await _context.Addresses.FindAsync(addressId);
                if (address != null)
                {
                    AddressOutputDTO addressOutputDTO = _mapper.Map<AddressOutputDTO>(address);

                    return addressOutputDTO;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }

        }

        public async Task<List<AddressOutputDTO>> GetAllAddressesAsync()
        {
            try
            {
                var addresses = await _context.Addresses.ToListAsync();
                if (addresses.Any())
                {
                    List<AddressOutputDTO> addressOutputDTO = _mapper.Map<List<AddressOutputDTO>>(addresses);

                    return addressOutputDTO;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public async Task<AddressOutputDTO> UpdateAddressAsync(Address address)
        {
            try
            {
                var nbRows = await _context.Addresses.Where(a => a.Id == address.Id).ExecuteUpdateAsync(
                updates => updates.SetProperty(a => a.StreetNb, address.StreetNb)
                .SetProperty(a => a.StreetName, address.StreetName)
                .SetProperty(a => a.City, address.City)
                .SetProperty(a => a.PostCode, address.PostCode));
                if (nbRows > 0)
                {
                    return await GetAddressByIdAsync(address.Id);
                }
                throw new Exception("The update unsucced");
            }     
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
    }
}
