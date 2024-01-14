using System.Collections.Generic;
using M450.Crypto.CLI;
using M450.Crypto.DLL;
using M450.Crypto.DLL.Cryptos;
using Moq;
using Xunit;

namespace M450.Crypto.Test
{
    public class CryptoApplicationTest
    {
        [Fact]
        public void Run_GetPriceCommand_CallsGetCurrentPrice()
        {
            // Arrange
            var cryptoDataMock = new Mock<ICryptoData>();
            cryptoDataMock.Setup(x => x.Currency).Returns(CryptoCurrency.BTC);
            cryptoDataMock.Setup(x => x.GetCurrentPrice()).Returns(1000);

            var cryptos = new List<ICryptoData> { cryptoDataMock.Object };
            var arguments = new CryptoArguments { Price = true, CryptoCurrency = CryptoCurrency.BTC };

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
            var cryptoCurrency = CryptoCurrency.BTC;
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
            consoleMock.Verify(x => x.WriteLine($"Price of {cryptoCurrency} is currently 1,000.0$"), Times.Once);
        }

        [Fact]
        public void Run_GetPriceCommand_InvalidCurrency_CallsConsoleWriteLineWithError()
        {
            // Arrange
            var cryptoCurrency = CryptoCurrency.SOL;
            var cryptoDataMock = new Mock<ICryptoData>();
            cryptoDataMock.Setup(x => x.Currency).Returns(CryptoCurrency.BTC);

            var cryptos = new List<ICryptoData> { cryptoDataMock.Object };
            var arguments = new CryptoArguments { Price = true, CryptoCurrency = cryptoCurrency };

            var consoleMock = new Mock<IConsoleWrapper>();
            consoleMock.Setup(x => x.WriteError(It.IsAny<string>()));

            var cryptoApp = new CryptoApplication(cryptos, consoleMock.Object);

            // Act
            cryptoApp.Run(arguments);

            // Assert
            consoleMock.Verify(x => x.WriteError("Error: Currency: \"SOL\" is not implemented."), Times.Once);
        }

        [Fact]
        public void Run_ListCommand_CallsConsoleWriteLineForAvailableCurrencies()
        {
            // Arrange
            var btcDataMock = new Mock<ICryptoData>();
            btcDataMock.Setup(x => x.Currency).Returns(CryptoCurrency.BTC);

            var ethDataMock = new Mock<ICryptoData>();
            btcDataMock.Setup(x => x.Currency).Returns(CryptoCurrency.ETH);
            
            var cryptos = new List<ICryptoData> { btcDataMock.Object, ethDataMock.Object };

            var consoleMock = new Mock<IConsoleWrapper>();
            consoleMock.Setup(x => x.WriteLine(It.IsAny<string>()));

            var cryptoApp = new CryptoApplication(cryptos, consoleMock.Object);

            // Act
            cryptoApp.Run(new CryptoArguments { List = true });

            // Assert
            consoleMock.Verify(x => x.WriteLine("Available Crypto Currencies are:"), Times.Once);
            consoleMock.Verify(x => x.WriteLine("\t-BTC"), Times.Once);
            consoleMock.Verify(x => x.WriteLine("\t-SOL"), Times.Once);
            consoleMock.Verify(x => x.WriteLine("\t-ETH"), Times.Once);
        }
    }
}