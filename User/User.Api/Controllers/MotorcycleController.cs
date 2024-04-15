using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver.GeoJsonObjectModel;
using User.Application.Interfaces;
using User.Application.Models.InputModels;
using User.Core.Entities;


namespace User.Api.Controllers
{
    [ApiController]
    [Route("api/motorcycle")]
    public class MotorcycleController : ControllerBase
    {
        private readonly IMotorcycleService _service;

        public MotorcycleController(IMotorcycleService service)
        {
            _service = service;
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] MotorcycleInput input)
        {
            var result = await _service.GetMotorcyclesByLicensePlateAsync(input.LicensePlate);

            if (result != null)
                return BadRequest("This motorcycle already exists.");

            await _service.CreateMotorcycleAsync(input);

            return Ok(result);
        }

        [HttpGet]
        public async Task<IActionResult> GetAllMotorcycles()
        {
            return Ok(await _service.GetAllAsync());
        }

        [HttpGet("{licensePlate}")]
        public async Task<IActionResult> GetByMotorcycleByLicensePlate(string licensePlate)
        {
            var result = await _service.GetMotorcyclesByLicensePlateAsync(licensePlate);

            if (result != null) return Ok(result);

            return NotFound();
        }

        [HttpPut("{wrongLicensePlate}")]
        public async Task<IActionResult> UpdateMotorcycle(string wrongLicensePlate, [FromBody] MotorcycleInput motorcycle)
        {
            var result = await _service.GetMotorcyclesByLicensePlateAsync(wrongLicensePlate);

            if (result == null) return NotFound("Motorcycle Not Found.");

            await _service.UpdateMotorcycleAsync(result, motorcycle, wrongLicensePlate);

            return Ok();
        }

        [HttpDelete("{licensePlate}")]
        public async Task<IActionResult> Delete(string licensePlate)
        {
            var result = await _service.GetMotorcyclesByLicensePlateAsync(licensePlate);

            if (result == null) return NotFound("Motorcycle Not Found.");

            if (result.Historic != null) return BadRequest("Motorcycle has historic rental.");

            await _service.DeleteMotorcycleAsync(licensePlate);


            return Ok();
        }
    }
}
