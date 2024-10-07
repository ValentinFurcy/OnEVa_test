using Businesses;
using IBusinesses;
using IRepositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using Repositories;
using System.Reflection;
using Swashbuckle.AspNetCore.Filters;
using DTOs.VehiclePropertiesDTOs.UpdateInput;
using Entities;
using DTOs.VehiclePropertiesDTOs.Output;
using DTOs.StateDTOs;
using DTOs.PersonDTOs;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using DTOs.RentalDTO;
using DTOs.VehicleDTOs;
using DTOs.VehiclePropertiesDTOs.CreateInput;
using DTOs.Address;
using Businesses.Tools;
using OnEVa_API.Fixtures;
using OnEVa_API.Tools;
using System.Text.Json.Serialization;


var builder = WebApplication.CreateBuilder(args);

builder.Services.AddAutoMapper(c =>
{
	c.CreateMap<EngineUpdateInputDTO, Engine>();
	c.CreateMap<Engine, EngineOutputDTO>()
    .ForMember(dest => dest.EngineId, c => c.MapFrom(src => src.Id));
    c.CreateMap<EngineCreateInputDTO, Engine>();
   
	c.CreateMap<StateCreateInputDTO, State>();
	c.CreateMap<State, StateOutputDTO>();

	c.CreateMap<PersonCreateInputDTO, Person>();
	c.CreateMap<Person, PersonOutputDTO>()
	.ForMember(dest => dest.Email, 
	c => c.MapFrom(src => src.AppUser.Email));
	c.CreateMap<PersonUpdateInputDTO, Person>();
    c.CreateMap<PersonOutputDTO, Person>();

	c.CreateMap<VehicleCreateInputDTO, Vehicle>();
    c.CreateMap<Vehicle, VehicleOutputDTO>();

	c.CreateMap<PictureCreateInputDTO, Picture>();
	c.CreateMap<Picture, PictureOutputDTO>();
	c.CreateMap<PictureUpdateInputDTO, Picture>();

    c.CreateMap<BrandCreateInputDTO, Brand>();
    c.CreateMap<BrandUpdateInputDTO, Brand>().ForMember(dest=> dest.Id, c=>c.MapFrom(src=>src.BrandId));
    c.CreateMap<Brand, BrandOutputDTO>().ForMember(dest=>dest.BrandId, c=>c.MapFrom(src=>src.Id));
   
    c.CreateMap<CategoryCreateInputDTO, Category>();
    c.CreateMap<CategoryUpdateInputDTO, Category>().ForMember(dest => dest.Id, c => c.MapFrom(src => src.CategoryId));
    c.CreateMap<Category, CategoryOutputDTO>().ForMember(dest => dest.CategoryId, c => c.MapFrom(src => src.Id));

	c.CreateMap<AddressCreateInputDTO, Address>();
    c.CreateMap<Address, AddressOutputDTO>();
    c.CreateMap<AddressUpdateInputDTO, Address>()
	.ForMember(dest => dest.Id, a => a.MapFrom(src => src.AddressId));


    c.CreateMap<Rental,  RentalOutputDTO>()
    .ForMember(dest => dest.StateLabel, 
        c => c.MapFrom(src => src.State.Label))
        .ForMember(dest => dest.FirstNamePerson, 
        c => c.MapFrom(src => src.Person.FirstName))
        .ForMember(dest => dest.LastNamePerson,
        c => c.MapFrom(src => src.Person.LastName))
        .ForMember(dest => dest.VehicleOutput, 
        c => c.MapFrom(src => src.Vehicle));

    c.CreateMap<RentalCreateInputDTO, Rental>();
});

// Add services to the container.
builder.Services.AddAuthorization();

builder.Services.AddIdentityApiEndpoints<AppUser>()
    .AddRoles<IdentityRole>()
    .AddEntityFrameworkStores<APIDbContext>();

builder.Services.AddControllers();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "My API", Version = "v1" });
    // Set the comments path for the Swagger JSON and UI.
    var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
    c.IncludeXmlComments(xmlPath);

    c.AddSecurityDefinition("oauth2", new Microsoft.OpenApi.Models.OpenApiSecurityScheme
    {
        In = Microsoft.OpenApi.Models.ParameterLocation.Header,
        Name = "Authorization",
        Type = Microsoft.OpenApi.Models.SecuritySchemeType.ApiKey
    });
    c.OperationFilter<SecurityRequirementsOperationFilter>();
}
);

builder.Services.AddScoped<IBrandRepository, BrandRepository>();
builder.Services.AddScoped<IBrandBusiness, BrandBusiness>();

builder.Services.AddScoped<IStateRepository, StateRepository>();
builder.Services.AddScoped<IStateBusiness, StateBusiness>();

builder.Services.AddScoped<IEngineRepository, EngineRepository>();
builder.Services.AddScoped<IEngineBusiness, EngineBusiness>();

builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();
builder.Services.AddScoped<ICategoryBusiness, CategoryBusiness>();

builder.Services.AddScoped<IModelRepository, ModelRepository>();
builder.Services.AddScoped<IModelBusiness, ModelBusiness>();

builder.Services.AddScoped<IPersonBusiness, PersonBusiness>();
builder.Services.AddScoped<IPersonRepository, PersonRepository>();

builder.Services.AddScoped<IPictureRepository, PictureRepository>();
builder.Services.AddScoped<IPictureBusiness, PictureBusiness>();

builder.Services.AddScoped<IAddressRepository, AddressRepository>();
builder.Services.AddScoped<IAddressBusiness, AddressBusiness>();

builder.Services.AddScoped<IRentalRepository, RentalRepository>();
builder.Services.AddScoped<IRentalBusiness, RentalBusiness>();

builder.Services.AddScoped<IVehicleRepository, VehicleRepository>();
builder.Services.AddScoped<IVehicleBusiness, VehicleBusiness>();

builder.Services.AddScoped<ICarpoolBusiness, CarpoolBusiness>();
builder.Services.AddScoped<ICarpoolRepository, CarpoolRepository>();

builder.Services.AddScoped<ToolsCarpool>();
builder.Services.AddScoped<SerializeHelper>();
builder.Services.AddScoped<DatasFixture>();
builder.Services.AddScoped<AuthBusiness>();
builder.Services.AddScoped<PicturesHelpers>();
builder.Services.AddScoped<PersonFixture>();

builder.Services.AddDbContext<APIDbContext>(o =>
{
    var connectionString = builder.Configuration["ConnectionStrings:OnEVa_BDD"];

    if (string.IsNullOrEmpty(connectionString))
    {
        throw new InvalidOperationException("Connection string 'DefaultConnection' is not found.");
    }
    o.UseSqlServer(connectionString);
    //o.UseSqlServer(builder.Configuration.GetConnectionString("OnEVa_BDD"));
});

builder.Services.AddCors(o =>
{
    o.AddDefaultPolicy(policy => { policy.WithOrigins(["http://localhost:4200"]).AllowAnyMethod().AllowAnyHeader(); });
});
var app = builder.Build();

app.MapIdentityApi<AppUser>();


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    builder.Configuration.AddUserSecrets<Program>();
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
