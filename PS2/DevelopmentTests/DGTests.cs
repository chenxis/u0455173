using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SpreadsheetUtilities;

//Author Chenxi Sun uid u0455173
namespace DevelopmentTests
{
    /// <summary>
    ///This is a test class for DependencyGraphTest and is intended
    ///to contain all DependencyGraphTest Unit Tests
    ///</summary>
    [TestClass()]
    public class DependencyGraphTest
    {

        /// <summary>
        ///Empty graph should contain nothing
        ///</summary>
        [TestMethod()]
        public void SimpleEmptyTest()
        {
            DependencyGraph t = new DependencyGraph();
            Assert.AreEqual(0, t.Size);
        }


        /// <summary>
        ///Empty graph should contain nothing
        ///</summary>
        [TestMethod()]
        public void SimpleEmptyRemoveTest()
        {
            DependencyGraph t = new DependencyGraph();
            t.AddDependency("x", "y");
            Assert.AreEqual(1, t.Size);
            t.RemoveDependency("x", "y");
            Assert.AreEqual(0, t.Size);
        }
        [TestMethod()]
        public void addreplaceremoveDependeesTest()
        {
            DependencyGraph t = new DependencyGraph();
            t.AddDependency("x", "y");
            HashSet<string> replacelist = new HashSet<string>();
            replacelist.Add("a");
            replacelist.Add("b");
            replacelist.Add("c");

            t.ReplaceDependees("y", replacelist);
            t.RemoveDependency("a", "y");
            IEnumerator<string> e2 = t.GetDependees("y").GetEnumerator();

            e2.MoveNext();
            Assert.AreEqual("b", e2.Current);
            e2.MoveNext();
            Assert.AreEqual("c", e2.Current);
            





        }

        /// <summary>
        ///Empty graph should contain nothing
        ///</summary>
        [TestMethod()]
        public void SimpleEmptyTest2()
        {
            DependencyGraph t = new DependencyGraph();
            t.AddDependency("x", "y");
            IEnumerator<string> e1 = t.GetDependees("y").GetEnumerator();
            Assert.IsTrue(e1.MoveNext());
            Assert.AreEqual("x", e1.Current);
            IEnumerator<string> e2 = t.GetDependents("x").GetEnumerator();
            Assert.IsTrue(e2.MoveNext());
            Assert.AreEqual("y", e2.Current);
            t.RemoveDependency("x", "y");
            Assert.IsFalse(t.GetDependees("y").GetEnumerator().MoveNext());
            Assert.IsFalse(t.GetDependents("x").GetEnumerator().MoveNext());
        }

        [TestMethod()]
        public void adddepencieswithSameDependentandDependencee()
        {
            DependencyGraph t = new DependencyGraph();
            t.AddDependency("x", "x");
            IEnumerator<string> e1 = t.GetDependees("x").GetEnumerator();
            IEnumerator<string> e2 = t.GetDependents("x").GetEnumerator();
            e1.MoveNext();
            e2.MoveNext();
            Assert.AreEqual(e1.Current, "x");
            Assert.AreEqual(e2.Current, "x");




        }


        /// <summary>
        /// add item replace dependencees and add item again
        /// </summary>
        [TestMethod()]
        public void addReplaceAddTest()
        {
            DependencyGraph t = new DependencyGraph();
            t.AddDependency("x", "y");
            HashSet<string> replacelist = new HashSet<string>();
            replacelist.Add("a");
           
            t.ReplaceDependents("x", replacelist);
            t.AddDependency("x", "y");
            IEnumerator<string> e2 = t.GetDependents("x").GetEnumerator();

            e2.MoveNext();
            Assert.AreEqual("a", e2.Current);
            e2.MoveNext();
            Assert.AreEqual("y", e2.Current);
            e2.MoveNext();
        

        }

        [TestMethod()]
        public void replaceDependeesAndReplaceDependentsTest()
        {

            DependencyGraph t = new DependencyGraph();
            t.AddDependency("x", "y");
            HashSet<string> replaceDependees = new HashSet<string>();
            replaceDependees.Add("a");
            replaceDependees.Add("b");
  
            HashSet<string> replaceDependents = new HashSet<string>();
            replaceDependents.Add("x");
            replaceDependents.Add("y");
         
            t.ReplaceDependees("y", replaceDependees);
            t.ReplaceDependents("x", replaceDependents);
            IEnumerator<string> e1 = t.GetDependents("x").GetEnumerator();
            IEnumerator<string> e2 = t.GetDependees("y").GetEnumerator();
            e1.MoveNext();
            Assert.AreEqual(e1.Current, "x");
            e2.MoveNext();
      
            Assert.AreEqual(e2.Current, "a");



        }


        [TestMethod()]
        public void replaceDependeesAndReplaceDependentsSizeTest()
        {
            DependencyGraph t = new DependencyGraph();
            t.AddDependency("x", "y");
            HashSet<string> replaceDependees = new HashSet<string>();
            replaceDependees.Add("a");
            replaceDependees.Add("b");
            replaceDependees.Add("c");
            replaceDependees.Add("d");
            replaceDependees.Add("e");
            replaceDependees.Add("f");
            HashSet<string> replaceDependents = new HashSet<string>();
            replaceDependees.Add("x");
            replaceDependees.Add("y");
            replaceDependees.Add("z");
            replaceDependees.Add("h");
            replaceDependees.Add("i");
            replaceDependees.Add("j");
            t.ReplaceDependents("y", replaceDependees);
            t.ReplaceDependees("x", replaceDependents);

            Assert.AreEqual(12, t.Size);







        }




        [TestMethod()]
        public void AddRemoveEmptySizeTest()
        {
            DependencyGraph t = new DependencyGraph();
            t.AddDependency("x", "y");
            t.RemoveDependency("x", "y");
            Assert.AreEqual(0, t.Size);



        }

        [TestMethod()]
        public void hasDependencesFalseTest()
        {
            DependencyGraph t = new DependencyGraph();
            t.AddDependency("x", "y");
            t.RemoveDependency("x", "y");
            Assert.AreEqual(false, t.HasDependees("y"));



        }
        [TestMethod()]
        public void hasDependentFalseTest()
        {
            DependencyGraph t = new DependencyGraph();
            t.AddDependency("x", "y");
            t.RemoveDependency("x", "y");
            Assert.AreEqual(false, t.HasDependees("x"));



        }


        /// <summary>
        ///Replace on an empty DG shouldn't fail
        ///</summary>
        [TestMethod()]
        public void replaceDependentsandRemoveTest()
        {
            DependencyGraph t = new DependencyGraph();
            t.AddDependency("x", "y");
            HashSet<string> replacelist = new HashSet<string>();
            replacelist.Add("a");
            replacelist.Add("b");
            replacelist.Add("A");
            replacelist.Add("c");
            replacelist.Add("C");
            t.ReplaceDependents("x", replacelist);
            t.RemoveDependency("x", "a");
            IEnumerator<string> e2 = t.GetDependents("x").GetEnumerator();
           
            e2.MoveNext();
            Assert.AreEqual("b", e2.Current);


        }
        /// <summary>
        ///check the size of Dependees("s")
        ///</summary>
        [TestMethod()]
        public void thisTest()
        {
            DependencyGraph t = new DependencyGraph();
            t.AddDependency("x", "y");
            HashSet<string> replacelist = new HashSet<string>();
            replacelist.Add("a");
            replacelist.Add("b");
            replacelist.Add("A");
            replacelist.Add("c");
            replacelist.Add("C");
            t.ReplaceDependees("x", replacelist);
            
        
            Assert.AreEqual(5, t["x"]);


        }
        [TestMethod()]
        public void thisAfterRemoveallTest()
        {
            DependencyGraph t = new DependencyGraph();
            t.AddDependency("x", "y");
            HashSet<string> replacelist = new HashSet<string>();
            replacelist.Add("a");
            replacelist.Add("b");
            replacelist.Add("A");
            replacelist.Add("c");
            replacelist.Add("C");
            t.ReplaceDependees("x", replacelist);
            t.RemoveDependency("b", "x");
            t.RemoveDependency("A", "x");
            t.RemoveDependency("c", "x");
            t.RemoveDependency("C", "x");
            t.RemoveDependency("a", "x");



            Assert.AreEqual(0, t["x"]);


        }


        /// <summary>
        ///check the size of Dependees("s")
        ///</summary>
        [TestMethod()]
        public void hasDependentsTest()
        {
            DependencyGraph t = new DependencyGraph();
            t.AddDependency("x", "y");
            Assert.IsTrue(t.HasDependents("x"));
           


        }


        /// <summary>
        ///check the size of Dependees("s")
        ///</summary>
        [TestMethod()]
        public void hasDependeesTest()
        {
            DependencyGraph t = new DependencyGraph();
            t.AddDependency("x", "y");
            Assert.IsTrue(t.HasDependees("y"));



        }


        /// <summary>
        ///Replace on an empty DG shouldn't fail
        ///</summary>
        [TestMethod()]
        public void checkDependentandDependeeTest()
        {
            DependencyGraph t = new DependencyGraph();
            t.AddDependency("x", "y");
            t.RemoveDependency("x", "y");
            t.AddDependency("x", "y");
            t.AddDependency("x", "y");
            IEnumerator<string> e = t.GetDependees("y").GetEnumerator();
            IEnumerator<string> b = t.GetDependents("x").GetEnumerator();
            e.MoveNext();
            b.MoveNext();
            string s2 = b.Current;

            String s1 = e.Current;
            Assert.AreEqual(s1, "x");
            Assert.AreEqual(s2, "y");
        }


        [TestMethod()]
        public void ReplaceTwiceDependentTest()
        {
            DependencyGraph t = new DependencyGraph();
            t.AddDependency("x", "y");
            HashSet<string> replacelist1 = new HashSet<string>();
            HashSet<string> replacelist2 = new HashSet<string>();
            replacelist1.Add("s");
            replacelist2.Add("t");
            t.ReplaceDependees("y", replacelist1);
            t.ReplaceDependees("y", replacelist2);
            IEnumerator<string> e1 = t.GetDependees("y").GetEnumerator();
            e1.MoveNext();
            Assert.AreEqual(e1.Current, "t");






        }

        [TestMethod()]
        public void ReplaceTwiceDependentSizeTest()
        {
            DependencyGraph t = new DependencyGraph();
            t.AddDependency("x", "y");
            HashSet<string> replacelist1 = new HashSet<string>();
            HashSet<string> replacelist2 = new HashSet<string>();
            replacelist1.Add("s");
            replacelist2.Add("t");
            replacelist2.Add("f");
            replacelist2.Add("o");
            t.ReplaceDependees("y", replacelist1);
            t.ReplaceDependees("y", replacelist2);
        
            Assert.AreEqual(3, t.Size);






        }



        /// <summary>
        ///Replace on an empty DG shouldn't fail
        ///</summary>
        [TestMethod()]
        public void SimpleReplaceTest()
        {
            DependencyGraph t = new DependencyGraph();
            t.AddDependency("x", "y");
            Assert.AreEqual(t.Size, 1);
            t.RemoveDependency("x", "y");
            t.ReplaceDependents("x", new HashSet<string>());
            t.ReplaceDependees("y", new HashSet<string>());
        }



        ///<summary>
        ///It should be possibe to have more than one DG at a time.
        ///</summary>
        [TestMethod()]
        public void StaticTest()
        {
            DependencyGraph t1 = new DependencyGraph();
            DependencyGraph t2 = new DependencyGraph();
            t1.AddDependency("x", "y");
            Assert.AreEqual(1, t1.Size);
            Assert.AreEqual(0, t2.Size);
        }




        /// <summary>
        ///Non-empty graph contains something
        ///</summary>
        [TestMethod()]
        public void SizeTest()
        {
            DependencyGraph t = new DependencyGraph();
            t.AddDependency("a", "b");
            t.AddDependency("a", "c");
            t.AddDependency("c", "b");
            t.AddDependency("b", "d");
            Assert.AreEqual(4, t.Size);
        }

        /// <summary>
        ///Non-empty graph contains something
        ///</summary>
        [TestMethod()]
        public void replaceDependentsSizeTest()
        {
            DependencyGraph t = new DependencyGraph();
            t.AddDependency("x", "y");
            HashSet<string> replacelist = new HashSet<string>();
            replacelist.Add("a");
            replacelist.Add("b");
            replacelist.Add("A");
            replacelist.Add("c");
            replacelist.Add("C");
            t.ReplaceDependents("y", replacelist);
            Assert.AreEqual(6, t.Size);


        }




        /// <summary>
        ///Non-empty graph contains something
        ///</summary>
        [TestMethod()]
        public void SizeTest2()
        {
            DependencyGraph t = new DependencyGraph();
            t.AddDependency("a", "b");
            t.AddDependency("a", "c");
            t.AddDependency("c", "b");
            t.AddDependency("b", "d");

            IEnumerator<string> e = t.GetDependees("a").GetEnumerator();
            Assert.IsFalse(e.MoveNext());

            e = t.GetDependees("b").GetEnumerator();
            Assert.IsTrue(e.MoveNext());
            String s1 = e.Current;
            Assert.IsTrue(e.MoveNext());
            String s2 = e.Current;
            Assert.IsFalse(e.MoveNext());
            Assert.IsTrue(((s1 == "a") && (s2 == "c")) || ((s1 == "c") && (s2 == "a")));

            e = t.GetDependees("c").GetEnumerator();
            Assert.IsTrue(e.MoveNext());
            Assert.AreEqual("a", e.Current);
            Assert.IsFalse(e.MoveNext());

            e = t.GetDependees("d").GetEnumerator();
            Assert.IsTrue(e.MoveNext());
            Assert.AreEqual("b", e.Current);
            Assert.IsFalse(e.MoveNext());
        }


        /// <summary>
        ///Non-empty graph contains something
        ///</summary>
        [TestMethod()]
        public void SizeTest3()
        {
            DependencyGraph t = new DependencyGraph();
            t.AddDependency("a", "b");
            t.AddDependency("a", "c");
            t.AddDependency("a", "b");
            t.AddDependency("c", "b");
            t.AddDependency("b", "d");
            t.AddDependency("c", "b");
            Assert.AreEqual(4, t.Size);
        }





        /// <summary>
        ///Non-empty graph contains something
        ///</summary>
        [TestMethod()]
        public void SizeTest4()
        {
            DependencyGraph t = new DependencyGraph();
            t.AddDependency("x", "y");
            t.AddDependency("a", "b");
            t.AddDependency("a", "c");
            t.AddDependency("a", "d");
            t.AddDependency("c", "b");
            t.RemoveDependency("a", "d");
            t.AddDependency("e", "b");
            t.AddDependency("b", "d");
            t.RemoveDependency("e", "b");
            t.RemoveDependency("x", "y");
            Assert.AreEqual(4, t.Size);
        }




        [TestMethod()]
        public void addingDependencySameItemSizeTest()
        {
            DependencyGraph t = new DependencyGraph();
            t.AddDependency("x", "y");
            t.AddDependency("x", "y");
            Assert.AreEqual(1, t.Size);
        }



        /// <summary>
        ///add dependencies and then remove it and try to see if the dg is empty
        ///</summary>
        [TestMethod()]
        public void AddRemoveAddTest()
        {
            DependencyGraph t = new DependencyGraph();
            t.AddDependency("x", "y");
            t.RemoveDependency("x", "y");
            t.AddDependency("x", "y");
            IEnumerator<string> e = t.GetDependees("y").GetEnumerator();

            e.MoveNext();
            String s1 = e.Current;
            Assert.AreEqual(s1, "x");
        }






        /// <summary>
        ///Non-empty graph contains something
        ///</summary>
        [TestMethod()]
    public void SizeTest5()
    {
      DependencyGraph t = new DependencyGraph();
      t.AddDependency("x", "b");
      t.AddDependency("a", "z");
      t.ReplaceDependents("b", new HashSet<string>());
      t.AddDependency("y", "b");
      t.ReplaceDependents("a", new HashSet<string>() { "c" });
      t.AddDependency("w", "d");
      t.ReplaceDependees("b", new HashSet<string>() { "a", "c" });
      t.ReplaceDependees("d", new HashSet<string>() { "b" });

      IEnumerator<string> e = t.GetDependees("a").GetEnumerator();
      Assert.IsFalse(e.MoveNext());

      e = t.GetDependees("b").GetEnumerator();
      Assert.IsTrue(e.MoveNext());
      String s1 = e.Current;
      Assert.IsTrue(e.MoveNext());
      String s2 = e.Current;
      Assert.IsFalse(e.MoveNext());
      Assert.IsTrue(((s1 == "a") && (s2 == "c")) || ((s1 == "c") && (s2 == "a")));

      e = t.GetDependees("c").GetEnumerator();
      Assert.IsTrue(e.MoveNext());
      Assert.AreEqual("a", e.Current);
      Assert.IsFalse(e.MoveNext());

      e = t.GetDependees("d").GetEnumerator();
      Assert.IsTrue(e.MoveNext());
      Assert.AreEqual("b", e.Current);
      Assert.IsFalse(e.MoveNext());
    }



    /// <summary>
    ///Using lots of data
    ///</summary>
    [TestMethod()]
    public void StressTest()
    {
      // Dependency graph
      DependencyGraph t = new DependencyGraph();

      // A bunch of strings to use
      const int SIZE = 200;
      string[] letters = new string[SIZE];
      for (int i = 0; i < SIZE; i++)
      {
        letters[i] = ("" + (char)('a' + i));
      }

      // The correct answers
      HashSet<string>[] dents = new HashSet<string>[SIZE];
      HashSet<string>[] dees = new HashSet<string>[SIZE];
      for (int i = 0; i < SIZE; i++)
      {
        dents[i] = new HashSet<string>();
        dees[i] = new HashSet<string>();
      }

      // Add a bunch of dependencies
      for (int i = 0; i < SIZE; i++)
      {
        for (int j = i + 1; j < SIZE; j++)
        {
          t.AddDependency(letters[i], letters[j]);
          dents[i].Add(letters[j]);
          dees[j].Add(letters[i]);
        }
      }

      // Remove a bunch of dependencies
      for (int i = 0; i < SIZE; i++)
      {
        for (int j = i + 4; j < SIZE; j += 4)
        {
          t.RemoveDependency(letters[i], letters[j]);
          dents[i].Remove(letters[j]);
          dees[j].Remove(letters[i]);
        }
      }

      // Add some back
      for (int i = 0; i < SIZE; i++)
      {
        for (int j = i + 1; j < SIZE; j += 2)
        {
          t.AddDependency(letters[i], letters[j]);
          dents[i].Add(letters[j]);
          dees[j].Add(letters[i]);
        }
      }

      // Remove some more
      for (int i = 0; i < SIZE; i += 2)
      {
        for (int j = i + 3; j < SIZE; j += 3)
        {
          t.RemoveDependency(letters[i], letters[j]);
          dents[i].Remove(letters[j]);
          dees[j].Remove(letters[i]);
        }
      }

      // Make sure everything is right
      for (int i = 0; i < SIZE; i++)
      {
        Assert.IsTrue(dents[i].SetEquals(new HashSet<string>(t.GetDependents(letters[i]))));
        Assert.IsTrue(dees[i].SetEquals(new HashSet<string>(t.GetDependees(letters[i]))));
      }
    }

  }
}



