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

        /// <summary>
        /// Create a new DeliveryMan
        /// </summary>
        /// <param name="input">Delivery Man Fields</param>
        /// <response code="200">Succes</response>
        /// <response code="400">Bad Request</response>        
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] DeliveryManInput input)
        {
            var result = await _deliveryManService.GetByCnhNumber(input.CnhNumber);

            if (result != null) return BadRequest("This CNH Number already exists.");

            await _deliveryManService.CreateDeliveryMan(input);

            return Ok();
        }

        /// <summary>
        /// Update a exists Delivery Man
        /// </summary>
        /// <param name="input">Delivery Man Fields</param>
        /// <response code="200">Succes</response>
        /// <response code="400">Bad Request</response> 
        [HttpPut("{cnhNumber}")]
        public async Task<IActionResult> Put([FromBody] DeliveryManUpdateInput input, string cnhNumber)
        {
            var result = await _deliveryManService.GetByCnhNumber(cnhNumber);

            if (result == null) return BadRequest("This CNH Number is not found.");

            await _deliveryManService.UpdateAsync(input, cnhNumber);

            return Ok();
        }

        /// <summary>
        /// Upload the CNH Image
        /// </summary>
        /// <param name="input">CNH Number and CNH IMage</param>
        /// <response code="200">Succes</response>
        /// <response code="400">Bad Request</response> 
        /// <response code="404">Bad Request</response> 
        [HttpPost("image")]
        public async Task<IActionResult> UploadCnhImage(UploadCnhImageInput input)
        {
            var result = await _deliveryManService.GetByCnhNumber(input.CnhNumber);

            if (result == null) return NotFound();

            await _deliveryManService.UpdateCnhImage(input);

            return Ok();
        }

        /// <summary>
        /// Delete a Deliveryman
        /// </summary>
        /// <param name="cnhNumber">Cnh Number</param>
        /// <response code="200">Succes</response>
        /// <response code="404">NotFound</response> 

        [HttpDelete("{cnhNumber}")]
        public async Task<IActionResult> Delete(string cnhNumber)
        {
            var result = await _deliveryManService.GetByCnhNumber(cnhNumber);

            if (result == null) return NotFound();

            await _deliveryManService.DeleteAsync(cnhNumber);

            return Ok();
        }
        /// <summary>
        /// Get details of Delivery Man
        /// </summary>
        /// <param name="cnhNumber">Cnh Number</param>
        /// <response code="200">Succes</response>
        /// <response code="404">NotFound</response>         
        [HttpGet("{cnhNumber}")]
        public async Task<IActionResult> GetDetailsByCnhNumber(string cnhNumber)
        {
            var result = await _deliveryManService.GetDetailsByCnhNumber(cnhNumber);

            if (result == null) return NotFound();

            return Ok(result);
        }
    }
}
