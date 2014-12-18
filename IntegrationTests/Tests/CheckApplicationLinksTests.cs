using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading;
using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.IE;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace IntegrationTests.Tests
{
    [TestClass]
    public class CheckApplicationLinksTests
    {
        private readonly IWebDriver _driver = new FirefoxDriver();
        private readonly wdSettings _webDriverSettings = wdUtilities.GetWebDriverSettings();

        [TestInitialize]
        public void TestInitialize()
        {

        }

        [TestMethod]
        public void CheckAllLinksInApplication()
        {
            // log in
            wdUtilities.Open_Application_HomePage(_driver, _webDriverSettings);

            var links = new List<string>();
            wdUtilities.TestAllLinksInPageRecursive(_driver, _webDriverSettings.ApplicationPath, ref links);
        }

        [TestCleanup]
        public void TestCleanup()
        {
            wdUtilities.TestCleanup(_driver);
        }
    }
}