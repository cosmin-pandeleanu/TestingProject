using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;

namespace TestingProject
{
    [TestClass]
    public class LoginTests
    {
        private IWebDriver driver;

        [TestMethod]
        public void TestInitialize()
        {
            driver = new ChromeDriver(); // open chrome browser
            driver.Manage().Window.Maximize(); // maximize the window
            driver.Navigate().GoToUrl("OUR URL");
        }
    }
}
