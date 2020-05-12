using System;
using Iktato;
using IktatogRPCClient.ViewModels;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ClientTests
{
    [TestClass]
    public class AddCsoportViewModelTests
    {
        AddCsoportViewModel target;

        [TestInitialize]
        public void InitTarget(){
            target = new AddCsoportViewModel();
            target.CsoportKod = "M";
            target.CsoportName = "TesztCsoportnév";
            target.ValasztottTelephely = new Telephely();
        }
        [TestMethod]
        public void CanAddCsoport()
        {
            bool expected = true;
            bool actual = target.CanDoAction;
            Assert.AreEqual(expected,actual);
        }
        [TestMethod]
        public void CanAddCsoportHosszuKod()
        {
            bool expected = false;
            target.CsoportKod = "mmmm";
            bool actual = target.CanDoAction;
            Assert.AreEqual(expected, actual);
        }
        [TestMethod]
        public void CanAddCsoportUresKod()
        {
            bool expected = false;
            target.CsoportKod = "";
            bool actual = target.CanDoAction;
            Assert.AreEqual(expected, actual);
        }
        [TestMethod]
        public void CanAddCsoportSpacesKod()
        {
            bool expected = false;
            target.CsoportKod = "     ";
            bool actual = target.CanDoAction;
            Assert.AreEqual(expected, actual);
        }
        [TestMethod]
        public void CanAddCsoport1hosszNev()
        {
            bool expected = false;
            target.CsoportName = "m";
            bool actual = target.CanDoAction;
            Assert.AreEqual(expected, actual);
        }
        [TestMethod]
        public void CanAddCsoport2hosszNev()
        {
            bool expected = false;
            target.CsoportName = "mm";
            bool actual = target.CanDoAction;
            Assert.AreEqual(expected, actual);
        }
        [TestMethod]
        public void CanAddCsoport3hosszNev()
        {
            bool expected = false;
            target.CsoportName = "mmn";
            bool actual = target.CanDoAction;
            Assert.AreEqual(expected, actual);
        }
        [TestMethod]
        public void CanAddCsoport4hosszNev()
        {
            bool expected = false;
            target.CsoportName = "mnnn";
            bool actual = target.CanDoAction;
            Assert.AreEqual(expected, actual);
        }
        [TestMethod]
        public void CanAddCsoport5hosszNev()
        {
            bool expected = true;
            target.CsoportName = "mnnnn";
            bool actual = target.CanDoAction;
            Assert.AreEqual(expected, actual);
        }
        [TestMethod]
        public void CanAddCsoportHosszuNev()
        {
            bool expected = false;
            target.CsoportName = "mmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmm" +
                "mmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmm";
            bool actual = target.CanDoAction;
            Assert.AreEqual(expected, actual);
        }
        [TestMethod]
        public void CanAddCsoportUresNev()
        {
            bool expected = false;
            target.CsoportName = "";
            bool actual = target.CanDoAction;
            Assert.AreEqual(expected, actual);
        }
        [TestMethod]
        public void CanAddCsoportSpacesNev()
        {
            bool expected = false;
            target.CsoportName = "     ";
            bool actual = target.CanDoAction;
            Assert.AreEqual(expected, actual);
        }
    }
}
