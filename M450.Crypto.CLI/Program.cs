using CommandLine;
using M450.Crypto.CLI;

class Program
{
    static void Main(string[] args)
    {
        CryptoApplication app = new ();

        Parser.Default.ParseArguments<CryptoArguments>(args)
            .WithParsed(app.Run);
    }
}