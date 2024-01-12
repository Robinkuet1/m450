using M450.Crypto.DLL;
using Xunit;

namespace M450.Crypto.Test;

public class CryptoArgumentValidatorTest
{
    [Fact]
    public void Valid_ReturnsTrue_WhenListIsTrue()
    {
        // Arrange
        var arguments = new CryptoArguments { List = true };

        // Act
        bool isValid = arguments.Valid();

        // Assert
        Assert.True(isValid);
    }

    [Fact]
    public void Valid_ReturnsTrue_WhenPriceIsTrueAndCryptoCurrencyIsNotNull()
    {
        // Arrange
        var arguments = new CryptoArguments { Price = true, CryptoCurrency = CryptoCurrency.BTC };

        // Act
        bool isValid = arguments.Valid();

        // Assert
        Assert.True(isValid);
    }

    [Fact]
    public void Valid_ReturnsFalse_WhenPriceIsTrueAndCryptoCurrencyIsNull()
    {
        // Arrange
        var arguments = new CryptoArguments { Price = true };

        // Act
        bool isValid = arguments.Valid();

        // Assert
        Assert.False(isValid);
    }

    [Fact]
    public void Valid_ReturnsTrue_WhenVolumeIsTrueAndCryptoCurrencyIsNotNull()
    {
        // Arrange
        var arguments = new CryptoArguments { Volume = true, CryptoCurrency = CryptoCurrency.SOL };

        // Act
        bool isValid = arguments.Valid();

        // Assert
        Assert.True(isValid);
    }

    [Fact]
    public void Valid_ReturnsFalse_WhenVolumeIsTrueAndCryptoCurrencyIsNull()
    {
        // Arrange
        var arguments = new CryptoArguments { Volume = true };

        // Act
        bool isValid = arguments.Valid();

        // Assert
        Assert.False(isValid);
    }

    [Fact]
    public void Valid_ReturnsFalse_WhenNeitherListNorPriceNorVolumeIsTrue()
    {
        // Arrange
        var arguments = new CryptoArguments();

        // Act
        bool isValid = arguments.Valid();

        // Assert
        Assert.False(isValid);
    }

    [Fact]
    public void Valid_ReturnsFalse_WhenPriceAndVolumeAreTrueButCryptoCurrencyIsNull()
    {
        // Arrange
        var arguments = new CryptoArguments { Price = true, Volume = true };

        // Act
        bool isValid = arguments.Valid();

        // Assert
        Assert.False(isValid);
    }

    [Fact]
    public void Valid_ReturnsTrue_WhenPriceAndVolumeAreTrueAndCryptoCurrencyIsNotNull()
    {
        // Arrange
        var arguments = new CryptoArguments { Price = true, Volume = true, CryptoCurrency = CryptoCurrency.ETH };

        // Act
        bool isValid = arguments.Valid();

        // Assert
        Assert.True(isValid);
    }
}