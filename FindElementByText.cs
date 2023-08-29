using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Remote;
using OpenQA.Selenium.Support.UI;

namespace FindElementByText
{
    public class FindElementByText
    {
        private static IWebDriver driver;
        public static string gridURL = "@hub.lambdatest.com/wd/hub";
        private static readonly string LT_USERNAME = Environment.GetEnvironmentVariable("LT_USERNAME");
        private static readonly string LT_ACCESS_KEY = Environment.GetEnvironmentVariable("LT_ACCESS_KEY");
        private static readonly string testURL = "https://www.lambdatest.com/selenium-playground/table-pagination-demo";

        [SetUp]
        public void Setup()
        {
            ChromeOptions capabilities = new ChromeOptions();
            capabilities.BrowserVersion = "114.0";
            Dictionary<string, object> ltOptions = new Dictionary<string, object>();
            ltOptions.Add("username", LT_USERNAME);
            ltOptions.Add("accessKey", LT_ACCESS_KEY);
            ltOptions.Add("platformName", "Windows 10");
            ltOptions.Add("build", "FindEmailInList");
            ltOptions.Add("project", "SelectElementByText");
            ltOptions.Add("w3c", true);
            ltOptions.Add("plugin", "c#-nunit");
            capabilities.AddAdditionalOption("LT:Options", ltOptions);
            driver = new RemoteWebDriver(new Uri($"https://{LT_USERNAME}:{LT_ACCESS_KEY}{gridURL}"), capabilities);
        }

        [Test]
        public void FindEmailOnPage()
        {
            driver.Navigate().GoToUrl(testURL);
            SelectElement dropDown = new SelectElement(driver.FindElement(By.XPath("//select[@class='form-control']")));
            dropDown.SelectByValue("5000");

            // Validate the element exists
            bool isEmailFound = driver.FindElement(By.XPath("//td[text()='r.smith@randatmail.com']")).Displayed;
            Assert.That(isEmailFound);

            // Validate the list of elements contains at least one web element
            var elements = driver.FindElements(By.XPath("//td[text()='r.smith@randatmail.com']"));
            Assert.That(elements.Count > 0);
        }

        [TearDown]
        public void CloseBrowser()
        {
            driver.Quit();
        }
    }
}