using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public class Model
    {
        public int Id { get; set; }
        public string Label { get; set; }
        public List<Vehicle>? Vehicles { get; set; }
        public List<Picture>? Pictures { get; set; }
    }
}
