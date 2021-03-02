using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace szkolenie1
{
    public class SeleniumTest1
    {
        ChromeDriver driver;

        [Test]
        public void CheckInput()
        {
            driver = new ChromeDriver();
            driver.Navigate().GoToUrl("https://www.seleniumeasy.com/test/basic-first-form-demo.html");

            Thread.Sleep(1000);
            driver.FindElement(By.Id("at-cv-lightbox-close")).Click();
            driver.FindElement(By.Id("user-message")).SendKeys("Sending Keys");
            driver.FindElement(By.ClassName("btn-default")).Click();

            var text = driver.FindElement(By.Id("display")).Text;

            Assert.AreEqual("Sending Keys", text);
            driver.Quit();
        }

        [Test]
        public void CheckSum()
        {
            driver = new ChromeDriver();
            driver.Navigate().GoToUrl("https://www.seleniumeasy.com/test/basic-first-form-demo.html");

            Thread.Sleep(1000);
            driver.FindElement(By.Id("at-cv-lightbox-close")).Click();



            driver.FindElement(By.Id("sum1")).SendKeys("8");
            driver.FindElement(By.Id("sum2")).SendKeys("4");
            driver.FindElements(By.ClassName("btn-default"))[1].Click();
            var total = driver.FindElement(By.Id("displayvalue")).Text;

            Assert.AreEqual("12", total);
            driver.Quit();

        }
    }
}
