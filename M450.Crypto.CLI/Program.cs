using CommandLine;
using M450.Crypto.CLI;
using M450.Crypto.DLL.Cryptos;

class Program
{
    static void Main(string[] args)
    {
        //temporary hide chrome error messages
        List<ICryptoData> services = new List<ICryptoData>()
        {
            new BitcoinData(),
        };
        
        CryptoApplication app = new (services, new ConsoleWrapper());

        Parser.Default.ParseArguments<CryptoArguments>(args)
            .WithParsed(app.Run);
    }
}