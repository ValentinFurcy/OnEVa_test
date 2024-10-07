using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTOs.RentalDTO
{
    public class RentalUpdateInputDTO : RentalCreateInputDTO
    {
        public int Id {  get; set; }
     
    }
}
