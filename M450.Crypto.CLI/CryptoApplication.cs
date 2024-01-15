using M450.Crypto.DLL;
using M450.Crypto.DLL.Cryptos;

namespace M450.Crypto.CLI;
public class CryptoApplication
{
    private readonly List<ICryptoData> cryptos;
    private readonly IConsoleWrapper console;

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
            DLL.WebDriverManager.Initialize();

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

        if (arguments.Date != null)
        {
            var price = service.GetPricePerDate((DateOnly)arguments.Date);
            if (price != -1)
                console.WriteLine(
                $"Price of 1 {arguments.CryptoCurrency} used to be approximately {price:N1}$ on {arguments.Date.Value.Day}.{arguments.Date.Value.Month}.{arguments.Date.Value.Year}");
        }
        else
        {
            var price = service.GetCurrentPrice();
            if (price != -1)
                console.WriteLine($"Price of 1 {arguments.CryptoCurrency} is currently {price:N1}$");
        }
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
