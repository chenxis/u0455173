using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpreadsheetUtilities
{
    class Program
    {
        static void Main(string[] args)
        {
            DependencyGraph t = new DependencyGraph();
            t.AddDependency("x", "y");
            t.RemoveDependency("x", "y");
            t.AddDependency("x", "y");
            IEnumerator<string> e = t.GetDependees("y").GetEnumerator();

            e.MoveNext();
            String s1 = e.Current;
            Console.WriteLine(e.Current);
            Console.ReadLine();
        }

    }
}

