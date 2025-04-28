using System.Net.Http.Headers;
using System.Text.Json;

namespace Fansoft.PocArc.Front.Services;

public class ApiService
    {
        private readonly HttpClient _httpClient;
        private readonly TokenService _tokenService;
        private readonly IConfiguration _configuration;
        private string? _cachedToken;
        private DateTime _tokenExpiration;

        public ApiService(HttpClient httpClient, TokenService tokenService, IConfiguration configuration)
        {
            _httpClient = httpClient;
            _tokenService = tokenService;
            _configuration = configuration;
        }

        private async Task EnsureAuthorizationHeaderAsync()
        {
            if (string.IsNullOrEmpty(_cachedToken) || DateTime.UtcNow >= _tokenExpiration)
            {
                var tokenResult = await _tokenService.GetTokenAsync();
                _cachedToken = tokenResult; // Você pode ajustar para pegar também o expires_in no futuro
                _tokenExpiration = DateTime.UtcNow.AddMinutes(5); // Vamos assumir 5 minutos (ajustável)
            }

            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _cachedToken);
        }

        public async Task<List<CustomerDto>> GetCustomersAsync(int page = 1, int pageSize = 10)
        {
            await EnsureAuthorizationHeaderAsync();

            var backendUrl = _configuration["Backend:BaseUrl"];
            var response = await _httpClient.GetAsync($"{backendUrl}/customers?page={page}&pageSize={pageSize}");

            if (!response.IsSuccessStatusCode)
            {
                if (response.StatusCode == System.Net.HttpStatusCode.Unauthorized)
                    throw new ApplicationException("Não autorizado a acessar a API.");

                if (response.StatusCode == System.Net.HttpStatusCode.Forbidden)
                    throw new ApplicationException("Acesso proibido à API.");

                throw new ApplicationException($"Erro ao acessar a API: {response.StatusCode}");
            }

            var content = await response.Content.ReadAsStringAsync();

            var result = JsonDocument.Parse(content);

            var customers = result.RootElement.GetProperty("items").Deserialize<List<CustomerDto>>(new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });

            return customers ?? new List<CustomerDto>();
        }
        
        public async Task<(List<CustomerDto> Customers, int Page, int PageSize, int TotalPages, int TotalItems)> GetCustomersPagedAsync(int page = 1, int pageSize = 10, string? search = null)
        {
            await EnsureAuthorizationHeaderAsync();

            var backendUrl = _configuration["Backend:BaseUrl"];
            var url = $"{backendUrl}/customers?page={page}&pageSize={pageSize}";

            if (!string.IsNullOrWhiteSpace(search))
            {
                url += $"&search={Uri.EscapeDataString(search)}";
            }

            var response = await _httpClient.GetAsync(url);

            if (!response.IsSuccessStatusCode)
            {
                throw new ApplicationException($"Erro ao acessar a API: {response.StatusCode}");
            }

            var content = await response.Content.ReadAsStringAsync();
            var result = JsonDocument.Parse(content);

            var customers = result.RootElement.GetProperty("items").Deserialize<List<CustomerDto>>(new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            }) ?? new List<CustomerDto>();

            int totalItems = result.RootElement.GetProperty("totalItems").GetInt32();
            int totalPages = result.RootElement.GetProperty("totalPages").GetInt32();

            return (customers, page, pageSize, totalPages, totalItems);
        }
        
        public async Task CreateCustomerAsync(CustomerDto customer)
        {
            await EnsureAuthorizationHeaderAsync();

            var backendUrl = _configuration["Backend:BaseUrl"];
            var response = await _httpClient.PostAsJsonAsync($"{backendUrl}/customers", customer);

            if (!response.IsSuccessStatusCode)
            {
                throw new ApplicationException($"Erro ao criar customer: {response.StatusCode}");
            }
        }
        
        public async Task<CustomerDto?> GetCustomerByIdAsync(Guid id)
        {
            await EnsureAuthorizationHeaderAsync();

            var backendUrl = _configuration["Backend:BaseUrl"];
            var response = await _httpClient.GetAsync($"{backendUrl}/customers/{id}");

            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadFromJsonAsync<CustomerDto>();
            }

            return null;
        }

        public async Task UpdateCustomerAsync(CustomerDto customer)
        {
            await EnsureAuthorizationHeaderAsync();

            var backendUrl = _configuration["Backend:BaseUrl"];
            var response = await _httpClient.PutAsJsonAsync($"{backendUrl}/customers/{customer.Id}", customer);

            if (!response.IsSuccessStatusCode)
            {
                throw new ApplicationException($"Erro ao atualizar customer: {response.StatusCode}");
            }
        }
        
        public async Task DeleteCustomerAsync(Guid id)
        {
            await EnsureAuthorizationHeaderAsync();

            var backendUrl = _configuration["Backend:BaseUrl"];
            var response = await _httpClient.DeleteAsync($"{backendUrl}/customers/{id}");

            if (!response.IsSuccessStatusCode)
            {
                throw new ApplicationException($"Erro ao deletar customer: {response.StatusCode}");
            }
        }
    }

public class CustomerDto
{
    public Guid Id { get; set; }
    public string Name { get; set; } = "";
    public string Email { get; set; } = "";
    public string Phone { get; set; } = "";
}