using Pedidos.Aplicacion.Dto;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text.Json;

namespace Pedidos.Aplicacion.Clientes
{
    public interface IUsuarioApiClient
    {
        Task<UsuarioResponseDto> ValidarToken(string token,int app);
        Task<string> Login();
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

        public async Task<string> Login()
        {
            var httpClient = new HttpClient();
            httpClient.BaseAddress = new Uri("https://usuarios-596275467600.us-central1.run.app");

            var loginData = new
            {
                username = "edw.538@gmail.com",
                contrasena = "edw.538@gmail.com",
                aplicacion = 1
            };

            var response = await httpClient.PostAsJsonAsync("/api/Usuarios/Login", loginData);

            if (!response.IsSuccessStatusCode)
                throw new Exception("No se pudo obtener el token");

            var content = await response.Content.ReadAsStringAsync();

            var result = JsonSerializer.Deserialize<UsuarioResponseDto>(content, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });

            return result.Token;
        }
    }
}
