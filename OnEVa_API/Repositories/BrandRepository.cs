using AutoMapper;
using DTOs.VehiclePropertiesDTOs.Output;
using Entities;
using IRepositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Repositories
{
    public class BrandRepository : IBrandRepository
    {
        private readonly APIDbContext _context;
        private readonly IMapper _mapper;
        public BrandRepository(APIDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<BrandOutputDTO> CreateBrandAsync(Brand brand)
        {
            try
            {
                await _context.Brands.AddAsync(brand);
                await _context.SaveChangesAsync();

                return await GetBrandByIdAsync(brand.Id);
            }
            catch (Exception)
            {
                throw new Exception("Error ! The new brand was not created");
            };
        }

        public async Task<BrandOutputDTO> UpdateBrandAsync(Brand brand)
        {
            try
            {
                var nbRows = await _context.Brands.Where(b => b.Id == brand.Id).ExecuteUpdateAsync(
                    updates => updates.SetProperty(b => b.Label, brand.Label));
                if (nbRows > 0)
                {
                    return await GetBrandByIdAsync(brand.Id);
                }
                throw new Exception("The update failed");
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        
        public async Task<List<BrandOutputDTO>> GetAllBrandsAsync()
        {
            try
            {
                var brands = await _context.Brands.ToListAsync();
                
                if (brands.Any())
                {
                    var brandsOutputDTO = _mapper.Map<List<BrandOutputDTO>>(brands);
                    return brandsOutputDTO;
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

        public async Task<BrandOutputDTO> GetBrandByIdAsync(int id)
        {
            try
            {
                var brand = await _context.Brands.FindAsync(id);
                if (brand != null)
                {
                    BrandOutputDTO brandOutputDTO = _mapper.Map<BrandOutputDTO>(brand);
                    return brandOutputDTO;
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

        public async Task<BrandOutputDTO> GetBrandByLabelAsync(string label)
        {
            try
            {
                Brand brand = await _context.Brands.FirstOrDefaultAsync(b => b.Label == label);
                if (brand == null)
                {
                    return null;
                }
                else
                {
                    var brandOutputDTO = _mapper.Map<BrandOutputDTO>(brand);
                    return brandOutputDTO;
                }
            }
            catch (Exception e)
            {
                throw new Exception("Error ! The new brand was not created", e);
            }
        }

        public async Task<bool> DeleteBrandAsync(int brandId)
        {
            try
            {
                var brandDeleted = await _context.Brands.Where(b => b.Id == brandId).ExecuteDeleteAsync();
                if (brandDeleted > 0)
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

    }
}
