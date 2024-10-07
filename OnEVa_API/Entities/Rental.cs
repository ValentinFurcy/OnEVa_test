using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
	public class Rental
	{
		public int Id { get; set; }
		public DateTime StartDate { get; set; }
		public DateTime EndDate { get; set; }
        public int StateId { get; set; }
        public State State { get; set; }
		public int PersonId { get; set; }
		public Person Person { get; set; }
		public int VehicleId {  get; set; }
		public Vehicle Vehicle { get; set; }
		public int AddressId { get; set; }
		public Address Address { get; set; }
    }
}
