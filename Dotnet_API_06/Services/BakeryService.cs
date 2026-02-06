using Dotnet_API_06.Data;
using Dotnet_API_06.Entities.Dtos;
using Dotnet_API_06.Entities.Models;
using Microsoft.EntityFrameworkCore;

namespace Dotnet_API_06.Services
{
    public class BakeryService : IBakeryService
    {
        private readonly BakeryDbContext _context;
        public BakeryService(BakeryDbContext context)
        {
            _context = context;
        }

        public async Task<List<GetAllBakeryDto>> GetallBackery()
        {
            return await _context.Bakeries.Select(s => new GetAllBakeryDto
            {
                BakeryId = s.BakeryId,
                BakeryName = s.BakeryName
            }).ToListAsync();
        }

        public async Task<GetBakeryByIdDto?> GetBackeryById(int id)
        {
            return await _context.Bakeries
                .Where(x => x.BakeryId == id)
                .Select(s => new GetBakeryByIdDto
                {
                    BakeryId = s.BakeryId,
                    BakeryName = s.BakeryName,
                    BakeryNumber = s.BakeryNumber,
                    BakeryDescription = s.BakeryDescription,
                    Address = s.Address,
                    Email = s.Email
                }).FirstOrDefaultAsync();
        }

        public async Task<GetBakeryByIdDto?> CreateBackery(CreateBakeryDto createBakery)
        {
            if (createBakery == null)
                return null;

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

            return new GetBakeryByIdDto
            {
                BakeryId = bakery.BakeryId,
                BakeryName = bakery.BakeryName,
                BakeryDescription = bakery.BakeryDescription,
                Address = bakery.Address,
                BakeryNumber = bakery.BakeryNumber,
                Email = bakery.Email,
            };
        }

        public async Task<bool> UpdateBakery(int id, UpdateBakeryDto updateBakery)
        {
            var bakery = await _context.Bakeries.FindAsync(id);

            if (bakery is null)
                return false;

            bakery.BakeryName = updateBakery.BakeryName;
            bakery.BakeryNumber = updateBakery.BakeryNumber;
            bakery.BakeryDescription = updateBakery.BakeryDescription;
            bakery.Address = updateBakery.Address;
            bakery.Email = updateBakery.Email;

            await _context.SaveChangesAsync();

            return true;
        }

        public async Task<bool> DeleteBakery(int id)
        {
            var bakery = await _context.Bakeries.FindAsync(id);
            if (bakery is null) return false;

            _context.Bakeries.Remove(bakery);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
