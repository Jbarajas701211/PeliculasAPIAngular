using System.ComponentModel.DataAnnotations;

namespace PeliculasAPI.DTOs
{
    public class CineCreacionDTO
    {
        [Required]
        public required string Name { get; set; }
        public LocationDTO? Ubicacion { get; set; }
    }
}
