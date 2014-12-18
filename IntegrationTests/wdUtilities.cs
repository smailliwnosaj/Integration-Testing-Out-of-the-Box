using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.IO;
using System.Xml;
using System.Xml.Serialization;
using OpenQA.Selenium;
using OpenQA.Selenium.Support;
using OpenQA.Selenium.Support.UI;

    class wdUtilities
    {
        public static wdSettings GetWebDriverSettings()
        {
            var WebDriverSettings = new wdSettings();
            
            var reader = default(XmlReader);
            try
            {
                reader = XmlReader.Create(WebDriverSettings.SettingsFileTempPath);
                var mySerializer = new XmlSerializer(typeof (wdSettings));
                WebDriverSettings = (wdSettings) mySerializer.Deserialize(reader);
            }
            catch (FileNotFoundException ex)
            {

            }
            finally
            {
                reader.Close();
                reader.Dispose();
            }
       
            return WebDriverSettings;
        }

        public static void Open_Application_HomePage(IWebDriver driver, wdSettings settings)
        {
            // Start browser
            Navigate(driver, settings.ApplicationPath);

            Thread.Sleep(2000);
        }

        public static void EnterText(IWebDriver driver, string elementid, string text)
        {
            // Enter username
            var txtUserName = driver.FindElement(By.Id(elementid));
            txtUserName.Click();
            txtUserName.SendKeys(text);

            Thread.Sleep(200);
        }

        public static void ClickButton(IWebDriver driver, string elementid)
        {
            // Enter username
            var button = driver.FindElement(By.Id(elementid));
            button.Click();

            Thread.Sleep(200);
        }

        public static void ClickButtonSafe(IWebDriver driver, string elementid)
        {
            // Enter username
            var button = driver.FindElement(By.Id(elementid));

            if (button != null)
            button.Click();

            Thread.Sleep(200);
        }

        public static void Navigate(IWebDriver driver, string URL)
        {
            driver.Navigate().GoToUrl(URL);

            Thread.Sleep(2000);
        }

        public static IWebElement FindElementSafe(IWebDriver driver, By by)
        {
            try
            {
                return driver.FindElement(by);
            }
            catch (NoSuchElementException)
            {
                return null;
            }
        }

         public static List<string> GetAllLinksInPage(IWebDriver driver)
         {
             return driver.FindElements(By.TagName("a")).Select(link => link.GetAttribute("href")).ToList();
         }

        // NOTE: This function might make a great delegate
         public static void TestAllLinksInPageRecursive(IWebDriver driver, string domain, ref List<string> usedlinks)
         {
             var links = GetAllLinksInPage(driver);

             foreach (var link in links)
             {
                 if (link == null) continue;
                 var lowerlink = link.ToLower();

                 if ( lowerlink.Contains("dopostback") ) continue;
                 if ( lowerlink.Contains("javascript:") ) continue;
                 if (lowerlink.Contains(".pdf")) continue;
                 if ( usedlinks.Contains(link) ) continue;
                 if ( !lowerlink.Contains(domain.ToLower()) ) continue;

                 usedlinks.Add(link);

                 driver.Navigate().GoToUrl(link);
                 Thread.Sleep(200);

                 TestAllLinksInPageRecursive(driver, domain, ref usedlinks);

                 driver.Navigate().Back();
                 Thread.Sleep(200);
             }
         }

        public static void TestCleanup(IWebDriver driver)
        {
            // close browser
            driver.Quit();
        }
    
    }

