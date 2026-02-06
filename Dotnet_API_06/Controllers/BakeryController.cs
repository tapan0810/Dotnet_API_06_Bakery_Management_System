using Dotnet_API_06.Entities.Dtos;
using Dotnet_API_06.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Dotnet_API_06.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BakeryController : ControllerBase
    {
        private readonly IBakeryService _service;

        public BakeryController(IBakeryService service)
        {
            _service = service;
        }

        [HttpGet("All")]
        public async Task<ActionResult<List<GetAllBakeryDto>>> GetAllBakery()
        {
            var bak = await _service.GetallBackery();
            return Ok(bak);
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<GetBakeryByIdDto?>> GetBakeryById(int id)
        {
            var bakery = await _service.GetBackeryById(id);
            if (bakery is null)
            {
                return BadRequest($"The Bakery with ID:{id} not available");
            }
            return Ok(bakery);
        }

        [HttpPost]
        public async Task<ActionResult<GetBakeryByIdDto?>> CreateBakery( CreateBakeryDto createBakery)
        {
            if (createBakery is null)
                return BadRequest("The createBakery is incorrect object");

            var bakery = await _service.CreateBackery(createBakery);

            return CreatedAtAction(nameof(CreateBakery), new { id = bakery?.BakeryId }, bakery);
        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult<bool>> UpdateBakery(int id, [FromBody] UpdateBakeryDto updateBakery)
        {
            var bakery = await _service.GetBackeryById(id);

            if(bakery is null)
                return BadRequest($"The Bakery with ID:{id} not available");

            if(updateBakery is null)
                return BadRequest("The createBakery is incorrect object");

            var updated = await _service.UpdateBakery(id, updateBakery);
            return Ok(updated);

        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult<bool>> DeleteBakery(int id)
        {
            var bakery = await _service.GetBackeryById(id);

            if (bakery is null)
                return BadRequest($"The Bakery with ID:{id} not available");

            var deleted = await _service.DeleteBakery(id);
            return Ok(deleted);
        }
    }
}
