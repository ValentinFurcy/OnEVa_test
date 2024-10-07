using DTOs.CarpoolDTOs;
using DTOs.RentalDTO;
using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTOs.PersonDTOs
{
    public class PersonOutputDTO
    {
        public int Id { get; set; }
        public string AppUserId { get; set; }
        public string Email {  get; set; }
        public string LastName { get; set; }
        public string FirstName { get; set; }
        public byte[]? ProfilePicture { get; set; }
        public Vehicle? Vehicle { get; set; }
    }
}
