using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using IktatogRPCServer;
using System.IO;
using Serilog.Events;
using IktatogRPCServer.Exceptions;

namespace IktatogRPCServerTesting
{
    /// <summary>
    /// Summary description for RegistryHelperUnitTests
    /// </summary>
    [TestClass]
    public class RegistryHelperUnitTests
    {



        #region Additional test attributes
        //
        // You can use the following additional attributes as you write your tests:
        //
        // Use ClassInitialize to run code before running the first test in the class
        // [ClassInitialize()]
        // public static void MyClassInitialize(TestContext testContext) { }
        //
        // Use ClassCleanup to run code after all tests in a class have run
        // [ClassCleanup()]
        // public static void MyClassCleanup() { }
        //
        // Use TestInitialize to run code before running each test 
        // [TestInitialize()]
        // public void MyTestInitialize() { }
        //
        // Use TestCleanup to run code after each test has run
        // [TestCleanup()]
        // public void MyTestCleanup() { }
        //
        #endregion
        [TestInitialize]
        public void InitDataBeforeTest() {
            RegistryHelper.SetLogLevel(3);
            RegistryHelper.SetLogLevelToShow(3);
            RegistryHelper.SetLogPath(Directory.GetCurrentDirectory());
        }
        #region SetLogLevelTestes
        [TestMethod]
        public void SetLogLevelToOne()
        {
            RegistryHelper.SetLogLevel(1);
            Assert.AreEqual(LogEventLevel.Debug, RegistryHelper.GetLogLevel());
        }
        [TestMethod]
        public void SetLogLevelToTwo() {
            RegistryHelper.SetLogLevel(2);
            Assert.AreEqual(LogEventLevel.Information, RegistryHelper.GetLogLevel());
        }
        [TestMethod]
        public void SetLogLevelToThree()
        {
            RegistryHelper.SetLogLevel(3);
            Assert.AreEqual(LogEventLevel.Warning, RegistryHelper.GetLogLevel());
        }
        [TestMethod]
        public void SetLogLevelToFour()
        {
            RegistryHelper.SetLogLevel(4);
            Assert.AreEqual(LogEventLevel.Error, RegistryHelper.GetLogLevel());
        }
        [TestMethod]
        public void SetLogLevelToFive()
        {
            RegistryHelper.SetLogLevel(5);
            Assert.AreEqual(LogEventLevel.Fatal, RegistryHelper.GetLogLevel());
        }
        [TestMethod]
        public void SetLogLevelToZero()
        {
            RegistryHelper.SetLogLevel(0);
            Assert.AreEqual(LogEventLevel.Verbose, RegistryHelper.GetLogLevel());
        }
        [TestMethod]
        public void SetLogLevelToMinusone()
        {
            InvalidLogLevelException expected = new InvalidLogLevelException("");
            InvalidLogLevelException actual = null;
            try {
                RegistryHelper.SetLogLevel(-1);
            }
            catch (InvalidLogLevelException) {
                actual = expected;
            }
            
            Assert.AreEqual(expected,actual);
        }
        [TestMethod]
        public void SetLogLevelToHundred()
        {
            InvalidLogLevelException expected = new InvalidLogLevelException("");
            InvalidLogLevelException actual = null;
            try
            {
                RegistryHelper.SetLogLevel(100);
            }
            catch (InvalidLogLevelException)
            {
                actual = expected;
            }

            Assert.AreEqual(expected, actual);
        }
        #endregion
        #region SetLogLevelToShowTestes
        [TestMethod]
        public void SetLogLevelToShowToOne()
        {
            int expected = 1;
            int actual = 0;
            RegistryHelper.SetLogLevel(0);
            RegistryHelper.SetLogLevelToShow(1);
            actual = (int)RegistryHelper.GetLogLevelToShow();
            Assert.AreEqual(expected, actual);
        }
        [TestMethod]
        public void SetLogLevelToShowToTwo()
        {
            int expected = 2;
            int actual = 0;
            RegistryHelper.SetLogLevel(0);
            RegistryHelper.SetLogLevelToShow(2);
            actual = (int)RegistryHelper.GetLogLevelToShow();
            Assert.AreEqual(expected, actual);
        }
        [TestMethod]
        public void SetLogLevelToShowToThree()
        {
            int expected = 3;
            int actual = 0;
            RegistryHelper.SetLogLevel(0);
            RegistryHelper.SetLogLevelToShow(3);
            actual = (int)RegistryHelper.GetLogLevelToShow();
            Assert.AreEqual(expected, actual);
        }
        [TestMethod]
        public void SetLogLevelToShowToFour()
        {
            int expected = 4;
            int actual = 0;
            RegistryHelper.SetLogLevel(0);
            RegistryHelper.SetLogLevelToShow(4);
            actual = (int)RegistryHelper.GetLogLevelToShow();
            Assert.AreEqual(expected, actual);
        }
        [TestMethod]
        public void SetLogLevelToShowToFive()
        {
            int expected = 5;
            int actual = 0;
            RegistryHelper.SetLogLevel(0);
            RegistryHelper.SetLogLevelToShow(5);
            actual = (int)RegistryHelper.GetLogLevelToShow();
            Assert.AreEqual(expected, actual);
        }
        [TestMethod]
        public void SetLogLevelToShowToZero()
        {
            int expected = 0;
            int actual = 0;
            RegistryHelper.SetLogLevel(0);
            RegistryHelper.SetLogLevelToShow(0);
            actual = (int)RegistryHelper.GetLogLevelToShow();
            Assert.AreEqual(expected, actual);
        }
        [TestMethod]
        public void SetLogLevelToShowToMinusOne()
        {
            InvalidLogLevelToShowException actual = null;
            RegistryHelper.SetLogLevel(0);
            try
            {
                RegistryHelper.SetLogLevelToShow(-1);
            }
            catch (InvalidLogLevelToShowException e) {
                actual = e;
            }
            Assert.AreNotEqual(null, actual);
        }
        [TestMethod]
        public void SetLogLevelToShowToOneWaitingException() {
            InvalidLogLevelToShowException expected = new InvalidLogLevelToShowException();
            InvalidLogLevelToShowException actual = null;
            try
            {
                RegistryHelper.SetLogLevelToShow(1);
            }
            catch (InvalidLogLevelToShowException) {
                actual = expected;
            }
            Assert.AreEqual(expected,actual);
        }
        [TestMethod]
        public void SetLogLevelToShowToTwoWaitingException()
        {
            InvalidLogLevelToShowException expected = new InvalidLogLevelToShowException();
            InvalidLogLevelToShowException actual = null;
            try
            {
                RegistryHelper.SetLogLevelToShow(2);
            }
            catch (InvalidLogLevelToShowException)
            {
                actual = expected;
            }
            Assert.AreEqual(expected, actual);
        }
        [TestMethod]
        public void SetLogLevelToShowToThreeNotWaitingException()
        {
            InvalidLogLevelToShowException expected = null;
            InvalidLogLevelToShowException actual = null;
            try
            {
                RegistryHelper.SetLogLevelToShow(3);
            }
            catch (InvalidLogLevelToShowException e)
            {
                actual = e;
            }
            Assert.AreEqual(expected, actual);
        }
        [TestMethod]
        public void SetLogLevelToShowToFourNotWaitingException()
        {
            InvalidLogLevelToShowException expected = null;
            InvalidLogLevelToShowException actual = null;
            try
            {
                RegistryHelper.SetLogLevelToShow(4);
            }
            catch (InvalidLogLevelToShowException e)
            {
                actual = e;
            }
            Assert.AreEqual(expected, actual);
        }
        [TestMethod]
        public void SetLogLevelToShowToFiveNotWaitingException()
        {
            InvalidLogLevelToShowException expected = null;
            InvalidLogLevelToShowException actual = null;
            try
            {
                RegistryHelper.SetLogLevelToShow(5);
            }
            catch (InvalidLogLevelToShowException e)
            {
                actual = e;
            }
            Assert.AreEqual(expected, actual);
        }
        [TestMethod]
        public void SetLogLevelToShowToZeroWaitingException()
        {
            InvalidLogLevelToShowException expected = new InvalidLogLevelToShowException();
            InvalidLogLevelToShowException actual = null;
            try
            {
                RegistryHelper.SetLogLevelToShow(0);
            }
            catch (InvalidLogLevelToShowException)
            {
                actual = expected;
            }
            Assert.AreEqual(expected, actual);
        }
        #endregion
        #region SetLogPathTestes
        [TestMethod]
        public void SetLogPathTestNotValidPath() {
            DirectoryNotFoundException expected = new DirectoryNotFoundException();
            DirectoryNotFoundException actual = null;
            try
            {
                RegistryHelper.SetLogPath("");
            }
            catch (DirectoryNotFoundException )
            {
                actual = expected;
            }
            Assert.AreEqual(expected, actual);
        }
        [TestMethod]
        public void SetLogPathTestValidPath()
        {
            DirectoryNotFoundException actual = null;
            try
            {
                RegistryHelper.SetLogPath("C:\\");
            }
            catch (DirectoryNotFoundException e)
            {
                actual = e;
            }
            Assert.AreEqual(null, actual);
        }
        #endregion
    }
}
