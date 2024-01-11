using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace M450.Crypto.DLL;

public static class WebDriverManager
{
    public static void Initialize()
    {
        //suppress log output and hide browser window
        var driverService = ChromeDriverService.CreateDefaultService();
        driverService.SuppressInitialDiagnosticInformation = true;
        
        var options = new ChromeOptions();
        options.AddArgument("ignore-certificate-errors");
        options.AddArgument("--ignore-ssl-errors=yes");
        options.AddArgument("--headless");

        driver = new ChromeDriver(driverService, options);
    }

    public static void Destroy()
    {
        driver.Quit();
    }

    public static WebDriver driver { get; private set; }
}