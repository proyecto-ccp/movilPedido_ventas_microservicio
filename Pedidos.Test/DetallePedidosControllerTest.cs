using Microsoft.AspNetCore.Mvc.Testing;
using Pedidos.Aplicacion.Dto;
using System.Net;
using System.Text;
using System.Text.Json;

namespace Pedidos.Test
{
    public class DetallePedidosControllerTest : IClassFixture<WebApplicationFactory<Program>>
    {
        private readonly HttpClient _client;
        private readonly Guid usuarioId = Guid.Parse("d7be0dc4-b41a-4719-83ee-84f11e68b622");
        private readonly Guid pedidoId = Guid.Parse("b9d65f53-1087-4f45-8a61-001828e2ba50");
        private readonly int productoId1 = 1223;
        private readonly int productoId2 = 1224;
        private Guid detalleId;
        public DetallePedidosControllerTest(WebApplicationFactory<Program> factory)
        {
            _client = factory.CreateClient();
        }

        [Fact]
        public async Task CrearDetallePedido_Ok()
        {
            var detallePedido = new DetallePedidoIn
            {
                IdUsuario = usuarioId,
                IdProducto = productoId1,
                Cantidad = 2,
                PrecioUnitario = 100
            };

            var content = new StringContent(
                JsonSerializer.Serialize(detallePedido),
                Encoding.UTF8,
                "application/json"
            );

            //Act
            var response = await _client.PostAsync("/api/DetallePedido/AgregarDetalle", content);

            //Assert
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            response.EnsureSuccessStatusCode();
            var responseString = await response.Content.ReadAsStringAsync();
            var detallePedidoResult = JsonSerializer.Deserialize<DetallePedidoOut>(responseString, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });
            Assert.NotNull(detallePedidoResult);
            Assert.Equal(Pedidos.Aplicacion.Enum.Resultado.Exitoso, detallePedidoResult.Resultado);
            Assert.Equal(HttpStatusCode.Created, detallePedidoResult.Status);
            detalleId = (Guid)detallePedidoResult.Id;
        }

        [Fact]
        public async Task eliminarDetalle_Ok()
        {
            if (detalleId == Guid.Empty)
                await CrearDetallePedido_Ok();

            //Act
            var response = await _client.DeleteAsync($"/api/DetallePedido/EliminarDetalle/{detalleId}");

            //Assert
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }

        [Fact]
        public async Task CrearDetallePedido_ConProductoInexistente()
        {
            var detallePedido = new DetallePedidoIn
            {
                IdUsuario = usuarioId,
                IdProducto = 9999, // Producto inexistente
                Cantidad = 2,
                PrecioUnitario = 100
            };
            var content = new StringContent(
                JsonSerializer.Serialize(detallePedido),
                Encoding.UTF8,
                "application/json"
            );
            //Act
            var response = await _client.PostAsync("/api/DetallePedido/AgregarDetalle", content);
            //Assert
            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        }

        [Fact]
        public async Task obtenerDetalle_OK()
        {
            if (detalleId == Guid.Empty)
                await CrearDetallePedido_Ok();
            //Act
            var response = await _client.GetAsync($"/api/DetallePedido/ObtenerDetalles/{pedidoId}");

            //Assert
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            response.EnsureSuccessStatusCode();
            var responseString = await response.Content.ReadAsStringAsync();
            var detallePedidoResult = JsonSerializer.Deserialize<DetallePedidoOutList>(responseString, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });

            Assert.NotNull(detallePedidoResult);
            Assert.IsType<DetallePedidoOutList>(detallePedidoResult);
        }

        [Fact]
        public async Task obtenerDetalle_BadRequest()
        {
            //Act
            var response = await _client.GetAsync($"/api/DetallePedido/ObtenerDetalles/{Guid.NewGuid()}");

            //Assert
            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
            var responseString = await response.Content.ReadAsStringAsync();
            Assert.Contains("No se encontraron detalles de pedido", responseString);
        }

        [Fact]
        public async Task obtenerDetalle_UsuarioOk()
        {
            if (detalleId == Guid.Empty)
                await CrearDetallePedido_Ok();
            //Act
            var response = await _client.GetAsync($"/api/DetallePedido/ObtenerDetallesUsuario/{usuarioId}");
            //Assert
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            response.EnsureSuccessStatusCode();
            var responseString = await response.Content.ReadAsStringAsync();
            var detallePedidoResult = JsonSerializer.Deserialize<DetallePedidoOutList>(responseString, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });
            Assert.NotNull(detallePedidoResult);
            Assert.IsType<DetallePedidoOutList>(detallePedidoResult);
        }
    }
}
