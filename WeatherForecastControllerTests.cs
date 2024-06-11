using System.Text.Json;
using Microsoft.Extensions.Configuration;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace XUnitTests;

public class WeatherforecastControllerTests
{
    private readonly IWebDriver _driver;
    private readonly IConfiguration _configuration;
    public WeatherforecastControllerTests()
    {
        // Setup code
        _driver = new ChromeDriver();
        var builder = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);

        _configuration = builder.Build();
    }

    [Fact]
    public void Get_ReturnsPopulatedListOfForecast()
    {
        _driver.Navigate().GoToUrl(_configuration.GetConnectionString("APIEndpoint"));

        // Get the page source and preelement which is the JSON data
        var preElement = _driver.FindElement(By.TagName("Pre"));

        // strip out the JSON data
        var jsonData = preElement.Text;

        // Parse the JSON data
        var forecasts = JsonSerializer.Deserialize<List<Root>>(jsonData);

        Assert.NotNull(forecasts);
        Assert.True(forecasts.Count > 0);

        // Example assertions
        // Assert.Equal("Strabane", forecasts[0].summary);
        // Assert.Equal(-18, forecasts[0].temperatureF);
        // Assert.Equal(0, forecasts[0].temperatureC);

        _driver.Quit();
    }
}