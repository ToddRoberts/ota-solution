using OnTestAutomation.Helpers;
using OpenQA.Selenium;
using TechTalk.SpecFlow;

namespace OnTestAutomation.Pages
{
    public class LoginErrorPage : OTALoadableComponent<LoginErrorPage>
    {
        private IWebDriver _driver;

        private By textlabelErrorMessage = By.XPath("//p[@class='error']");

        public LoginErrorPage()
        {
            _driver = ScenarioContext.Current.Get<IWebDriver>();
        }

        protected override void ExecuteLoad()
        {
        }

        protected override bool EvaluateLoadedStatus()
        {
            if (!OTAElements.WaitForElementOnPageLoad(_driver, textlabelErrorMessage))
            {
                UnableToLoadMessage = "Could not load Login error page within the designated timeout period";
                return false;
            }
            return true;
        }

        public string GetErrorMessage()
        {
            return OTAElements.GetElementText(_driver, textlabelErrorMessage);
        }
    }
}
