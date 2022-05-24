using OpenQA.Selenium;
using TestingProject.Helpers;

namespace TestingProject.PageObjects
{
    public class LoginPage
    {
        private IWebDriver driver;

        public LoginPage(IWebDriver browser)
        {
            driver = browser;
        }

        private By Email = By.Id("loginusername");
        private IWebElement TxtEmail => driver.FindElement(Email);

        private By Password = By.Id("loginpassword");
        private IWebElement TxtPassword => driver.FindElement(Password);

        private By Login = By.CssSelector("#logInModal > div > div > div.modal-footer > button.btn.btn-primary");
        private IWebElement BtnLogin => driver.FindElement(Login);

        public HomePage LoginApplication(string username, string password)
        {
            WaitHelpers.WaitForElementToBeVisible(driver, Email);

            TxtEmail.SendKeys(username);
            TxtPassword.SendKeys(password);
            BtnLogin.Click();

            return new HomePage(driver);
        }

    }
}
