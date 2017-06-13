using System;
using System.IO;
using System.Runtime.Remoting.Channels;
using System.Xml;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SchedAero.Core.Services;
using SchedAreo.Core;
using System.Xml.Linq;

namespace Test.SchedAero.Core.ServiceTests
{
    [TestClass]
    public class TestServicesMenuParser
    {
        private string _filePath;

        public string _activePage;

        [TestInitialize]
        public void TestInitialization()
        {
            _filePath = @"C:\Users\scottalzheimer\Desktop\Avinode assesment\SchedAero Menu.xml";
            _activePage = "/Requests/OpenQuotes.aspx";
        }

        [TestMethod]
        public void Can_Get_XML_File()
        {

            var parser = new MenuParser();

            var xmlDoc = parser.GetMenuFile(_filePath);


            Assert.IsNotNull(xmlDoc, "Get xml file failed, nothing was returned");
            Assert.IsTrue(typeof(XDocument) == xmlDoc.GetType(), "Get file reutrned wrong type");
        }

        [TestMethod]
        [ExpectedException(typeof(FileFormatException))]
        public void Can_Get_File_Fail()
        {


            var parser = new MenuParser();

            var xmlDoc = parser.GetMenuFile(@"C:\Users\scottalzheimer\Desktop\Avinode assesment\SchedAero Menu.txt");

            Assert.IsInstanceOfType(xmlDoc, typeof(FileFormatException), "Did not receive expected file format exception");
            
        }
        [TestMethod]
        public void Can_Parse_SchedAero_Xml_Menu_XDocument()
        {
            var parser = new MenuParser();


            var result = parser.ParseMenuFile(_filePath, _activePage);

            Assert.IsNotNull(result, "SchedAero xml menu was null");
            Assert.IsTrue(result.MenuItems.Count > 0, "SchedAero xml menu contained no menu items");

        }
        [TestMethod]
        public void Can_Parse_Wyvern_Xml_Menu_XDocument()
        {
            var parser = new MenuParser();



            var result = parser.ParseMenuFile(@"C:\Users\scottalzheimer\Desktop\Avinode assesment\Wyvern Menu.xml", _activePage);
            Assert.IsNotNull(result, "Wyvern xml menu was null");
            Assert.IsTrue(result.MenuItems.Count > 0, "Wyvern xml menu contained no menu items");
        }

    }
}
