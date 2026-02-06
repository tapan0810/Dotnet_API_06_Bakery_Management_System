using Dotnet_API_06.Entities.Dtos;
using Dotnet_API_06.Services;
using Microsoft.AspNetCore.Mvc;

namespace Dotnet_API_06.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BakeryController : ControllerBase
    {
        private readonly IBakeryService _service;
        private readonly ILogger<BakeryController> _logger;

        public BakeryController(
            IBakeryService service,
            ILogger<BakeryController> logger)
        {
            _service = service;
            _logger = logger;
        }

        [HttpGet("All")]
        public async Task<ActionResult<List<GetAllBakeryDto>>> GetAllBakery()
        {
            _logger.LogInformation("Controller: Request received to fetch all bakeries");

            var bakeries = await _service.GetallBackery();

            _logger.LogInformation(
                "Controller: Returning {Count} bakeries",
                bakeries.Count);

            return Ok(bakeries);
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<GetBakeryByIdDto?>> GetBakeryById(int id)
        {
            _logger.LogInformation(
                "Controller: Request received to fetch bakery with ID {Id}",
                id);

            var bakery = await _service.GetBackeryById(id);

            if (bakery is null)
            {
                _logger.LogWarning(
                    "Controller: Bakery with ID {Id} not found",
                    id);

                return BadRequest($"The Bakery with ID:{id} not available");
            }

            _logger.LogInformation(
                "Controller: Bakery with ID {Id} retrieved successfully",
                id);

            return Ok(bakery);
        }

        [HttpPost]
        public async Task<ActionResult<GetBakeryByIdDto?>> CreateBakery(
            [FromBody] CreateBakeryDto createBakery)
        {
            if (createBakery is null)
            {
                _logger.LogWarning(
                    "Controller: CreateBakery called with null payload");

                return BadRequest("The createBakery is incorrect object");
            }

            _logger.LogInformation(
                "Controller: Creating bakery with name {Name}",
                createBakery.BakeryName);

            var bakery = await _service.CreateBackery(createBakery);

            _logger.LogInformation(
                "Controller: Bakery created successfully with ID {Id}",
                bakery?.BakeryId);

            return CreatedAtAction(
                nameof(CreateBakery),
                new { id = bakery?.BakeryId },
                bakery);
        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult<bool>> UpdateBakery(
            int id,
            [FromBody] UpdateBakeryDto updateBakery)
        {
            _logger.LogInformation(
                "Controller: Request received to update bakery with ID {Id}",
                id);

            if (updateBakery is null)
            {
                _logger.LogWarning(
                    "Controller: UpdateBakery called with null payload");

                return BadRequest("The createBakery is incorrect object");
            }

            var bakery = await _service.GetBackeryById(id);

            if (bakery is null)
            {
                _logger.LogWarning(
                    "Controller: Bakery with ID {Id} not found for update",
                    id);

                return BadRequest($"The Bakery with ID:{id} not available");
            }

            var updated = await _service.UpdateBakery(id, updateBakery);

            _logger.LogInformation(
                "Controller: Bakery with ID {Id} updated successfully",
                id);

            return Ok(updated);
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult<bool>> DeleteBakery(int id)
        {
            _logger.LogInformation(
                "Controller: Request received to delete bakery with ID {Id}",
                id);

            var bakery = await _service.GetBackeryById(id);

            if (bakery is null)
            {
                _logger.LogWarning(
                    "Controller: Bakery with ID {Id} not found for deletion",
                    id);

                return BadRequest($"The Bakery with ID:{id} not available");
            }

            var deleted = await _service.DeleteBakery(id);

            _logger.LogInformation(
                "Controller: Bakery with ID {Id} deleted successfully",
                id);

            return Ok(deleted);
        }
    }
}
