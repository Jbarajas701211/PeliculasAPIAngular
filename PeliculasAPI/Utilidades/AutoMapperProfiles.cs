using AutoMapper;
using NetTopologySuite.Geometries;
using PeliculasAPI.DTOs;
using PeliculasAPI.Entidades;

namespace PeliculasAPI.Utilidades
{
    public class AutoMapperProfiles: Profile
    {
        public AutoMapperProfiles()
        {
            ConfigurarMapeoGeneros();
            ConfigurarMapeoActores();
        }

        private void ConfigurarMapeoGeneros()
        {
            CreateMap<GeneroCreacionDTO, Genero>();
            CreateMap<Genero, GeneroDTO>();
        }

        private void ConfigurarMapeoActores()
        {
            CreateMap<ActorCreacionDTO, Actor>()
                .ForMember(x => x.Foto, opciones => opciones.Ignore());
            CreateMap<Actor, ActorDTO>();
        }

        private void ConfigurarCine()
        {
            CreateMap<Point, LocationDTO>()
                .ForMember(dest => dest.Longitude, opt => opt.MapFrom(src => src.X))
                .ForMember(dest => dest.Latitude, opt => opt.MapFrom(src => src.Y));

            CreateMap<LocationDTO, Point>()
                .ConstructUsing(src => new Point(src.Longitude, src.Latitude) { SRID = 4326 });

            CreateMap<Cine, CineDTO>();

            CreateMap<>
        }
    }
}
