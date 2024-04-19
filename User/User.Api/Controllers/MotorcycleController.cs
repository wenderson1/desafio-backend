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

        /// <summary>
        /// Create a new motorcycle
        /// </summary>
        /// <remarks>
        /// {
        /// "Year": 2014
        /// "Model": "Xjotao"
        /// "LicensePlate":"ABC123"
        /// }
        /// </remarks>
        /// <param name="input">Motorcycle Fields</param>
        /// <response code="200">Succes</response>
        /// <response code="400">Bad Request</response>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Post([FromBody] MotorcycleInput input)
        {
            var result = await _service.GetMotorcyclesByLicensePlateAsync(input.LicensePlate);

            if (result != null)
                return BadRequest("This motorcycle already exists.");

            await _service.CreateMotorcycleAsync(input);

            return Created();
        }

        /// <summary>
        /// Get all motorcycles.
        /// </summary>
        /// <returns>list of motorcycles</returns>
        /// <response code="200">Succes</response>
        [HttpGet]
        public async Task<IActionResult> GetAllMotorcycles()
        {
            return Ok(await _service.GetAllAsync());
        }


        /// <summary>
        /// Get a motorcycle by license plate.
        /// </summary>
        /// <param name="licensePlate">license plate</param>
        /// <returns>details about the motorcycle</returns>
        /// <response code="200">Succes</response>
        /// <response code="404">NotFound</response>    
        [HttpGet("{licensePlate}")]
        public async Task<IActionResult> GetByMotorcycleByLicensePlate(string licensePlate)
        {
            var result = await _service.GetMotorcyclesByLicensePlateAsync(licensePlate);

            if (result != null) return Ok(result);

            return NotFound();
        }

        /// <summary>
        /// Update a exists motorcycle.
        /// </summary>
        /// <param name="motorcycle">Motorcycle fields</param>
        /// <returns>details about price</returns>
        /// <response code="200">Succes</response>
        /// <response code="404">NotFound</response>    
        [HttpPut("{wrongLicensePlate}")]
        public async Task<IActionResult> UpdateMotorcycle(string wrongLicensePlate, [FromBody] MotorcycleInput motorcycle)
        {
            var result = await _service.GetMotorcyclesByLicensePlateAsync(wrongLicensePlate);

            if (result == null) return NotFound();

            await _service.UpdateMotorcycleAsync(result, motorcycle, wrongLicensePlate);

            return Ok();
        }

        /// <summary>
        /// Delete a Motorcycle
        /// </summary>
        /// <param name="licensePlate">License Plate of Motorcycle</param>
        /// <response code="200">Succes</response>
        /// <response code="404">NotFound</response>
        [HttpDelete("{licensePlate}")]
        public async Task<IActionResult> Delete(string licensePlate)
        {
            var result = await _service.GetMotorcyclesByLicensePlateAsync(licensePlate);

            if (result == null) return NotFound("Motorcycle Not Found.");

            if (result.Historic.Count != 0) return BadRequest("Motorcycle has historic rental.");

            await _service.DeleteMotorcycleAsync(licensePlate);

            return Ok();
        }
    }
}
