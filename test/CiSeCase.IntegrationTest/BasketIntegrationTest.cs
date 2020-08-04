using FluentAssertions;
using Newtonsoft.Json;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace CiSeCase.IntegrationTest
{
    public class BasketIntegrationTest
    {
        private const string BASE_PATH = "/api/Basket/";

        [Theory]
        [InlineData(1, 1, 2)]
        public async Task Add_ShouldBeReturnOk_WhenRightParameters(int userId, int productId, int quantity)
        {
            var requestModel = new
            {
                userId = userId,
                productId = productId,
                quantity = quantity
            };
            var jsonData = JsonConvert.SerializeObject(requestModel);

            using var httpClient = new ClientProvider().HttpClient;
            var response = await httpClient.PostAsync($"{BASE_PATH}add", new StringContent(jsonData, Encoding.UTF8, "application/json"));

            response.StatusCode.Should().Be(HttpStatusCode.OK);
        }

        [Theory]
        [InlineData(1, 1, 11)]
        public async Task Add_ShouldBeReturnBadRequest_WhenOutOfStock(int userId, int productId, int quantity)
        {
            var requestModel = new
            {
                userId = userId,
                productId = productId,
                quantity = quantity
            };
            var jsonData = JsonConvert.SerializeObject(requestModel);

            using var httpClient = new ClientProvider().HttpClient;
            var response = await httpClient.PostAsync($"{BASE_PATH}add", new StringContent(jsonData, Encoding.UTF8, "application/json"));

            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        }

        [Theory]
        [InlineData(1, 3, 2)]
        public async Task Add_ShouldBeReturnNotFound_WhenWrongProductId(int userId, int productId, int quantity)
        {
            var requestModel = new
            {
                userId = userId,
                productId = productId,
                quantity = quantity
            };
            var jsonData = JsonConvert.SerializeObject(requestModel);

            using var httpClient = new ClientProvider().HttpClient;
            var response = await httpClient.PostAsync($"{BASE_PATH}add", new StringContent(jsonData, Encoding.UTF8, "application/json"));

            response.StatusCode.Should().Be(HttpStatusCode.NotFound);
        }

        [Theory]
        [InlineData(5, 1, 2)]
        public async Task Add_ShouldBeReturnNotFound_WhenWrongUserId(int userId, int productId, int quantity)
        {
            var requestModel = new
            {
                userId = userId,
                productId = productId,
                quantity = quantity
            };
            var jsonData = JsonConvert.SerializeObject(requestModel);

            using var httpClient = new ClientProvider().HttpClient;
            var response = await httpClient.PostAsync($"{BASE_PATH}add", new StringContent(jsonData, Encoding.UTF8, "application/json"));

            response.StatusCode.Should().Be(HttpStatusCode.NotFound);
        }

        [Theory]
        [InlineData(1)]
        public async Task Get_ShouldBeReturnOk_WhenRightUserId(int userId)
        {
            using var httpClient = new ClientProvider().HttpClient;
            var response = await httpClient.GetAsync($"{BASE_PATH}get?userId={userId}");
            response.StatusCode.Should().Be(HttpStatusCode.OK);
        }

        [Theory]
        [InlineData(5)]
        public async Task Get_ShouldBeReturnNotFound_WhenWrongUserId(int userId)
        {
            using var httpClient = new ClientProvider().HttpClient;
            var response = await httpClient.GetAsync($"{BASE_PATH}get?userId={userId}");
            response.StatusCode.Should().Be(HttpStatusCode.NotFound);
        }
    }
}