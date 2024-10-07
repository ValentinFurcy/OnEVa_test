using DTOs.Address;
using DTOs.PersonDTOs;
using DTOs.VehicleDTOs;
using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTOs.RentalDTO
{
    public class RentalOutputDTO 
    {
        public int Id { get; set; }
        public string FirstNamePerson { get; set; }
        public string LastNamePerson { get; set; }
        public int PersonId { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string StateLabel { get; set; }
        public AddressOutputDTO AddressOutput { get; set; }
        public VehicleOutputDTO VehicleOutput { get; set; }
    }
}
