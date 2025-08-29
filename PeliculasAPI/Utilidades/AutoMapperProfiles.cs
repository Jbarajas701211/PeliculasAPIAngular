using AutoMapper;
using Microsoft.AspNetCore.Identity;
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
            ConfigurarMapeoCines();
            ConfigurarMapeoPeliculas();
            ConfigurarMapeoUsuarios();

        }

        private void ConfigurarMapeoUsuarios()
        {
            CreateMap<IdentityUser, UsuarioDTO>();
        }

        private void ConfigurarMapeoPeliculas()
        {
            CreateMap<PeliculaCreacionDTO, Pelicula>()
                .ForMember(x => x.Poster, opciones => opciones.Ignore())
                .ForMember(x => x.PeliculasGeneros, dto =>
                dto.MapFrom(p => p.GenerosIds!.Select(id => new PeliculaGenero { GeneroId = id })))
                .ForMember(x => x.PeliculasCines, dto =>
                dto.MapFrom(p => p.CinesIds!.Select(id => new PeliculaCine { CineId = id })))
                .ForMember(x => x.PeliculasActores, dto =>
                dto.MapFrom(p => p.Actores!.Select(actor => new PeliculaActor { ActorId = actor.Id, Personaje = actor.Personaje })));

            CreateMap<Pelicula, PeliculaDTO>();

            CreateMap<Pelicula, PeliculaDetallesDTO>()
                .ForMember(p => p.Generos, entidad => entidad.MapFrom(p => p.PeliculasGeneros))
                .ForMember(p => p.Cines, entidad => entidad.MapFrom(p => p.PeliculasCines))
                .ForMember(p => p.Actores, entidad => entidad.MapFrom(p => p.PeliculasActores.OrderBy(o => o.Orden)));

            CreateMap<PeliculaGenero, GeneroDTO>()
                .ForMember(g => g.Id, pg => pg.MapFrom(p => p.Genero.Id))
                .ForMember(g => g.Nombre, pg => pg.MapFrom(p => p.Genero.Nombre));

            CreateMap<PeliculaCine, CineDTO>()
                .ForMember(g => g.Id, pc => pc.MapFrom(p => p.CineId))
                .ForMember(g => g.Nombre, pc => pc.MapFrom(p => p.Cine.Nombre))
                .ForPath(g => g.Ubicacion.Latitud, pc => pc.MapFrom(p => p.Cine.Ubicacion.Y))
                .ForPath(g => g.Ubicacion.Longitud, pc => pc.MapFrom(p => p.Cine.Ubicacion.X));

            CreateMap<PeliculaActor, PeliculaActorDTO>()
                .ForMember(dto => dto.Id, entidad => entidad.MapFrom(p => p.ActorId))
                .ForMember(dto => dto.Nombre, entidad => entidad.MapFrom(p => p.Actor.Nombre))
                .ForMember(dto => dto.Foto, entidad => entidad.MapFrom(p => p.Actor.Foto));
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
            CreateMap<Actor, PeliculaActorDTO>();
        }

        private void ConfigurarMapeoCines()
        {
            CreateMap<Point, LocationDTO>()
                .ForMember(dest => dest.Longitud, opt => opt.MapFrom(src => src.X))
                .ForMember(dest => dest.Latitud, opt => opt.MapFrom(src => src.Y));

            CreateMap<LocationDTO, Point>()
                .ConstructUsing(src => new Point(src.Longitud, src.Latitud) { SRID = 4326 });

            CreateMap<Cine, CineDTO>();
            CreateMap<CineCreacionDTO, Cine>();
        }
    }
}
