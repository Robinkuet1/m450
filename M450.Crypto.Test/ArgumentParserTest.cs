using System;
using CommandLine;
using M450.Crypto.CLI;
using M450.Crypto.DLL;
using Xunit;

namespace M450.Crypto.Test;

public class CryptoArgumentsTest
{
    [Fact]
    public void ParseArguments_ListOption_ParsesSuccessfully()
    {
        string[] args = { "crypto", "-l" };
        CryptoArguments arguments = null;

        Parser.Default.ParseArguments<CryptoArguments>(args)
            .WithParsed(parsedArguments => arguments = parsedArguments);

        Assert.NotNull(arguments);
        Assert.True(arguments.List);
    }

    [Fact]
    public void ParseArguments_InvalidArguments_ThrowsError()
    {
        string[] args = { "crypto", "-c", "BTC", "-b", "abc", "-t", "14.12.2010" };
        CryptoArguments arguments = null;

        Parser.Default.ParseArguments<CryptoArguments>(args)
            .WithNotParsed(errors => arguments = null);

        Assert.Null(arguments);
    }

    [Fact]
    public void ParseArguments_NoOptions_DefaultValuesSet()
    {
        string[] args = { "crypto" };
        CryptoArguments arguments = null;

        Parser.Default.ParseArguments<CryptoArguments>(args)
            .WithParsed(parsedArguments => arguments = parsedArguments);

        Assert.NotNull(arguments);
        Assert.Null(arguments!.CryptoCurrency);
        Assert.Equal(default(DateOnly), arguments.Date);
        Assert.False(arguments.Price);
        Assert.False(arguments.List);
        Assert.False(arguments.Volume);
    }

    [Fact]
    public void ParseArguments_InvalidBaseValue_ThrowsError()
    {
        string[] args = { "crypto", "-c", "BTC", "-b", "abc" };
        CryptoArguments arguments = null;

        Parser.Default.ParseArguments<CryptoArguments>(args)
            .WithNotParsed(errors => arguments = null);

        Assert.Null(arguments);
    }

    [Fact]
    public void ParseArguments_ValidDate_ParsesSuccessfully()
    {
        string[] args = { "crypto", "-c", "BTC", "-t", "2021-10-25" };
        CryptoArguments arguments = null;

        Parser.Default.ParseArguments<CryptoArguments>(args)
            .WithParsed(parsedArguments => arguments = parsedArguments);

        Assert.NotNull(arguments);
        Assert.Equal(CryptoCurrency.BTC, arguments.CryptoCurrency);
        Assert.Equal(new (2021, 10, 25), arguments.Date);
        Assert.False(arguments.Price);
        Assert.False(arguments.List);
        Assert.False(arguments.Volume);
    }
}