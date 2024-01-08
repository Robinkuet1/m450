using CommandLine;
using M450.Crypto.DLL.Cryptos;

namespace M450.Crypto.CLI;

public class CryptoApplication
{
    private List<ICryptoData> cryptos = new List<ICryptoData>();
    public CryptoApplication()
    {
        cryptos.Add(new BitcoinData());
    }
    
    public void Run(CryptoArguments arguments)
    {
        if (arguments.List)
        {
            RunListCommand();
        }
        else if (arguments.Price)
        {
            RunGetPriceCommand(arguments);  
        }
    }

    private void RunGetPriceCommand(CryptoArguments arguments)
    {
        var service = cryptos.Find(x => x.Currency == arguments.CryptoCurrency);
        if (service == null)
        {
            Console.WriteLine($"Error: Currency: \"{arguments.CryptoCurrency}\" is not supported.");
            return;
        }
        Console.WriteLine($"Price of {arguments.CryptoCurrency} is currently {service.GetCurrentPrice()}$");
    }
    
    private void RunListCommand()
    {
        Console.WriteLine("Available Crypto Currencies are:");
        foreach(var c in Enum.GetValues(typeof(CryptoCurrencies)))
        {
            Console.WriteLine($"\t-{c.ToString()}");
        }
    }
}