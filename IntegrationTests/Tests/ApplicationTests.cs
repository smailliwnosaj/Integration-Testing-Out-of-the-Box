using System;

using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.IE;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace IntegrationTests.Tests
{
    [TestClass]
    public class ApplicationTests
    {
        private readonly IWebDriver _driver = new FirefoxDriver();
        private readonly wdSettings _webDriverSettings = wdUtilities.GetWebDriverSettings();

        [TestInitialize]
        public void TestInitialize()
        {

        }

        [TestMethod]
        public void HomePageTest()
        {
            // navigate to home page
            wdUtilities.Open_Application_HomePage(_driver, _webDriverSettings);
        }

        [TestCleanup]
        public void TestCleanup()
        {
            wdUtilities.TestCleanup(_driver);
        }

    }
}


