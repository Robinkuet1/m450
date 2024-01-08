using OpenQA.Selenium.Chrome;

namespace M450.Crypto.DLL;

public abstract class WebdriverManager
{
    protected ChromeDriver driver = new();
}