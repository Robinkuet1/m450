using System.Globalization;
using CommandLine;
using M450.Crypto.DLL;

namespace M450.Crypto.CLI;

public class CryptoArguments
{
    [Option('c', HelpText = "Crypto currency. Use crypto -l to get available currencies")]
    public CryptoCurrency? CryptoCurrency { get; set; }

    [Option('t', HelpText = "Approximate Date to get the price from")] public string? DateTime { get; set; }

    public DateOnly? Date => DateTime == null ? null : DateOnly.Parse(DateTime, new CultureInfo("de-CH"));

    [Option('p', HelpText = "Price")] public bool Price { get; set; }

    [Option('l', HelpText = "List")] public bool List { get; set; }

    [Option('v', Required = false, HelpText = "Volume")]
    public bool Volume { get; set; }

    [Option('o', HelpText = "Output file path")]
    public string? OutFile { get; set; }
    
    public bool Valid()
    {
        //if -l is used we dont need anything else
        if (this.List) return true;

        //if -p is used we need the crypto currency
        if (this.Price)
        {
            if (this.CryptoCurrency != null) return true;
            Console.WriteLine("Argument Error: If \"-p\" is used a Cryptocurrency has to be provided with \"-c\"");
        }

        if (this.Volume)
        {
            if (this.Date != null)
            {
                Console.WriteLine("Argument Error: Option -t is not supported with Option -v");
                return false;
            } 
            if (this.CryptoCurrency != null) return true;
            Console.WriteLine("Argument Error: If \"-v\" is used a Cryptocurrency has to be provided with \"-c\"");
        }

        Console.WriteLine("Use --help to get all available commands.");
        return false;
    }
}