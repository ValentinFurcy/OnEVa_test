using DTOs.CarpoolDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTOs.PersonDTOs
{
    public class PersonWithCarpoolOutputDTO : PersonOutputDTO
    {
        public List<CarpoolOutputDTO>? CarpoolsOutputDTO { get; set; }
    }
}
