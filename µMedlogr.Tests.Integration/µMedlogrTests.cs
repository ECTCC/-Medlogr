using Microsoft.AspNetCore.Mvc.Testing;
using System.Net;

namespace µMedlogr.Tests.Integration;

public class µMedlogrTests {
    public class TodoTests : IClassFixture<WebApplicationFactory<Program>> {
        private readonly HttpClient _httpClient;
        public TodoTests(WebApplicationFactory<Program> factory) {
            _httpClient = factory.CreateDefaultClient();
        }

        [Fact]
        [Trait("Category", "Integration")]
        public async Task CanGetIndexPage() {
            // Arrange
            var navbar = "<ul class=\"navbar-nav\">";
            var contentType = "text/html";
            //Act
            var response = await _httpClient.GetAsync("/");
            var content = await response.Content.ReadAsStringAsync();

            //Assert
            response.EnsureSuccessStatusCode();
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            Assert.Equal(contentType, response.Content.Headers.ContentType?.MediaType);
            Assert.True(response.Content.Headers.ContentLength > 0);
            Assert.Contains(navbar, content);
        }

        [Fact]
        [Trait("Category", "Integration")]
        public async Task CanLoginAsValidUser() {
            var identifyingElement = "<form class=\"form-inline\" action=\"/Identity/Account/Logout?returnUrl=%2F\" method=\"post\">";

        }

        [Fact]
        [Trait("Category", "Integration")]
        public async Task LoginPage_ReturnsFailureForInvalidUser() {

            //Act
            var response = await _httpClient.PostAsync("/Identity/Account/Login", new StringContent("username=invaliduser&password=invalidpassword"));
            var content = await response.Content.ReadAsStringAsync();

            //Assert
            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        }

    }
}