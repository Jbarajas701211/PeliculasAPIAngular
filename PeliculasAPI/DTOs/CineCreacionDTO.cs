using System.ComponentModel.DataAnnotations;

namespace PeliculasAPI.DTOs
{
    public class CineCreacionDTO
    {
        [Required]
        [StringLength(75)]
        public required string Nombre { get; set; }
        public LocationDTO? Ubicacion { get; set; }
    }
}
