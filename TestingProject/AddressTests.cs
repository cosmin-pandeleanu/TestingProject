using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using System;
using TestingProject.InputDataBO;

namespace TestingProject
{

    [TestClass]
    public class AddressTests
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

        private By AccountAddress = By.CssSelector("#account-layer > ul > li.account-navigation-list-addresses > a");
        private IWebElement BtnAccountAddress => driver.FindElement(AccountAddress);

        private By EditAddress = By.XPath("/html/body/div[2]/div/div[9]/div/div[3]/div[6]/div/div[1]/div[1]/div[1]/div[1]/div[2]/a[2]");
        public IWebElement BtnEditAddress => driver.FindElement(EditAddress);

        private By NewAddress = By.CssSelector("body > div.wrapper > div > div.account-wrapper > div > div.col-md-9.account-content > div.my-account-address-list.shift-content > div > div:nth-child(1) > a");
        private IWebElement BtnNewAddress => driver.FindElement(NewAddress);

        private By DeleteAddress = By.XPath("/html/body/div[2]/div/div[9]/div/div[3]/div[6]/div/div[1]/div[1]/div[1]/div[1]/div[2]/a[1]");
        private IWebElement BtnDeleteAddress => driver.FindElement(DeleteAddress);

        private By DeleteConfirmation = By.CssSelector("body > div.modal.fade.in > div > div > form > div.modal-footer > button");
        private IWebElement BtnDeleteConfrimation => driver.FindElement(DeleteConfirmation);
        // Add / Edit Form
        private By LastName = By.XPath("/html/body/div[3]/div/div/form/div[1]/div[1]/div/div/fieldset[1]/div[1]/div/div/input");
        private IWebElement TxtLastName => driver.FindElement(LastName);

        private By FirstName = By.XPath("/html/body/div[3]/div/div/form/div[1]/div[1]/div/div/fieldset[1]/div[2]/div/div/input");
        private IWebElement TxtFirstName => driver.FindElement(FirstName);

        private By PhoneNumber = By.XPath("/html/body/div[3]/div/div/form/div[1]/div[1]/div/div/fieldset[1]/div[3]/div/div/input");
        private IWebElement TxtPhoneNumber => driver.FindElement(PhoneNumber);

        private By Country = By.XPath("/html/body/div[3]/div/div/form/div[1]/div[1]/div/div/fieldset[2]/div[1]/div/div/select");
        private IWebElement DdlCountry => driver.FindElement(Country);

        private By County = By.XPath("/html/body/div[3]/div/div/form/div[1]/div[1]/div/div/fieldset[2]/div[2]/div/div/select");
        private IWebElement DdlCounty => driver.FindElement(County);

        private By City = By.XPath("/html/body/div[3]/div/div/form/div[1]/div[1]/div/div/fieldset[2]/div[3]/div/div/div/select");
        private IWebElement DdlCity => driver.FindElement(City);

        private By Address = By.XPath("/html/body/div[3]/div/div/form/div[1]/div[1]/div/div/fieldset[3]/div/div/div/input");
        private IWebElement TxtAddress => driver.FindElement(Address);

        private By Save = By.CssSelector("body > div.modal.fade.in > div > div > form > div.modal-footer > button.btn.btn-primary");
        private IWebElement BtnSave => driver.FindElement(Save);

        public void LoginApplication(string username, string password)
        {
            TxtEmail.SendKeys(username);
            TxtPassword.SendKeys(password);
            BtnLogin.Click();
        }

        [TestInitialize]
        public void TestInitialize()
        {
            driver = new ChromeDriver();
            driver.Manage().Window.Maximize();
            driver.Navigate().GoToUrl("https://www.elefant.ro/login");

            // Click the button for cookies
            var btnCookie = driver.FindElement(By.Id("CybotCookiebotDialogBodyLevelButtonLevelOptinAllowAll"));
            btnCookie.Click();

            // Implicit wait
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(1);

            LoginApplication("test016SM@outlook.com", "test00");
        }

        [TestMethod]
        public void User_Should_Add_Address_Successfully()
        {
            var inputData = new AddAddressBO
            {
                LastName = "Popa",
                FirstName = "Ion",
                PhoneNumber = "0712345678",
                Country = "Romania",
                County = "Iasi",
                City = "Iasi",
                Address = "Tudor Vladimirescu 145"
            };

            BtnIconAccount.Click();
            BtnAccountAddress.Click();
            BtnNewAddress.Click();
            // Fill Form
            TxtLastName.Clear();
            TxtLastName.SendKeys(inputData.LastName);
            TxtFirstName.Clear();
            TxtFirstName.SendKeys(inputData.FirstName);
            TxtPhoneNumber.Clear();
            TxtPhoneNumber.SendKeys(inputData.PhoneNumber);
            TxtAddress.Clear();
            TxtAddress.SendKeys(inputData.Address);
            var selectCountry = new SelectElement(DdlCountry);
            selectCountry.SelectByText(inputData.Country);
            var selectCounty = new SelectElement(DdlCounty);
            selectCounty.SelectByText(inputData.County);
            var selectCity = new SelectElement(DdlCity);
            selectCity.SelectByText(inputData.City);
            // Save
            BtnSave.Click();
        }

        [TestMethod]
        public void User_Should_Edit_Address_Successfully()
        {
            var inputData = new AddAddressBO
            {
                LastName = "Florea",
                FirstName = "Ionel",
                PhoneNumber = "0789525353",
                Address = "Tudor Vladimirescu 145"
            };
            BtnIconAccount.Click();
            BtnAccountAddress.Click();
            BtnEditAddress.Click();
            TxtLastName.Clear();
            TxtLastName.SendKeys(inputData.LastName);
            TxtFirstName.Clear();
            TxtFirstName.SendKeys(inputData.FirstName);
            TxtPhoneNumber.Clear();
            TxtPhoneNumber.SendKeys(inputData.PhoneNumber);
            TxtAddress.Clear();
            TxtAddress.SendKeys(inputData.Address);
            BtnSave.Click();
        }

        [TestMethod]
        public void User_Should_Delete_Address_Successfully()
        {
            BtnIconAccount.Click();
            BtnAccountAddress.Click();
            BtnDeleteAddress.Click();
            BtnDeleteConfrimation.Click();
        }

        [TestCleanup]
        public void TestCleanup()
        {
           driver.Quit();
        }
    }
}
