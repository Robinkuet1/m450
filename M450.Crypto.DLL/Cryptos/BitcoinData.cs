using OpenQA.Selenium;

namespace M450.Crypto.DLL.Cryptos;

public class BitcoinData : WebdriverManager, ICryptoData
{
    public BitcoinData() : base()
    {
        
    }
    
    public string Currency
    {
        get
        {
            return "BTC";
        }
    }

    public decimal GetCurrentPrice()
    {
        driver.Url = "https://www.blockchain.com/explorer/assets/btc";

        var element = driver.FindElement(By.ClassName("sc-bb87d037-10"));
        var value = element.Text.Replace("$", "").Replace(",","");
        decimal price = decimal.Parse(value);
        return price;
    }

    public decimal GetTransactionVolume(TimeSpan s)
    {
        throw new NotImplementedException();
    }

    public decimal GetTransactionFees(TimeSpan s)
    {
        throw new NotImplementedException();
    }
}