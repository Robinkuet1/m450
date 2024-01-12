using CommandLine;
using M450.Crypto.DLL;


public class CryptoArguments
{
    [Option('c', HelpText = "Crypto currency. Use crypto -l to get available currencies")]
    public CryptoCurrency? CryptoCurrency { get; set; }

    [Option('b', HelpText = "Block")] public int Block { get; set; }

    [Option('t', HelpText = "Date")] public DateTime Date { get; set; }

    [Option('p', HelpText = "Price")] public bool Price { get; set; }

    [Option('l', HelpText = "List")] public bool List { get; set; }

    [Option('v', Required = false, HelpText = "Volume")]
    public bool Volume { get; set; }

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
            if (this.CryptoCurrency != null) return true;
            Console.WriteLine("Argument Error: If \"-v\" is used a Cryptocurrency has to be provided with \"-c\"");
        }

        return false;
    }
}