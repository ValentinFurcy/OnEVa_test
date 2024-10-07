using DTOs.VehiclePropertiesDTOs.Output;
using Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;


namespace Repositories
{
    public class APIDbContext : IdentityDbContext<AppUser>
	{
		public DbSet<Address> Addresses { get; set; }
		public DbSet<Vehicle> Vehicles { get; set; }
		public DbSet<Rental> Rentals { get; set; }
		public DbSet<State> States { get; set; }
		public DbSet<Person> Persons { get; set; }
		public DbSet<Carpool> Carpools { get; set; }
		public DbSet<Brand> Brands { get; set; }
		public DbSet<Engine> Engines { get; set; }
		public DbSet<Model> Models { get; set; }
		public DbSet<Category> Categories { get; set; }
		public DbSet<Picture> Pictures { get; set; }
		public DbSet<Step> Steps { get; set; }

		public APIDbContext(DbContextOptions dbContextOptions) : base(dbContextOptions) { }

		protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
		{
			if (!optionsBuilder.IsConfigured)
			{
				optionsBuilder.UseSqlite("Data Source=OnEVa_BDD.db");
			}
		}

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			//Permet de préciser les relations pour enlever les ambiguïtés
			modelBuilder.Entity<Carpool>().HasOne(c => c.StartAddress).WithMany(a => a.StartAddressCarpool).HasForeignKey(c => c.StartAddressId).OnDelete(DeleteBehavior.NoAction);
			modelBuilder.Entity<Carpool>().HasOne(c => c.EndAddress).WithMany(a => a.EndAddressCarpool).HasForeignKey(c => c.EndAddressId).OnDelete(DeleteBehavior.NoAction);
			modelBuilder.Entity<Carpool>().HasMany(c => c.Attendees).WithMany(p => p.CarpoolsAttended);
			modelBuilder.Entity<Carpool>().HasOne(c => c.Organizer).WithMany(p => p.CarpoolsOrganized).HasForeignKey(c => c.OrganizerId).OnDelete(DeleteBehavior.NoAction);

			//Data by Default in the State table
			modelBuilder.Entity<State>().HasData(new State { Id = 1, Label = "On going" }, new State { Id = 2, Label = "Completed" });

			base.OnModelCreating(modelBuilder);
		}

		internal async Task<ModelOutputDTO> GetModelByIdAsync(int id)
		{
			throw new NotImplementedException();
		}
	}
}
