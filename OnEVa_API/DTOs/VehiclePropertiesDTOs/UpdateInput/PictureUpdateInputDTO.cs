using DTOs.VehiclePropertiesDTOs.CreateInput;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTOs.VehiclePropertiesDTOs.UpdateInput;
public class PictureUpdateInputDTO : PictureCreateInputDTO
{
    public int Id { get; set; }
}
