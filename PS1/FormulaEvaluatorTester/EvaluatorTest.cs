using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FormulaEvaluator
{
    static class EvaluatorTest
    {






     


      
            public static bool InvalidTest(string ab, Evaluator.Lookup look)
        {
        

            try
            {

                Evaluator.Evaluate(ab, look);
                return false;
            }
            catch(ArgumentException)
            {
                return true;
            }

            catch(Exception)
            {
                return false;
            }



           
        }


        public static bool ValidTest(string ab, Evaluator.Lookup look, int expected)
        {

            try
            {
                
                return Evaluator.Evaluate(ab, look) == expected;
            }
            catch (Exception)
            {
                return false;


            }
           




        }
        public static bool LookupTest(string ab, int expected)
        {
            try
            {
                return Evaluator.NumberLookUp(ab)==expected;
            }
            catch (ArgumentException)
            {
                return false;
            }
        }

    }
}

