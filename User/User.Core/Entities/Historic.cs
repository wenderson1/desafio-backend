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
        public string Id { get; set; }
        public DateTime WithdrawalDate { get; set; }
        public DateTime ReturnDate { get; set; }
        public DateTime ExpectedReturnDate { get; set; }
        public string IdDeliveryMan { get; set; }
    }
}
