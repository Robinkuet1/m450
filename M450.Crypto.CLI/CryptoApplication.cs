using CommandLine;
using M450.Crypto.DLL;
using M450.Crypto.DLL.Cryptos;
using OpenQA.Selenium;

namespace M450.Crypto.CLI;
public class CryptoApplication
{
    private List<ICryptoData> cryptos;
    private IConsoleWrapper console;

    public CryptoApplication(List<ICryptoData> cryptos, IConsoleWrapper console)
    {
        this.cryptos = cryptos;
        this.console = console;
    }

    public void Run(CryptoArguments arguments)
    {
        if (arguments.List)
        {
            RunListCommand();
        }
        else
        {
            var currentConsole = Console.Out;
            Console.SetOut(TextWriter.Null);
            DLL.WebDriverManager.Initialize();
            Console.SetOut(currentConsole);
            
            RunGetPriceCommand(arguments);
            
            DLL.WebDriverManager.Destroy();
        }
    }

    private void RunGetPriceCommand(CryptoArguments arguments)
    {
        var service = cryptos.Find(x => x.Currency == arguments.CryptoCurrency);
        if (service == null)
        {
            console.WriteError($"Error: Currency: \"{arguments.CryptoCurrency}\" is not supported.");
            return;
        }
        console.WriteLine($"Price of {arguments.CryptoCurrency} is currently {service.GetCurrentPrice()}$");
    }
    private void RunGetTransactionVolumeCommand(CryptoArguments arguments)
    {
        var service = cryptos.Find(x => x.Currency == arguments.CryptoCurrency);
        if (service == null)
        {
            Console.WriteLine($"Error: Currency: \"{arguments.CryptoCurrency}\" is not supported.");
            return;
        }
        Console.WriteLine($"Price of {arguments.CryptoCurrency} is currently {service.GetTransactionVolume()}$");
    }
    
    private void RunListCommand()
    {
        console.WriteLine("Available Crypto Currencies are:");
        foreach (var c in Enum.GetValues(typeof(CryptoCurrency)))
        {
            console.WriteLine($"\t-{c.ToString()}");
        }
    }
}
