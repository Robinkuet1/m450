using OpenQA.Selenium;

namespace M450.Crypto.DLL.Cryptos;

public class SolanaData : ICryptoData
{
    private WebDriver driver => WebDriverManager.driver;

    public CryptoCurrency Currency => CryptoCurrency.BTC;

    public decimal GetCurrentPrice()
    {
        driver.Navigate().GoToUrl("https://www.blockchain.com/explorer/assets/sol");
        var element = driver.FindElement(By.ClassName("sc-bb87d037-10"));
        var value = element.Text.Replace("$", "").Replace(",", "");

        try
        {
            decimal price = decimal.Parse(value);
            return price;
        }
        catch (Exception e)
        {
            Console.WriteLine($"Internal Error: {e.ToString()}");
        }

        return -1;
    }

    public decimal GetTransactionVolume()
    {        
        driver.Navigate().GoToUrl("https://www.blockchain.com/explorer/assets/sol");
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
            Console.WriteLine($"Internal Error: {e.ToString()}");
        }

        return -1;
    }

    public decimal GetTransactionFees(TimeSpan s)
    {
        return null;
    }
}