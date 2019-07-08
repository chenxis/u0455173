using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SpreadsheetUtilities;
using SS;
using System.Xml;
using System.Collections;


//Author Chenxi Sun uid u0455173
namespace PS5_tester
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void IsValidTest()
        {
            Spreadsheet a = new Spreadsheet();
            a.SetContentsOfCell("A1", "3.0");
            Assert.AreEqual(3.0, a.GetCellValue("A1"));
        }
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void ArgumentNullTest1()
        {
            Spreadsheet a = new Spreadsheet();
            a.SetContentsOfCell("A1", null);



        }
        [TestMethod]
        [ExpectedException(typeof(InvalidNameException))]
        public void getCellContentInvalidNameExceptionTest1()
        {
            Spreadsheet a = new Spreadsheet();
            a.SetContentsOfCell("A1", "abc");
            a.GetCellContents("");



        }
        [TestMethod]
        [ExpectedException(typeof(InvalidNameException))]
        public void getCellContentInvalidNameExceptionTest2()
        {
            Spreadsheet a = new Spreadsheet();
            a.SetContentsOfCell("A1", "abc");
            a.GetCellContents(null);



        }
        [TestMethod]
        [ExpectedException(typeof(CircularException))]
        public void CircularExceptionTest1()
        {
            Spreadsheet a = new Spreadsheet();

            a.SetContentsOfCell("A1", "=A1");



        }

        [TestMethod]
        [ExpectedException(typeof(InvalidNameException))]
        public void IsValidDoubleTest()
        {
            Spreadsheet a = new Spreadsheet();
            a.SetContentsOfCell("A56845fsdjjdsks", "3.0");
            Assert.AreEqual(3.0, a.GetCellValue("A1"));
        }


        [TestMethod]
        [ExpectedException(typeof(InvalidNameException))]


        public void InvalidExcepitionTest()
        {
            Spreadsheet a = new Spreadsheet();
            a.SetContentsOfCell("", "3.0");
            Assert.AreEqual(3.0, a.GetCellValue("A1"));
        }

        [TestMethod]
     
        public void getCellValueDoubleTest()
        {
            Spreadsheet a = new Spreadsheet();
            a.SetContentsOfCell("A1", "3.0");
            Assert.AreEqual(3.0, (double)a.GetCellValue("A1"));
        }
        [TestMethod]
        public void getCellValueFormulaTest()
        {
            Spreadsheet a = new Spreadsheet();
            a.SetContentsOfCell("A1", "=A2+A3");
            a.SetContentsOfCell("A2", "1");
            a.SetContentsOfCell("A3", "2");
            Assert.AreEqual(3.0, (double)a.GetCellValue("A1"));
        }


        [TestMethod]
        [ExpectedException(typeof(InvalidNameException))]
        public void getCellValueInvalidNameTest1()
        {
            Spreadsheet a = new Spreadsheet(s => true, s => s.ToUpper(), "");

            a.GetCellValue("");


        }
        [TestMethod]
        [ExpectedException(typeof(InvalidNameException))]
        public void getCellValueInvalidNameTest2()
        {
            Spreadsheet a = new Spreadsheet(s => true, s => s.ToUpper(), "");

            a.GetCellValue(null);


        }
        [TestMethod()]
        public void NormalizeTest()
        {
            Spreadsheet a = new Spreadsheet(s => true, s => s.ToUpper(), "");
            a.SetContentsOfCell("A1", "h");

            Assert.AreEqual("h", a.GetCellContents("a1"));
        }

        [TestMethod()]
        [ExpectedException(typeof(SpreadsheetReadWriteException))]
        public void SaveNullpathTest()
        {
            Spreadsheet a = new Spreadsheet(s => true, s => s.ToUpper(), "");
            a.SetContentsOfCell("A1", "h");
            a.Save(null);

        }

        [TestMethod()]
        [ExpectedException(typeof(SpreadsheetReadWriteException))]
        public void SaveEmptypathTest()
        {
            Spreadsheet a = new Spreadsheet(s => true, s => s.ToUpper(), "");
            a.SetContentsOfCell("A1", "h");
            a.Save("");


        }
        [TestMethod()]

        public void SaveTest()
        {
            Spreadsheet a = new Spreadsheet(s => true, s => s.ToUpper(), "");
            a.SetContentsOfCell("A1", "h");
            a.Save("save1");
            Spreadsheet b = new Spreadsheet("save1", s => true, s => s.ToUpper(), "");
            Assert.AreEqual("h", b.GetCellContents("a1"));
        }
        [TestMethod()]

        public void ChangedTest()
        {
            Spreadsheet a = new Spreadsheet(s => true, s => s.ToUpper(), "");
            a.SetContentsOfCell("A1", "h");
            a.Save("save1");
            Spreadsheet b = new Spreadsheet("save1", s => true, s => s.ToUpper(), "");
            Assert.AreEqual(false, a.Changed);
        }
        [TestMethod()]

        public void Changed2Test()
        {
            Spreadsheet a = new Spreadsheet(s => true, s => s.ToUpper(), "");
            a.SetContentsOfCell("A1", "h");

            Assert.AreEqual(true, a.Changed);
        }
        [TestMethod()]

        public void Changed3Test()
        {
            Spreadsheet a = new Spreadsheet(s => true, s => s.ToUpper(), "");
            a.SetContentsOfCell("A1", "h");
            a.Save("save1");
            Assert.AreEqual(false, a.Changed);
        }




        [TestMethod()]
        public void getSavedVersionTest()
        {
            Spreadsheet a = new Spreadsheet(s => true, s => s.ToUpper(), "default");
            a.SetContentsOfCell("A1", "h");
            a.Save("save1");
            Spreadsheet b = new Spreadsheet("save1", s => true, s => s.ToUpper(), "default");
            a.SetContentsOfCell("A2", "h");
            b.Save("save1");
            Assert.AreEqual(b.GetSavedVersion("save1"), "default");

        }
        [TestMethod()]
        public void getDirectDependentsTest()
        {
            Spreadsheet a = new Spreadsheet(s => true, s => s.ToUpper(), "default");
            a.SetContentsOfCell("A1", "=A2");
            a.SetContentsOfCell("A2", "=A3");
            a.SetContentsOfCell("A3", "=A4");
            a.SetContentsOfCell("A4", "=A5");
            a.SetContentsOfCell("A5", "5.0");
            Assert.AreEqual(5.0, (double)a.GetCellValue("A1"));
            a.SetContentsOfCell("A5", "6.0");
            Assert.AreEqual(6.0, (double)a.GetCellValue("A1"));
        }
        [TestMethod()]
        public void getNonEmptyCellsTest()
        {
            Spreadsheet a = new Spreadsheet(s => true, s => s.ToUpper(), "default");
            a.SetContentsOfCell("A1", "=A2");
            a.SetContentsOfCell("A2", "=A3");
            a.SetContentsOfCell("A3", "=A4");
            a.SetContentsOfCell("A4", "=A5");
            a.SetContentsOfCell("A5", "5.0");
            IEnumerable b = a.GetNamesOfAllNonemptyCells();
            IEnumerator b1 = b.GetEnumerator();
            b1.MoveNext();
            Assert.AreEqual("A1", b1.Current);


        }

        [TestMethod()]
        public void getCellContentsTest()
        {
            Spreadsheet a = new Spreadsheet(s => true, s => s.ToUpper(), "default");
            a.SetContentsOfCell("A1", "abcd");
            Assert.AreEqual("abcd", a.GetCellContents("A1"));




        }
        [TestMethod()]
        [ExpectedException(typeof(SpreadsheetReadWriteException))]
        public void SpreadsheetReadWriteExceptionTest1()
        {
            Spreadsheet a = new Spreadsheet(s => true, s => s.ToUpper(), "default");
            a.Save(null);




        }
        [TestMethod()]
        [ExpectedException(typeof(InvalidNameException))]
        public void setCellContentInvalidNameExceptionTest1()
        {
            Spreadsheet a = new Spreadsheet(s => true, s => s.ToUpper(), "default");
            a.SetContentsOfCell(null, "abc");




        }
        [TestMethod()]
        [ExpectedException(typeof(InvalidNameException))]
        public void setCellContentInvalidNameExceptionTest2()
        {
            Spreadsheet a = new Spreadsheet(s => true, s => s.ToUpper(), "default");
            a.SetContentsOfCell("", "abc");




        }
    }
}