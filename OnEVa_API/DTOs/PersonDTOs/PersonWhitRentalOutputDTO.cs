using DTOs.RentalDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTOs.PersonDTOs
{
    public class PersonWhitRentalOutputDTO : PersonOutputDTO
    {
        public List<RentalOutputDTO> RentalsOutputDTO { get; set; }
    }
}
