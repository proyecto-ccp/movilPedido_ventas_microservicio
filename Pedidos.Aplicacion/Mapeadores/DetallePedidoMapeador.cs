using AutoMapper;
using Pedidos.Aplicacion.Dto;
using Pedidos.Dominio.Entidades;

namespace Pedidos.Aplicacion.Mapeadores
{
    public class DetallePedidoMapeador: Profile
    {
        public DetallePedidoMapeador() 
        {
            CreateMap<DetallePedido, DetallePedidoDto>()
                .ForMember(dest => dest.IdPedido, opt => opt.MapFrom(src => src.IdPedido))
                .ForMember(dest => dest.IdUsuario, opt => opt.MapFrom(src => src.IdUsuario))
                .ForMember(dest => dest.IdProducto, opt => opt.MapFrom(src => src.IdProducto))
                .ForMember(dest => dest.Cantidad, opt => opt.MapFrom(src => src.Cantidad))
                .ForMember(dest => dest.PrecioUnitario, opt => opt.MapFrom(src => src.PrecioUnitario))
                .ReverseMap();

            CreateMap<DetallePedido,DetallePedidoIn>()
                .ForMember(dest => dest.IdPedido, opt => opt.MapFrom(src => src.IdPedido))
                .ForMember(dest => dest.IdUsuario, opt => opt.MapFrom(src => src.IdUsuario))
                .ForMember(dest => dest.IdProducto, opt => opt.MapFrom(src => src.IdProducto))
                .ForMember(dest => dest.Cantidad, opt => opt.MapFrom(src => src.Cantidad))
                .ForMember(dest => dest.PrecioUnitario, opt => opt.MapFrom(src => src.PrecioUnitario))
                .ReverseMap();

            CreateMap<DetallePedidoOut,DetallePedidoIn>()
                .ForMember(dest => dest.IdPedido, opt => opt.MapFrom(src => src.DetallePedido.IdPedido))
                .ForMember(dest => dest.IdUsuario, opt => opt.MapFrom(src => src.DetallePedido.IdUsuario))
                .ForMember(dest => dest.IdProducto, opt => opt.MapFrom(src => src.DetallePedido.IdProducto))
                .ForMember(dest => dest.Cantidad, opt => opt.MapFrom(src => src.DetallePedido.Cantidad))
                .ForMember(dest => dest.PrecioUnitario, opt => opt.MapFrom(src => src.DetallePedido.PrecioUnitario))
                .ReverseMap();
        }
    }
}
