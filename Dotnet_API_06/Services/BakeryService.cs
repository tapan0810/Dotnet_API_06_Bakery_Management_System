using Dotnet_API_06.Data;
using Dotnet_API_06.Entities.Dtos;
using Dotnet_API_06.Entities.Models;
using Microsoft.EntityFrameworkCore;

namespace Dotnet_API_06.Services
{
    public class BakeryService : IBakeryService
    {
        private readonly BakeryDbContext _context;
        private readonly ILogger<BakeryService> _logger;

        public BakeryService(
            BakeryDbContext context,
            ILogger<BakeryService> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<List<GetAllBakeryDto>> GetallBackery()
        {
            _logger.LogInformation("Service: Fetching all bakeries");

            var bakeries = await _context.Bakeries
                .Select(s => new GetAllBakeryDto
                {
                    BakeryId = s.BakeryId,
                    BakeryName = s.BakeryName
                })
                .ToListAsync();

            _logger.LogInformation(
                "Service: Retrieved {Count} bakeries",
                bakeries.Count);

            return bakeries;
        }

        public async Task<GetBakeryByIdDto?> GetBackeryById(int id)
        {
            _logger.LogInformation(
                "Service: Fetching bakery with ID {Id}",
                id);

            var bakery = await _context.Bakeries
                .Where(x => x.BakeryId == id)
                .Select(s => new GetBakeryByIdDto
                {
                    BakeryId = s.BakeryId,
                    BakeryName = s.BakeryName,
                    BakeryNumber = s.BakeryNumber,
                    BakeryDescription = s.BakeryDescription,
                    Address = s.Address,
                    Email = s.Email
                })
                .FirstOrDefaultAsync();

            if (bakery == null)
            {
                _logger.LogWarning(
                    "Service: Bakery with ID {Id} not found",
                    id);
            }

            return bakery;
        }

        public async Task<GetBakeryByIdDto?> CreateBackery(CreateBakeryDto createBakery)
        {
            if (createBakery == null)
            {
                _logger.LogWarning(
                    "Service: CreateBackery called with null payload");
                return null;
            }

            _logger.LogInformation(
                "Service: Creating bakery with name {Name}",
                createBakery.BakeryName);

            var bakery = new Bakery
            {
                BakeryName = createBakery.BakeryName,
                Address = createBakery.Address,
                Email = createBakery.Email,
                BakeryDescription = createBakery.BakeryDescription,
                BakeryNumber = createBakery.BakeryNumber
            };

            _context.Bakeries.Add(bakery);
            await _context.SaveChangesAsync();

            _logger.LogInformation(
                "Service: Bakery created successfully with ID {Id}",
                bakery.BakeryId);

            return new GetBakeryByIdDto
            {
                BakeryId = bakery.BakeryId,
                BakeryName = bakery.BakeryName,
                BakeryDescription = bakery.BakeryDescription,
                Address = bakery.Address,
                BakeryNumber = bakery.BakeryNumber,
                Email = bakery.Email
            };
        }

        public async Task<bool> UpdateBakery(int id, UpdateBakeryDto updateBakery)
        {
            _logger.LogInformation(
                "Service: Updating bakery with ID {Id}",
                id);

            var bakery = await _context.Bakeries.FindAsync(id);

            if (bakery == null)
            {
                _logger.LogWarning(
                    "Service: Bakery with ID {Id} not found for update",
                    id);
                return false;
            }

            bakery.BakeryName = updateBakery.BakeryName;
            bakery.BakeryNumber = updateBakery.BakeryNumber;
            bakery.BakeryDescription = updateBakery.BakeryDescription;
            bakery.Address = updateBakery.Address;
            bakery.Email = updateBakery.Email;

            await _context.SaveChangesAsync();

            _logger.LogInformation(
                "Service: Bakery with ID {Id} updated successfully",
                id);

            return true;
        }

        public async Task<bool> DeleteBakery(int id)
        {
            _logger.LogInformation(
                "Service: Deleting bakery with ID {Id}",
                id);

            var bakery = await _context.Bakeries.FindAsync(id);

            if (bakery == null)
            {
                _logger.LogWarning(
                    "Service: Bakery with ID {Id} not found for deletion",
                    id);
                return false;
            }

            _context.Bakeries.Remove(bakery);
            await _context.SaveChangesAsync();

            _logger.LogInformation(
                "Service: Bakery with ID {Id} deleted successfully",
                id);

            return true;
        }
    }
}
