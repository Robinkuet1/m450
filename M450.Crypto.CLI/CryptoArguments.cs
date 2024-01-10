using CommandLine;
using M450.Crypto.DLL;


public class CryptoArguments
{
    [Option('c', Required = false, HelpText = "Crypto currency. Use crypto -l to get available currencies")]
    public CryptoCurrency? CryptoCurrency { get; set; }

    [Option('b', Required = false, HelpText = "Block")]
    public int BaseValue { get; set; }

    [Option('t', Required = false, HelpText = "Date")]
    public DateTime Date { get; set; }
    
    [Option('p', Required = false, HelpText = "")]
    public bool Price { get; set; }
    
    [Option('l', HelpText = "List")]
    public bool List { get; set; }
    
    [Option('v', Required = false, HelpText = "")]
    public bool Volume { get; set; }
}