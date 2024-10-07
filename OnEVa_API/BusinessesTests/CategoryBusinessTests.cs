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
using DTOs.VehiclePropertiesDTOs.CreateInput;
using BusinessesTests;
using System.Reflection.Emit;
using AutoMapper;
using DTOs.VehiclePropertiesDTOs.Output;
using DTOs.VehiclePropertiesDTOs.UpdateInput;

namespace Businesses.Tests;

[TestClass()]
public class CategoryBusinessTests
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
            cfg.CreateMap<CategoryUpdateInputDTO, Category>();
            cfg.CreateMap<Category, CategoryOutputDTO>();
        });
    }

    [TestMethod()]
    public async Task CreateCategoryAsyncTestIsNotEmptyAndNotExistInDb()
    {
        var connection = new SqliteConnection("DataSource = :memory:");
        connection.Open();

        var options = new DbContextOptionsBuilder<APIDbContext>().UseSqlite(connection).Options;

        using (var context = new APIDbContext(options))
        {
            context.Database.EnsureCreated();

            CategoryRepository categoryRepository = new CategoryRepository(context, _mapper);

            CategoryCreateInputDTO categoryCreateInputDTO = new CategoryCreateInputDTO { Label = "LabelTest" };

            var mockBusiness = new MockCategoryBusiness();

            var categoryExpected = await mockBusiness.CreateCategoryAsync(categoryCreateInputDTO);

            //Assert
            Assert.IsNotNull(categoryExpected);
        }
    }

    [TestMethod()]
    [ExpectedException(typeof(Exception))]
    public async Task CreateCategoryAsyncTestExceptionExistsInDb()
    {
        var connection = new SqliteConnection("DataSource=:memory:");
        connection.Open();

        var options = new DbContextOptionsBuilder<APIDbContext>().UseSqlite(connection).Options;

        using (var context = new APIDbContext(options))
        {
            context.Database.EnsureCreated();

            CategoryCreateInputDTO categoryCreateInputDTO = new CategoryCreateInputDTO { Label = "LabelCategory" };

            var mockBusiness = new MockCategoryBusiness();

            var categoryExpected = await mockBusiness.CreateCategoryAsync(categoryCreateInputDTO);

        }
    }

    [TestMethod()]
    [ExpectedException(typeof(Exception))]
    public async Task CreateCategoryAsyncTestExceptionIsNull()
    {

        var connection = new SqliteConnection("DataSource=:memory:");
        connection.Open();

        var options = new DbContextOptionsBuilder<APIDbContext>().UseSqlite(connection).Options;

        using (var context = new APIDbContext(options))
        {
            context.Database.EnsureCreated();

            CategoryCreateInputDTO categoryCreateInputDTO = new CategoryCreateInputDTO { Label = "" };

            var mockBusiness = new MockCategoryBusiness();

            var categoryExpected = await mockBusiness.CreateCategoryAsync(categoryCreateInputDTO);

        }
    }

}