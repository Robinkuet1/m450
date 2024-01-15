using OpenQA.Selenium;

namespace M450.Crypto.DLL.Cryptos;

public class EthereumData : ICryptoData
{
    private static WebDriver Driver => WebDriverManager.driver;

    public CryptoCurrency Currency => CryptoCurrency.ETH;

    public decimal GetCurrentPrice()
    {
        Driver.Navigate().GoToUrl("https://www.blockchain.com/explorer/assets/eth");
        var element = Driver.FindElement(By.ClassName("sc-bb87d037-10"));
        var value = element.Text.Replace("$", "").Replace(",", "");

        try
        {
            decimal price = decimal.Parse(value);
            return price;
        }
        catch (Exception e)
        {
            Console.WriteLine($"Internal Error: {e}");
        }

        return -1;
    }

    public decimal GetPricePerDate(DateOnly date)
    {
        Driver.Navigate().GoToUrl($"https://www.blockchain.com/explorer/blocks/eth/{CalculateBlockNumber(date)}");
        var found = Driver.FindElements(By.XPath("/html/body/div/div[2]/div[2]/main/div/div/div[1]/h1"));
        if (found.Count != 0)
        {
            return -1;
        }

        try
        {
            var ethValue =
                decimal.Parse(Driver.FindElement(By.XPath(
                        "//*[@id=\"__next\"]/div[2]/div[2]/main/div/div/div/div[1]/section[1]/div/div/div[2]/div[2]/div[6]/div[2]/div/div"))
                    .Text
                    .Replace(",", "").Replace("USD", "")) /
                decimal.Parse(Driver.FindElement(By.XPath(
                        "//*[@id=\"__next\"]/div[2]/div[2]/main/div/div/div/div[1]/section[1]/div/div/div[2]/div[2]/div[5]/div[2]/div/div"))
                    .Text);

            return ethValue;
        }
        catch (DivideByZeroException)
        {
            Console.WriteLine("Error: This date isn't available to get price from. Try a later date");
        }
        catch (Exception e)
        {
            Console.WriteLine($"Internal Error: {e}");
        }

        return -1;
    }

    public decimal GetTransactionVolume()
    {
        Driver.Navigate().GoToUrl("https://www.blockchain.com/explorer/assets/eth");
        var element =
            Driver.FindElement(By.XPath("//*[@id=\"__next\"]/div[2]/div[2]/main/div/div/div[4]/div[2]/div[3]/div[2]"));
        var value = element.Text.Replace("$", "").Replace(",", "");
        try
        {
            decimal volume = decimal.Parse(value);
            return volume;
        }
        catch (Exception e)
        {
            Console.WriteLine($"Internal Error: {e}");
        }

        return -1;
    }

    public decimal GetTransactionFees()
    {
        Driver.Navigate().GoToUrl("https://www.blockchain.com/explorer/assets/eth");

        var blockNumberElement = Driver.FindElement(By.XPath("//*[@id=\"__next\"]/div[2]/div[2]/main/div/div/div[3]/section/div[2]/div/div/div/div[2]/div/div/div[3]/div/a[2]/div/div/div[2]"));
        var blockNumber = blockNumberElement.Text.Replace("#", "");

        Driver.Navigate().GoToUrl($"https://www.blockchain.com/explorer/blocks/eth/{blockNumber}");

        var usdValueElement = Driver.FindElement(By.XPath("//*[@id=\"__next\"]/div[2]/div[2]/main/div/div/div/div[1]/section[1]/div/div/div[2]/div[2]/div[16]/div[2]/div/div"));
        var usdValue = usdValueElement.Text.Replace(" USD", "").Replace(",", ".");

        var transactionsElement = usdValueElement.FindElement(By.XPath("//*[@id=\"__next\"]/div[2]/div[2]/main/div/div/div/div[1]/section[1]/div/div/div[2]/div[2]/div[4]/div[2]/div/div"));
        var transactions = transactionsElement.Text;
        try
        {
            decimal fee = decimal.Parse(usdValue);
            decimal trans = decimal.Parse(transactions);
            return fee / trans;
        }
        catch (Exception e)
        {
            Console.WriteLine($"Internal Error: {e}");
        }

        return -1;
    }

    static long CalculateBlockNumber(DateOnly targetDate)
    {
        // Ethereum started on July 30, 2015
        DateOnly genesisBlockDate = new DateOnly(2015, 7, 30);

        // Calculate the number of seconds between the target date and the genesis block date
        long secondsSinceGenesis = (targetDate.DayNumber - genesisBlockDate.DayNumber) * 3600 * 24;

        // Average block time is around 13-15 seconds
        double averageBlockTime = 13.5;

        // Calculate blocks per second
        double blocksPerSecond = 1 / averageBlockTime;

        // Calculate the block number
        long blockNumber = (long)(secondsSinceGenesis * blocksPerSecond);

        if (blockNumber < 0) Console.WriteLine("Error: You can't get the price of Ethereum before it existed.");
        return blockNumber;
    }
}