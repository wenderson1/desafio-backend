using User.Core.Entities;

namespace User.Application.Models.Output
{
    public class HistoricOutput
    {
        public HistoricOutput(string id, DateTime withdrawalDate, DateTime returnDate)
        {
            Id = id;
            WithdrawalDate = withdrawalDate;
            ReturnDate = returnDate;
        }

        public string Id { get; set; }
        public DateTime WithdrawalDate { get; set; }
        public DateTime ReturnDate { get; set; }
        public DeliveryMan DeliveryMan { get; set; }
    }
}
