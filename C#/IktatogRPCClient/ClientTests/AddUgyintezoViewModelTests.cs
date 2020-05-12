using System;
using Iktato;
using IktatogRPCClient.ViewModels;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ClientTests
{
    [TestClass]
    public class AddUgyintezoViewModelTests
    {
        AddUgyintezoViewModel target;
        [TestInitialize]
        public void InitData() {
            target = new AddUgyintezoViewModel();
            target.UgyintezoNeve = "TesztUgyintezo";
            target.ValasztottTelephely = new Telephely();
        }
        [TestMethod]
        public void CanAddUgyintezo()
        {
            bool expected = true;
            bool actual = target.CanDoAction;
            Assert.AreEqual(expected,actual);
        }
        [TestMethod]
        public void CanAddUgyintezoUresNev()
        {
            bool expected = false;
            target.UgyintezoNeve = "";
            bool actual = target.CanDoAction;
            Assert.AreEqual(expected, actual);
        }
        [TestMethod]
        public void CanAddUgyintezoHossuNev()
        {
            bool expected = false;
            target.UgyintezoNeve = "TesztUgyintezoTesztUgyintezoTesztUgyintezoTesztUgyintezoTesztUgyintezoTesztUgyintezoTesztUgyintezoTesztUgyintezoTesztUgyintezoTesztUgyintezoTesztUgyintezo";
            bool actual = target.CanDoAction;
            Assert.AreEqual(expected, actual);
        }
        [TestMethod]
        public void CanAddUgyintezoSpacesNev()
        {
            bool expected = false;
            target.UgyintezoNeve = "        ";
            bool actual = target.CanDoAction;
            Assert.AreEqual(expected, actual);
        }
        [TestMethod]
        public void CantWrite()
        {
            bool expected = false;
            target.ValasztottTelephely = null;
            bool actual = target.CanWriteUgyintezoNeve;
            Assert.AreEqual(expected, actual);
        }
        [TestMethod]
        public void CanWrite()
        {
            bool expected = true;
            bool actual = target.CanWriteUgyintezoNeve;
            Assert.AreEqual(expected, actual);
        }
        [TestMethod]
        public void CanAddUgyintezo1Hossz()
        {
            bool expected = false;
            target.UgyintezoNeve = "T";
            bool actual = target.CanDoAction;
            Assert.AreEqual(expected, actual);
        }
        [TestMethod]
        public void CanAddUgyintezo2Hossz()
        {
            bool expected = false;
            target.UgyintezoNeve = "Te";
            bool actual = target.CanDoAction;
            Assert.AreEqual(expected, actual);
        }
        [TestMethod]
        public void CanAddUgyintezo3Hossz()
        {
            bool expected = false;
            target.UgyintezoNeve = "Tes";
            bool actual = target.CanDoAction;
            Assert.AreEqual(expected, actual);
        }
        [TestMethod]
        public void CanAddUgyintezo4Hossz()
        {
            bool expected = false;
            target.UgyintezoNeve = "Test";
            bool actual = target.CanDoAction;
            Assert.AreEqual(expected, actual);
        }
    }
}
