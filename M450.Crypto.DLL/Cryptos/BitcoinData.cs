using OpenQA.Selenium;

namespace M450.Crypto.DLL.Cryptos;

public class BitcoinData : ICryptoData
{
    private WebDriver driver = WebDriverManager.driver;

    public BitcoinData() : base()
    {
        driver.Url = "https://www.blockchain.com/explorer/assets/btc";
    }

    public CryptoCurrency Currency => CryptoCurrency.BTC;

    public decimal GetCurrentPrice()
    {
        var element = driver.FindElement(By.ClassName("sc-bb87d037-10"));
        var value = element.Text.Replace("$", "").Replace(",", "");
        decimal price = decimal.Parse(value);
        return price;
    }

    public decimal GetTransactionVolume()
    {
        var element =
            driver.FindElement(By.XPath("//*[@id=\"__next\"]/div[2]/div[2]/main/div/div/div[4]/div[2]/div[3]/div[2]"));
        var value = element.Text;
        decimal price = decimal.Parse(value);

        return price;
    }

    public decimal GetTransactionFees(TimeSpan s)
    {
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