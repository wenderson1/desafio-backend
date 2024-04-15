using Amazon.Util;
using Microsoft.AspNetCore.Mvc;
using RentalCompany.Application.Interfaces;
using RentalCompany.Application.Models.Input;

namespace RentalCompany.Api.Controllers
{
    [ApiController]
    [Route("api/deliveryman")]
    public class DeliveryManController : ControllerBase
    {
        private readonly IDeliveryManService _deliveryManService;

        public DeliveryManController(IDeliveryManService deliveryManService)
        {
            _deliveryManService = deliveryManService;
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody]DeliveryManInput input)
        {
            var result = await _deliveryManService.GetByCnhNumber(input.CnhNumber);

            if (result != null) return BadRequest("This CNH Number already exists.");

            await _deliveryManService.CreateDeliveryMan(input);

            return Ok();
        }

        [HttpPut("cnhNumber")]
        public async Task<IActionResult> Put([FromBody] DeliveryManUpdateInput input, string cnhNumber)
        {
            var result = await _deliveryManService.GetByCnhNumber(cnhNumber);

            if (result == null) return BadRequest("This CNH Number is not found.");

            await _deliveryManService.UpdateAsync(input, cnhNumber);

            return Ok();
        }

        [HttpPost("cnhNumber")]
        public async Task<IActionResult> UploadCnhImage(UploadCnhImageInput input)
        {

            await _deliveryManService.UpdateCnhImage(input);

            return Ok();
        }

        [HttpDelete("cnhNumber")]
        public async Task<IActionResult> Delete(string cnhNumber)
        {
            var result = await _deliveryManService.GetByCnhNumber(cnhNumber);

            if (result == null) return BadRequest("This CNH Number is not found.");

            await _deliveryManService.DeleteAsync(cnhNumber);

            return Ok();
        }

        [HttpGet("cnhNumber")]
        public async Task<IActionResult> GetDetailsByCnhNumber(string cnhNumber)
        {
            var result = await _deliveryManService.GetDetailsByCnhNumber(cnhNumber);

            if (result == null) return NotFound();

            return Ok(result);
        }
    }
}
