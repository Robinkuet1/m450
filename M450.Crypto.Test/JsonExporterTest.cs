using M450.Crypto.CLI;
using M450.Crypto.DLL;
using Xunit;

namespace M450.Crypto.Test;

public class JsonExporterTest
{
    [Fact]
    public void GetJsonString_Should_Return_Updated_JsonString()
    {
        // Arrange
        var currency = CryptoCurrency.BTC;
        var args = new CryptoArguments { Price = true };
        var value = 100.0m;
        var existingJson = "{\"BTC\":{\"price\":{\"2022-01-01\":50.0}}}";

        // Act
        var result = JsonExporter.GetJsonString(currency, args, value, existingJson);

        // Assert
        var expectedJson = "{\"BTC\":{\"price\":{\"2022-01-01\":50.0,\"15-01-2024\":100.0}}}";
        Assert.Equal(expectedJson, result);
    }

    [Fact]
    public void GetJsonString_Should_Return_Original_JsonString_When_Deserialization_Fails()
    {
        // Arrange
        var currency = CryptoCurrency.BTC;
        var args = new CryptoArguments { Price = true };
        var value = 100.0m;
        var existingJson = "InvalidJsonString";
        var expected = "Invalid JSON";

        // Act
        var result = JsonExporter.GetJsonString(currency, args, value, existingJson);

        // Assert
        Assert.Equal(expected, result);
    }

    [Fact]
    public void GetJsonString_GeneratingJsonEvenWhenCurrencyNotPresentYet()
    {
        // Arrange
        var currency = CryptoCurrency.ETH;
        var args = new CryptoArguments { Price = true };
        var value = 100.0m;
        var existingJson = "{\"BTC\":{\"price\":{\"2022-01-01\":\"50.0\"}}}";
        var expected = "{\"BTC\":{\"price\":{\"2022-01-01\":50.0}},\"ETH\":{\"price\":{\"15-01-2024\":100.0}}}";

        // Act
        var result = JsonExporter.GetJsonString(currency, args, value, existingJson);

        // Assert
        Assert.Equal(expected, result);
    }

    [Fact]
    public void GetJsonString_Should_Return_NewStringEvenWhenOperationNotFound()
    {
        // Arrange
        var currency = CryptoCurrency.BTC;
        var args = new CryptoArguments { Volume = true };
        var value = 100.0m;
        var existingJson = "{\"BTC\":{\"price\":{\"2022-01-01\":50.0}}}";
        var expected = "{\"BTC\":{\"price\":{\"2022-01-01\":50.0},\"volume\":{\"15-01-2024\":100.0}}}";

        // Act
        var result = JsonExporter.GetJsonString(currency, args, value, existingJson);

        // Assert
        Assert.Equal(expected, result);
    }

    [Fact]
    public void GetJsonString_Should_Return_Original_JsonString_When_Date_Not_Provided()
    {
        // Arrange
        var currency = CryptoCurrency.BTC;
        var args = new CryptoArguments { Price = true };
        var value = 100.0m;
        var existingJson = "{\"BTC\":{\"price\":{\"2022-01-01\":50.0}}}";
        var expected = "{\"BTC\":{\"price\":{\"2022-01-01\":50.0,\"15-01-2024\":100.0}}}";

        // Act
        var result = JsonExporter.GetJsonString(currency, args, value, existingJson);

        // Assert
        Assert.Equal(expected, result);
    }
}