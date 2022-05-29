using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;

namespace TestingProject
{
    [TestClass]
    public class LogoutTests
    {
        private IWebDriver driver;

        private By Email = By.Id("ShopLoginForm_Login");
        private IWebElement TxtEmail => driver.FindElement(Email);

        private By Password = By.Id("ShopLoginForm_Password");
        private IWebElement TxtPassword => driver.FindElement(Password);

        private By Login = By.CssSelector("body > div.wrapper > div > div.row.account-login-page > div.col-sm-12.col-md-offset-1.col-md-10.col-lg-offset-2.col-lg-8 > div > div:nth-child(1) > div > form > div:nth-child(5) > div > button");
        private IWebElement BtnLogin => driver.FindElement(Login);

        private By IconAccount = By.XPath("//*[@id='HeaderRow']/div[4]/div/ul/li[1]/a[1]/div/span[2]");
        private IWebElement BtnIconAccount => driver.FindElement(IconAccount);

        private By Logout = By.CssSelector("#account-layer > a");
        private IWebElement BtnLogout => driver.FindElement(Logout);

        public void LoginApplication(string username, string password)
        {
            TxtEmail.SendKeys(username);
            TxtPassword.SendKeys(password);
            BtnLogin.Click();
        }

        [TestInitialize]
        public void TestInitialize()
        {
            driver = new ChromeDriver(); // Open chrome browser
            driver.Manage().Window.Maximize(); // Maximize the window
            driver.Navigate().GoToUrl("https://www.elefant.ro/login");

            // Click the button for cookies
            var btnCookie = driver.FindElement(By.Id("CybotCookiebotDialogBodyLevelButtonLevelOptinAllowAll"));
            btnCookie.Click();

            // Implicit wait
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(1);
        }

        [TestMethod]
        public void User_Should_Logout_Successfully()
        {
            LoginApplication("test016SM@outlook.com", "test00");
            BtnIconAccount.Click();
            BtnLogout.Click();
            IWebElement NameOfUser = driver.FindElement(By.XPath("//*[@id='HeaderRow']/div[4]/div/ul/li[1]/a[1]/span"));
            Assert.IsTrue(NameOfUser.Text != "TEST TEST");
        }

        [TestCleanup]
        public void TestCleanup()
        {
            driver.Quit();
        }
    }
}
