using System.ComponentModel.DataAnnotations;

namespace Dotnet_API_06.Entities.Models
{
    public class Bakery
    {
        public int BakeryId { get; set; }
        public required string BakeryName { get; set; }
        public string BakeryDescription { get; set; } = String.Empty;
        [Phone]
        public required string BakeryNumber { get; set; }
        public required string Address { get; set; }
        [EmailAddress]
        public required string Email { get; set; }
    }
}
