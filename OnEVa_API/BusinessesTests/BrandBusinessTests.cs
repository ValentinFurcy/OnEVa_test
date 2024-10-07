using Microsoft.VisualStudio.TestTools.UnitTesting;
using Businesses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entities;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Repositories;
using DTOs.VehiclePropertiesDTOs.Output;
using DTOs.VehiclePropertiesDTOs.CreateInput;
using BusinessesTests;
using DTOs.VehiclePropertiesDTOs.UpdateInput;
using AutoMapper;

namespace Businesses.Tests
{
    [TestClass()]
    public class BrandBusinessTests
    {
        private DbContextOptions<APIDbContext> _options;
        private IMapper _mapper;

		[TestInitialize]
        public void Initialize()
        {
            var connection = new SqliteConnection("DataSource=:memory:");
            connection.Open();

            _options = new DbContextOptionsBuilder<APIDbContext>()
                .UseSqlite(connection)
                .Options;

            using (var context = new APIDbContext(_options))
            {
                context.Database.EnsureCreated();
            }

            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<BrandUpdateInputDTO, Brand>();
                cfg.CreateMap<Brand, BrandOutputDTO>();
            });
        }

        #region Create
        [TestMethod()]
        public async Task CreateBrandAsyncTestIsNotEmpty()
        {
            using (var context = new APIDbContext(_options))
            {
                BrandRepository brandRepository = new BrandRepository(context, _mapper);

                BrandCreateInputDTO brandCreateInputDTO = new BrandCreateInputDTO { Label = "LabelTest" };

                var mockBusiness = new MockBrandBusiness();

                var brandExpected = await mockBusiness.CreateBrandAsync(brandCreateInputDTO);

                // Asserts
                Assert.IsNotNull(brandExpected);
            }
        }

        [TestMethod()]
        public async Task CreateBrandAsyncTestIsNotExistInDb()
        {

            using (var context = new APIDbContext(_options))
            {
                BrandRepository brandRepository = new BrandRepository(context, _mapper);

                BrandCreateInputDTO brandCreateInputDTO = new BrandCreateInputDTO { Label = "LabelTest" };

                var mockBusiness = new MockBrandBusiness();

                var brandExpected = await mockBusiness.CreateBrandAsync(brandCreateInputDTO);

                // Asserts
                Assert.IsNotNull(brandExpected);
            }
        }

        [TestMethod()]
        [ExpectedException(typeof(Exception))]
        public async Task CreateBrandAsyncTestExceptionExistsInDb()
        {
            using (var context = new APIDbContext(_options))
            {
                BrandCreateInputDTO brandCreateInputDTO = new BrandCreateInputDTO { Label = "LabelBrand" };

                var mockBusiness = new MockBrandBusiness();

                var brandExpected = await mockBusiness.CreateBrandAsync(brandCreateInputDTO);
            }
        }

        [TestMethod()]
        [ExpectedException(typeof(Exception))]
        public async Task CreateBrandAsyncTestExceptionLabelIsNullOrWhiteSpace()
        {
            using (var context = new APIDbContext(_options))
            {
                BrandCreateInputDTO brandCreateInputDTO = new BrandCreateInputDTO { Label = "" };

                var mockBusiness = new MockBrandBusiness();

                await mockBusiness.CreateBrandAsync(brandCreateInputDTO);
            }
        }

        #endregion

        #region Update
        [TestMethod()]
        [ExpectedException(typeof(Exception))]
        public async Task UpdateBrandAsyncTestExceptionLabelIsNullOrWhiteSpace()
        {
            using (var context = new APIDbContext(_options))
            {
                BrandUpdateInputDTO brandUpdateInputDTO = new BrandUpdateInputDTO { BrandId = 1, Label = " " };

                var mockBusiness = new MockBrandBusiness();

                await mockBusiness.UpdateBrandAsync(brandUpdateInputDTO);
            }
        }

        [TestMethod()]
        [ExpectedException(typeof(Exception))]
        public async Task UpdateBrandAsyncTestExceptionIdEqualOrLessThanZero()
        {
            using (var context = new APIDbContext(_options))
            {
                BrandUpdateInputDTO brandUpdateInputDTO = new BrandUpdateInputDTO { BrandId = 0, Label = " " };

                var mockBusiness = new MockBrandBusiness();

                await mockBusiness.UpdateBrandAsync(brandUpdateInputDTO);
            }
        }

        [TestMethod()]
        public async Task UpdateBrandAsyncTest()
        {
            using (var context = new APIDbContext(_options))
            {
                BrandUpdateInputDTO brandUpdateInputDTO = new BrandUpdateInputDTO { BrandId = 1, Label = "testUpdate" };

                var mockBusiness = new MockBrandBusiness();

                var brandUpdated = await mockBusiness.UpdateBrandAsync(brandUpdateInputDTO);

                Assert.AreEqual(brandUpdateInputDTO.Label, brandUpdated.Label);
            }
        }

        #endregion

        #region Delete

        [TestMethod()]
        public async Task DeleteBrandAsyncTest()
        {
            using (var context = new APIDbContext(_options))
            {
                var mockBusiness = new MockBrandBusiness();

                BrandUpdateInputDTO brandUpdateInputDTO = new BrandUpdateInputDTO { BrandId = 1, Label = "testUpdate" };

                var brandId = brandUpdateInputDTO.BrandId;

                var brandDeleted = await mockBusiness.DeleteBrandAsync(brandId);

                Assert.IsTrue(brandDeleted);
            }
        }

        [TestMethod()]
        [ExpectedException(typeof(Exception))]
        public async Task DeleteBrandAsyncTestBrandIsNotInDb()
        {
            using (var context = new APIDbContext(_options))
            {
                var mockBusiness = new MockBrandBusiness();

                BrandUpdateInputDTO brandUpdateInputDTO = new BrandUpdateInputDTO { BrandId = 0, Label = "testUpdate" };

                var brandId = 0;

                 await mockBusiness.DeleteBrandAsync(brandId);
            }
        }
        #endregion

        #region Get

        [TestMethod()]
        public async Task GetBrandByLabelAsyncTest()
        {
            using (var context = new APIDbContext(_options))
            {
                BrandUpdateInputDTO brandUpdateInputDTO = new BrandUpdateInputDTO { BrandId = 1, Label = "LabelBrand" };

                var mockBusiness = new MockBrandBusiness();

                var brand = await mockBusiness.GetBrandByLabelAsync(brandUpdateInputDTO.Label);
               
                Assert.AreEqual(brandUpdateInputDTO.Label, brand.Label);
            }         
        }

        [TestMethod()]
        [ExpectedException(typeof(Exception))]
        public async Task GetBrandByLabelAsyncTestLabelIsNullOrWhiteSpace()
        {
            using (var context = new APIDbContext(_options))
            {
                BrandUpdateInputDTO brandUpdateInputDTO = new BrandUpdateInputDTO { BrandId = 1, Label = " " };

                var mockBusiness = new MockBrandBusiness();

                await mockBusiness.GetBrandByLabelAsync(brandUpdateInputDTO.Label);
            }
        }
        #endregion
    }
}