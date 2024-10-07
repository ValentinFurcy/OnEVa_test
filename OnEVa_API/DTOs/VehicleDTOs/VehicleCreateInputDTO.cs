using DTOs.VehiclePropertiesDTOs.CreateInput;
using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTOs.VehicleDTOs
{
    public class VehicleCreateInputDTO
    {
        public int VehicleNumber { get; set; }
        public string Registration { get; set; }
        public int BrandId { get; set; }
        public int ModelId { get; set; }
        public int CategoryId { get; set; }
        public List<int>? PicturesId { get; set; }
        public List<PictureCreateInputDTO>? PicturesToAdd { get; set; }
        public int EngineId { get; set; }
        public double Co2Emission { get; set; }
        public int MaxSeatNb { get; set; }
    }
}
