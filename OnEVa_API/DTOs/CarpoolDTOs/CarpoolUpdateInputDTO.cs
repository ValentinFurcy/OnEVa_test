using DTOs.VehicleDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTOs.CarpoolDTOs
{
    public class CarpoolUpdateInputDTO : CarpoolCreateInputDTO
    {
        public int CarpoolId { get; set; }

    }
}
