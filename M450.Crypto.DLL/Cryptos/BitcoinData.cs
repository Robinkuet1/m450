using OpenQA.Selenium;

namespace M450.Crypto.DLL.Cryptos;

public class BitcoinData : ICryptoData
{
    private WebDriver driver => WebDriverManager.driver;

    public CryptoCurrency Currency => CryptoCurrency.BTC;

    public decimal GetCurrentPrice()
    {
        driver.Navigate().GoToUrl("https://www.blockchain.com/explorer/assets/btc");
        var element = driver.FindElement(By.ClassName("sc-bb87d037-10"));
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
        driver.Navigate().GoToUrl($"https://www.blockchain.com/explorer/blocks/btc/{CalculateBlockNumber(date)}");
        var found = driver.FindElements(By.XPath("/html/body/div/div[2]/div[2]/main/div/div/div[1]/h1"));
        if (found.Count != 0)
        {
            return -1;
        }

        try
        {
            var btcValue =
                decimal.Parse(driver.FindElement(By.XPath(
                        "//*[@id=\"__next\"]/div[2]/div[2]/main/div/div/div/div[1]/section[1]/div/div/div[2]/div[1]/div[5]/div[2]/div/div"))
                    .Text
                    .Replace(",", "").Replace("$", "")) /
                decimal.Parse(driver.FindElement(By.XPath(
                        "/html/body/div/div[2]/div[2]/main/div/div/div/div[1]/section[1]/div/div/div[2]/div[1]/div[9]/div[2]/div/div"))
                    .Text
                    .Replace(",", "").Replace(" BTC", ""));

            return btcValue;
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
        driver.Navigate().GoToUrl("https://www.blockchain.com/explorer/assets/btc");
        var element =
            driver.FindElement(By.XPath("//*[@id=\"__next\"]/div[2]/div[2]/main/div/div/div[4]/div[2]/div[3]/div[2]"));
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
        driver.Navigate().GoToUrl("https://www.blockchain.com/explorer/assets/btc");
        var element = driver.FindElement(By.XPath(
            "//*[@id=\"__next\"]/div[2]/div[2]/main/div/div/div[3]/section/div[2]/div/div/div/div[2]/div/div/div[3]/div/a[1]"));
        element.Click();
        element.FindElement(By.XPath(
            "//*[@id=\"__next\"]/div[2]/div[2]/main/div/div/div/div[1]/section[1]/div/div/div[2]/div[1]/div[16]/div[2]/div/div"));
        var value = element.Text;
        decimal fee = decimal.Parse(value);

        return fee;
    }
    
    private static long CalculateBlockNumber(DateOnly targetDate)
    {
        // Bitcoin started on January 3, 2009
        DateOnly genesisBlockDate = new(2009, 1, 3);
        
        // Calculate the number of days between the target date and the genesis block date
        double daysSinceGenesis = targetDate.DayNumber - genesisBlockDate.DayNumber;

        // Average block time is 10 minutes, so calculate blocks per day
        double blocksPerDay = 24.0 * 60.0 / 10.0 * 1.044762504807595;

        // Calculate the block number
        long blockNumber = (long)Math.Floor(daysSinceGenesis * blocksPerDay);

        if(blockNumber < 0) Console.WriteLine("Error: You can't get the price of Bitcoin before it existed.");
        return blockNumber;
    }
}