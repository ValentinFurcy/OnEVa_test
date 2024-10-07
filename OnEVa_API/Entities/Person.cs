using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
	public class Person
	{
		public int Id { get; set; }
		public string LastName { get; set; }
		public string FirstName { get; set; }
		public byte[]? ProfilePicture { get; set; }
		public string AppUserId { get; set; }
		public AppUser AppUser { get; set; }
        public int? VehicleId { get; set; }
		public Vehicle? Vehicles { get; set; }

		public int? AddressId { get; set; }
		public Address? Address { get; set; }
		public List<Carpool>? CarpoolsAttended { get; set; }
		public List<Rental>? Rentals { get; set; }

		[ForeignKey("OrganizerId")]
		public List<Carpool>? CarpoolsOrganized {  get; set; }
		
	}
}
