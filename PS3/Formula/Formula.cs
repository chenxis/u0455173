// Skeleton written by Joe Zachary for CS 3500, September 2013
// Read the entire skeleton carefully and completely before you
// do anything else!

// Version 1.1 (9/22/13 11:45 a.m.)

// Change log:
//  (Version 1.1) Repaired mistake in GetTokens
//  (Version 1.1) Changed specification of second constructor to
//                clarify description of how validation works

// (Daniel Kopta) 
// Version 1.2 (9/10/17) 

// Change log:
//  (Version 1.2) Changed the definition of equality with regards
//                to numeric tokens

//Author Chenxi Sun Uid u0455173
using System;
using System.Collections.Generic;

using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace SpreadsheetUtilities
{
    /// <summary>
    /// Represents formulas written in standard infix notation using standard precedence
    /// rules.  The allowed symbols are non-negative numbers written using double-precision 
    /// floating-point syntax; variables that consist of a letter or underscore followed by 
    /// zero or more letters, underscores, or digits; parentheses; and the four operator 
    /// symbols +, -, *, and /.  
    /// 
    /// Spaces are significant only insofar that they delimit tokens.  For example, "xy" is
    /// a single variable, "x y" consists of two variables "x" and y; "x23" is a single variable; 
    /// and "x 23" consists of a variable "x" and a number "23".
    /// 
    /// Associated with every formula are two delegates:  a normalizer and a validator.  The
    /// normalizer is used to convert variables into a canonical form, and the validator is used
    /// to add extra restrictions on the validity of a variable (beyond the standard requirement 
    /// that it consist of a letter or underscore followed by zero or more letters, underscores,
    /// or digits.)  Their use is described in detail in the constructor and method comments.
    /// </summary>
    /// 


    public class Formula
    {

        private HashSet<string> normalizeditems;
        private List<string> itemsList;


        /// <summary>
        /// Creates a Formula from a string that consists of an infix expression written as
        /// described in the class comment.  If the expression is syntactically invalid,
        /// throws a FormulaFormatException with an explanatory Message.
        /// 
        /// The associated normalizer is the identity function, and the associated validator
        /// maps every string to true.  
        /// </summary>
        public Formula(String formula) :
            this(formula, s => s, s => true)
        {
        }

        /// <summary>
        /// Creates a Formula from a string that consists of an infix expression written as
        /// described in the class comment.  If the expression is syntactically incorrect,
        /// throws a FormulaFormatException with an explanatory Message.
        /// 
        /// The associated normalizer and validator are the second and third parameters,
        /// respectively.  
        /// 
        /// If the formula contains a variable v such that normalize(v) is not a legal variable, 
        /// throws a FormulaFormatException with an explanatory message. 
        /// 
        /// If the formula contains a variable v such that isValid(normalize(v)) is false,
        /// throws a FormulaFormatException with an explanatory message.
        /// 
        /// Suppose that N is a method that converts all the letters in a string to upper case, and
        /// that V is a method that returns true only if a string consists of one letter followed
        /// by one digit.  Then:
        /// 
        /// new Formula("x2+y3", N, V) should succeed
        /// new Formula("x+y3", N, V) should throw an exception, since V(N("x")) is false
        /// new Formula("2x+y3", N, V) should throw an exception, since "2x+y3" is syntactically incorrect.
        /// </summary>
        public Formula(String formula, Func<string, string> normalize, Func<string, bool> isValid)
        {

            if (formula == String.Empty || ReferenceEquals(formula, null)) // throw if the formula string is empty or null
            {
                throw new FormulaFormatException("your formula is empty or null");
            }

            normalizeditems = new HashSet<string>();
            itemsList = new List<string>(GetTokens(formula));

            int OpenParathensisCounter = 0;
            int CloseParathensisCounter = 0;

            string firstitem = itemsList.First<string>();
            string lastitem = itemsList.Last<string>();
            string prev = "";

            if (!Double.TryParse(firstitem, out double number) && !variablechecker(firstitem) && !firstitem.Equals("("))
            {
                throw new FormulaFormatException("the first item in the formula have to be an operand, variable, or open parentheisis");
            }

            if (!Double.TryParse(lastitem, out double number2) && !variablechecker(lastitem) && !lastitem.Equals(")"))
            {
                throw new FormulaFormatException("the last item in the formula have to be a double, variable, or a closed parenthesis ");
            }

            for (int i = 0; i < itemsList.Count; i++)
            {
                string curr = itemsList[i];

                if (curr.Equals("("))
                {
                    OpenParathensisCounter++;

                }
                else if (curr.Equals(")"))
                {
                    CloseParathensisCounter++;
                    if (CloseParathensisCounter > OpenParathensisCounter)
                    {
                        throw new FormulaFormatException("the number of closing parenthesis is greater than the number of open parenthesis");
                    }
                }
                else if (justOperators(curr))
                {

                }
                else if (Double.TryParse(itemsList[i], out number))
                {
                    string item = number.ToString(); //turn the double to a string and added back to the list
                    itemsList[i] = item;             //this will limit double precision to 6th digit instead of 16th digit

                }
                else if (!variablechecker(curr))
                {
                    throw new FormulaFormatException("the variable is invalid");
                }

                if (OperatorsOrOpenParanthesis(prev))
                {
                    if (!NumberVariableOpenParanthesis(curr))
                        throw new FormulaFormatException("an open parenthesis must be followed by an operand or open parenthesis");
                }
                else if (NumberVariableClosingParenthesis(prev))
                {
                    if (!OperatorsOrClosingParanthesis(curr))
                        throw new FormulaFormatException("A closed parenthesis must be followed by an operator or closed parentheis");
                }






                prev = curr;// will put prev as previous current character
            }
            if (OpenParathensisCounter != CloseParathensisCounter)
            {
                throw new FormulaFormatException("the number of open parenthesis is not equal to the number of closed parenthesis so the formula is unbalanced");
            }


            for (int i = 0; i < itemsList.Count; i++)
            {

                if (variablechecker(itemsList[i])) // only normalize variables
                {
                    if (!variablechecker(normalize(itemsList[i])))// if the normalized is not variable throw
                    {
                        throw new FormulaFormatException("The normalized item is not in correct variable format");
                    }
                    if (!isValid(normalize(itemsList[i])))// if not valid throw
                    {
                        throw new FormulaFormatException("The normalized item is not an valid item according to the functor");
                    }
                    else
                    {
                        itemsList[i] = normalize(itemsList[i]);// put normalized back itno the normalizeditems set
                        normalizeditems.Add(itemsList[i]);
                    }
                }
            }



        }






        /// <summary>
        /// Helper function that will check if the string a is a number variable or an ( open parenthesis
        /// </summary>
        /// <param name="a"></param>
        /// <returns>bool</returns>
        private bool NumberVariableOpenParanthesis(string a)
        {
            if (variablechecker(a) || a.Equals("(") || Double.TryParse(a, out double number))
            {
                return true;
            }
            else
            {
                return false;
            }


        }



        /// <summary>
        /// 
        /// Helper function that will check if string a is an operators or open parenthesis so +,-,*,/ or (
        /// </summary>
        /// <param name="a"></param>
        /// <returns>bool</returns>

        private bool OperatorsOrOpenParanthesis(string a)
        {
            if (a.Equals("(") || a.Equals("+") || a.Equals("-") || a.Equals("*") || a.Equals("/"))
            {
                return true;
            }
            else
            {
                return false;
            }

        }

        /// <summary>
        /// 
        /// Helper function that check if string a is an operators or closing parenthesis so check for ), +, -, *, /
        /// </summary>
        /// <param name="a"></param>
        /// <returns></returns>
        private bool OperatorsOrClosingParanthesis(string a)
        {
            if (a.Equals(")") || a.Equals("+") || a.Equals("-") || a.Equals("*") || a.Equals("/"))
            {
                return true;
            }
            else
            {
                return false;
            }

        }

        /// <summary>
        /// 
        /// Helper method that check if a string is a double, ) or a variable
        /// </summary>
        /// <param name="a"></param>
        /// <returns>bool</returns>
        private bool NumberVariableClosingParenthesis(string a)
        {
            if (variablechecker(a) || Double.TryParse(a, out double number) || a.Equals(")"))
            {
                return true;
            }
            return false;

        }


        /// <summary>
        /// Helper method that check if string a is just an operator of +, -, * or /
        /// </summary>
        /// <param name="a"></param>
        /// <returns>bool</returns>
        private bool justOperators(string a)
        {
            if (a.Equals("+") || a.Equals("-") || a.Equals("*") || a.Equals("/"))
            {
                return true;

            }
            return false;
        }

        /// <summary>
        /// Variable checker that checks if a string is a variable through regex checking for variable of letter followed by a number
        /// </summary>
        /// <param name="item"></param>
        /// <returns>bool</returns>
        private bool variablechecker(string item)
        {
            if (Regex.IsMatch(item, @"[a-zA-Z_](?: [a-zA-Z_]|\d)*", RegexOptions.Singleline))
                return true;
            else return false;
        }



        /// <summary>
        /// Evaluates this Formula, using the lookup delegate to determine the values of
        /// variables.  When a variable symbol v needs to be determined, it should be looked up
        /// via lookup(normalize(v)). (Here, normalize is the normalizer that was passed to 
        /// the constructor.)
        /// 
        /// For example, if L("x") is 2, L("X") is 4, and N is a method that converts all the letters 
        /// in a string to upper case:
        /// 
        /// new Formula("x+7", N, s => true).Evaluate(L) is 11
        /// new Formula("x+7").Evaluate(L) is 9
        /// 
        /// Given a variable symbol as its parameter, lookup returns the variable's value 
        /// (if it has one) or throws an ArgumentException (otherwise).
        /// 
        /// If no undefined variables or divisions by zero are encountered when evaluating 
        /// this Formula, the value is returned.  Otherwise, a FormulaError is returned.  
        /// The Reason property of the FormulaError should have a meaningful explanation.
        ///
        /// This method should never throw an exception.
        /// </summary>
        public object Evaluate(Func<string, double> lookup)
        {
            Stack<string> operators = new Stack<string>();
            Stack<double> operands = new Stack<double>();
            string[] substrings = itemsList.Cast<string>().ToArray<string>();

            for (int i = 0; i < substrings.Length; i++)
            {

                if (substrings[i].Equals("*") || substrings[i].Equals("/") || substrings[i].Equals("("))
                {
                    operators.Push(substrings[i]);
                }
                else if (substrings[i].Equals("+") || substrings[i].Equals("-") || substrings[i].Equals(")"))
                {
                    string top = "";
                    if (operators.Count > 0)
                    {
                        top = operators.Peek();
                    }
                    if (top.Equals("+") || top.Equals("-"))
                    {

                        operands.Push(operation(operators.Pop(), operands.Pop(), operands.Pop()));
                    }
                    if (substrings[i].Equals(")"))
                    {
                        string newtop = "";
                        if (operators.Count > 0)
                        {
                            newtop = operators.Peek();
                        }

                        operators.Pop();
                        if (operators.Count > 0)
                        {
                            newtop = operators.Peek(); // put string to check for operators.peek() due to stack exception when peeking too much
                        }
                        if (newtop.Equals("*") || newtop.Equals(("/")))
                        {

                            if (operands.Peek() == 0 && newtop.Equals("/"))
                            {
                                return new FormulaError("division by zero"); // divided by zero return object FormulaError
                            }
                            operands.Push(operation(operators.Pop(), operands.Pop(), operands.Pop()));
                        }
                    }
                    else
                    {
                        operators.Push(substrings[i]); //+ or - sign push it back into operators stack
                    }
                }
                else if (Double.TryParse(substrings[i], out double value))
                {
                    string top = "";
                    if (operators.Count > 0)
                    {
                        top = operators.Peek();
                    }
                    if (top.Equals("*") || top.Equals("/"))
                    {
                        if (value == 0 && top.Equals("/"))
                        {
                            return new FormulaError("division by zero");
                        }
                        operands.Push(operation(operators.Pop(), value, operands.Pop()));
                    }
                    else
                    {
                        operands.Push(value);
                    }
                }
                else                 //must be a variable
                {
                    string variable = substrings[i];
                    double number;
                    try
                    {
                        number = lookup(variable);
                    }
                    catch (ArgumentException e)
                    {
                        return new FormulaError("the variable is not a valid variable according to the lookup functor");
                    }


                    double variablevalue = lookup(substrings[i]);
                    string top = "";
                    if (operators.Count > 0)
                    {
                        top = operators.Peek();
                    }
                    if (top.Equals("*") || top.Equals("/"))
                    {
                        if (variablevalue == 0 && top.Equals("/"))
                        {
                            return new FormulaError("division by zero");
                        }
                        operands.Push(operation(operators.Pop(), variablevalue, operands.Pop()));
                    }
                    else
                    {
                        operands.Push(variablevalue);
                    }
                }
            }
            if (operators.Count == 0)
            {

                return operands.Pop();
            }
            else
            {

                while (operators.Count != 0)
                {
                    operands.Push(operation(operators.Pop(), operands.Pop(), operands.Pop()));
                }
                return operands.Pop();
            }
        }


        /// <summary>
        /// Enumerates the normalized versions of all of the variables that occur in this 
        /// formula.  No normalization may appear more than once in the enumeration, even 
        /// if it appears more than once in this Formula.
        /// 
        /// For example, if N is a method that converts all the letters in a string to upper case:
        /// 
        /// new Formula("x+y*z", N, s => true).GetVariables() should enumerate "X", "Y", and "Z"
        /// new Formula("x+X*z", N, s => true).GetVariables() should enumerate "X" and "Z".
        /// new Formula("x+X*z").GetVariables() should enumerate "x", "X", and "z".
        /// </summary>
        public IEnumerable<String> GetVariables()
        {
            HashSet<string> normalizeditems2 = new HashSet<string>(normalizeditems);


            return normalizeditems2;
        }

        /// <summary>
        /// Helper method that will do the operation on operand 2 and operand 1 with operation of string st
        /// this will return a double once the operation is complete
        /// </summary>
        /// <param name="st"></param>
        /// <param name="operand2"></param>
        /// <param name="operand1"></param>
        /// <returns>double</returns>
        private double operation(string st, double operand2, double operand1)
        {
            if (st.Equals("+"))
            {
                return operand1 + operand2;
            }
            else if (st.Equals("-"))
            {
                return operand1 - operand2;
            }
            else if (st.Equals("*"))
            {
                return operand1 * operand2;
            }
            else if (st.Equals("/"))
            {

                return operand1 / operand2;
            }
            return 0;
        }



        /// <summary>
        /// Returns a string containing no spaces which, if passed to the Formula
        /// constructor, will produce a Formula f such that this.Equals(f).  All of the
        /// variables in the string should be normalized.
        /// 
        /// For example, if N is a method that converts all the letters in a string to upper case:
        /// 
        /// new Formula("x + y", N, s => true).ToString() should return "X+Y"
        /// new Formula("x + Y").ToString() should return "x+Y"
        /// </summary>
        public override string ToString()
        {
            string formulastring = "";
            for (int i = 0; i < itemsList.Count; i++)
            {
                formulastring += itemsList[i];
            }



            return formulastring;
        }

        /// <summary>
        /// If obj is null or obj is not a Formula, returns false.  Otherwise, reports
        /// whether or not this Formula and obj are equal.
        /// 
        /// Two Formulae are considered equal if they consist of the same tokens in the
        /// same order.  To determine token equality, all tokens are compared as strings 
        /// except for numeric tokens and variable tokens.
        /// Numeric tokens are considered equal if they are equal after being "normalized" 
        /// by C#'s standard conversion from string to double, then back to string. This 
        /// eliminates any inconsistencies due to limited floating point precision.
        /// Variable tokens are considered equal if their normalized forms are equal, as 
        /// defined by the provided normalizer.
        /// 
        /// For example, if N is a method that converts all the letters in a string to upper case:
        ///  
        /// new Formula("x1+y2", N, s => true).Equals(new Formula("X1  +  Y2")) is true
        /// new Formula("x1+y2").Equals(new Formula("X1+Y2")) is false
        /// new Formula("x1+y2").Equals(new Formula("y2+x1")) is false
        /// new Formula("2.0 + x7").Equals(new Formula("2.000 + x7")) is true
        /// </summary>
        public override bool Equals(object obj)
        {
            if (ReferenceEquals(obj, null) || obj.GetType() != this.GetType())
            {
                return false;
            }
            Formula objFormula = (Formula)obj;
            int length = 0;

            if (this.itemsList.Count > objFormula.itemsList.Count)
            {
                return false;
            }
            else if (this.itemsList.Count < objFormula.itemsList.Count)

            {
                return false;
            }
            else
            {
                length = this.itemsList.Count;
            }


            for (int i = 0; i < length; i++)
            {
                string objstring = objFormula.itemsList[i];
                string thisstring = this.itemsList[i];


                if (Double.TryParse(objstring, out double objnumber) && Double.TryParse(thisstring, out double thisnumber))
                {

                    if (thisnumber != objnumber)
                    {
                        return false;
                    }
                }
                else if(!thisstring.Equals(objstring))
                {
                   
                        return false;
                  
                }


            }


            return true;
        }

        /// <summary>
        /// Reports whether f1 == f2, using the notion of equality from the Equals method.
        /// Note that if both f1 and f2 are null, this method should return true.  If one is
        /// null and one is not, this method should return false.
        /// </summary>
        public static bool operator ==(Formula f1, Formula f2)
        {
            if (ReferenceEquals(f1, null) && ReferenceEquals(f2, null)) //use referenceEquals due to == will cause infinite loop due to recursion
            {
                return true;
            }
            else if (ReferenceEquals(f1, null) && !ReferenceEquals(f2, null))
            {
                return false;
            }
            else if (ReferenceEquals(f2, null) && !ReferenceEquals(f1, null))
            {
                return false;
            }
            else
            {
                return f1.Equals(f2);
            }





        }

        /// <summary>
        /// Reports whether f1 != f2, using the notion of equality from the Equals method.
        /// Note that if both f1 and f2 are null, this method should return false.  If one is
        /// null and one is not, this method should return true.
        /// </summary>
        public static bool operator !=(Formula f1, Formula f2)
        {
            if (!(f1 == f2))
            {
                return true;
            }


            return false;
        }

        /// <summary>
        /// Returns a hash code for this Formula.  If f1.Equals(f2), then it must be the
        /// case that f1.GetHashCode() == f2.GetHashCode().  Ideally, the probability that two 
        /// randomly-generated unequal Formulae have the same hash code should be extremely small.
        /// </summary>
        public override int GetHashCode()
        {
            int HashCode = this.ToString().GetHashCode();
            return HashCode;
        }

        /// <summary>
        /// Given an expression, enumerates the tokens that compose it.  Tokens are left paren;
        /// right paren; one of the four operator symbols; a string consisting of a letter or underscore
        /// followed by zero or more letters, digits, or underscores; a double literal; and anything that doesn't
        /// match one of those patterns.  There are no empty tokens, and no token contains white space.
        /// </summary>
        private static IEnumerable<string> GetTokens(String formula)
        {
            // Patterns for individual tokens
            String lpPattern = @"\(";
            String rpPattern = @"\)";
            String opPattern = @"[\+\-*/]";
            String varPattern = @"[a-zA-Z_](?: [a-zA-Z_]|\d)*";
            String doublePattern = @"(?: \d+\.\d* | \d*\.\d+ | \d+ ) (?: [eE][\+-]?\d+)?";
            String spacePattern = @"\s+";

            // Overall pattern
            String pattern = String.Format("({0}) | ({1}) | ({2}) | ({3}) | ({4}) | ({5})",
                                            lpPattern, rpPattern, opPattern, varPattern, doublePattern, spacePattern);

            // Enumerate matching tokens that don't consist solely of white space.
            foreach (String s in Regex.Split(formula, pattern, RegexOptions.IgnorePatternWhitespace))
            {
                if (!Regex.IsMatch(s, @"^\s*$", RegexOptions.Singleline))
                {
                    yield return s;
                }
            }

        }
    }

    /// <summary>
    /// Used to report syntactic errors in the argument to the Formula constructor.
    /// </summary>
    public class FormulaFormatException : Exception
    {
        /// <summary>
        /// Constructs a FormulaFormatException containing the explanatory message.
        /// </summary>
        public FormulaFormatException(String message)
            : base(message)
        {
        }
    }





    /// <summary>
    /// Used as a possible return value of the Formula.Evaluate method.
    /// </summary>
    public struct FormulaError
    {
        /// <summary>
        /// Constructs a FormulaError containing the explanatory reason.
        /// </summary>
        /// <param name="reason"></param>
        public FormulaError(String reason)
            : this()
        {
            Reason = reason;
        }

        /// <summary>
        ///  The reason why this FormulaError was created.
        /// </summary>
        public string Reason { get; private set; }
    }
}
