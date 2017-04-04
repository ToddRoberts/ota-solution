using System;
using AventStack.ExtentReports;
using NUnit.Framework;
using OpenQA.Selenium;

namespace OnTestAutomation.Helpers
{
    public class OTAAssert
    {
        public static void AssertTrue(IWebDriver driver, ExtentTest extentTest, bool assertedValue, string reportingMessage)
        {
            try
            {
                Assert.IsTrue(assertedValue);
                extentTest.Pass(reportingMessage);
            }
            catch (AssertionException)
            {
                extentTest.Fail("Failure occurred when executing check '" + reportingMessage + "'", MediaEntityBuilder.CreateScreenCaptureFromPath(ReportingMethods.CreateScreenshot(driver)).Build());
                throw;
            }
        }

        public static void AssertEquals(IWebDriver driver, ExtentTest extentTest, string actualValue, string expectedValue, string reportingMessage)
        {
            try
            {
                Assert.AreEqual(expectedValue,actualValue);
                extentTest.Pass(reportingMessage);
            }
            catch (AssertionException)
            {
                if(driver != null)
                {
                    extentTest.Fail("Failure occurred when executing check '" + reportingMessage + "'", MediaEntityBuilder.CreateScreenCaptureFromPath(ReportingMethods.CreateScreenshot(driver)).Build());
                }
                else
                {
                    extentTest.Fail("Failure occurred when executing check '" + reportingMessage + "'");
                }                
                throw;
            }
        }

        public static void AssertEquals(IWebDriver driver, ExtentTest extentTest, int actualValue, int expectedValue, string reportingMessage)
        {
            try
            {
                Assert.AreEqual(expectedValue, actualValue);
                extentTest.Pass(reportingMessage);
            }
            catch (AssertionException)
            {
                if (driver != null)
                {
                    extentTest.Fail("Failure occurred when executing check '" + reportingMessage + "', actual value was " + actualValue, MediaEntityBuilder.CreateScreenCaptureFromPath(ReportingMethods.CreateScreenshot(driver)).Build());
                }
                else
                {
                    extentTest.Fail("Failure occurred when executing check '" + reportingMessage + "', actual value was " + actualValue);
                }
                throw;
            }
        }
    }
}
