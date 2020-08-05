using Hexagonal_Exercise.entry_point.catalog.v1.model;
using Microsoft.AspNetCore.Mvc.Testing;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using Xunit;

namespace Exagonal_exercise.test.entry_point.catalog.product.v1
{
    public class ProductsPutControllerShould: IClassFixture<WebApplicationFactory<Hexagonal_Exercise.Startup>>
    {
        private readonly WebApplicationFactory<Hexagonal_Exercise.Startup> factory;
        private readonly HttpClient httpClient;

        public ProductsPutControllerShould(WebApplicationFactory<Hexagonal_Exercise.Startup> factory)
        {
            this.factory = factory;

            httpClient = this.factory.CreateClient(new WebApplicationFactoryClientOptions
            {
                AllowAutoRedirect = false,
                BaseAddress = new System.Uri("https://localhost:44339")
            });

        }

        [Fact]
        public async void It_should_put_a_new_name_to_a_existing_product()
        {

            var product = RenameProductModelMother.Create();
            var request = new HttpRequestMessage(HttpMethod.Put,
            "/api/Products?productId=1");
            var productJson = new StringContent(JsonSerializer.Serialize(product), Encoding.UTF8, "application/json");
            request.Content = productJson;

            var response = await httpClient.SendAsync(request).ConfigureAwait(false);

            Assert.True(response.IsSuccessStatusCode);
        }
    }
}
