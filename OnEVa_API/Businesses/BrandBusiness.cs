using AutoMapper;
using DTOs.VehiclePropertiesDTOs.CreateInput;
using DTOs.VehiclePropertiesDTOs.Output;
using DTOs.VehiclePropertiesDTOs.UpdateInput;
using Entities;
using IBusinesses;
using IRepositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Businesses
{
    public class BrandBusiness : IBrandBusiness
    {
        private readonly IBrandRepository _brandRepository;
        private readonly IMapper _mapper;
        public BrandBusiness(IBrandRepository brandRepository, IMapper mapper)
        {
            _brandRepository = brandRepository;
            _mapper = mapper;
        }

        public async Task<BrandOutputDTO> CreateBrandAsync(BrandCreateInputDTO brandCreateInputDTO)
        {
            var isExist = await _brandRepository.GetBrandByLabelAsync(brandCreateInputDTO.Label);

            if (string.IsNullOrWhiteSpace(brandCreateInputDTO.Label))
            {
                throw new Exception("Brand format is not correct.");
            }
            else if (isExist != null)
            {
                throw new Exception($"This brand : {brandCreateInputDTO.Label} already exists.");
            }
            else
            {
                Brand brand = _mapper.Map<Brand>(brandCreateInputDTO);
                return await _brandRepository.CreateBrandAsync(brand);
            }
        }

        public async Task<BrandOutputDTO> UpdateBrandAsync(BrandUpdateInputDTO brandUpdateInputDTO)
        {
            if (string.IsNullOrWhiteSpace(brandUpdateInputDTO.Label) || brandUpdateInputDTO.BrandId <= 0)
            {
                throw new Exception("Field required is empty.");
            }
            else
            {
                var isExist = await _brandRepository.GetBrandByLabelAsync(brandUpdateInputDTO.Label);
                if (isExist != null && isExist.BrandId == brandUpdateInputDTO.BrandId)
                {
                    Brand brand = _mapper.Map<Brand>(brandUpdateInputDTO);
                    return await _brandRepository.UpdateBrandAsync(brand);
                }
                else if (isExist == null)
                {
                    Brand brand = _mapper.Map<Brand>(brandUpdateInputDTO);
                    return await _brandRepository.UpdateBrandAsync(brand);
                }
                else
                {
                    throw new Exception($"A Brand with this label ({brandUpdateInputDTO.Label}) already exists.");
                }
            }
        }

        public async Task<List<BrandOutputDTO>> GetAllBrandsAsync()
        {
            return await _brandRepository.GetAllBrandsAsync();
        }

        public async Task<BrandOutputDTO> GetBrandByIdAsync(int brandId)
        {
            return await _brandRepository.GetBrandByIdAsync(brandId);
        }

        public async Task<BrandOutputDTO> GetBrandByLabelAsync(string label)
        {
            if (string.IsNullOrWhiteSpace(label))
            {
                throw new Exception("Brand format is not correct.");
            }
            else return await _brandRepository.GetBrandByLabelAsync(label);
        }

        public async Task<bool> DeleteBrandAsync(int brandId)
        {
            var brand = await _brandRepository.GetBrandByIdAsync(brandId);

            if (brand == null)
            {
                throw new Exception("Cannot delete a brand that does not exist");
            }
            else return await _brandRepository.DeleteBrandAsync(brandId);
        }
    }
}
