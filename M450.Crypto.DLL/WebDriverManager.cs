using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace M450.Crypto.DLL;

public static class WebDriverManager
{
    public static void Initialize()
    {
        var currentConsole = Console.Out;
        Console.SetOut(TextWriter.Null);

        var driverService = ChromeDriverService.CreateDefaultService();
        driverService.SuppressInitialDiagnosticInformation = true;

        var options = new ChromeOptions();
        options.AddArgument("ignore-certificate-errors");
        options.AddArgument("--ignore-ssl-errors=yes");
        options.AddArgument("--headless");

        _driver = new ChromeDriver(driverService, options);

        Console.SetOut(currentConsole);
    }


    public static void Destroy()
    {
        driver.Quit();
    }

    private static WebDriver? _driver;
    public static WebDriver driver
    {
        get
        {
            return _driver!;
        }
        private set
        {
            _driver = value;
        }
    }
}
