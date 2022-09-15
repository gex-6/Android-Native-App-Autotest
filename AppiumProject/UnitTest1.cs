namespace AppiumProject;

public class Tests
{
    private AppiumDriver<AndroidElement> _driver;

    [SetUp]
    public void Setup()
    {
        // Platform, Device, Application

        var driverOption = new AppiumOptions();
        driverOption.AddAdditionalCapability(MobileCapabilityType.PlatformName, "android");
        driverOption.AddAdditionalCapability(MobileCapabilityType.PlatformVersion, "11");
        driverOption.AddAdditionalCapability(MobileCapabilityType.DeviceName, "Pixel_1");
        driverOption.AddAdditionalCapability(MobileCapabilityType.AutomationName, "UiAutomator2");
        driverOption.AddAdditionalCapability("AppPackage", "com.google.android.deskclock");
        driverOption.AddAdditionalCapability("AppActivity", "com.android.deskclock.DeskClock");

        driverOption.AddAdditionalCapability("chromedriverExecutable", @"/Users/tarasstepanyuk/My files");

        _driver = new AndroidDriver<AndroidElement>(new Uri("http://localhost:4723/wd/hub"), driverOption);

        var contexts = ((IContextAware)_driver).Contexts;
        string webviewContext = null;
        for(var i = 0; i < contexts.Count; i++)
        {
            Console.WriteLine(contexts[i]);
            if(contexts[i].Contains("WEBVIEW"))
            {
                webviewContext = contexts[i];
                break;
            }
        }
        ((IContextAware)_driver).Context = webviewContext;
    }

    [Test]
    public void Test1()
    {
        _driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(5);

        _driver.FindElementByXPath("//android.widget.TextView[@content-desc='Clock']").Click();
        _driver.FindElementByAccessibilityId("Timer").Click();
        _driver.FindElementById("com.google.android.deskclock:id/timer_setup_digit_4").Click();
        _driver.FindElementByAccessibilityId("Start").Click();

        var alarm = _driver.FindElementByXPath("//rk[@content-desc='Alarm']");

        Assert.That(alarm.Equals(_driver.FindElementByXPath("//rk[@content-desc='Alarm']")));
    }
}
