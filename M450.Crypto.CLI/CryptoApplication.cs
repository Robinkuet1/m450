using CommandLine;
using M450.Crypto.DLL.Cryptos;

namespace M450.Crypto.CLI;

public class CryptoApplication
{
    public CryptoApplication()
    {
        BitcoinData x = new();
        x.GetPrice();
    }
    
    public void Run(CryptoArguments arguments)
    {
        if (arguments.List)
        {
            RunListCommand();
        }
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