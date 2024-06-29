using AutoMapper;
using ExamenCGastos.DTOs;
using ExamenCGastos.Models;

namespace ExamenCGastos
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile() 
        {
            CreateMap<Proveedor, ProveedorDto>().ReverseMap();
            CreateMap<Producto, ProductoDto>().ReverseMap();
            CreateMap<Inventario, InventarioDto>().ReverseMap();
            CreateMap<Usuario, UsuarioDTO>().ReverseMap();
        }
    }
}
