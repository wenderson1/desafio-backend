﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using User.Core.Enums;

namespace User.Core.Entities
{
    public class Motorcycle
    {
        public string? Id { get; set; }
        public int Year { get; set; }
        public string Model { get; set; }
        public string LicensePlate { get; set; }
        public MotorcycleStatusEnum Status { get; set; }

        public Motorcycle(string id, int year, string model, string licensePlate, MotorcycleStatusEnum status)
        {
            Id = id;
            Year = year;
            Model = model;
            LicensePlate = licensePlate;
            Status = status;
        }
    }
}
