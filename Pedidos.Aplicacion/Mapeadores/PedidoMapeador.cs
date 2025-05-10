using AutoMapper;
using Pedidos.Aplicacion.Dto;
using Pedidos.Dominio.Entidades;

namespace Pedidos.Aplicacion.Mapeadores
{
    public class PedidoMapeador: Profile
    {
        public PedidoMapeador()
        {
            CreateMap<Pedido,PedidoDto>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.IdCliente, opt => opt.MapFrom(src => src.IdCliente))
                .ForMember(dest => dest.FechaRealizado, opt => opt.MapFrom(src => src.FechaRealizado))
                .ForMember(dest => dest.FechaEntrega, opt => opt.MapFrom(src => src.FechaEntrega))
                .ForMember(dest => dest.EstadoPedido, opt => opt.MapFrom(src => src.EstadoPedido))
                .ForMember(dest => dest.ValorTotal, opt => opt.MapFrom(src => src.ValorTotal))
                .ForMember(dest => dest.IdVendedor, opt => opt.MapFrom(src => src.IdVendedor))
                .ForMember(dest => dest.Comentarios, opt => opt.MapFrom(src => src.Comentarios))
                .ForMember(dest => dest.IdMoneda, opt => opt.MapFrom(src => src.IdMoneda))
                .ReverseMap();

            CreateMap<Pedido,PedidoIn>()
                .ForMember(dest => dest.IdCliente, opt => opt.MapFrom(src => src.IdCliente))
                .ForMember(dest => dest.FechaEntrega, opt => opt.MapFrom(src => src.FechaEntrega))
                .ForMember(dest => dest.EstadoPedido, opt => opt.MapFrom(src => src.EstadoPedido))
                .ForMember(dest => dest.ValorTotal, opt => opt.MapFrom(src => src.ValorTotal))
                .ForMember(dest => dest.IdVendedor, opt => opt.MapFrom(src => src.IdVendedor))
                .ForMember(dest => dest.Comentarios, opt => opt.MapFrom(src => src.Comentarios))
                .ForMember(dest => dest.IdMoneda, opt => opt.MapFrom(src => src.IdMoneda))
                .ReverseMap();

            CreateMap<Pedido, PedidoActualizarIn>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.IdCliente, opt => opt.MapFrom(src => src.IdCliente))
                .ForMember(dest => dest.FechaEntrega, opt => opt.MapFrom(src => src.FechaEntrega))
                .ForMember(dest => dest.FechaRealizado, opt => opt.MapFrom(src => src.FechaRealizado))
                .ForMember(dest => dest.EstadoPedido, opt => opt.MapFrom(src => src.EstadoPedido))
                .ForMember(dest => dest.ValorTotal, opt => opt.MapFrom(src => src.ValorTotal))
                .ForMember(dest => dest.IdVendedor, opt => opt.MapFrom(src => src.IdVendedor))
                .ForMember(dest => dest.Comentarios, opt => opt.MapFrom(src => src.Comentarios))
                .ForMember(dest => dest.IdMoneda, opt => opt.MapFrom(src => src.IdMoneda))
                .ReverseMap();

            CreateMap<PedidoOut,PedidoIn>()
                .ForMember(dest => dest.IdCliente, opt => opt.MapFrom(src => src.Pedido.IdCliente))
                .ForMember(dest => dest.FechaEntrega, opt => opt.MapFrom(src => src.Pedido.FechaEntrega))
                .ForMember(dest => dest.EstadoPedido, opt => opt.MapFrom(src => src.Pedido.EstadoPedido))
                .ForMember(dest => dest.ValorTotal, opt => opt.MapFrom(src => src.Pedido.ValorTotal))
                .ForMember(dest => dest.IdVendedor, opt => opt.MapFrom(src => src.Pedido.IdVendedor))
                .ForMember(dest => dest.Comentarios, opt => opt.MapFrom(src => src.Pedido.Comentarios))
                .ForMember(dest => dest.IdMoneda, opt => opt.MapFrom(src => src.Pedido.IdMoneda))
                .ReverseMap();
        }
    }
}
