using M450.Crypto.DLL;
using M450.Crypto.DLL.Cryptos;

namespace M450.Crypto.CLI;
public class CryptoApplication
{
    private readonly List<ICryptoData> _cryptos;
    private readonly IConsoleWrapper _console;

    public CryptoApplication(List<ICryptoData> cryptos, IConsoleWrapper console)
    {
        this._cryptos = cryptos;
        this._console = console;
    }

    public void Run(CryptoArguments arguments)
    {
        if (arguments.List)
        {
            RunListCommand();
        }
        else
        {
            DLL.WebDriverManager.Initialize();

            if (arguments.Volume)
            {
                RunGetTransactionVolumeCommand(arguments);
            }

            if (arguments.Price)
            {
                RunGetPriceCommand(arguments);
            }

            if (arguments.Fee)
            {
                RunGetTransactionFeesCommand(arguments);
            }
            DLL.WebDriverManager.Destroy();
        }
    }

    private void RunGetPriceCommand(CryptoArguments arguments)
    {
        var service = _cryptos.Find(x => x.Currency == arguments.CryptoCurrency);
        if (service == null)
        {
            _console.WriteError($"Error: Currency: \"{arguments.CryptoCurrency}\" is not implemented.");
            return;
        }

        decimal price;
        if (arguments.Date != null)
        {
            price = service.GetPricePerDate((DateOnly)arguments.Date);
            if (price != -1)
                _console.WriteLine(
                $"Price of 1 {arguments.CryptoCurrency} used to be approximately {price:N1}$ on {arguments.Date.Value.Day}.{arguments.Date.Value.Month}.{arguments.Date.Value.Year}");
        }
        else
        {
            price = service.GetCurrentPrice();
            if (price != -1)
                _console.WriteLine($"Price of 1 {arguments.CryptoCurrency} is currently {price:N1}$");
        }

        if (arguments.OutFile is not ("" or null))
        {
            WriteToFile(arguments, price);
        }
    }

    private void RunGetTransactionVolumeCommand(CryptoArguments arguments)
    {
        var service = _cryptos.Find(x => x.Currency == arguments.CryptoCurrency);
        if (service == null)
        {
            _console.WriteError($"Error: Currency: \"{arguments.CryptoCurrency}\" is not implemented.");
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
        if (!File.Exists(args.OutFile)) File.WriteAllText(args.OutFile!, "{}");
        string existingJson = File.ReadAllText(args.OutFile!);
        string editedFile = JsonExporter.GetJsonString((CryptoCurrency)args.CryptoCurrency!, args, value, existingJson);
        File.WriteAllText(args.OutFile!, editedFile);
    }

    private void RunGetTransactionFeesCommand(CryptoArguments arguments)
    {
        var service = _cryptos.Find(x => x.Currency == arguments.CryptoCurrency);
        if (service == null)
        {
            _console.WriteError($"Error: Currency: \"{arguments.CryptoCurrency}\" is not implemented.");
            return;
        }
        Console.WriteLine($"Transaction Fees of {arguments.CryptoCurrency} is currently {service.GetTransactionFees():N1}$");
    }

    private void RunListCommand()
    {
        _console.WriteLine("Available Crypto Currencies are:");
        foreach (var c in Enum.GetValues(typeof(CryptoCurrency)))
        {
            _console.WriteLine($"\t-{c}");
        }
    }
}
