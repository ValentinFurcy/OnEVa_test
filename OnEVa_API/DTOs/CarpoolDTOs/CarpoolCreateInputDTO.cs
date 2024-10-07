using DTOs.Address;
using DTOs.VehicleDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTOs.CarpoolDTOs
{
    public class CarpoolCreateInputDTO
    {
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public AddressCreateInputDTO StartAddress { get; set; }
        public AddressCreateInputDTO EndAddress { get; set; }
        //public string OrganizerId {  get; set; }
        public int CarSeatNb { get; set; }
        public int VehicleId { get; set; }
    }
}
