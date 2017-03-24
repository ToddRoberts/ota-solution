using System;
using OpenQA.Selenium;
using TechTalk.SpecFlow;
using AventStack.ExtentReports;
using AventStack.ExtentReports.Reporter;
using AventStack.ExtentReports.Reporter.Configuration;
using OnTestAutomation.Helpers;
using NUnit.Framework;

namespace OnTestAutomation.StepDefinitions
{
    [Binding]
    public class SetupAndTeardownSteps
    {
        private static ExtentReports extentReports;
        private static ExtentTest extentTest;
        private static ExtentHtmlReporter htmlReporter;

        private static IWebDriver driver;

        [BeforeFeature]
        public static void InitializeReport()
        {
            extentReports = new ExtentReports();

            htmlReporter = new ExtentHtmlReporter(@"C:\Git\" + FeatureContext.Current.FeatureInfo.Title + ".html");

            htmlReporter.Configuration().Theme = Theme.Dark;

            extentReports.AttachReporter(htmlReporter);

            FeatureContext.Current.Set<ExtentReports>(extentReports);
        }

        [BeforeScenario]
        public static void InitializeDriver()
        {
            extentTest = FeatureContext.Current.Get<ExtentReports>().CreateTest(ScenarioContext.Current.ScenarioInfo.Title);

            ScenarioContext.Current.Set<ExtentTest>(extentTest);

            driver = DriverMethods.GetDriver();

            ScenarioContext.Current.Set<IWebDriver>(driver);
        }

        [AfterScenario]
        public static void CloseDriver()
        {
            driver.Quit();

            switch (TestContext.CurrentContext.Result.Outcome.Status.ToString().ToLower())
            {
                case "passed":
                    ScenarioContext.Current.Get<ExtentTest>().Pass("Scenario execution completed successfully");
                    break;
                case "failed":
                    ScenarioContext.Current.Get<ExtentTest>().Fail("One or more errors occurred during scenario execution");
                    break;
                case "inconclusive":
                    ScenarioContext.Current.Get<ExtentTest>().Warning("Unable to determine status of scenario execution");
                    break;
                case "skipped":
                    ScenarioContext.Current.Get<ExtentTest>().Skip("Skipped scenario execution");
                    break;
                default:
                    ScenarioContext.Current.Get<ExtentTest>().Error("Error occurred during determination of scenario status");
                    break;
            }

            ScenarioContext.Current.Clear();
        }

        [AfterFeature]
        public static void CreateReport()
        {
            FeatureContext.Current.Get<ExtentReports>().Flush();

            FeatureContext.Current.Clear();
        }
    }
}
