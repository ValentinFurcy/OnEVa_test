using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTOs.Address
{
    public class AddressOutputDTO
    {
        public int Id { get; set; }
        public string StreetNb { get; set; }
        public string StreetName { get; set; }
        public string City { get; set; }
        public string PostCode { get; set; }
    }
}
