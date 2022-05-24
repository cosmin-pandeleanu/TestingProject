using TestingProject.PageObjects;
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
        private LoginPage loginPage;

        [TestInitialize]
        public void TestInitialize()
        {
            driver = new ChromeDriver(); // open chrome browser
            loginPage = new LoginPage(driver);

            driver.Manage().Window.Maximize(); // maximize the window
            driver.Navigate().GoToUrl("https://www.demoblaze.com/");

            var btnSignIn = driver.FindElement(By.Id("login2"));
            btnSignIn.Click();

            // implicit wait
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(1);
        }

        [TestMethod]
        public void User_Should_Login_Successfully()
        {
            loginPage.LoginApplication("TPS_proiect", "test");
            IWebElement NameOfUser = driver.FindElement(By.Id("nameofuser"));
            string NameOfUserText = NameOfUser.Text;
            Assert.IsTrue(NameOfUser.Text.Contains("TPS_proiect"));
        }

        [TestMethod]
        public void User_Should_Fail_Login_With_WrongEmail()
        {
            //loginPage.LoginApplication("test@test.testWrong", "test");
            //.AreEqual("Bad email or password.", loginPage.ErrorMessage);
        }

        [TestMethod]
        public void User_Should_Fail_Login_With_WrongPassword()
        {
            //loginPage.LoginApplication("test@test.test", "testWrong");
            //Assert.AreEqual("Bad email or password.", loginPage.ErrorMessage);
        }

        [TestCleanup]
        public void TestCleanup()
        {
            //driver.Quit();
        }
    }
}
