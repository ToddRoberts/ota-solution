using OnTestAutomation.Helpers;
using OpenQA.Selenium;
using TechTalk.SpecFlow;

namespace OnTestAutomation.Pages
{
    public class AccountsOverviewPage : OTALoadableComponent<AccountsOverviewPage>
    {
        private IWebDriver _driver;

        private By textlabelPageHeader = By.XPath("//h1[@class='title' and text()='Accounts Overview']");

        public AccountsOverviewPage()
        {
            _driver = ScenarioContext.Current.Get<IWebDriver>();
        }

        protected override void ExecuteLoad()
        {
        }

        protected override bool EvaluateLoadedStatus()
        {
            if (!OTAElements.WaitForElementOnPageLoad(_driver, textlabelPageHeader))
            {
                UnableToLoadMessage = "Could not load Accounts Overview page within the designated timeout period";
                return false;
            }
            return true;
        }

        public bool IsAt()
        {
            return OTAElements.CheckElementIsVisible(_driver, textlabelPageHeader);
        }
    }
}
