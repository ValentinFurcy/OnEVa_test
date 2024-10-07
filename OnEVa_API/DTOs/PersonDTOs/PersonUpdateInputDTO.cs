using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTOs.PersonDTOs
{
    public class PersonUpdateInputDTO
    {
        public string? CurrentEmail { get; set; } //if admin for updating the right person
        public string? NewEmail { get; set; }
        //public Vehicle Vehicle { get; set; } //if not exist create vehicle
    }
}
