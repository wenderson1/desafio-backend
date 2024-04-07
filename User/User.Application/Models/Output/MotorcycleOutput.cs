using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace User.Application.Models.Output
{
    public class MotorcycleOutput
    {
        public MotorcycleOutput(string id, int year, string model, string licensePlate)
        {
            Id = id;
            Year = year;
            Model = model;
            LicensePlate = licensePlate;
        }

        public string Id { get; set; }
        public int Year { get; set; }
        public string Model { get; set; }
        public string LicensePlate { get; set; }


    }
}
