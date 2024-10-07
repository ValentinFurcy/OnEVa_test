using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
	public class Carpool
	{
		public int Id { get; set; }
		public int CarSeatNb { get; set; }
		public DateTime StartDate { get; set; }
		public DateTime EndDate { get; set; }

		public int StartAddressId { get; set; }
		public Address StartAddress { get; set; }

		public int EndAddressId { get; set; }
		public Address EndAddress { get; set; }

		public int StateId { get; set; }
		public State State { get; set; }

		public int VehicleId { get; set; }
		public Vehicle Vehicle { get; set; }

		public int? StepId { get; set; }
		public List<Step>? Steps { get; set; }
	
		public int OrganizerId { get; set; }
		public Person Organizer { get; set; }
		public List<Person>? Attendees { get; set; }

	}
}
