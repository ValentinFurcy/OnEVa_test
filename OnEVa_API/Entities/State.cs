using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
	public class State
	{
		public int Id {  get; set; }
		public string Label { get; set; }

		public List<Rental>? Rentals { get; set; }

		public List<Carpool>? CarPools { get; set; }
	}
}
