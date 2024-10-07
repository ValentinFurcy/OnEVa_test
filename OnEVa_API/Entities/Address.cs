using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
	public class Address
	{
		public int Id {  get; set; }
		public string StreetNb { get; set; }
		public string StreetName { get; set; }
		public string City { get; set; }
		public string PostCode { get; set; }
		
		[ForeignKey("StartAddressId")]
		public List<Carpool>? StartAddressCarpool { get; set; }

		[ForeignKey("EndAddressId")]
		public List<Carpool>? EndAddressCarpool { get; set; }

		public List<Person>? Persons { get; set; }
		public List<Rental>? Rentals {  get; set; } 

	}
}
