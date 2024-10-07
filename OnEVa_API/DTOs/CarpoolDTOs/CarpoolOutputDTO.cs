using DTOs.Address;
using DTOs.PersonDTOs;
using DTOs.StateDTOs;
using DTOs.VehicleDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTOs.CarpoolDTOs
{
    public class CarpoolOutputDTO
    {
        public int Id { get; set; }
        public int CarSeatNb { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public AddressOutputDTO StartAddress { get; set; }
        public AddressOutputDTO EndAddress { get; set; }
        public string OrganizerFirstName { get; set; }
        public string OrganizerLastName { get; set; }
        public int OrganizerId { get; set; }
        public List<PersonOutputDTO>? PersonsAttendees { get; set; }
        public StateOutputDTO State { get; set; }
        public VehicleOutputDTO Vehicle { get; set; }
    }
}
