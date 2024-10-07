using DTOs.VehiclePropertiesDTOs.CreateInput;
using DTOs.VehiclePropertiesDTOs.Output;
using DTOs.VehiclePropertiesDTOs.UpdateInput;
using Entities;
using IBusinesses;
using IRepositories;
using Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessesTests
{
    public class MockBrandBusiness : IBrandBusiness
    {
        Brand brandInDb1 = new Brand { Id = 1, Label = "LabelBrand" };
        Brand brandInNotDb = new Brand { Id = 0, Label = "LabelBrand" };

        public async Task<BrandOutputDTO> CreateBrandAsync(BrandCreateInputDTO brandCreateInputDTO)
        {

            if (string.IsNullOrWhiteSpace(brandCreateInputDTO.Label))
            {
                throw new Exception("Brand format is not correct.");

            }
            else if (brandCreateInputDTO.Label == brandInDb1.Label)
            {
                throw new Exception($"This brand : {brandCreateInputDTO.Label} already exists.");
            }
            else
            {
                BrandOutputDTO brand = new BrandOutputDTO { Label = brandCreateInputDTO.Label };
                return brand;
            }
        }

        public async Task<BrandOutputDTO> UpdateBrandAsync(BrandUpdateInputDTO brandUpdateInputDTO)
        {
            if (string.IsNullOrWhiteSpace(brandUpdateInputDTO.Label) || brandUpdateInputDTO.BrandId <= 0)
            {
                throw new Exception("Field required empty.");
            }
            else
            {
                BrandOutputDTO brand = new BrandOutputDTO { Label = brandUpdateInputDTO.Label };
                return brand;
            }
        }

        public async Task<bool> DeleteBrandAsync(int brandId)
        {
           
            if (brandId == brandInNotDb.Id)
            {
                throw new Exception("Cannot delete a brand that does not exist");
            }
            else return true;
        }

        public async Task<List<BrandOutputDTO>> GetAllBrandsAsync()
        {
            throw new NotImplementedException();
        }

        public async Task<BrandOutputDTO> GetBrandByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<BrandOutputDTO> GetBrandByLabelAsync(string label)
        {
            if (string.IsNullOrWhiteSpace(label))
            {
                throw new Exception("Brand format is not correct.");
            }
            else
            {
                BrandOutputDTO brand = new BrandOutputDTO { Label = brandInDb1.Label };
                
                return brand;
            }
        }
    }
}
