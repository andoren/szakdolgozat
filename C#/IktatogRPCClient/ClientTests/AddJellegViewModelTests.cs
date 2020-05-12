using System;
using Iktato;
using IktatogRPCClient.ViewModels;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ClientTests
{
    [TestClass]
    public class AddJellegViewModelTests
    {
        AddJellegViewModel target;
        [TestInitialize]
        public void InitData() {
            target = new AddJellegViewModel();
            target.SelectedTelephely = new Telephely();
            target.JellegNeve = "TesztJelleg";
        }

        [TestMethod]
        public void CanAddJelleg()
        {
            bool expected = true;
            bool actual = target.CanDoAction;
            Assert.AreEqual(expected,actual);
        }
        [TestMethod]
        public void CanAddJellegUresNev()
        {
            bool expected = false;
            target.JellegNeve = "";
            bool actual = target.CanDoAction;
            Assert.AreEqual(expected, actual);
        }
        [TestMethod]
        public void CanAddJellegHossuNev()
        {
            bool expected = false;
            target.JellegNeve = "TesztJellegTesztJellegTesztJellegTesztJellegTesztJellegTesztJellegTesztJellegTesztJellegTesztJellegTesztJellegTesztJelleg";
            bool actual = target.CanDoAction;
            Assert.AreEqual(expected, actual);
        }
        [TestMethod]
        public void CanAddJellegSpacesNev()
        {
            bool expected = false;
            target.JellegNeve = "        ";
            bool actual = target.CanDoAction;
            Assert.AreEqual(expected, actual);
        }
        [TestMethod]
        public void CantWrite()
        {
            bool expected = false;
            target.SelectedTelephely = null;
            bool actual = target.CanWriteJellegNeve;
            Assert.AreEqual(expected, actual);
        }
        [TestMethod]
        public void CanWrite()
        {
            bool expected = true;
            bool actual = target.CanWriteJellegNeve;
            Assert.AreEqual(expected, actual);
        }
        [TestMethod]
        public void CanAddJelleg1Hossz()
        {
            bool expected = false;
            target.JellegNeve = "T";
            bool actual = target.CanDoAction;
            Assert.AreEqual(expected, actual);
        }
        [TestMethod]
        public void CanAddJelleg2Hossz()
        {
            bool expected = false;
            target.JellegNeve = "Te";
            bool actual = target.CanDoAction;
            Assert.AreEqual(expected, actual);
        }
        [TestMethod]
        public void CanAddJelleg3Hossz()
        {
            bool expected = false;
            target.JellegNeve = "Tes";
            bool actual = target.CanDoAction;
            Assert.AreEqual(expected, actual);
        }
        [TestMethod]
        public void CanAddJelleg4Hossz()
        {
            bool expected = false;
            target.JellegNeve = "Test";
            bool actual = target.CanDoAction;
            Assert.AreEqual(expected, actual);
        }
    }
}
