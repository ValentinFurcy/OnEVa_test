using DTOs.VehiclePropertiesDTOs.Output;
using Entities;
using Entities.CONST;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTOs.VehicleDTOs
{
    public class VehicleOutputDTO
    {
        public int Id { get; set; }
        public int VehicleNumber {  get; set; }
        public string Registration { get; set; }
        public string BrandLabel { get; set; }
        public string ModelLabel { get; set; }
        public string CategoryLabel { get; set; }
        public IEnumerable<PictureOutputDTO> PicturesOutputDTO { get; set; }
        public string EngineLabel { get; set; }
        public double Co2Emission { get; set; }
        public int MaxSeatNb { get; set; }
        public Entities.Status Status { get; set; }
    }
}
