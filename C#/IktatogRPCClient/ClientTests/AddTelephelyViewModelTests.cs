using System;
using IktatogRPCClient.ViewModels;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ClientTests
{
    [TestClass]
    public class AddTelephelyViewModelTests
    {
        AddTelephelyViewModel target;
        [TestInitialize]
        public void InitData()
        {
            target = new AddTelephelyViewModel();
            target.TelephelyNeve = "Teszttelephely";
        }
        [TestMethod]
        public void CanAddTelephely()
        {
            bool expected = true;
            bool actual = target.CanDoAction;
            Assert.AreEqual(expected, actual);
        }
        [TestMethod]
        public void CanAddTelephelyUresNev()
        {
            bool expected = false;
            target.TelephelyNeve = "";
            bool actual = target.CanDoAction;
            Assert.AreEqual(expected, actual);
        }
        [TestMethod]
        public void CanAddTelephelyHossuNev()
        {
            bool expected = false;
            target.TelephelyNeve = "TesztTelephelyTesztTelephelyTesztTelephelyTesztTelephelyTesztTelephelyTesztTelephelyTesztTelephelyTesztTelephelyTesztTelephelyTesztTelephelyTesztTelephely";
            bool actual = target.CanDoAction;
            Assert.AreEqual(expected, actual);
        }
        [TestMethod]
        public void CanAddTelephelySpacesNev()
        {
            bool expected = false;
            target.TelephelyNeve = "        ";
            bool actual = target.CanDoAction;
            Assert.AreEqual(expected, actual);
        }    

        [TestMethod]
        public void CanAddTelephely1Hossz()
        {
            bool expected = false;
            target.TelephelyNeve = "T";
            bool actual = target.CanDoAction;
            Assert.AreEqual(expected, actual);
        }
        [TestMethod]
        public void CanAddTelephely2Hossz()
        {
            bool expected = false;
            target.TelephelyNeve = "Te";
            bool actual = target.CanDoAction;
            Assert.AreEqual(expected, actual);
        }
        [TestMethod]
        public void CanAddTelephely3Hossz()
        {
            bool expected = false;
            target.TelephelyNeve = "Tes";
            bool actual = target.CanDoAction;
            Assert.AreEqual(expected, actual);
        }
        [TestMethod]
        public void CanAddTelephely4Hossz()
        {
            bool expected = false;
            target.TelephelyNeve = "Test";
            bool actual = target.CanDoAction;
            Assert.AreEqual(expected, actual);
        }
    }
}
