using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;



//Author Chenxi Sun Uid u0455173
namespace SpreadsheetUtilities
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        [ExpectedException(typeof(FormulaFormatException))]
        public void TestEmptyFormula()
        {
            Formula a = new Formula("");

            
        }

        [TestMethod]
        [ExpectedException(typeof(FormulaFormatException))]
        public void TestFirstItemOperator()
        {
            Formula a = new Formula("+34+A4");


        }

        [TestMethod]
        [ExpectedException(typeof(FormulaFormatException))]
        public void TestFirstItemClosedParenthesis()
        {
            Formula a = new Formula(")+6");


        }

        [TestMethod]
        [ExpectedException(typeof(FormulaFormatException))]
        public void TestLastItemOpenParenthesis()
        {
            Formula a = new Formula("6+45+(");


        }
        [TestMethod]
        [ExpectedException(typeof(FormulaFormatException))]
        public void TestLastItemOperator()
        {
            Formula a = new Formula("6+45+");


        }
        [TestMethod]
        [ExpectedException(typeof(FormulaFormatException))]
        public void TestVariableInvalid()
        {
            Formula a = new Formula("6+45+A#");


        }
        [TestMethod]
        [ExpectedException(typeof(FormulaFormatException))]
        public void TestVariableOpenParenthesisFollowed()
        {
            Formula a = new Formula("6+45+()");


        }
        [TestMethod]
        [ExpectedException(typeof(FormulaFormatException))]
        public void TestVariableClosedParenthesisFollowed()
        {
            Formula a = new Formula("(6+45+)54");


        }
        [TestMethod]
        [ExpectedException(typeof(FormulaFormatException))]
        public void TestParenthesisCounter()
        {
            Formula a = new Formula("((6+45+)))");


        }

        [TestMethod]
        public void TestDividedByzero()
        {
            Formula a = new Formula("A3/0");
            object b = a.Evaluate( s => 6);
            Assert.IsTrue(b is FormulaError);
        }
      

        [TestMethod]
        public void TestNormalizer()
        {
            Formula a = new Formula("a3+4+B5", s=>s.ToLower(),s=>true);
            Formula b = new Formula("A3+4+b5", s => s.ToLower(), s => true);
            Assert.AreEqual(a, b);


        }

        [TestMethod]
        [ExpectedException(typeof(FormulaFormatException))]
        public void TestNormalizer2()
        {
            Formula a = new Formula("a3+4+B5", s => s.ToLower(), s => false);
            Formula b = new Formula("A3+4+b5", s => s.ToLower(), s => true);
            Assert.AreEqual(a, b);


        }

        [TestMethod]
    
        public void TestNormalizer3()
        {
            Formula a = new Formula("a3+4+b5", s => s.ToUpper(), s => true);
            Formula b = new Formula("A3+4+b5", s => s.ToUpper(), s => true);
            Assert.AreEqual(a, b);


        }
        [TestMethod]
        [ExpectedException(typeof(FormulaFormatException))]
        public void TestNormalizer4()
        {
            Formula a = new Formula("a3+4+b5", s => "7", s => true);
           
           


        }
        [TestMethod]
        [ExpectedException(typeof(FormulaFormatException))]
        public void TestInvalidVariable()
        {
            Formula a = new Formula("7Dslr+5+6+7", s=>s.ToUpper(),s=>true);
           


        }
        [TestMethod]
      
        public void TestBasicArithmatic()
        {
            Formula a = new Formula("3+5",s=>s.ToLower(),s=>true);
            object b = a.Evaluate(d=>1);

            Assert.AreEqual(8.0, b);    
    


        }
        [TestMethod]

        public void TestLongArithmatic()
        {
            Formula a = new Formula("A3+A3*5-(A3/A3)*A3", s => s.ToLower(), s => true);
            object b = a.Evaluate(d => 5);

            Assert.AreEqual(25.0, b);



        }

        [TestMethod]

        public void TestDoubleEqualsign()
        {
            Formula a = null;
            Formula b = null;


            Assert.IsTrue(a==b);



        }
        [TestMethod]
        public void TestnotEqualsign()
        {
            Formula a = null;
            Formula b = new Formula("3+4");


            Assert.IsTrue(a != b);



        }
        [TestMethod]
        public void TestAreEqual()
        {
            Formula a = new Formula("3+4");
            Formula b = new Formula("3+4");


            Assert.IsTrue(a.Equals(b));



        }
        [TestMethod]
        public void TestAreEqual2()
        {
            Formula a = new Formula("3+4+a2", s=>s.ToUpper(),s=>true);
            Formula b = new Formula("3+4+A2");


            Assert.IsTrue(a.Equals(b));



        }
        [TestMethod]
        public void TestHashCode()
        {
            Formula a = new Formula("3+4+a2", s => s.ToUpper(), s => true);
            Formula b = new Formula("3+4+A2",s => s.ToUpper(), s => true);
            Assert.AreEqual(a.GetHashCode(), b.GetHashCode());

        }
        [TestMethod]
        public void TestToString()
        {
            Formula a = new Formula("3+4+a2", s => s.ToUpper(), s => true);
            Formula b = new Formula("3+4+A2");
            Assert.AreEqual(a.ToString(), b.ToString());

        }

        [TestMethod]
        public void TestGetVariable()
        {
            Formula a = new Formula("3+4+a2+a3+A4", s => s.ToUpper(), s => true);
            Formula b = new Formula("3+4+A2+A3+A4");
            IEnumerator<string> c = a.GetVariables().GetEnumerator();
            IEnumerator<string> d = b.GetVariables().GetEnumerator();
            c.MoveNext();
            d.MoveNext();

            Assert.AreEqual(c.Current,d.Current);
            c.MoveNext();
            d.MoveNext();
            Assert.AreEqual(c.Current, d.Current);
            c.MoveNext();
            d.MoveNext();
            Assert.AreEqual(c.Current, d.Current);

        }

        [TestMethod]
        public void TestDoubleEqual2()
        {
            Formula a = new Formula("3+4+a2+a3+A4", s => s.ToUpper(), s => true);
            Formula b = new Formula("2+1");
            Assert.IsFalse(a == b);
            

        }
        [TestMethod]
        public void TestDoubleEqual3()
        {
            Formula b = new Formula("3+4+a2+a3+A4", s => s.ToUpper(), s => true);
            Formula a = new Formula("3+4");
            Assert.IsTrue(a != b);


        }

        [TestMethod]
        public void TestDoubleEqual4()
        {
            Formula b = new Formula("3+4+a2+a3+A4", s => s.ToUpper(), s => true);
            Formula a = new Formula("3+4+a2+a3+a4",s => s.ToUpper(), s => true);
            Assert.IsTrue(a == b);


        }
        [TestMethod]
        public void TestEvaluate()
        {
            Formula b = new Formula("3+.512-6", s => s.ToUpper(), s => true);
            object a = b.Evaluate(s => 0);
            Assert.AreEqual(a, -2.488);

        }
        [TestMethod]
        public void TestEqual2()
        {
            Formula b = new Formula("3+A1+A2+a5", s => s.ToUpper(), s => true);
            Formula c = new Formula("3+A1+A2", s => s.ToUpper(), s => true);
            Assert.IsFalse(b.Equals(c));

        }
        [TestMethod]
        public void TestArithmatic()
        {
            Formula a = new Formula("6-3*4/4", s => s.ToLower(), s => true);
            object b = a.Evaluate(d => 5);

            Assert.AreEqual(3.0, b);



        }

    }
}
