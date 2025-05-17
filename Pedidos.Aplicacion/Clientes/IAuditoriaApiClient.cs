using Pedidos.Aplicacion.Dto;
using System.Net.Http.Json;
using System.Text.Json;

namespace Pedidos.Aplicacion.Clientes
{
    public interface IAuditoriaApiClient
    {
        Task<AuditoriaResponseDto> RegistrarAuditoria(AuditoriaDto auditoria);
    }

    public class AuditoriaApiClient: IAuditoriaApiClient
    {
        private readonly HttpClient _httpClient;
        public AuditoriaApiClient(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
        public async Task<AuditoriaResponseDto> RegistrarAuditoria(AuditoriaDto auditoria)
        {
            var response = await _httpClient.PostAsJsonAsync("/api/Auditoria/RegistrarAuditoria", auditoria);
            if (!response.IsSuccessStatusCode)
            {
                var errorResponse = await response.Content.ReadFromJsonAsync<AuditoriaResponseDto>();
                throw new Exception($"Error al registrar auditoría: {errorResponse.Mensaje}");
            }
            var content = await response.Content.ReadAsStringAsync();
            var auditoriaResponse = JsonSerializer.Deserialize<AuditoriaResponseDto>(content, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });
            return auditoriaResponse;
        }
    }
}
