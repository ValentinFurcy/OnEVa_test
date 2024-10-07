using AutoMapper;
using DTOs.VehiclePropertiesDTOs.UpdateInput;
using Entities;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.Tests
{
    [TestClass()]
    public class BrandRepositoryTests
    {
        private DbContextOptions<APIDbContext> _options;
        private readonly IMapper _mapper;

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
		}

		[TestMethod()]
		public async Task CreateBrandAsyncTest()
		{
			using (var context = new APIDbContext(_options))
			{

                BrandRepository brandRepository = new BrandRepository(context, _mapper);

				//Arrange
				Brand brand1 = new Brand { Id = 1, Label = "BrandTest1" };

				//Act
				var brandExpected = await brandRepository.CreateBrandAsync(brand1);

				// Assert
				Assert.AreEqual(brand1.Label, brandExpected.Label);
			}
		}

		[TestMethod()]
		[ExpectedException(typeof(Exception))]
		public async Task CreateBrandAsyncTestExceptionOnConnectionError()
		{
			var connection = new SqliteConnection("DataSource=:memory:");
			connection.Close();

			_options = new DbContextOptionsBuilder<APIDbContext>()
				.UseSqlite(connection)
				.Options;

			using (var context = new APIDbContext(_options))
			{
				context.Database.EnsureCreated();
			}

            using (var context = new APIDbContext(_options))
            {
                BrandRepository brandRepository = new BrandRepository(context, _mapper);

				//Arrange
				Brand brand1 = new Brand { Id = 1, Label = "BrandTest1" };

				//Act
				await brandRepository.CreateBrandAsync(brand1);
			}
		}

        [TestMethod()]
        public async Task DeleteBrandAsyncTest()
        {
            using (var context = new APIDbContext(_options))
            {
                BrandRepository brandRepository = new BrandRepository(context, _mapper);

				//Arrange
				Brand brand = new Brand { Id = 1, Label = "BrandTest1" };

				//Act
				await brandRepository.DeleteBrandAsync(brand.Id);
				bool brandIsExists = await context.Brands.AnyAsync();
				// Assert
				Assert.IsFalse(brandIsExists);
			}
		}

        [TestMethod()]
        public async Task GetAllBrandsAsyncTest()
        {
            using (var context = new APIDbContext(_options))
            {
                BrandRepository brandRepository = new BrandRepository(context, _mapper);

				//Arrange

				await context.Brands.AddAsync(new Brand { Id = 1, Label = "BrandTest1" });
				await context.Brands.AddAsync(new Brand { Id = 2, Label = "BrandTest2" });
				await context.Brands.AddAsync(new Brand { Id = 3, Label = "BrandTest3" });

				context.SaveChanges();
				//Act
				var brands = await brandRepository.GetAllBrandsAsync();

				// Assert
				Assert.AreEqual(3, brands.Count());
			}
		}

        [TestMethod()]
        public async Task GetAllBrandsAsyncTestEmpty()
        {
            using (var context = new APIDbContext(_options))
            {
                BrandRepository brandRepository = new BrandRepository(context, _mapper);

				//Act
				var brands = await brandRepository.GetAllBrandsAsync();

				// Assert
				Assert.IsNull(brands);
			}
		}

        [TestMethod()]
        public async Task GetBrandByIdAsyncTest()
        {
            using (var context = new APIDbContext(_options))
            {
                BrandRepository brandRepository = new BrandRepository(context, _mapper);

				//Arrange
				Brand brand1 = new Brand { Id = 1, Label = "BrandTest1" };
				await context.Brands.AddAsync(brand1);

				context.SaveChanges();
				//Act
				var brand = await brandRepository.GetBrandByIdAsync(1);

				// Assert
				Assert.AreEqual(brand1.Label = "BrandTest1", brand.Label);
			}
		}

        [TestMethod()]
        public async Task GetBrandByIdAsyncTestReturnNull()
        {
            using (var context = new APIDbContext(_options))
            {
                BrandRepository brandRepository = new BrandRepository(context, _mapper);

				//Arrange
				Brand brand1 = new Brand { Id = 1, Label = "BrandTest1" };
				await context.Brands.AddAsync(brand1);

				context.SaveChanges();
				//Act
				var brand = await brandRepository.GetBrandByIdAsync(2);

				// Assert
				Assert.IsNull(brand);
			}
		}

        [TestMethod()]
        public async Task GetBrandByLabelAsyncTest()
        {
            using (var context = new APIDbContext(_options))
            {
                BrandRepository brandRepository = new BrandRepository(context, _mapper);

				//Arrange
				Brand brand1 = new Brand { Id = 1, Label = "BrandTest1" };
				await context.Brands.AddAsync(brand1);

				context.SaveChanges();
				//Act

				var brand = await brandRepository.GetBrandByLabelAsync("BrandTest1");

				// Assert
				Assert.AreEqual(brand1.Label, brand.Label);
			}
		}

        [TestMethod()]
        public async Task GetBrandByLabelAsyncTestReturnNull()
        {
            using (var context = new APIDbContext(_options))
            {
                BrandRepository brandRepository = new BrandRepository(context, _mapper);

				//Arrange
				Brand brand1 = new Brand { Id = 1, Label = "BrandTest1" };
				await context.Brands.AddAsync(brand1);

				await context.SaveChangesAsync();
				//Act

				var brand = await brandRepository.GetBrandByLabelAsync("BrandInconue");

				// Assert
				Assert.IsNull(brand);
			}
		}

        [TestMethod()]
        public async Task UpdateBrandAsyncTest()
        {
            using (var context = new APIDbContext(_options))
            {
                BrandRepository brandRepository = new BrandRepository(context, _mapper);

				//Arrange

				await context.Brands.AddAsync(new Brand { Id = 1, Label = "BrandTest1" });
				await context.Brands.AddAsync(new Brand { Id = 2, Label = "BrandTest2" });
				await context.Brands.AddAsync(new Brand { Id = 3, Label = "BrandTest3" });

				await context.SaveChangesAsync();
				//Act
				var brandToUpdate = new Brand { Id = 2, Label = "BrandUpdate" };
				var brandUpdated = await brandRepository.UpdateBrandAsync(brandToUpdate);

				// Assert
				Assert.AreEqual("BrandUpdate", brandUpdated.Label);
			}
		}
	}
}