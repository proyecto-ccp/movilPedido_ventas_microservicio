
using Pedidos.Aplicacion.Dto;
using System.Net.Http.Json;

namespace Pedidos.Aplicacion.Clientes
{
    public interface IInventariosApiClient
    {
        Task<InventarioResponseDto> AgregarInventarioAsync(int idProducto, int cantidad);
        Task<InventarioResponseDto> RetirarInventarioAsync(int idProducto, int cantidad);
    }
    public class InventariosApiClient : IInventariosApiClient
    {
        private readonly HttpClient _httpClient;

        public InventariosApiClient(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<InventarioResponseDto> AgregarInventarioAsync(int idProducto, int cantidad)
        {
            var inventario = new Inventario
            {
                IdProducto = idProducto,
                CantidadStock = cantidad
            };

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
        public async Task<InventarioResponseDto> RetirarInventarioAsync(int idProducto, int cantidad)
        {
            var inventario = new Inventario
            {
                IdProducto = idProducto,
                CantidadStock = cantidad
            };

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
