using AutoMapper;
using ControlBiblioteca.DTOs;
using ControlBiblioteca.Models;

namespace ControlBiblioteca
{
    // Definición de una clase que hereda de Profile, que es una clase de AutoMapper para configuración de mapeos
    public class AutoMapperProfile : Profile
    {
        // Constructor de la clase
        public AutoMapperProfile()
        {
            // Dentro del constructor se definen los mapeos entre clases

            // CreateMap<TOrigen, TDestino>() define un mapeo entre la clase TOrigen y la clase TDestino
            // En este caso, se está mapeando la clase Autor a la clase AutorDto, y viceversa (ReverseMap)
            CreateMap<Autor, AutorDto>().ReverseMap();

            CreateMap<GeneroLiterario, GeneroLiterarioDto>().ReverseMap();

            CreateMap<Libro, LibroDto>().ReverseMap();
            // Esto significa que AutoMapper puede convertir automáticamente un Autor en un AutorDto
            // y viceversa, siempre que las propiedades tengan los mismos nombres y tipos compatibles.
        }
    }

}