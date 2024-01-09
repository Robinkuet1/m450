using M450.Crypto.DLL.Cryptos;
using Moq;
using Xunit;

namespace M450.Crypto.CLI.Tests
{
    public class CryptoApplicationTest
    {
        [Fact]
        public void Run_GetPriceCommand_CallsGetCurrentPrice()
        {
            // Arrange
            var cryptoDataMock = new Mock<ICryptoData>();
            cryptoDataMock.Setup(x => x.Currency).Returns("BTC");
            cryptoDataMock.Setup(x => x.GetCurrentPrice()).Returns(1000);

            var cryptos = new List<ICryptoData> { cryptoDataMock.Object };
            var arguments = new CryptoArguments { Price = true, CryptoCurrency = "BTC" };

            var cryptoApp = new CryptoApplication(cryptos, new ConsoleWrapper());

            // Act
            cryptoApp.Run(arguments);

            // Assert
            cryptoDataMock.Verify(x => x.GetCurrentPrice(), Times.Once);
        }

        [Fact]
        public void Run_GetPriceCommand_CallsConsoleWriteLine()
        {
            // Arrange
            var cryptoCurrency = "BTC";
            var cryptoDataMock = new Mock<ICryptoData>();
            cryptoDataMock.Setup(x => x.Currency).Returns(cryptoCurrency);
            cryptoDataMock.Setup(x => x.GetCurrentPrice()).Returns(1000);

            var cryptos = new List<ICryptoData> { cryptoDataMock.Object };
            var arguments = new CryptoArguments { Price = true, CryptoCurrency = cryptoCurrency };

            var consoleMock = new Mock<IConsoleWrapper>();
            consoleMock.Setup(x => x.WriteLine(It.IsAny<string>()));

            var cryptoApp = new CryptoApplication(cryptos, consoleMock.Object);

            // Act
            cryptoApp.Run(arguments);

            // Assert
            consoleMock.Verify(x => x.WriteLine($"Price of {cryptoCurrency} is currently 1000$"), Times.Once);
        }

        [Fact]
        public void Run_GetPriceCommand_InvalidCurrency_CallsConsoleWriteLineWithError()
        {
            // Arrange
            var cryptoCurrency = "BTC";
            var cryptoDataMock = new Mock<ICryptoData>();
            cryptoDataMock.Setup(x => x.Currency).Returns("ETH");

            var cryptos = new List<ICryptoData> { cryptoDataMock.Object };
            var arguments = new CryptoArguments { Price = true, CryptoCurrency = cryptoCurrency };

            var consoleMock = new Mock<IConsoleWrapper>();
            consoleMock.Setup(x => x.WriteLine(It.IsAny<string>()));

            var cryptoApp = new CryptoApplication(cryptos, consoleMock.Object);

            // Act
            cryptoApp.Run(arguments);

            // Assert
            consoleMock.Verify(x => x.WriteLine($"Error: Currency: \"{cryptoCurrency}\" is not supported."), Times.Once);
        }

        [Fact]
        public void Run_ListCommand_CallsConsoleWriteLineForAvailableCurrencies()
        {
            // Arrange
            var cryptoDataMock = new Mock<ICryptoData>();
            cryptoDataMock.Setup(x => x.Currency).Returns("BTC");

            var cryptos = new List<ICryptoData> { cryptoDataMock.Object };

            var consoleMock = new Mock<IConsoleWrapper>();
            consoleMock.Setup(x => x.WriteLine(It.IsAny<string>()));

            var cryptoApp = new CryptoApplication(cryptos, consoleMock.Object);

            // Act
            cryptoApp.Run(new CryptoArguments { List = true });

            // Assert
            consoleMock.Verify(x => x.WriteLine("-Bitcoin"), Times.Once);
        }
    }
}