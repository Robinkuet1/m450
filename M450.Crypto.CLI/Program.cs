﻿using CommandLine;
using M450.Crypto.DLL.Cryptos;

namespace M450.Crypto.CLI;

abstract class Program
{
    static void Main(string[] args)
    {
        List<ICryptoData> services = new List<ICryptoData>()
        {
            new BitcoinData(),
            new EthereumData(),
            new SolanaData()
        };

        CryptoApplication app = new(services, new ConsoleWrapper());
        Parser.Default.ParseArguments<CryptoArguments>(args)
            .WithParsed(parsedArgs =>
            {
                if (parsedArgs.Valid())
                {
                    app.Run(parsedArgs);
                }
            });
    }
}