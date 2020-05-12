using System;
using Iktato;
using IktatogRPCClient.ViewModels;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ClientTests
{
    [TestClass]
    public class AddPartnerUgyintezoViewModelTest
    {
        AddPartnerUgyintezoViewModel target;
        [TestInitialize]
        public void InitData() {
            target = new AddPartnerUgyintezoViewModel();
            target.SelectedPartner = new Partner();
            target.UgyintezoNeve = "TesztPartnerUgyintezo";
        }
        [TestMethod]
        public void CanAddPartnerUgyintezo()
        {
            bool expected = true;
            bool actual = target.CanDoAction;
            Assert.AreEqual(actual,expected);
        }
        [TestMethod]
        public void CanAddPartnerUgyintezoUresNev()
        {
            bool expected = false;
            target.UgyintezoNeve = "";
            bool actual = target.CanDoAction;
            Assert.AreEqual(actual, expected);
        }
        [TestMethod]
        public void CanAddPartnerUgyintezoHosszuNev()
        {
            bool expected = false;
            target.UgyintezoNeve = "TesztPartnerUgyintezoTesztPartnerUgyintezoTesztPartnerUgyintezoTesztPartnerUgyintezoTesztPartnerUgyintezoTesztPartnerUgyintezo";
            bool actual = target.CanDoAction;
            Assert.AreEqual(actual, expected);
        }
        [TestMethod]
        public void CanAddPartnerUgyintezoRovidNev()
        {
            bool expected = false;
            target.UgyintezoNeve = "11";
            bool actual = target.CanDoAction;
            Assert.AreEqual(actual, expected);
        }
        [TestMethod]
        public void CanAddPartnerUgyintezoSpacesNev()
        {
            bool expected = false;
            target.UgyintezoNeve = "       ";
            bool actual = target.CanDoAction;
            Assert.AreEqual(actual, expected);
        }
        [TestMethod]
        public void CanWriteNevWithNull()
        {
            bool expected = false;
            target.SelectedPartner = null;
            bool actual = target.CanWriteUgyintezoNeve;
            Assert.AreEqual(actual, expected);
        }
        [TestMethod]
        public void CanWriteNevWithData()
        {
            bool expected = true;
            bool actual = target.CanDoAction;
            Assert.AreEqual(actual, expected);
        }
    }
}
