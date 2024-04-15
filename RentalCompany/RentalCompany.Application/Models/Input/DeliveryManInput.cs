using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentalCompany.Application.Models.Input
{
    public class DeliveryManInput
    {
        public string Name { get; set; }
        public string CNPJ { get; set; }
        public DateTime BirthDate { get; set; }
        public string CnhNumber { get; set; }
        public string CnhType { get; set; }
    }
}
