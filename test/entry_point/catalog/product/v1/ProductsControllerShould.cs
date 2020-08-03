using Hexagonal_Exercise.entry_point.catalog.v1.model;
using Microsoft.AspNetCore.Mvc.Testing;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using Xunit;

namespace Exagonal_exercise.test.entry_point.catalog.product.v1
{
    public class ProductsControllerShould: IClassFixture<WebApplicationFactory<Hexagonal_Exercise.Startup>>
    {
        private readonly WebApplicationFactory<Hexagonal_Exercise.Startup> _factory;
        private readonly HttpClient _httpClient;

        public ProductsControllerShould(WebApplicationFactory<Hexagonal_Exercise.Startup> factory)
        {
            _factory = factory;

            _httpClient = _factory.CreateClient(new WebApplicationFactoryClientOptions
            {
                AllowAutoRedirect = false
            });

        }

        [Fact]
        public async void It_should_post_a_new_product()
        {
            var product = CreateProductModelMother.Create();
            var request = new HttpRequestMessage(HttpMethod.Post,
            "https://localhost:44339/api/Products");
            var productJson = new StringContent(JsonSerializer.Serialize(product), Encoding.UTF8, "application/json");
            request.Content = productJson;

            var response = await _httpClient.SendAsync(request).ConfigureAwait(false);

            Assert.True(response.IsSuccessStatusCode);
        }

        [Fact]
        public async void It_should_get_a_product_by_Id()
        {
            var request = new HttpRequestMessage(HttpMethod.Get,
            "https://localhost:44339/api/Products?productId=1");

            var response = await _httpClient.SendAsync(request).ConfigureAwait(false);

            Assert.True(response.IsSuccessStatusCode);
        }

        [Fact]
        public async void It_should_put_a_new_name_to_a_existing_product()
        {

            var product = RenameProductModelMother.Create();
            var request = new HttpRequestMessage(HttpMethod.Put,
            "https://localhost:44339/api/Products?productId=1");
            var productJson = new StringContent(JsonSerializer.Serialize(product), Encoding.UTF8, "application/json");
            request.Content = productJson;

            var response = await _httpClient.SendAsync(request).ConfigureAwait(false);

            Assert.True(response.IsSuccessStatusCode);
        }

        [Fact]
        public async void It_should_delete_an_existing_product()
        {
            var product = CreateProductModelMother.Create(2);
            var requestArr = new HttpRequestMessage(HttpMethod.Post,
            "https://localhost:44339/api/Products");
            var productJson = new StringContent(JsonSerializer.Serialize(product), Encoding.UTF8, "application/json");
            requestArr.Content = productJson;
            var responseArr = await _httpClient.SendAsync(requestArr).ConfigureAwait(false);

            var request = new HttpRequestMessage(HttpMethod.Delete,
            "https://localhost:44339/api/Products?productId=2");

            var response = await _httpClient.SendAsync(request).ConfigureAwait(false);

            Assert.True(response.IsSuccessStatusCode);
        }
    }
}
