using Hexagonal_Exercise.entry_point.catalog.v1.model;
using Microsoft.AspNetCore.Mvc.Testing;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using Xunit;

namespace Exagonal_exercise.test.entry_point.catalog.product.v1
{
    public class ProductsGetControllerShould: IClassFixture<WebApplicationFactory<Hexagonal_Exercise.Startup>>
    {
        private readonly WebApplicationFactory<Hexagonal_Exercise.Startup> _factory;
        private readonly HttpClient _httpClient;

        public ProductsGetControllerShould(WebApplicationFactory<Hexagonal_Exercise.Startup> factory)
        {
            _factory = factory;

            _httpClient = _factory.CreateClient(new WebApplicationFactoryClientOptions
            {
                AllowAutoRedirect = false,
                BaseAddress = new System.Uri("https://localhost:44339")
            });

        }

        [Fact]
        public async void It_should_get_a_product_by_Id()
        {
            var request = new HttpRequestMessage(HttpMethod.Get,
            "/api/Products?productId=1");

            var response = await _httpClient.SendAsync(request).ConfigureAwait(false);

            Assert.True(response.IsSuccessStatusCode);
        }

    }
}
