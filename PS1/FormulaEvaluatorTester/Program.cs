using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FormulaEvaluator
{
    class Program
    {
        static void Main(string[] args)
        {

            if (!FormulaEvaluator.EvaluatorTest.InvalidTest("1+-3",FormulaEvaluator.Evaluator.NumberLookUp)) 
            Console.WriteLine("test1 failed");

            if (!FormulaEvaluator.EvaluatorTest.InvalidTest("A4-209(*(3+2*3 A4))", FormulaEvaluator.Evaluator.NumberLookUp)) 
            Console.WriteLine("test2 failed");

            if (!FormulaEvaluator.EvaluatorTest.InvalidTest("2+3-", FormulaEvaluator.Evaluator.NumberLookUp)) 
            Console.WriteLine("test3 failed");

            if (!FormulaEvaluator.EvaluatorTest.InvalidTest("2+3-A$", FormulaEvaluator.Evaluator.NumberLookUp)) 
            Console.WriteLine("test4 failed");

            if (!FormulaEvaluator.EvaluatorTest.InvalidTest("2+(3-A7*)", FormulaEvaluator.Evaluator.NumberLookUp)) 
            Console.WriteLine("test5 failed");

            if (!FormulaEvaluator.EvaluatorTest.InvalidTest("2+-*3-A7*)", FormulaEvaluator.Evaluator.NumberLookUp)) 
            Console.WriteLine("test6 failed");

            if (!FormulaEvaluator.EvaluatorTest.InvalidTest("2+3-88 9", FormulaEvaluator.Evaluator.NumberLookUp)) 
            Console.WriteLine("test7 failed");

            if (!FormulaEvaluator.EvaluatorTest.InvalidTest("2+3-88 A4", FormulaEvaluator.Evaluator.NumberLookUp))
                Console.WriteLine("test8 failed");

            if (!FormulaEvaluator.EvaluatorTest.InvalidTest("2+3-88-9+", FormulaEvaluator.Evaluator.NumberLookUp))
                Console.WriteLine("test9 failed");
            if (!FormulaEvaluator.EvaluatorTest.InvalidTest("A4-209(*(3+2*3)+)", FormulaEvaluator.Evaluator.NumberLookUp))
                Console.WriteLine("test10 failed");
            if (!FormulaEvaluator.EvaluatorTest.InvalidTest("A4-209(*(3+2*(3+6))", FormulaEvaluator.Evaluator.NumberLookUp))
                Console.WriteLine("test10 failed");

            if (!FormulaEvaluator.EvaluatorTest.InvalidTest("A4-209/(3-3)", FormulaEvaluator.Evaluator.NumberLookUp))
                Console.WriteLine("test12 failed");

            
            Console.WriteLine("All Invalid Tests Completed");

            if (!FormulaEvaluator.EvaluatorTest.ValidTest("1 + 3 * 4 / 2 - 6", FormulaEvaluator.Evaluator.NumberLookUp, 1))
                Console.WriteLine("test1 failed");
            if (!FormulaEvaluator.EvaluatorTest.ValidTest("A4-209*((3+2)*3)", FormulaEvaluator.Evaluator.NumberLookUp, -3128))
                Console.WriteLine("test2 failed");
            if (!FormulaEvaluator.EvaluatorTest.ValidTest("1+3*(4/2)-6", FormulaEvaluator.Evaluator.NumberLookUp, 1))
                Console.WriteLine("test3 failed");
            if (!FormulaEvaluator.EvaluatorTest.ValidTest("1+3*4/2-6", FormulaEvaluator.Evaluator.NumberLookUp, 1))
                Console.WriteLine("test4 failed");
            if (!FormulaEvaluator.EvaluatorTest.ValidTest("3*12/(2*2)", FormulaEvaluator.Evaluator.NumberLookUp, 9))
                Console.WriteLine("test5 failed");
            if (!FormulaEvaluator.EvaluatorTest.ValidTest("3*(15-8*5)+4/2", FormulaEvaluator.Evaluator.NumberLookUp, -73))
                Console.WriteLine("test6 failed");
            if (!FormulaEvaluator.EvaluatorTest.ValidTest("3+15-8*5+4/2", FormulaEvaluator.Evaluator.NumberLookUp, -20))
                Console.WriteLine("test7 failed");
            if (!FormulaEvaluator.EvaluatorTest.ValidTest("A4*A4*12/(2*2)", FormulaEvaluator.Evaluator.NumberLookUp, 147))
                Console.WriteLine("test8 failed");







            Console.WriteLine("All Valid Tests Completed");

            if(!FormulaEvaluator.EvaluatorTest.LookupTest("A4",7))
                Console.WriteLine("test1 failed");
            if (!FormulaEvaluator.EvaluatorTest.LookupTest("C1", 2))
                Console.WriteLine("test2 failed");
            if (!FormulaEvaluator.EvaluatorTest.LookupTest("A1", 14))
                Console.WriteLine("test1 failed");
            if (!FormulaEvaluator.EvaluatorTest.LookupTest("A2", 20))
                Console.WriteLine("test1 failed");
            if (!FormulaEvaluator.EvaluatorTest.LookupTest("C2", 3))
                Console.WriteLine("test1 failed");
            if (!FormulaEvaluator.EvaluatorTest.LookupTest("C3", 5))
                Console.WriteLine("test1 failed");
            Console.WriteLine("All LookUp Tests Completed");


          
            Console.ReadLine();



            


























        }
    }
}
