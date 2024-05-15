namespace WebShopClient.Services
{
    public class ApiServices
    {
        private readonly HttpClient _client;

        public ApiServices(IHttpClientFactory clientFactory)
        {
            _client = clientFactory.CreateClient("API Client");
        }

        //public async Task<ICollection<Product>> GetProductsByIdsAsync(ICollection<int> productIds)
        //{
        //    // Anropa API:t för att hämta produktdetaljer baserat på ID:na
        //    // Implementera logik för att göra HTTP-anrop till API:t och hantera svar här
        //}
    }
}
