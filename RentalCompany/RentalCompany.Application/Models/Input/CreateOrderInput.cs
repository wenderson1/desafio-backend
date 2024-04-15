
namespace RentalCompany.Application.Models.Input
{
    public class CreateOrderInput
    {
        public string IdMotorcycle { get; set; }
        public string IdDeliveryMan { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime ExpectedReturnDate { get; set; }
    }
}
