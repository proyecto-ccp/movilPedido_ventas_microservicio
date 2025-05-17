using Pedidos.Aplicacion.Dto;
using System.Net.Http.Json;
using System.Text.Json;

namespace Pedidos.Aplicacion.Clientes
{
    public interface IUsuarioApiClient
    {
        Task<UsuarioResponseDto> ValidarToken(string token,int app);
    }

    public class UsuarioApiClient : IUsuarioApiClient
    {
        private readonly HttpClient _httpClient;
        public UsuarioApiClient(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
        public async Task<UsuarioResponseDto> ValidarToken(string token, int app)
        {
            _httpClient.DefaultRequestHeaders.Clear();
            _httpClient.DefaultRequestHeaders.Add("Authorization", $"Bearer {token}");
            var response = await _httpClient.GetAsync($"/api/Usuarios/Autorizar/{app}");

            if (!response.IsSuccessStatusCode)
            {
                var errorResponse = await response.Content.ReadFromJsonAsync<UsuarioResponseDto>();
                throw new Exception($"Error al validar token: {errorResponse.Mensaje}");
            }
            var content = await response.Content.ReadAsStringAsync();
            var usuarioResponse = JsonSerializer.Deserialize<UsuarioResponseDto>(content, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });

            return usuarioResponse;
        }
    }
}
