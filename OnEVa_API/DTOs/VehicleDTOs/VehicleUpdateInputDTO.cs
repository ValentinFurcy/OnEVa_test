using Entities;
using Entities.CONST;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTOs.VehicleDTOs
{
    public class VehicleUpdateInputDTO : VehicleCreateInputDTO
    {
        public int VehicleId { get; set; }
        public Entities.Status Status { get; set; }
        
    }
}
