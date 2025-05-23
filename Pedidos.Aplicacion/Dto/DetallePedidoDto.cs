﻿namespace Pedidos.Aplicacion.Dto
{
    public  class DetallePedidoDto
    {
        public Guid? IdPedido { get; set; }
        public Guid IdUsuario { get; set; }
        public int IdProducto { get; set; }
        public int Cantidad { get; set; }
        public decimal PrecioUnitario { get; set; }
        public string NombreProducto { get; set; }
        public string UrlFotoProducto1 { get; set; }
        public string UrlFotoProducto2 { get; set; }
    }

    public class DetallePedidoOut : BaseOut
    {
        public DetallePedidoDto DetallePedido { get; set; }
    }

    public class DetallePedidoOutList : BaseOut
    {
        public List<DetallePedidoDto> DetallePedidos { get; set; }
    }
}

