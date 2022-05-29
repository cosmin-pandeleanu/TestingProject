using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;
using System.Threading;

namespace TestingProject
{
    [TestClass]
    public class CartTests
    {
        private IWebDriver driver;

        private By SearchBar = By.CssSelector("#HeaderRow > div.col-md-9.header-search-container-wrapper > div > div.col-md-7.col-lg-8.search-container.header-search-container.hidden-xs.hidden-sm > form > input.form-control.searchTerm.js-has-overlay");
        private IWebElement TxtSearchBar => driver.FindElement(SearchBar);

        private By Search = By.CssSelector("#HeaderRow > div.col-md-9.header-search-container-wrapper > div > div.col-md-7.col-lg-8.search-container.header-search-container.hidden-xs.hidden-sm > form > button");
        private IWebElement BtnSearch => driver.FindElement(Search);

        private By FirstBook = By.XPath("/html/body/div[2]/div/div[2]/div[2]/div[2]/div[1]/div[1]/div/div/div[1]/a/img");
        private IWebElement BtnFirstBook => driver.FindElement(FirstBook);

        private By AddCart = By.XPath("/html/body/div[2]/div/div[10]/div[1]/div[3]/div/div[3]/div[2]/form/div/div[2]/button");
        private IWebElement BtnAddCart => driver.FindElement(AddCart);

        private By IconCart = By.XPath("/html/body/header/div[1]/nav/div/div[4]/div/ul/li[3]/div/a/div");
        private IWebElement BtnIconCart => driver.FindElement(IconCart);

        private By ShowCart = By.XPath("/html/body/header/div[1]/nav/div/div[4]/div/ul/li[3]/div/div[2]/div[3]/a");
        private IWebElement BtnShowCart => driver.FindElement(ShowCart);

        private By MyCart = By.XPath("/html/body/div[2]/div/div[11]/div/div[1]/div[1]/h1/span");
        private IWebElement LblMyCart => driver.FindElement(MyCart);

        private By Delete = By.XPath("/html/body/div[2]/div/div[11]/div/div[2]/form/div/div[1]/div[1]/div/div[2]/a/i");
        private IWebElement BtnDelete => driver.FindElement(Delete);

        private By EmtpyCart = By.XPath("/html/body/div[2]/div/div[10]/h2");
        private IWebElement LblEmptyCart => driver.FindElement(EmtpyCart);


        [TestInitialize]
        public void TestInitialize()
        {
            driver = new ChromeDriver(); // open chrome browser
            driver.Manage().Window.Maximize(); // maximize the window
            driver.Navigate().GoToUrl("https://www.elefant.ro/login");

            // Click the button for cookies
            var btnCookie = driver.FindElement(By.Id("CybotCookiebotDialogBodyLevelButtonLevelOptinAllowAll"));
            btnCookie.Click();

            // Implicit wait
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(1);
        }
        [TestMethod]
        public void User_Should_Add_A_Book_In_Cart_Successfully()
        {
            // Search the book
            TxtSearchBar.SendKeys("carti");
            BtnSearch.Click();
            Thread.Sleep(2000);
            BtnFirstBook.Click();
            // Add to the cart
            BtnAddCart.Click();
            BtnIconCart.Click();
            BtnShowCart.Click();
            Assert.IsTrue(LblMyCart.Text.Contains("COSUL TAU"));
        }

        [TestMethod]
        public void User_Should_Delete_A_Book_From_Cart_Successfully()
        {
            // Search the book
            TxtSearchBar.SendKeys("carti");
            BtnSearch.Click();
            BtnFirstBook.Click();
            // Add to the cart
            BtnAddCart.Click();
            BtnIconCart.Click();
            BtnShowCart.Click();
            // Delete the book
            BtnDelete.Click();
            Assert.IsTrue(LblEmptyCart.Text.Contains("NICI UN PRODUS"));
        }

        [TestCleanup]
        public void TestCleanup()
        {
            driver.Quit();
        }
    }
}
