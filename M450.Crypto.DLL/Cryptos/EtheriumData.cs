using OpenQA.Selenium;

namespace M450.Crypto.DLL.Cryptos;

public class EtheriumData : ICryptoData
{
    private WebDriver driver => WebDriverManager.driver;

    public CryptoCurrency Currency => CryptoCurrency.ETH;

    public decimal GetCurrentPrice()
    {
        driver.Navigate().GoToUrl("https://www.blockchain.com/explorer/assets/eth");
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
        driver.Navigate().GoToUrl("https://www.blockchain.com/explorer/assets/eth");
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
        driver.Navigate().GoToUrl("https://www.blockchain.com/explorer/assets/eth");
        var element = driver.FindElement(By.XPath(
            "//*[@id=\"__next\"]/div[2]/div[2]/main/div/div/div[3]/section/div[2]/div/div/div/div[2]/div/div/div[3]/div/a[1]"));
        element.Click();
        element.FindElement(By.XPath(
            "//*[@id=\"__next\"]/div[2]/div[2]/main/div/div/div/div[1]/section[1]/div/div/div[2]/div[1]/div[16]/div[2]/div/div"));
        var value = element.Text;
        decimal fee = decimal.Parse(value);

        return fee;
    }
}