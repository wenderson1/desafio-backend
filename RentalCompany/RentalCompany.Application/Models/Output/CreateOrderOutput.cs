﻿
namespace RentalCompany.Application.Models.Output
{
    public class CreateOrderOutput
    {
        public string Id { get; set; }
        public double Price { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime ExpectedReturnDate { get; set; }


    }
}
