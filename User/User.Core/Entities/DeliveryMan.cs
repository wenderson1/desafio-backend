using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace User.Core.Entities
{
    public class DeliveryMan
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string CNPJ { get; set; }
        public string BirthDate { get; set; }
        public string CnhNumber { get; set; }
        public string CnhType { get; set; }
        public byte[] CnhImage { get; set; }

        public DeliveryMan(string name, string cNPJ, string birthDate, string cnhNumber, string cnhType)
        {
            Id = new Guid().ToString();
            Name = name;
            CNPJ = cNPJ;
            BirthDate = birthDate;
            CnhNumber = cnhNumber;
            CnhType = cnhType;
            CnhImage = [];
        }
    }
}
