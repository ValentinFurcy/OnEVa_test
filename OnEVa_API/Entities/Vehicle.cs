using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entities.CONST;

namespace Entities
{
	[Index(nameof(VehicleNumber), IsUnique = true)]
	[Index(nameof(Registration), IsUnique = true)]
	public class Vehicle
    {
        public int Id { get; set; }
		public int VehicleNumber { get; set; }
        public string Registration { get; set; }
        public int MaxSeatNb { get; set; }
        public double Co2Emission { get; set; }
        public bool IsEnabled { get; set; }
        public Status Status { get; set; }
        public List<Rental>? Rentals { get; set; }
        public List<Carpool>? CarPools { get; set; }
        public List<Picture> Pictures { get; set; }
        public int EngineId { get; set; }
        public Engine Engine { get; set; }
        public int CategoryId { get; set; }
        public Category Category { get; set; }
        public int ModelId { get; set; }
        public Model Model { get; set; }
        public int BrandId { get; set; }
        public Brand Brand { get; set; }
        public Person? Person { get; set; }

    }
}
