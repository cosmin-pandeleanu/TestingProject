using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;

namespace TestingProject
{
    [TestClass]
    public class SearchTests
    {
        private IWebDriver driver;

        private By SearchBar = By.CssSelector("#HeaderRow > div.col-md-9.header-search-container-wrapper > div > div.col-md-7.col-lg-8.search-container.header-search-container.hidden-xs.hidden-sm > form > input.form-control.searchTerm.js-has-overlay");
        private IWebElement TxtSearchBar => driver.FindElement(SearchBar);

        private By Search = By.CssSelector("#HeaderRow > div.col-md-9.header-search-container-wrapper > div > div.col-md-7.col-lg-8.search-container.header-search-container.hidden-xs.hidden-sm > form > button");
        private IWebElement BtnSearch => driver.FindElement(Search);

        private By HeadingResult = By.CssSelector("#SortingRow > div.col-md-6.col-sm-4.hidden-xs > h1");
        private IWebElement TxtHeadingResult => driver.FindElement(HeadingResult);

        private By Reducere50 = By.XPath("//*[@id='DiscountLevel']/li[4]/a/span");
        private IWebElement BtnReducere50 => driver.FindElement(Reducere50);

        private By HeadingResultTest2 = By.CssSelector("#SortingRow > div.col-md-6.col-sm-4.hidden-xs > h1");
        private IWebElement TxtHeadingResultTest2 => driver.FindElement(HeadingResultTest2);

        [TestInitialize]
        public void TestInitialize()
        {
            driver = new ChromeDriver(); // open chrome browser
            driver.Manage().Window.Maximize(); // maximize the window
            driver.Navigate().GoToUrl("https://www.elefant.ro/login");

            // Click the button for cookies
            var btnCookie = driver.FindElement(By.Id("CybotCookiebotDialogBodyLevelButtonLevelOptinAllowAll"));
            btnCookie.Click();

            // implicit wait
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(1);
        }

        [TestMethod]
        public void User_Should_Search_Successfully()
        {
            TxtSearchBar.SendKeys("calend");
            BtnSearch.Click();
            Assert.IsTrue(TxtHeadingResult.Text.Contains("calend"));
        }

        [TestMethod]
        public void User_Should_Search_History_Books_With_50_Discount_Successfully()
        {
            TxtSearchBar.SendKeys("carti istorie");
            BtnSearch.Click();
            BtnReducere50.Click();
            Assert.IsTrue(TxtHeadingResultTest2.Text.Contains("carti istorie"));
        }

        [TestCleanup]
        public void TestCleanup()
        {
            driver.Quit();
        }
    }
}
