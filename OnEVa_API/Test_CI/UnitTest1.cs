using AutoMapper;
using BusinessesTests;
using DTOs.VehiclePropertiesDTOs.CreateInput;
using DTOs.VehiclePropertiesDTOs.Output;
using DTOs.VehiclePropertiesDTOs.UpdateInput;
using Entities;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Repositories;

namespace Test_CI
{
    public class BrandBusinessTests
    {
        private DbContextOptions<APIDbContext> _options;
        private IMapper _mapper;
        private SqliteConnection _connection;

        public BrandBusinessTests()
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
        public void Dispose()
        {
            _connection.Close();
        }

        #region Create
        [Fact]
        public async Task CreateBrandAsyncTestIsNotEmpty()
        {
            using (var context = new APIDbContext(_options))
            {
                BrandRepository brandRepository = new BrandRepository(context, _mapper);

                BrandCreateInputDTO brandCreateInputDTO = new BrandCreateInputDTO { Label = "LabelTest" };

                var mockBusiness = new MockBrandBusiness();

                var brandExpected = await mockBusiness.CreateBrandAsync(brandCreateInputDTO);

                // Asserts
                Assert.NotNull(brandExpected);
            }
        }

        [Fact]
        public async Task CreateBrandAsyncTestIsNotExistInDb()
        {

            using (var context = new APIDbContext(_options))
            {
                BrandRepository brandRepository = new BrandRepository(context, _mapper);

                BrandCreateInputDTO brandCreateInputDTO = new BrandCreateInputDTO { Label = "LabelTest" };

                var mockBusiness = new MockBrandBusiness();

                var brandExpected = await mockBusiness.CreateBrandAsync(brandCreateInputDTO);

                // Asserts
                Assert.NotNull(brandExpected);
            }
        }

        [Fact]
        public async Task CreateBrandAsyncTestExceptionExistsInDb()
        {
            using (var context = new APIDbContext(_options))
            {
                BrandCreateInputDTO brandCreateInputDTO = new BrandCreateInputDTO { Label = "LabelBrand" };

                var mockBusiness = new MockBrandBusiness();

                // Vérifie que la méthode lève une exception si la marque existe déjà dans la DB
                await Assert.ThrowsAsync<Exception>(async () =>
                    await mockBusiness.CreateBrandAsync(brandCreateInputDTO)
                );
            }
        }

        [Fact]
        public async Task CreateBrandAsyncTestExceptionLabelIsNullOrWhiteSpace()
        {
            using (var context = new APIDbContext(_options))
            {
                BrandCreateInputDTO brandCreateInputDTO = new BrandCreateInputDTO { Label = "" };

                var mockBusiness = new MockBrandBusiness();

                // Vérifie que la méthode lève une exception du type attendu
                await Assert.ThrowsAsync<Exception>(async () =>
                    await mockBusiness.CreateBrandAsync(brandCreateInputDTO)
                );
            }
        }

        #endregion

        #region Update
        [Fact]
        public async Task UpdateBrandAsyncTestExceptionLabelIsNullOrWhiteSpace()
        {
            using (var context = new APIDbContext(_options))
            {
                BrandUpdateInputDTO brandUpdateInputDTO = new BrandUpdateInputDTO { BrandId = 1, Label = " " };

                var mockBusiness = new MockBrandBusiness();

                await Assert.ThrowsAsync<Exception>(async () =>
                    await mockBusiness.UpdateBrandAsync(brandUpdateInputDTO)
                );
            }
        }

        [Fact]
        public async Task UpdateBrandAsyncTestExceptionIdEqualOrLessThanZero()
        {
            using (var context = new APIDbContext(_options))
            {
                BrandUpdateInputDTO brandUpdateInputDTO = new BrandUpdateInputDTO { BrandId = 0, Label = " " };

                var mockBusiness = new MockBrandBusiness();

                // Vérifie que la méthode lève une exception du type attendu
                await Assert.ThrowsAsync<Exception>(async () =>
                    await mockBusiness.UpdateBrandAsync(brandUpdateInputDTO)
                );
            }
        }


        [Fact]
        public async Task UpdateBrandAsyncTest()
        {
            using (var context = new APIDbContext(_options))
            {
                BrandUpdateInputDTO brandUpdateInputDTO = new BrandUpdateInputDTO { BrandId = 1, Label = "testUpdate" };

                var mockBusiness = new MockBrandBusiness();

                var brandUpdated = await mockBusiness.UpdateBrandAsync(brandUpdateInputDTO);

                Assert.Equal(brandUpdateInputDTO.Label, brandUpdated.Label);
            }
        }

        #endregion

        #region Delete

        [Fact]
        public async Task DeleteBrandAsyncTest()
        {
            using (var context = new APIDbContext(_options))
            {
                var mockBusiness = new MockBrandBusiness();

                BrandUpdateInputDTO brandUpdateInputDTO = new BrandUpdateInputDTO { BrandId = 1, Label = "testUpdate" };

                var brandId = brandUpdateInputDTO.BrandId;

                var brandDeleted = await mockBusiness.DeleteBrandAsync(brandId);

                Assert.True(brandDeleted);
            }
        }
        [Fact]
        public async Task DeleteBrandAsyncTestBrandIsNotInDb()
        {
            using (var context = new APIDbContext(_options))
            {
                var mockBusiness = new MockBrandBusiness();

                BrandUpdateInputDTO brandUpdateInputDTO = new BrandUpdateInputDTO { BrandId = 0, Label = "testUpdate" };

                var brandId = 0;

                // Vérifie que la méthode lève une exception du type attendu
                await Assert.ThrowsAsync<Exception>(async () =>
                    await mockBusiness.DeleteBrandAsync(brandId)
                );
            }
        }
        #endregion

        #region Get

        [Fact]
        public async Task GetBrandByLabelAsyncTest()
        {
            using (var context = new APIDbContext(_options))
            {
                BrandUpdateInputDTO brandUpdateInputDTO = new BrandUpdateInputDTO { BrandId = 1, Label = "LabelBrand" };

                var mockBusiness = new MockBrandBusiness();

                var brand = await mockBusiness.GetBrandByLabelAsync(brandUpdateInputDTO.Label);

                Assert.Equal(brandUpdateInputDTO.Label, brand.Label);
            }
        }

        [Fact]
        public async Task GetBrandByLabelAsyncTestLabelIsNullOrWhiteSpace()
        {
            using (var context = new APIDbContext(_options))
            {
                BrandUpdateInputDTO brandUpdateInputDTO = new BrandUpdateInputDTO { BrandId = 1, Label = " " };

                var mockBusiness = new MockBrandBusiness();

                // Vérifie que la méthode lève une exception du type attendu
                await Assert.ThrowsAsync<Exception>(async () =>
                    await mockBusiness.GetBrandByLabelAsync(brandUpdateInputDTO.Label)
                );
            }
        }
        #endregion
    }
}