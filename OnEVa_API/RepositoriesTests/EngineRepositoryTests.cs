using AutoMapper;
using DTOs.VehiclePropertiesDTOs.Output;
using DTOs.VehiclePropertiesDTOs.UpdateInput;
using Entities;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.Tests
{
	[TestClass()]
	public class EngineRepositoryTests
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
				cfg.CreateMap<EngineUpdateInputDTO, Engine>();
				cfg.CreateMap<Engine, EngineOutputDTO>();
			});
		}

		[TestMethod()]
		public async Task CreateEngineAsyncTest()
		{

			using (var context = new APIDbContext(_options))
			{

				EngineRepository engineRepository = new EngineRepository(context, _mapper);

				//Arrange
				Engine engine1 = new Engine { Id = 1, Label = "EngineTest1" };

				//Act
				var engineExpected = await engineRepository.CreateEngineAsync(engine1);

				// Assert
				Assert.AreEqual(engineExpected.Label, engine1.Label);

			}
		}
	}
}