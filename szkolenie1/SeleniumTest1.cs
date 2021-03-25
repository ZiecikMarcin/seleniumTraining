using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace szkolenie1
{
    public class SeleniumTest1
    {
        ChromeDriver driver;
        [SetUp]
        public void SetUp()
        {
            driver = new ChromeDriver();
            /*driver.Navigate().GoToUrl("https://www.seleniumeasy.com/test/basic-first-form-demo.html");
            Thread.Sleep(1000);
            var xOnPopup = driver.FindElement(By.Id("at-cv-lightbox-close"));
            xOnPopup.Click();*/
        }
        [TearDown]
        public void TearDown()
        {
            driver.Quit();
        }

        [Test]
        public void CheckInput()
        {
            //arrange
            var testText = "Sending Keys";
            var enterMessage = driver.FindElement(By.Id("user-message"));
            var showMessage = driver.FindElement(By.ClassName("btn-default"));
            var text = driver.FindElement(By.Id("display")).Text;
            //act
            enterMessage.SendKeys(testText);
            showMessage.Click();
            //assert
            Assert.AreEqual(testText, text);
        }

        [Test]
        public void CheckSum()
        {
            //arrange
            var firstDigit = "8";
            var secondDigit = "4";
            var sum = firstDigit + secondDigit;
            var sum1 = driver.FindElement(By.Id("sum1"));
            var sum2 = driver.FindElement(By.Id("sum2"));
            var countButton = driver.FindElements(By.ClassName("btn-default"))[1];            
            //act
            sum1.SendKeys(firstDigit);
            sum2.SendKeys(secondDigit);
            countButton.Click();
            var total = driver.FindElement(By.Id("displayvalue")).Text;
            //assert
            Assert.AreEqual(sum, total);
        }

        [Test]
        public void DifferentSelectors()
        {
            driver.Navigate().GoToUrl("https://www.seleniumeasy.com/test/basic-checkbox-demo.html");
            //arrange
            var checkBox = driver.FindElement(By.XPath("//*[@id=\"isAgeSelected\"]"));
            var option1 = driver.FindElement(By.CssSelector(".checkbox:nth-child(4) .cb1-element"));
            var seleniumLink = driver.FindElement(By.LinkText("Selenium Easy"));
            var expectedTitle = "Learn Selenium with Best Practices and Examples | Selenium Easy";

            //act
            checkBox.Click();
            option1.Click();
            seleniumLink.Click();
            var title = driver.Title;

            //assert
            Assert.AreEqual(expectedTitle, title);
        }
        
        [Test]
        public void ElementsInElement()
        {
            driver.Navigate().GoToUrl("https://www.seleniumeasy.com/test/table-pagination-demo.html");

            //arrange
            var table = driver.FindElement(By.ClassName("table-responsive"));
            var rows = table.FindElements(By.TagName("tr"));
            var cellsInFirstRow = rows[1].FindElements(By.TagName("td"));
            var secondCell = cellsInFirstRow[1];
            var expectedText = "Table cell";

            //act
            //assert
            Assert.AreEqual(expectedText, secondCell.Text);
        }

        [Test]
        public void DropDown()
        {
            //arrange
            driver.Navigate().GoToUrl("https://www.seleniumeasy.com/test/basic-select-dropdown-demo.html");

            var selectSelector = driver.FindElement(By.Id("select-demo"));
            var selectDay = new SelectElement(selectSelector);
            
            //act
            selectDay.SelectByIndex(2);
            selectDay.SelectByText("Tuesday");
            selectDay.SelectByValue("Friday");

            var selectedValue = driver.FindElement(By.ClassName("selected-value"));
            var selectedValueEdited = selectedValue.Text.Substring(16);
            
            //assert
            Assert.AreEqual("Friday", selectedValueEdited);
        }
        [Test]
        public void JQueryDropDown()
        {
            //arrange
            driver.Navigate().GoToUrl("https://www.seleniumeasy.com/test/jquery-dropdown-search-demo.html");
            var newZealandString = "New Zealand";           

            //act
            var dropDown = driver.FindElement(By.ClassName("selection"));
            dropDown.Click();

            var countryContainer = driver.FindElement(By.Id("select2-results__options")).FindElements(By.TagName("li"));
            var countryNetherlands = countryContainer.Where(d => d.Text == "Netherlands").FirstOrDefault();
            var countryNewZealand = countryContainer.First(d => d.Text == newZealandString);
            countryNewZealand.Click();

            //assert
            Assert.AreEqual(newZealandString, dropDown.Text);
            Assert.IsTrue(newZealandString == dropDown.Text);
            Assert.That(newZealandString == dropDown.Text);
            //Assert.Fail("Test shouldn't get here");
        }

        [Test]
        public void ExplicitWait()
        {
            driver.Navigate().GoToUrl("https://www.seleniumeasy.com/test/basic-first-form-demo.html");

            var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(5));
            wait.Until(d => d.FindElement(By.Id("at-cv-lightbox-close")).Displayed);

            var xOnPopup = driver.FindElement(By.Id("at-cv-lightbox-close"));
            xOnPopup.Click();
        }

        [Test]
        public void DragDrop()
        {
            //arrange
            driver.Navigate().GoToUrl("https://www.globalsqa.com/demo-site/draganddrop/");

            Thread.Sleep(1000);

            var frame = driver.FindElement(By.ClassName("demo-frame"));            
            driver.SwitchTo().Frame(frame);

            var image1 = driver.FindElements(By.ClassName("ui-draggable")).FirstOrDefault();
            var trash = driver.FindElement(By.Id("trash"));

            //act
            var action = new Actions(driver);
            action.DragAndDrop(image1, trash).Build().Perform();
            var trashAfterMove = trash.FindElements(By.TagName("img"));

            //assert
            Thread.Sleep(1000);
            Assert.That(trashAfterMove.Count == 1);
            driver.SwitchTo().DefaultContent();
        }

        [Test]
        public void NewWindow()
        {
            //arrange
            driver.Navigate().GoToUrl("https://www.seleniumeasy.com/test/table-pagination-demo.html");
            Thread.Sleep(1000);
            var windowHandlesBefore = driver.WindowHandles;
            var titleBefore = driver.Title;

            var crossBrowserLink = driver.FindElement(By.ClassName("top-banner"));
            crossBrowserLink.Click();

            var windowHandlesAfter = driver.WindowHandles;            

            //act
            driver.SwitchTo().Window(windowHandlesAfter.Last());
            var titleAfter = driver.Title;

            //assert
            Assert.AreEqual("Cross Browser Testing Tool: 2050+ Real Browsers & Devices", driver.Title);

            driver.Close();

            var aaa = driver.WindowHandles;

            driver.SwitchTo().Window(driver.WindowHandles.Last());

            var titleaaaafeter = driver.Title;
        }

        [Test]
        public void DynamicData()
        {
            //arrange
            driver.Navigate().GoToUrl("https://www.seleniumeasy.com/test/dynamic-data-loading-demo.html");
            var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(5));

            var button = driver.FindElement(By.Id("save"));

            //act
            button.Click();

            var loader = driver.FindElement(By.Id("loading"));
            wait.Until(l => loader.Text.Contains("First Name"));

            //assert
            Assert.That(loader.Text.Contains("First Name"));
            Assert.That(loader.Text.Contains("Last Name"));
        }

        [Test]

        public void ProgressBar()
        {
            //arrange
            driver.Navigate().GoToUrl("https://www.seleniumeasy.com/test/jquery-download-progress-bar-demo.html");
            var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));

            var buttonDownload = driver.FindElement(By.Id("downloadButton"));

            //act
            buttonDownload.Click();

            var progressLabel = driver.FindElement(By.ClassName("progress-label"));
            wait.Until(l => progressLabel.Text.Equals("Complete!"));

            //assert
            Assert.That(progressLabel.Text.Equals("Complete!"));
        }

        [Test]
        public void UploadFile()
        {
            //arrange
            driver.Navigate().GoToUrl("http://the-internet.herokuapp.com/upload");
            var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
            var pathToDebug = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            var fileName = "sample30k.pdf";
            //var filePath = pathToDebug + "\\" + fileName;
            var filePath = Path.Combine(pathToDebug, fileName);
            var chooseFile = driver.FindElement(By.Id("file-upload"));
            var uploadButton = driver.FindElement(By.Id("file-submit"));

            //act
            chooseFile.SendKeys(filePath);
            uploadButton.Click();

            var uploadedFiles = driver.FindElement(By.Id("uploaded-files"));
            wait.Until(l => uploadedFiles.Displayed);

            //assert
            Assert.That(uploadedFiles.Text == fileName);
        }

        [Test]
        public void AlertBox()
        {
            //arrange
            driver.Navigate().GoToUrl("https://www.seleniumeasy.com/test/javascript-alert-box-demo.html");

            var clickMe = driver.FindElements(By.ClassName("btn-default"))[0];
            var clickMeConfirm = driver.FindElements(By.ClassName("btn-default"))[1];
            var clickForPrompt = driver.FindElements(By.ClassName("btn-default"))[2];

            var dummyText = "text123";

            //act
            clickMe.Click();
            driver.SwitchTo().Alert().Accept();

            clickMeConfirm.Click();
            driver.SwitchTo().Alert().Accept();
            
            //assert
            var confirmDemoText = driver.FindElement(By.Id("confirm-demo")).Text;
            Assert.AreEqual(confirmDemoText, "You pressed OK!");

            //act
            clickMeConfirm.Click();
            driver.SwitchTo().Alert().Dismiss();

            //assert
            confirmDemoText = driver.FindElement(By.Id("confirm-demo")).Text;
            Assert.AreEqual(confirmDemoText, "You pressed Cancel!");

            //act
            clickForPrompt.Click();
            driver.SwitchTo().Alert().SendKeys(dummyText);
            driver.SwitchTo().Alert().Accept();

            //assert
            var promptDemoText = driver.FindElement(By.Id("prompt-demo")).Text;
            Assert.AreEqual(promptDemoText, "You have entered '" + dummyText + "' !");
        }

        [Test]
        public void Screenshot()
        {
            //arrange
            driver.Navigate().GoToUrl("https://www.seleniumeasy.com/test/javascript-alert-box-demo.html");

            Screenshot screenshot = driver.GetScreenshot();

            //act
            screenshot.SaveAsFile("C:\\Users\\mzc\\Documents\\testScreen.png");
        }
    }
}
