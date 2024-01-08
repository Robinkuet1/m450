using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Chromium;

namespace M450.Crypto.DLL;

public abstract class WebdriverManager
{
    protected ChromeDriver driver = new ChromeDriver();
}