using System;
using OpenQA.Selenium;
using TechTalk.SpecFlow;
using AventStack.ExtentReports;
using AventStack.ExtentReports.Reporter;
using AventStack.ExtentReports.Reporter.Configuration;
using OnTestAutomation.Helpers;
using NUnit.Framework;
using OnTestAutomation.Globals;

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

            htmlReporter = new ExtentHtmlReporter(Constants.ReportingFolder + FeatureContext.Current.FeatureInfo.Title + ".html");

            htmlReporter.Configuration().Theme = Theme.Dark;

            extentReports.AttachReporter(htmlReporter);

            FeatureContext.Current.Set<ExtentReports>(extentReports);
        }

        [BeforeScenario]
        public static void InitializeReportTest()
        {
            extentTest = FeatureContext.Current.Get<ExtentReports>().CreateTest(ScenarioContext.Current.ScenarioInfo.Title);

            ScenarioContext.Current.Set<ExtentTest>(extentTest);
        }

        [BeforeScenario("browser")]
        public static void InitializeDriver()
        {
            driver = DriverMethods.GetDriver();

            ScenarioContext.Current.Set<IWebDriver>(driver);
        }

        [BeforeStep]
        public static void LogCurrentStep()
        {
            ScenarioContext.Current.Get<ExtentTest>().Info(LogTraceListener.LastGherkinMessage);
        }

        [AfterScenario]
        public static void WrapUpReport()
        {
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

        [AfterScenario("browser")]
        public static void CloseBrowser()
        {
            driver.Quit();
        }

        [AfterFeature]
        public static void CreateReport()
        {
            FeatureContext.Current.Get<ExtentReports>().Flush();

            FeatureContext.Current.Clear();
        }
    }
}
