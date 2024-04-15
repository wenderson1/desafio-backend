using RentalCompany.Core.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentalCompany.Core.Entities
{
    public class Order
    {
        public Order(string idDeliveryMan, string idMotorcycle, OrderStatusEnum status, DateTime startDate, DateTime expectedReturnDate, DateTime? returnDate, double price)
        {
            Id = Guid.NewGuid().ToString();
            IdDeliveryMan = idDeliveryMan;
            IdMotorcycle = idMotorcycle;
            Status = status;
            StartDate = startDate;
            ExpectedReturnDate = expectedReturnDate;
            ReturnDate = returnDate;
            Price = price;
        }

        public string Id { get; set; }
        public string IdDeliveryMan { get; set; }
        public string IdMotorcycle { get; set; }
        public OrderStatusEnum Status{ get; set; }
        public DateTime StartDate { get; set; }
        public DateTime ExpectedReturnDate { get; set; }
        public DateTime? ReturnDate { get; set; }
        public double Price { get; set; }
    }
}
