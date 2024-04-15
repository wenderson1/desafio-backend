using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace RentalCompany.Core.Entities
{
    public class DeliveryMan
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string CNPJ { get; set; }
        public DateTime BirthDate { get; set; }
        public string CnhNumber { get; set; }
        public string CnhType { get; set; }
        public DeliveryMan(string name, string cNPJ, DateTime birthDate, string cnhNumber, string cnhType)
        {
            Id = Guid.NewGuid().ToString();
            Name = name;
            CNPJ = cNPJ;
            BirthDate = birthDate.Date;
            CnhNumber = cnhNumber;
            CnhType = cnhType;
        }

        public DeliveryMan(string name, string cNPJ, DateTime birthDate, string cnhType)
        {
            Name = name;
            CNPJ = cNPJ;
            BirthDate = birthDate;
            CnhType = cnhType;
        }
    }
}
