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

        decimal price;
        if (arguments.Date != null)
        {
            price = service.GetPricePerDate((DateOnly)arguments.Date);
            if (price != -1)
                console.WriteLine(
                $"Price of 1 {arguments.CryptoCurrency} used to be approximately {price:N1}$ on {arguments.Date.Value.Day}.{arguments.Date.Value.Month}.{arguments.Date.Value.Year}");
        }
        else
        {
            price = service.GetCurrentPrice();
            if (price != -1)
                console.WriteLine($"Price of 1 {arguments.CryptoCurrency} is currently {price:N1}$");
        }
        
        if (arguments.OutFile is not ("" or null))
        {
            WriteToFile(arguments, price);
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

        var volume = service.GetTransactionVolume();
        Console.WriteLine($"24h Transaction Volume of {arguments.CryptoCurrency} is currently {volume:N1}$");

        if (arguments.OutFile is not ("" or null))
        {
            WriteToFile(arguments, volume);
        }
    }

    private void WriteToFile(CryptoArguments args, decimal value)
    {
        if (!File.Exists(args.OutFile)) File.WriteAllText(args.OutFile!,"{}");
        // Read the existing content
        string existingJson = File.ReadAllText(args.OutFile!);

        // Edit the content
        string editedFile = JsonExporter.GetJsonString((CryptoCurrency)args.CryptoCurrency!, args, value, existingJson);

        // Write the edited content back to the file
        File.WriteAllText(args.OutFile!, editedFile);
    }
    
    private void RunListCommand()
    {
        console.WriteLine("Available Crypto Currencies are:");
        foreach (var c in Enum.GetValues(typeof(CryptoCurrency)))
        {
            console.WriteLine($"\t-{c}");
        }
    }
}
