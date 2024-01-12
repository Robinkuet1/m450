using M450.Crypto.DLL;
using M450.Crypto.DLL.Cryptos;

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
            //only initialize webdriver when needed
            //hide console output during initialisation
            var currentConsole = Console.Out;
            Console.SetOut(TextWriter.Null);
            DLL.WebDriverManager.Initialize();
            Console.SetOut(currentConsole);

            if (arguments.Volume)
            {
                RunGetTransactionVolumeCommand(arguments);
            }

            if (arguments.Price)
            {
                RunGetPriceCommand(arguments);
            }
            
            DLL.WebDriverManager.Destroy();
        }
    }

    private void RunGetPriceCommand(CryptoArguments arguments)
    {
        var service = cryptos.Find(x => x.Currency == arguments.CryptoCurrency);
        if (service == null)
        {
            console.WriteError($"Error: Currency: \"{arguments.CryptoCurrency}\" is not implemented.");
            return;
        }
        console.WriteLine($"Price of {arguments.CryptoCurrency} is currently {service.GetCurrentPrice():N1}$");
    }
    private void RunGetTransactionVolumeCommand(CryptoArguments arguments)
    {
        var service = cryptos.Find(x => x.Currency == arguments.CryptoCurrency);
        if (service == null)
        {
            console.WriteError($"Error: Currency: \"{arguments.CryptoCurrency}\" is not implemented.");
            return;
        }
        Console.WriteLine($"24h Transaction Volume of {arguments.CryptoCurrency} is currently {service.GetTransactionVolume():N1}$");
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
