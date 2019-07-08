// Skeleton implementation written by Joe Zachary for CS 3500, September 2013.
// Version 1.1 (Fixed error in comment for RemoveDependency.)
// Version 1.2 - Daniel Kopta 
//               (Clarified meaning of dependent and dependee.)
//               (Clarified names in solution/project structure.)



//Author Chenxi Sun uid u0455173
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;

namespace SpreadsheetUtilities
{

    /// <summary>
    /// (s1,t1) is an ordered pair of strings
    /// t1 depends on s1; s1 must be evaluated before t1
    /// 
    /// A DependencyGraph can be modeled as a set of ordered pairs of strings.  Two ordered pairs
    /// (s1,t1) and (s2,t2) are considered equal if and only if s1 equals s2 and t1 equals t2.
    /// Recall that sets never contain duplicates.  If an attempt is made to add an element to a 
    /// set, and the element is already in the set, the set remains unchanged.
    /// 
    /// Given a DependencyGraph DG:
    /// 
    ///    (1) If s is a string, the set of all strings t such that (s,t) is in DG is called dependents(s).
    ///        (The set of things that depend on s)    
    ///        
    ///    (2) If s is a string, the set of all strings t such that (t,s) is in DG is called dependees(s).
    ///        (The set of things that s depends on) 
    //
    // For example, suppose DG = {("a", "b"), ("a", "c"), ("b", "d"), ("d", "d")}
    //     dependents("a") = {"b", "c"}
    //     dependents("b") = {"d"}
    //     dependents("c") = {}
    //     dependents("d") = {"d"}
    //     dependees("a") = {}
    //     dependees("b") = {"a"}
    //     dependees("c") = {"a"}
    //     dependees("d") = {"b", "d"}
    /// </summary>
    public class DependencyGraph
    {
        /// <summary>
        /// Creates an empty DependencyGraph.
        /// </summary>
        /// 




        private Dictionary<string, HashSet<string>> dependees;          //set to private to protect data
        private Dictionary<string, HashSet<string>> dependents;
  

        public DependencyGraph()
        {
            dependees = new Dictionary<string, HashSet<string>>();
           
            dependents = new Dictionary<string, HashSet<string>>();
           


        }


        /// <summary>
        /// The number of ordered pairs in the DependencyGraph.
        /// </summary>
        public int Size
        {
            get { return dependees.Sum(item => item.Value.Count); } //this will return size as the number of sets in the dependees hashmap
        }                                                           //this is using lambda so it is returning all the keys hashset items number 
                                                                    //So it is returning the numbers of items in all the hashset int the hashmap


        /// <summary>
        /// The size of dependees(s).
        /// This property is an example of an indexer.  If dg is a DependencyGraph, you would
        /// invoke it like this:
        /// dg["a"]
        /// It should return the size of dependees("a")
        /// </summary>
        public int this[string s]
        {
            

            get {
                if (!dependents.ContainsKey(s)) //the number of dependees is stored in the dependents( dependents name) hashmap
                    return 0;

                return dependents[s].Count(); }
        }


        /// <summary>
        /// Reports whether dependents(s) is non-empty.
        /// </summary>
        public bool HasDependents(string s)
        {
            if (!dependees.ContainsKey(s)) //the number of dependents is stored in the dependees( dependees name) hashmap
            {
                return false;
            }

            return true;
        }


        /// <summary>
        /// Reports whether dependees(s) is non-empty.
        /// </summary>
        public bool HasDependees(string s)
        {
            if (!dependents.ContainsKey(s))
            {
                return false;
            }

            return true;
        }


        /// <summary>
        /// Enumerates dependents(s).
        /// </summary>
        public IEnumerable<string> GetDependents(string s)
        {
            if (dependees.ContainsKey(s))
            {
                HashSet<string> list = new HashSet<string>(dependees[s]); //A copy needs to be returned since IEnumerable is immutable so this copies 
                wrapper listwrapper = new wrapper(list);                  //a wrapper needs to be created to store the new changed set of dependees
                                                                          //this will protect the internal data of the original set so user can't change
                                                                          // it
                return listwrapper;                                       // return the wrapper out
            }
            else
            {
                HashSet<string> emptyset = new HashSet<string>();         //if the dependees does not contain s return a newly created empty set since 
                wrapper listwrapper = new wrapper(emptyset);              // original can't be changed or returned
                return emptyset;
            }


        }

        /// <summary>
        /// Enumerates dependees(s).
        /// </summary>
        public IEnumerable<string> GetDependees(string s)
        {
            if (dependents.ContainsKey(s))
            {
                HashSet<string> list = new HashSet<string>(dependents[s]);          //same as above copy the dependents[s] hashset for dependees(s)
                wrapper listwrapper = new wrapper(list);                            //so you can return it with the updated version instead of the immutable IEnumerable
             

                return listwrapper;
            }
            else
            {
                HashSet<string> emptyset = new HashSet<string>();
                wrapper listwrapper = new wrapper(emptyset);
                return emptyset;
            }
        }


        /// <summary>
        /// <para>Adds the ordered pair (s,t), if it doesn't exist</para>
        /// 
        /// <para>This should be thought of as:</para>   
        /// 
        ///   t depends on s
        ///
        /// </summary>
        /// <param name="s"> s must be evaluated first. T depends on S</param>
        /// <param name="t"> t cannot be evaluated until s is</param>        /// 
        public void AddDependency(string s, string t)
        {
     


            if (dependees.ContainsKey(s))
            {
                dependees[s].Add(t);
            }
            else if (!dependees.ContainsKey(s))                     //if dependees does not contain the key of dependees key matched to dependents list create
            {                                                       //one and add the dependee in 
                HashSet<string> dependents = new HashSet<string>();
                dependents.Add(t);
                dependees.Add(s, dependents);
            }
            if (dependents.ContainsKey(t))
            {
                dependents[t].Add(s);
            }
            else if (!dependents.ContainsKey(t))
            {
                HashSet<string> dependees = new HashSet<string>();
                dependees.Add(s);
                dependents.Add(t, dependees);
            }




        }


        /// <summary>
        /// Removes the ordered pair (s,t), if it exists
        /// </summary>
        /// <param name="s"></param>
        /// <param name="t"></param>
        public void RemoveDependency(string s, string t)
        {
          
            if (dependents.ContainsKey(t)){
                dependents[t].Remove(s);
                if (dependents[t].Count == 0)
                {
                    dependents.Remove(t);
                }

            }
            if (dependees.ContainsKey(s))
            {
                dependees[s].Remove(t);
                if (dependees[s].Count == 0)
                {
                    dependees.Remove(s);
                }
            }



        }


        /// <summary>
        /// Removes all existing ordered pairs of the form (s,r).  Then, for each
        /// t in newDependents, adds the ordered pair (s,t).
        /// </summary>
        public void ReplaceDependents(string s, IEnumerable<string> newDependents)
        {
            IEnumerable<string> oldDependents = GetDependents(s);
            foreach(string item in oldDependents)
            {
                RemoveDependency(s,item);
            }

            
            foreach(string item in newDependents)
            {
                AddDependency(s, item);
            }






        }


        /// <summary>
        /// Removes all existing ordered pairs of the form (r,s).  Then, for each 
        /// t in newDependees, adds the ordered pair (t,s).
        /// </summary>
        public void ReplaceDependees(string s, IEnumerable<string> newDependees)
        {
            IEnumerable<string> oldDependencees = GetDependees(s);
            foreach (string item in oldDependencees)
            {
                RemoveDependency(item,s);
            }

            
            foreach (string item in newDependees)
            {
                AddDependency(item,s);
            }



        }

        /// <summary>
        /// wrapper class for HashSet that is extended as IEnumerable that will keep the user from changing
        /// the internal data itself so there are no add or remove 
        /// 
        /// </summary>
        private class  wrapper:IEnumerable<string>
        {
            private HashSet<string> wrapperset;             //pass the hashset as private into the wrapper a copy will be created when returned
            public wrapper(HashSet<string> set)
            {
                wrapperset = set;
            }

            public IEnumerator<string> GetEnumerator()
            {


              return wrapperset.GetEnumerator();
            }

            IEnumerator IEnumerable.GetEnumerator()   //inherited .getEnumerator() method will return wrapperset.GetEnumerator
            {


                return this.GetEnumerator();
            }

        }
  


}



        

}