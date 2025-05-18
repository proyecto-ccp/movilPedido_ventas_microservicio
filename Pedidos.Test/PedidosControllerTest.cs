using Bogus;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.VisualStudio.TestPlatform.TestHost;
using Newtonsoft.Json.Linq;
using Pedidos.Aplicacion.Clientes;
using Pedidos.Aplicacion.Dto;
using System.Net;
using System.Text;
using System.Text.Json;

namespace Pedidos.Test
{
    public class PedidosControllerTest : IClassFixture<WebApplicationFactory<Program>>
    {
        private readonly HttpClient _client;
        private readonly Guid clienteId = Guid.Parse("d7be0dc4-b41a-4719-83ee-84f11e68b622");
        private readonly Guid vendedorId = Guid.Parse("b07e8ab8-b787-4f6d-8a85-6c506a3616f5");
        private Guid pedidoId;
        private string token = string.Empty;
        public PedidosControllerTest(WebApplicationFactory<Program> factory)
        {
            _client = factory.CreateClient();
            IUsuarioApiClient _usuarioApiClient = new UsuarioApiClient(_client);
            token = _usuarioApiClient.Login().Result;
            _client.DefaultRequestHeaders.Add("Authorization", $"Bearer {token}");
        }

        [Fact]
        public async Task crearPedido_Ok()
        {
            var faker = new Faker();

            var pedido = new PedidoIn
            {
                IdCliente = clienteId,
                FechaEntrega = DateTime.UtcNow.AddDays(5),
                EstadoPedido = "CREADO",
                ValorTotal = faker.Random.Decimal(100, 1000),
                IdVendedor = vendedorId,
                Comentarios = faker.Lorem.Sentence(),
                IdMoneda = 1
            };

            var content = new StringContent(
                JsonSerializer.Serialize(pedido),
                Encoding.UTF8,
                "application/json"
            );

            //Act
            var response = await _client.PostAsync("/api/Pedido/CrearPedido", content);

            //Assert
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            response.EnsureSuccessStatusCode();
            var responseString = await response.Content.ReadAsStringAsync();
            var pedidoResult = JsonSerializer.Deserialize<PedidoOut>(responseString, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });

            Assert.NotNull(pedidoResult);
            Assert.Equal(Pedidos.Aplicacion.Enum.Resultado.Exitoso, pedidoResult.Resultado);
            Assert.Equal(HttpStatusCode.Created, pedidoResult.Status);

            pedidoId = (Guid)pedidoResult.Id;
        }


        [Fact]
        public async Task crearPedido_BadRequest()
        {
            var faker = new Faker();

            var pedido = new PedidoIn
            {
                IdCliente = Guid.NewGuid(),
                FechaEntrega = DateTime.Now.AddDays(5),
                EstadoPedido = "CREADO",
                ValorTotal = faker.Random.Decimal(100, 1000),
                IdVendedor = Guid.NewGuid(),
                Comentarios = faker.Lorem.Sentence(),
                IdMoneda = 1
            };
            var content = new StringContent(
                JsonSerializer.Serialize(pedido),
                Encoding.UTF8,
                "application/json"
            );
            //Act
            var response = await _client.PostAsync("/api/Pedido/CrearPedido", content);
            //Assert
            Assert.Equal(HttpStatusCode.InternalServerError, response.StatusCode);
        }

        [Fact]
        public async Task ObtenerPedidoPorId_Ok()
        {
            if (pedidoId == Guid.Empty)
            {
                await crearPedido_Ok();
            }

            //Act
            var response = await _client.GetAsync($"/api/Pedido/ObtenerPedido/{pedidoId}");
            //Assert
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            response.EnsureSuccessStatusCode();
            var responseString = await response.Content.ReadAsStringAsync();
            var pedidoResult = JsonSerializer.Deserialize<PedidoOut>(responseString, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });
            Assert.NotNull(pedidoResult);
            Assert.Equal(Pedidos.Aplicacion.Enum.Resultado.Exitoso, pedidoResult.Resultado);
        }

        [Fact]
        public async Task ObtenerPedidoPorId_NotFound()
        {
            //Act
            var response = await _client.GetAsync($"/api/Pedido/ObtenerPedido/{Guid.NewGuid()}");
            //Assert
            Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
        }

        [Fact]
        public async Task ObtenerPedidosPorCliente_Ok()
        {
            //Act
            var response = await _client.GetAsync($"/api/Pedido/ObtenerPedidosPorCliente/{clienteId}/CREADO");
            //Assert
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            response.EnsureSuccessStatusCode();
            var responseString = await response.Content.ReadAsStringAsync();
            var pedidoResult = JsonSerializer.Deserialize<PedidoOutList>(responseString, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });
            Assert.NotNull(pedidoResult);
            Assert.IsType<PedidoOutList>(pedidoResult);
        }

        [Fact]
        public async Task ObtenerPedidosPorCliente_NotFound()
        {
            //Act
            var response = await _client.GetAsync($"/api/Pedido/ObtenerPedidosPorCliente/{Guid.NewGuid()}/CREADO");
            //Assert
            Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
        }

        [Fact]
        public async Task ObtenerPedidosPorVendedor_Ok()
        {
            //Act
            var response = await _client.GetAsync($"/api/Pedido/ObtenerPedidosPorVendedor/{vendedorId}/CREADO");
            //Assert
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            response.EnsureSuccessStatusCode();
            var responseString = await response.Content.ReadAsStringAsync();
            var pedidoResult = JsonSerializer.Deserialize<PedidoOutList>(responseString, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });
            Assert.NotNull(pedidoResult);
            Assert.IsType<PedidoOutList>(pedidoResult );
        }

        [Fact]
        public async Task ObtenerPedidosPorVendedor_NotFound()
        {
            //Act
            var response = await _client.GetAsync($"/api/Pedido/ObtenerPedidosPorVendedor/{Guid.NewGuid()}/CREADO");
            //Assert
            Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
        }

        [Fact]
        public async Task ObtenerPedidosPorEstado_Ok()
        {
            //Act
            var response = await _client.GetAsync($"/api/Pedido/ObtenerPedidosPorEstado/CREADO");
            //Assert
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            response.EnsureSuccessStatusCode();
            var responseString = await response.Content.ReadAsStringAsync();
            var pedidoResult = JsonSerializer.Deserialize<PedidoOutList>(responseString, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });
            Assert.NotNull(pedidoResult);
            Assert.IsType<PedidoOutList>(pedidoResult);
        }

        [Fact]
        public async Task actualizarPedido_Ok()
        {
            if (pedidoId == Guid.Empty)
            {
                await crearPedido_Ok();
            }
            var faker = new Faker();
            var pedido = new PedidoActualizarIn
            {
                IdCliente = clienteId,
                FechaEntrega = DateTime.UtcNow.AddDays(5),
                FechaRealizado = DateTime.UtcNow.AddDays(5),
                EstadoPedido = "CONFIRMADO",
                ValorTotal = faker.Random.Decimal(100, 1000),
                IdVendedor = vendedorId,
                Comentarios = faker.Lorem.Sentence(),
                IdMoneda = 1
            };
            var content = new StringContent(
                JsonSerializer.Serialize(pedido),
                Encoding.UTF8,
                "application/json"
            );
            //Act
            var response = await _client.PutAsync($"/api/Pedido/ActualizarPedido/{pedidoId}", content);
            //Assert
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            response.EnsureSuccessStatusCode();
        }
    }
}