using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using PlatformService.Dtos;

namespace PlatformService.SyncDataService.Http
{
    public class HttpCommandDataClient : ICommandDataClient
    {
        public HttpCommandDataClient(HttpClient httpClient, IConfiguration configuration)
        {
            _httpClient = httpClient;
            _configuration = configuration;
        }

        private readonly HttpClient _httpClient;
        
        private readonly IConfiguration _configuration;

        public async Task SendPlatformToCommand(PlatformReadDto platformReadDto)
        {
            var stringContent = new StringContent(JsonSerializer.Serialize(platformReadDto), Encoding.UTF8, "application/json");
            await _httpClient.PostAsync($"{_configuration["CommandService"]}/api/c/Platforms/", stringContent);
        }
    }
}