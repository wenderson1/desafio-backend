using Microsoft.AspNetCore.Mvc;
using RentalCompany.Application.Interfaces;
using RentalCompany.Application.Models.Input;

namespace RentalCompany.Api.Controllers
{
    [ApiController]
    [Route("api/order")]
    public class OrderController : ControllerBase
    {
        private readonly IOrderService _orderService;

        public OrderController(IOrderService orderService)
        {
            _orderService = orderService;
        }

        [HttpGet("simulation")]
        public IActionResult GetSimulationPlan([FromQuery] RentalPlanInput input)
        {
            return Ok(_orderService.GetSimulationPlan(input));
        }

        [HttpGet("motorcycles/available")]
        public IActionResult GetAllMotorcyclesAvailable()
        {
            return Ok(_orderService.GetAllMotorcyclesAvailable());
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] CreateOrderInput input)
        {
            return Ok(await _orderService.CreateOrder(input));
        }

        [HttpPut("finish/{id}")]
        public async Task<IActionResult> FinishOrder(string id)
        {
            var result = await _orderService.FinishOrder(id);

            return Ok(result);
        }

        [HttpGet("deliveryMan/{deliveryManId}")]
        public async Task<IActionResult> GetByDeliveryManId(string deliveryManId)
        {
            return Ok(await _orderService.GetByDeliveryManId(deliveryManId));
        }

        [HttpGet("motorcycle/{motorcycleId}")]
        public async Task<IActionResult> GetByMotorcycleId(string deliveryManId)
        {
            return Ok(await _orderService.GetByMotorcycleId(deliveryManId));
        }

    }
}
