using OpenQA.Selenium;
using TestingProject.Helpers;

namespace TestingProject.PageObjects
{
    public class HomePage
    {
        private IWebDriver driver;

        public HomePage(IWebDriver browser)
        {
            driver = browser;
        }
    }
}
