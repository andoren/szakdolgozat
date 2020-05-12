using System;
using Iktato;
using IktatogRPCClient.ViewModels;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ClientTests
{
    [TestClass]
    public class AddPartnerViewModelTests
    {
        AddPartnerViewModel target;
        [TestInitialize]
        public void initTest() {
            target = new AddPartnerViewModel();
            target.PartnerNeve = "TestPartner";
            target.SelectedTelephely = new Telephely();
        }
        [TestMethod]
        public void CanAddPartner()
        {
            bool expected = true;
            bool actual = target.CanDoAction;
            Assert.AreEqual(expected,actual);
        }
        [TestMethod]
        public void CanAddPartner0HosszuNev()
        {
            bool expected = false;
            target.PartnerNeve = "";
            bool actual = target.CanDoAction;
            Assert.AreEqual(expected, actual);
        }
        [TestMethod]
        public void CanAddPartner1HosszuNev()
        {
            bool expected = false;
            target.PartnerNeve = "T";
            bool actual = target.CanDoAction;
            Assert.AreEqual(expected, actual);
        }
        [TestMethod]
        public void CanAddPartner2HosszuNev()
        {
            bool expected = false;
            target.PartnerNeve = "Te";
            bool actual = target.CanDoAction;
            Assert.AreEqual(expected, actual);
        }
        [TestMethod]
        public void CanAddPartner3HosszuNev()
        {
            bool expected = true;
            target.PartnerNeve = "Tes";
            bool actual = target.CanDoAction;
            Assert.AreEqual(expected, actual);
        }
        [TestMethod]
        public void CanAddPartner49HosszuNev()
        {
            bool expected = true;
            target.PartnerNeve = "TestPartnerTestPartnerTestPartnerTestPartnerTestPa";
            bool actual = target.CanDoAction;
            Assert.AreEqual(expected, actual);
        }
        [TestMethod]
        public void CanAddPartner50HosszuNev()
        {
            bool expected = false;
            target.PartnerNeve = "TestPartnerTestPartnerTestPartnerTestPartnerTestPar";
            bool actual = target.CanDoAction;
            Assert.AreEqual(expected, actual);
        }
        [TestMethod]
        public void CanAddPartner51HosszuNev()
        {
            bool expected = false;
            target.PartnerNeve = "TestPartnerTestPartnerTestPartnerTestPartnerTestPart";
            bool actual = target.CanDoAction;
            Assert.AreEqual(expected, actual);
        }
        [TestMethod]
        public void CanWritePartnerNeveWithNull()
        {
            bool expected = false;
            target.SelectedTelephely = null;
            bool actual = target.CanWritePartnerNeve;
            Assert.AreEqual(expected, actual);
        }
        [TestMethod]
        public void CanWritePartnerNeveWithTelephely()
        {
            bool expected = true;
         
            bool actual = target.CanWritePartnerNeve;
            Assert.AreEqual(expected, actual);
        }
    }
}
