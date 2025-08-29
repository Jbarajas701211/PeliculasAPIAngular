using System.ComponentModel.DataAnnotations;

namespace PeliculasAPI.DTOs
{
    public class LocationDTO
    {
        [Range(-90, 90)]
        public double Latitud { get; set; }
        [Range(-180, 180)]
        public double Longitud { get; set; }
    }
}
