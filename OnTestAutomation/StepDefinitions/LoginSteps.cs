using System;
using TechTalk.SpecFlow;
using OnTestAutomation.DataEntities;
using OnTestAutomation.Pages;
using OnTestAutomation.Helpers;
using AventStack.ExtentReports;

namespace OnTestAutomation.StepDefinitions
{
    [Binding]
    public class LoginSteps
    {
        private ExtentTest test = ScenarioContext.Current.Get<ExtentTest>();

        [Given(@"I have a registered user (.*) with username (.*) and password (.*)")]
        public void GivenIHaveARegisteredUserWithUsernameAndPassword(string firstName, string username, string password)
        {
            User user = new User();
            user.FirstName = firstName;
            user.Username = username;
            user.Password = password;

            ScenarioContext.Current.Set<User>(user);
        }
        
        [Given(@"he is on the ParaBank home page")]
        public void GivenHeIsOnTheParaBankHomePage()
        {
            new LoginPage().
                Load();
        }
        
        [When(@"he logs in using his credentials")]
        public void WhenHeLogsInUsingHisCredentials()
        {
            User user = ScenarioContext.Current.Get<User>();

            new LoginPage().
                Load().
                SetUsername(user.Username).
                SetPassword(user.Password).
                ClickLoginButton();
        }

        [When(@"he logs in using an invalid password")]
        public void WhenHeLogsInUsingAnInvalidPassword()
        {
            User user = ScenarioContext.Current.Get<User>();

            new LoginPage().
                Load().
                SetUsername(user.Username).
                SetPassword("incorrectpassword").
                ClickLoginButton();
        }

        [Then(@"he should land on the Accounts Overview page")]
        public void ThenHeShouldLandOnTheAccountsOverviewPage()
        {
            OTAAssert.AssertTrue(test, new AccountsOverviewPage().Load().IsAt(), "User landed on the Accounts Overview page after a successful login");
        }

        [Then(@"he should see an error message stating that the login request was denied")]
        public void ThenHeShouldSeeAnErrorMessageStatingThatTheLoginRequestWasDenied()
        {
            OTAAssert.AssertEquals(test, new LoginErrorPage().Load().GetErrorMessage(), "The username and password could not be verified.", "Error message indicating an unsuccessful login is displayed");
        }
    }
}
