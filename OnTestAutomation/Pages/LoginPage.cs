using OnTestAutomation.Globals;
using OnTestAutomation.Helpers;
using OpenQA.Selenium;
using TechTalk.SpecFlow;

namespace OnTestAutomation.Pages
{
    public class LoginPage : OTALoadableComponent<LoginPage>
    {
        private IWebDriver _driver;

        private By textfieldUsername = By.Name("username");
        private By textfieldPassword = By.Name("password");
        private By buttonLogin = By.XPath("//input[@value='Log In']");

        public LoginPage()
        {
            _driver = ScenarioContext.Current.Get<IWebDriver>();
            if (!(_driver.Url.Equals(Constants.ParaBankHomePage)))
            {
                _driver.Navigate().GoToUrl(Constants.ParaBankHomePage);
            }
        }

        protected override void ExecuteLoad()
        {
        }

        protected override bool EvaluateLoadedStatus()
        {
            if (!OTAElements.WaitForElementOnPageLoad(_driver, textfieldUsername))
            {
                UnableToLoadMessage = "Could not load login page within the designated timeout period";
                return false;
            }
            return true;
        }

        public LoginPage SetUsername(string username)
        {
            OTAElements.SendKeys(_driver, textfieldUsername, username);
            return this;
        }

        public LoginPage SetPassword(string password)
        {
            OTAElements.SendKeys(_driver, textfieldPassword, password);
            return this;
        }

        public void ClickLoginButton()
        {
            OTAElements.Click(_driver, buttonLogin);
        }
    }
}
