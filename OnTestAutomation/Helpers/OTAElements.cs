using System;
using OpenQA.Selenium;
using OnTestAutomation.Globals;
using OpenQA.Selenium.Support.UI;
using AventStack.ExtentReports;
using TechTalk.SpecFlow;
using NUnit.Framework;

namespace OnTestAutomation.Helpers
{
    public class OTAElements
    {
        // Tries to send the given input string to the element specified taking into account the predefined timeout
        // Catches and handles exceptions that might occur
        public static void SendKeys(IWebDriver driver, By by, string valueToType, bool inputValidation = false)
        {
            try
            {
                new WebDriverWait(driver, TimeSpan.FromSeconds(Constants.DefaultTimeout)).Until(ExpectedConditions.ElementIsVisible(by));
                driver.FindElement(by).Clear();
                driver.FindElement(by).SendKeys(valueToType);
            }
            catch (Exception ex) when (ex is NoSuchElementException || ex is WebDriverTimeoutException)
            {
                ExtentTest test = ScenarioContext.Current.Get<ExtentTest>();
                test.Error("Could not perform SendKeys on element identified by " + by.ToString() + " after " + Constants.DefaultTimeout.ToString() + " seconds", MediaEntityBuilder.CreateScreenCaptureFromPath(ReportingMethods.CreateScreenshot(driver)).Build());
                Assert.Fail();
            }
            catch (Exception ex) when (ex is StaleElementReferenceException)
            {
                // find element again and retry
                new WebDriverWait(driver, TimeSpan.FromSeconds(Constants.DefaultTimeout)).Until(ExpectedConditions.ElementIsVisible(by));
                driver.FindElement(by).Clear();
                driver.FindElement(by).SendKeys(valueToType);
            }
        }

        // Tries to click an element taking into account a predefined timeout
        // This can generate a variety of exception that are all handled in this method
        public static void Click(IWebDriver driver, By by)
        {
            try
            {
                new WebDriverWait(driver, TimeSpan.FromSeconds(Constants.DefaultTimeout)).Until(ExpectedConditions.ElementToBeClickable(by));
                driver.FindElement(by).Click();
            }
            catch (Exception ex) when (ex is WebDriverTimeoutException || ex is NoSuchElementException)
            {
                ExtentTest test = ScenarioContext.Current.Get<ExtentTest>();
                test.Error("Element identified by " + by.ToString() + " not clickable after " + Constants.DefaultTimeout.ToString() + " seconds", MediaEntityBuilder.CreateScreenCaptureFromPath(ReportingMethods.CreateScreenshot(driver)).Build());
                Assert.Fail();
            }
        }

        // Returns whether an element is visible
        // Takes into account a predefined timeout
        // Logs to HTML if the element is not present and visible after this timeout
        public static bool CheckElementIsVisible(IWebDriver driver, By by)
        {
            try
            {
                new WebDriverWait(driver, TimeSpan.FromSeconds(Constants.DefaultTimeout)).Until(ExpectedConditions.ElementIsVisible((by)));
            }
            catch (Exception ex) when (ex is NoSuchElementException || ex is WebDriverTimeoutException)
            {
                ScenarioContext.Current.Get<ExtentTest>().Error("Element identified by " + by.ToString() + " not visible after " + Constants.DefaultTimeout.ToString() + " seconds, but was expected to be visible", MediaEntityBuilder.CreateScreenCaptureFromPath(ReportingMethods.CreateScreenshot(driver)).Build());
                return false;
            }
            return true;
        }

        // Waits for an element to be clickable (visible AND enabled)
        // Takes into account a predefined timeout
        // Preferred method to be used for determining whether a page has been loaded
        // See for example all Page Object constructors
        public static bool WaitForElementOnPageLoad(IWebDriver driver, By by)
        {
            try
            {
                new WebDriverWait(driver, TimeSpan.FromSeconds(Constants.DefaultTimeout)).Until(ExpectedConditions.ElementToBeClickable(by));
            }
            catch (Exception ex) when (ex is NoSuchElementException || ex is WebDriverTimeoutException)
            {
                return false;
            }
            return true;
        }

        // Returns the value of the text property for the specified element
        // Mostly used for retrieving values for input elements (text boxes)
        // Catches and handles exceptions that might occur
        public static string GetElementText(IWebDriver driver, By by)
        {
            string returnValue = "";

            try
            {
                new WebDriverWait(driver, TimeSpan.FromSeconds(Constants.DefaultTimeout)).Until(ExpectedConditions.ElementIsVisible(by));
                returnValue = driver.FindElement(by).Text;
            }
            catch (Exception ex) when (ex is NoSuchElementException || ex is WebDriverTimeoutException)
            {
                ExtentTest test = ScenarioContext.Current.Get<ExtentTest>();
                test.Error("Could not perform GetElementText on element identified by " + by.ToString() + " after " + Constants.DefaultTimeout.ToString() + " seconds", MediaEntityBuilder.CreateScreenCaptureFromPath(ReportingMethods.CreateScreenshot(driver)).Build());
                Assert.Fail();
            }

            return returnValue;
        }
    }
}
