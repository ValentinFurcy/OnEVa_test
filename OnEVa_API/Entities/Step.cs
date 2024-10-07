using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
	public class Step
	{
		public int Id { get; set; }
		public int Order { get; set; }

		public int AddressId { get; set; }
		public List<Address> StepAddresses { get; set; }

		public int CarpoolId { get; set; }
		public List<Carpool> StepCarpools { get; set; }
	}
}
