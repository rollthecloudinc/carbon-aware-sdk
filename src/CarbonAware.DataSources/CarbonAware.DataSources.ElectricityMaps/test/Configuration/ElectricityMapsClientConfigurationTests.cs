using CarbonAware.DataSources.ElectricityMaps.Configuration;
using CarbonAware.DataSources.ElectricityMaps.Constants;
using CarbonAware.Exceptions;

namespace CarbonAware.DataSources.ElectricityMaps.Tests;

[TestFixture]
public class ElectricityMapsClientConfigurationTests
{
    [TestCase("x-token-header", "faketoken", "http://example.com", TestName = "Validate does not throw: header; value; url")]
    [TestCase(Headers.TokenAuthHeader, "faketoken", BaseUrls.TokenBaseUrl, TestName = "Validate does not throw: token header; value; token url")]
    [TestCase(Headers.TrialAuthHeader, "faketoken", BaseUrls.TrialBaseUrl, TestName = "Validate does not throw: trial header; value; trial url")]
    public void Validate_DoesNotThrow(string? tokenHeader, string? tokenValue, string? url)
    {
        // Arrange
        var config = new ElectricityMapsClientConfiguration();
        if (tokenHeader != null)
            config.APITokenHeader = tokenHeader;
        if (tokenValue != null)
            config.APIToken = tokenValue;
        if (url != null)
            config.BaseUrl = url;

        // Act & Assert
        Assert.DoesNotThrow(() => config.Validate());
    }

    [TestCase("x-token-header", "faketoken", "not a url", TestName = "Validate throws: header; value; bad url")]
    [TestCase(null, "faketoken", "http://example.com", TestName = "Validate throws: no header; value; url")]
    [TestCase("x-token-header", null, "http://example.com", TestName = "Validate throws: no header; value; url")]
    [TestCase(null, null, "http://example.com", TestName = "Validate throws: no header; no value; url")]
    [TestCase(Headers.TrialAuthHeader, "faketoken", BaseUrls.TokenBaseUrl, TestName = "Validate throws: trial header; value; token url")]
    [TestCase(Headers.TokenAuthHeader, "faketoken", BaseUrls.TrialBaseUrl, TestName = "Validate throws: token header; value; trial url")]
    public void Validate_Throws(string? tokenHeader, string? tokenValue, string? url)
    {
        // Arrange
        var config = new ElectricityMapsClientConfiguration();
        if (tokenHeader != null)
            config.APITokenHeader = tokenHeader;
        if (tokenValue != null)
            config.APIToken = tokenValue;
        if (url != null)
            config.BaseUrl = url;

        // Act & Assert
        Assert.Throws<ConfigurationException>(() => config.Validate());
    }
}