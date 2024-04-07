using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace User.Core.Entities
{
    public class Historic
    {
        public string Id { get; private set; }
        public DateTime WithdrawalDate { get; private set; }
        public DateTime ReturnDate { get; private set; }
        public DateTime ExpectedReturnDate { get; private set; }
        public string IdDeliveryMan { get; private set; }

        public Historic( DateTime withdrawalDate, DateTime returnDate, string idDeliveryMan, DateTime expectedReturnDate)
        {
            Id = Guid.NewGuid().ToString();
            WithdrawalDate = withdrawalDate;
            ReturnDate = returnDate;
            IdDeliveryMan = idDeliveryMan;
            ExpectedReturnDate = expectedReturnDate;
        }
    }
}
