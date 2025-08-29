using PeliculasAPI.Entidades;
using System.ComponentModel.DataAnnotations;

namespace PeliculasAPI.DTOs
{
    public class CineDTO: IId
    {
        public int Id { get; set; }
        [Required]
        [StringLength(75)]
        public required string Nombre { get; set; }
        public required LocationDTO Ubicacion { get; set; }
        
    }
}
