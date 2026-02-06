using System.ComponentModel.DataAnnotations;

namespace Dotnet_API_06.Entities.Dtos
{
    public class UpdateBakeryDto
    {
        public required string BakeryName { get; set; }
        public string BakeryDescription { get; set; } = String.Empty;
        [Phone]
        public required string BakeryNumber { get; set; }
        public required string Address { get; set; }
        [EmailAddress]
        public required string Email { get; set; }
    }
}
