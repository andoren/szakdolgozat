using System;
using IktatogRPCClient.ViewModels;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Iktato;
namespace ClientTests
{
    [TestClass]
    public class AddFelhasznaloViewModelTests
    {
        AddFelhasznaloViewModel target;
        [TestInitialize]
        public void InitData() {
            target = new AddFelhasznaloViewModel(true);
            target.SelectedTelephelyek = new Caliburn.Micro.BindableCollection<Telephely>() { new Telephely()};
            target.SelectedPrivilege = new Privilege();
            target.NewUsername = "Test";
            target.NewFullname = "Test TEst";
            target.NewPassword = "TestPasswordMeow";

        }
        
        [TestMethod]
        public void CanAddFelhasznalo()
        {
            bool expected = true;
            bool actual = target.CanDoAction;
            Assert.AreEqual(expected,actual);
        }
        [TestMethod]
        public void CanAddFelhasznaloUresUserName()
        {
            bool expected = false;
            target.NewUsername = "";
            bool actual = target.CanDoAction;
            Assert.AreEqual(expected, actual);
        }
        [TestMethod]
        public void CanAddFelhasznaloHosszuUserName()
        {
            bool expected = false;
            target.NewUsername = "TestTestTestTestTestTestTestTestTestTest";
            bool actual = target.CanDoAction;
            Assert.AreEqual(expected, actual);
        }
        [TestMethod]
        public void CanAddFelhasznaloSpacesUserName()
        {
            bool expected = false;
            target.NewUsername = "     ";
            bool actual = target.CanDoAction;
            Assert.AreEqual(expected, actual);
        }
        [TestMethod]
        public void CanAddFelhasznaloUresFullName()
        {
            bool expected = false;
            target.NewFullname = "";
            bool actual = target.CanDoAction;
            Assert.AreEqual(expected, actual);
        }
        [TestMethod]
        public void CanAddFelhasznaloHosszuFullName()
        {
            bool expected = false;
            target.NewFullname = "TestTestTestTestTest";
            bool actual = target.CanDoAction;
            Assert.AreEqual(expected, actual);
        }
        [TestMethod]
        public void CanAddFelhasznaloSpacesFullName()
        {
            bool expected = false;
            target.NewFullname = "    ";
            bool actual = target.CanDoAction;
            Assert.AreEqual(expected, actual);
        }
        [TestMethod]
        public void CanAddFelhasznaloUresPassword()
        {
            bool expected = false;
            target.NewPassword = "";
            bool actual = target.CanDoAction;
            Assert.AreEqual(expected, actual);
        }
        [TestMethod]
        public void CanAddFelhasznaloHossuPassword()
        {
            bool expected = false;
            target.NewPassword = "TestPasswordMeowTestPasswordMeowTestPasswordMeow";
            bool actual = target.CanDoAction;
            Assert.AreEqual(expected, actual);
        }
        [TestMethod]
        public void CanAddFelhasznaloSpacesPassword()
        {
            bool expected = false;
            target.NewPassword = "               ";
            bool actual = target.CanDoAction;
            Assert.AreEqual(expected, actual);
        }
        [TestMethod]
        public void CanAddFelhasznaloNincsTelephely()
        {
            bool expected = false;
            target.SelectedTelephelyek = new Caliburn.Micro.BindableCollection<Telephely>();
            bool actual = target.CanDoAction;
            Assert.AreEqual(expected, actual);
        }
        [TestMethod]
        public void CanAddFelhasznaloVanTelephely()
        {
            bool expected = true;
            bool actual = target.CanDoAction;
            Assert.AreEqual(expected, actual);
        }
        [TestMethod]
        public void CanAddFelhasznaloNincsJog()
        {
            bool expected = false;
            target.SelectedPrivilege = null;
            bool actual = target.CanDoAction;
            Assert.AreEqual(expected, actual);
        }
        [TestMethod]
        public void CanAddFelhasznaloVanJog()
        {
            bool expected = true;
            target.SelectedPrivilege = new Privilege();
            bool actual = target.CanDoAction;
            Assert.AreEqual(expected, actual);
        }
    }
}
