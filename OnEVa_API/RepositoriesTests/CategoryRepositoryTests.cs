using AutoMapper;
using Entities;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Repositories;
using System.Net.Sockets;


namespace Repositories.Tests;

[TestClass()]
public class CategoryRepositoryTests
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
    }


    [TestMethod()]
    public async Task CreateCategoryAsyncTest()
    {
        var connection = new SqliteConnection("DataSource = :memory:");
        connection.Open();

        var options = new DbContextOptionsBuilder<APIDbContext>().UseSqlite(connection).Options;

        using (var context = new APIDbContext(options))
        {
            context.Database.EnsureCreated();
            CategoryRepository categoryRepository = new CategoryRepository(context, _mapper);

            //Arrange
            Category category = new Category { Id = 1, Label = "CategoryTest1" };

            //Act
            var categoryExpected = await categoryRepository.CreateCategoryAsync(category);

            //Assert
            Assert.AreEqual(categoryExpected.Label, category.Label);
        }
    }
}