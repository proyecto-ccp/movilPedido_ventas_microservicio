using Pedidos.Aplicacion.Dto;
using System.Net.Http.Json;
using System.Text.Json;

namespace Pedidos.Aplicacion.Clientes
{
    public interface IProductosApiClient
    {
        Task<ProductoDto> ObtenerProductoPorIdAsync(int idProducto, string authorization);
    }

    public class ProductosApiClient : IProductosApiClient
    {
        private readonly HttpClient _httpClient;
        public ProductosApiClient(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
        public async Task<ProductoDto> ObtenerProductoPorIdAsync(int idProducto, string authorization)
        {
            _httpClient.DefaultRequestHeaders.Clear();
            _httpClient.DefaultRequestHeaders.Add("Authorization", $"{authorization}");
            var response = await _httpClient.GetAsync($"/api/Productos/ConsultarPorId?IdProducto={idProducto}");

            if (!response.IsSuccessStatusCode)
            {
                var errorResponse = await response.Content.ReadFromJsonAsync<ProductoResponseDto>();
                throw new Exception($"Error al obtener producto: {errorResponse.Mensaje}");
            }

            var content = await response.Content.ReadAsStringAsync();

            var productoResponse = JsonSerializer.Deserialize<ProductoResponseDto>(content, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });

            return productoResponse.Producto;

        }
    }
}
