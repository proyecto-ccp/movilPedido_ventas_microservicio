
using Pedidos.Aplicacion.Dto;
using System.Diagnostics.CodeAnalysis;
using System.Net;
using System.Net.Http.Json;

namespace Pedidos.Aplicacion.Clientes
{
    public interface IInventariosApiClient
    {
        [ExcludeFromCodeCoverageAttribute]
        Task<InventarioResponseDto> AgregarInventarioAsync(int idProducto, int cantidad, string authorization);
        Task<InventarioResponseDto> RetirarInventarioAsync(int idProducto, int cantidad, string authorization);
    }
    public class InventariosApiClient : IInventariosApiClient
    {
        private readonly HttpClient _httpClient;

        public InventariosApiClient(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        [ExcludeFromCodeCoverage]
        public async Task<InventarioResponseDto> AgregarInventarioAsync(int idProducto, int cantidad, string authorization)
        {
            var inventario = new Inventario
            {
                IdProducto = idProducto,
                CantidadStock = cantidad
            };

            _httpClient.DefaultRequestHeaders.Clear();
            _httpClient.DefaultRequestHeaders.Add("Authorization", $"{authorization}");
            var response = await _httpClient.PostAsJsonAsync("/api/Inventarios/Agregar", inventario);

            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadFromJsonAsync<InventarioResponseDto>();
            }
            else
            {
                var errorResponse = await response.Content.ReadFromJsonAsync<InventarioResponseDto>();
                throw new Exception($"Error al agregar inventario: {errorResponse.Mensaje}");
            }
        }
        public async Task<InventarioResponseDto> RetirarInventarioAsync(int idProducto, int cantidad, string authorization)
        {
            var inventario = new Inventario
            {
                IdProducto = idProducto,
                CantidadStock = cantidad
            };

            _httpClient.DefaultRequestHeaders.Clear();
            _httpClient.DefaultRequestHeaders.Add("Authorization", $"{authorization}");
            var response = await _httpClient.PostAsJsonAsync("/api/Inventarios/Retirar", inventario);

            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadFromJsonAsync<InventarioResponseDto>();
            }
            else
            {
                var errorResponse = await response.Content.ReadFromJsonAsync<InventarioResponseDto>();
                throw new Exception($"Error al agregar inventario: {errorResponse.Mensaje}");
            }
        }
    }
    
}
