using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTOs.Address
{
    public class AddressUpdateInputDTO : AddressCreateInputDTO
    {
        public int AddressId { get; set; }
        public string? AppUserId { get; set; }
    }
}
