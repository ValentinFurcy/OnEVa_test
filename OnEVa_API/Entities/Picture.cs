using Entities.CONST;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
	public class Picture
	{
		public int Id { get; set; }
		public string Title { get; set; }
		public string Url { get; set; }
		public Colors Color { get; set; }
        public List<Vehicle>? Vehicles { get; set; }
		public List<Model>? Models { get; set; }
    }
}
