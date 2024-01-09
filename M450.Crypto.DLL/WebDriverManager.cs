using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace M450.Crypto.DLL;

public static class WebDriverManager
{
    static WebDriverManager()
    {
        //suppress log output and hide browser window
        var driverService = ChromeDriverService.CreateDefaultService();
        driverService.SuppressInitialDiagnosticInformation = true;
        
        var options = new ChromeOptions();
        options.AddArgument("--headless");

        driver = new ChromeDriver(driverService, options);
    }

    public static WebDriver driver { get; }
}