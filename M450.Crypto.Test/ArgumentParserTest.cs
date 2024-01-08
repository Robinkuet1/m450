using Xunit;
using CommandLine;
using System;

public class CryptoArgumentsTests
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
}