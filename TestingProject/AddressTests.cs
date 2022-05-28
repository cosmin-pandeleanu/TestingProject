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

        private By EditAddress = By.XPath("/html/body/div[2]/div/div[9]/div/div[3]/div[6]/div/div[1]/div[1]/div/div[1]/div[2]/a");
        public IWebElement BtnEditAddress => driver.FindElement(EditAddress);
        private By NewAddress = By.CssSelector("body > div.wrapper > div > div.account-wrapper > div > div.col-md-9.account-content > div.my-account-address-list.shift-content > div > div:nth-child(1) > a");
        private IWebElement BtnNewAddress => driver.FindElement(NewAddress);

        private By DeleteAddress = By.XPath("/html/body/div[2]/div/div[9]/div/div[3]/div[6]/div/div[1]/div[1]/div[2]/div[1]/div[2]/a[1]");
        private IWebElement BtnDeleteAddress => driver.FindElement(DeleteAddress);

        private By DeleteConfirmation = By.CssSelector("body > div.modal.fade.in > div > div > form > div.modal-footer > button");
        private IWebElement BtnDeleteConfrimation => driver.FindElement(DeleteConfirmation);
        // Add / Edit Form
        private By LastName = By.Id("address_LastName");
        private IWebElement TxtLastName => driver.FindElement(LastName);

        private By FirstName = By.Id("address_FirstName");
        private IWebElement TxtFirstName => driver.FindElement(FirstName);

        private By PhoneNumber = By.Id("address_PhoneHome");
        private IWebElement TxtPhoneNumber => driver.FindElement(PhoneNumber);

        private By Country = By.Id("address_CountryCode_M7kKAQNo7osAAAGBIJglWgq0");
        private IWebElement DdlCountry => driver.FindElement(Country);

        private By County = By.Id("address_State");
        private IWebElement DdlCounty => driver.FindElement(County);

        private By City = By.Id("address_City");
        private IWebElement DdlCity => driver.FindElement(City);

        private By Address = By.Id("address_Address1");
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

            // implicit wait
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(1);

            LoginApplication("test016SM@outlook.com", "test00");
        }

        public void Fill_Form_Address(AddAddressBO inputData)
        {
            TxtLastName.Clear();
            TxtLastName.SendKeys(inputData.LastName);
            TxtFirstName.Clear();
            TxtFirstName.SendKeys(inputData.FirstName);
            TxtPhoneNumber.Clear();
            TxtPhoneNumber.SendKeys(inputData.FirstName);
            TxtAddress.Clear();
            TxtAddress.SendKeys(inputData.Address);
            var selectCountry = new SelectElement(DdlCountry);
            selectCountry.SelectByText(inputData.Country);
            var selectCounty = new SelectElement(DdlCounty);
            selectCounty.SelectByText(inputData.County);
            var selectCity = new SelectElement(DdlCity);
            selectCity.SelectByText(inputData.City);
        }

        [TestMethod]
        public void User_Should_Add_Address_Successfully()
        {
            var inputData = new AddAddressBO
            {
                LastName = "Popa",
                FirstName = "Ion",
                PhoneNumber = "12345678",
                Country = "Romania",
                County = "Iasi",
                City = "Iasi",
                Address = "Tudor Vladimirescu 145"
            };
            BtnIconAccount.Click();
            BtnAccountAddress.Click();
            BtnNewAddress.Click();
            Fill_Form_Address(inputData);
            BtnSave.Click();
        }

        [TestMethod]
        public void User_Should_Edit_Address_Successfully()
        {
            var inputData = new AddAddressBO
            {
                LastName = "Florea",
                FirstName = "Ionel",
                PhoneNumber = "89525353",
                Country = "Romania",
                County = "Vaslui",
                City = "Vaslui",
                Address = "Tudor Vladimirescu 145"
            };
            BtnIconAccount.Click();
            BtnAccountAddress.Click();
            BtnEditAddress.Click();
            Fill_Form_Address(inputData);
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
