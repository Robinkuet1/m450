using CommandLine;
using M450.Crypto.CLI;
using M450.Crypto.DLL.Cryptos;

class Program
{
    static void Main(string[] args)
    {
        //temporary hide chrome error messages
        var currentConsole = Console.Out;
        Console.SetOut(TextWriter.Null);
        List<ICryptoData> services = new List<ICryptoData>()
        {
            new BitcoinData(),
        };
        Console.SetOut(currentConsole);
        
        CryptoApplication app = new (services, new ConsoleWrapper());

        Parser.Default.ParseArguments<CryptoArguments>(args)
            .WithParsed(app.Run);
    }
}