using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentalCompany.Core.Entities
{
    public class RentalPlan
    {
        public string Id { get; set; }
        public string IdDeliveryMan { get; set; }
        public string IdMotorcycle { get; set; }
        public double ExpectedPrice { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime ExpectedReturnDate { get; set; }

        public RentalPlan(string idDeliveryMan, string idMotorcycle, double expectedPrice, DateTime startDate, DateTime expectedReturnDate)
        {
            Id = Guid.NewGuid().ToString();
            IdDeliveryMan = idDeliveryMan;
            IdMotorcycle = idMotorcycle;
            ExpectedPrice = expectedPrice;
            StartDate = startDate;
            ExpectedReturnDate = expectedReturnDate;
        }
    }
}
