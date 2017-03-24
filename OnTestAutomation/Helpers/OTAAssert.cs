using System;
using AventStack.ExtentReports;
using NUnit.Framework;
using TechTalk.SpecFlow;

namespace OnTestAutomation.Helpers
{
    public class OTAAssert
    {
        public static void AssertTrue(ExtentTest extentTest, bool assertedValue, string reportingMessage, bool optionalCreateScreenshotIfSuccessful = false)
        {
            try
            {
                Assert.IsTrue(assertedValue);
                extentTest.Pass(reportingMessage);
            }
            catch (AssertionException)
            {
                extentTest.Fail("Failure occurred when executing check '" + reportingMessage + "'");
                throw;
            }
        }

        public static void AssertEquals(ExtentTest extentTest, string actualValue, string expectedValue, string reportingMessage, bool optionalCreateScreenshotIfSuccessful = false)
        {
            try
            {
                Assert.AreEqual(expectedValue,actualValue);
                extentTest.Pass(reportingMessage);
            }
            catch (AssertionException)
            {
                extentTest.Fail("Failure occurred when executing check '" + reportingMessage + "'");
                throw;
            }
        }
    }
}
