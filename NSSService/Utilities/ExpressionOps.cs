//------------------------------------------------------------------------------
//----- ExpressionOps -------------------------------------------------------------
//------------------------------------------------------------------------------

//-------1---------2---------3---------4---------5---------6---------7---------8
//       01234567890123456789012345678901234567890123456789012345678901234567890
//-------+---------+---------+---------+---------+---------+---------+---------+

// copyright:   2014 WiM - USGS

//    authors:  Jeremy K. Newson USGS Wisconsin Internet Mapping
//             
// 
//   purpose:  parsing and calculating algorithm based on Shunting-yard's algorithm that parses mathematical 
//             expressions in infix notation.
//          
//discussion:  http://en.wikipedia.org/wiki/Shunting-yard_algorithm
//             tokens pushed in outputstacks 
//             operations pushed to tokenStack 
//             Infix notation :         3 + 4 * area / ( 1 − 5 ) ^ 2 ^ 3
//             Reverse Polish Notation: 3 4 area * 1 5 − 2 3 ^ ^ / +
//
//              Queue - A FIFO (first in, first out) list where you push records on top and pop them off the bottom
//              Stack - A LIFO (last in, first out) list where you push/pop records on top of each other.

#region "Comments"
//08.12.2014 jkn - Created
#endregion

#region "Imports"
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Text.RegularExpressions;
#endregion

namespace WiM.Utilities
{
    public class ExpressionOps
    {
        #region "Fields"
	    
        #endregion
        #region "Properties"
        
        public string InfixExpression { get; private set; }
        public String ReversePolishExpression { get; private set; }
        public Double Value { get; private set; }
        public Boolean IsValid { get; private set; }
        
        #endregion
        #region "Collections & Dictionaries"

        private IDictionary<OperationEnum, OperatorStruc> _operators;
        private Queue<String> outputQueue = new Queue<String>();
        private IDictionary<String, Double?> _variables;

        #endregion
        #region Constructors

        public ExpressionOps(string expr)
        {
            InfixExpression = expr;
            init();
            parseEquation();
            if (IsValid) evaluate();            
        }
        public ExpressionOps(String expr, Dictionary<String, Double?> variables)
        {
            InfixExpression = expr;
            init();

            _variables = variables;
            parseEquation();
            if (IsValid) evaluate();         
        }

        #endregion
        
        #region "Methods"
        
        #endregion
        #region "Helper Methods"

        private void init()
        {
            this.IsValid = false;

            _operators = new Dictionary<OperationEnum, OperatorStruc>();
            
            _operators.Add(OperationEnum.e_plus, new OperatorStruc( 1, AssociativityEnum.e_left));
            _operators.Add(OperationEnum.e_minus, new OperatorStruc(1, AssociativityEnum.e_left));
            _operators.Add(OperationEnum.e_multiply, new OperatorStruc(2, AssociativityEnum.e_left));
            _operators.Add(OperationEnum.e_divide, new OperatorStruc(2, AssociativityEnum.e_left));
            _operators.Add(OperationEnum.e_percent, new OperatorStruc(2, AssociativityEnum.e_left));
            _operators.Add(OperationEnum.e_exponent, new OperatorStruc(3, AssociativityEnum.e_right));

        }
        private void evaluate()
        {
            Stack<String> expressionStack = new Stack<String>();
            try
            {
                while (outputQueue.Count > 0)
                {
                    String operand = outputQueue.Dequeue();

                    switch (this.getTokenClass(operand))
                    {
                        case TokenClassEnum.e_value:
                            expressionStack.Push(operand);
                            break;
                        case TokenClassEnum.e_function:
                            String function = operand;
                            throw new NotImplementedException();
                        //for (int argNum = 0; argNum < function.NumArguments; ++argNum)
                        //{
                        //    function.Arguments.Add(expressionStack.Pop());
                        //}
                        //break;
                        case TokenClassEnum.e_operator:

                            String rightOperand = expressionStack.Pop();
                            String leftOperand = expressionStack.Pop();

                            var result = doOperation(Convert.ToDouble(leftOperand), Convert.ToDouble(rightOperand), getOperationEnum(operand));

                            expressionStack.Push(result.ToString());
                            break;
                        default:
                            throw new Exception(operand + " found in calculate");
                    }//end switch                     
                }//next

                if (expressionStack.Count != 1) new ArgumentException("Invalid formula");

                this.Value = Convert.ToDouble(expressionStack.Pop());
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        private void parseEquation()
        {
            Stack<String> tokenStack = new Stack<String>();
            try
            {
                //Dijkstra's Shunting Yard Algorithm
                Regex re = new Regex(@"([\+\-\*\(\)\^\/\ ])");
                List<String> tokenList = re.Split(InfixExpression).Select(t => t.Trim()).Where(t => t != "").ToList();
            
                adjustForNegative(ref tokenList);

                for (int tokenNumber = 0; tokenNumber < tokenList.Count(); ++tokenNumber)
                {
                    String token = tokenList[tokenNumber];
                    TokenClassEnum tokenClass = getTokenClass(token);

                    switch (tokenClass)
                    {
                        case TokenClassEnum.e_variable:
                            outputQueue.Enqueue(Convert.ToString(_variables[token].Value));
                            break;
                        case TokenClassEnum.e_value:                    
                            outputQueue.Enqueue(token);
                            break;
                        case TokenClassEnum.e_function:
                            tokenStack.Push(token);
                            break;
                        case TokenClassEnum.e_operator:
                            if (token == "-" && (tokenStack.Count == 0 || tokenList[tokenNumber - 1] == "("))
                            {
                                tokenStack.Push(token);
                                break;
                            }//end if

                            if (tokenStack.Count > 0)
                            {
                                String stackTopToken = tokenStack.Peek();
                                if (getTokenClass(stackTopToken) == TokenClassEnum.e_operator)
                                {
                                    //TODO: utilize _operators list
                                    AssociativityEnum tokenAssociativity = getOperatorAssociativity(token);
                                    int tokenPrecedence = getOperatorPrecedence(token);
                                    int stackTopPrecedence = getOperatorPrecedence(stackTopToken);

                                    if (tokenAssociativity == AssociativityEnum.e_left && tokenPrecedence <= stackTopPrecedence ||
                                            tokenAssociativity == AssociativityEnum.e_right && tokenPrecedence < stackTopPrecedence)
                                        outputQueue.Enqueue(tokenStack.Pop());                                
                                }//end if
                            }//end if
                            tokenStack.Push(token);
                            break;
                        case TokenClassEnum.e_leftparenthesis:
                            tokenStack.Push(token);
                            break;
                        case TokenClassEnum.e_rightparenthesis:
                            while (!(tokenStack.Peek().Equals("(")))
                            {
                                outputQueue.Enqueue(tokenStack.Pop());
                            }//next

                            tokenStack.Pop();

                            if (tokenStack.Count > 0 && this.getTokenClass(tokenStack.Peek()) == TokenClassEnum.e_function)
                                outputQueue.Enqueue(tokenStack.Pop()); 
                       
                            break;
                    }//end switch

                    if (tokenClass == TokenClassEnum.e_value || tokenClass == TokenClassEnum.e_rightparenthesis || tokenClass == TokenClassEnum.e_variable)
                    {
                        if (tokenNumber < tokenList.Count() - 1)
                        {
                            String nextToken = tokenList[tokenNumber + 1];
                            TokenClassEnum nextTokenClass = getTokenClass(nextToken);
                        
                            if (nextTokenClass != TokenClassEnum.e_operator && nextTokenClass != TokenClassEnum.e_rightparenthesis)
                                tokenList.Insert(tokenNumber + 1, "*");
                        
                        }//end if
                    }//end if

                }//next token

                queOperationStack(tokenStack);

                //---------------- set equation -----------------------
                ReversePolishExpression = String.Join(",", outputQueue.Select(t => t).ToArray());

                IsValid = true;
            }
            catch (Exception)
            {

                IsValid = false;
            }
        }
        private void queOperationStack(Stack<string> stack)
        {

            while (stack.Count > 0)
            {
                String operand = stack.Pop();
                if (this.getTokenClass(operand) == TokenClassEnum.e_leftparenthesis || this.getTokenClass(operand) == TokenClassEnum.e_rightparenthesis)
                {
                    IsValid = false;
                    throw new ArgumentException("Mismatched parentheses");
                }

                outputQueue.Enqueue(operand);
            }
        }
        private void adjustForNegative(ref List<string> tokenList)
        {

            for (int tokenNumber = 0; tokenNumber < tokenList.Count(); ++tokenNumber)
            {
                String token = tokenList[tokenNumber];
                if (getOperationEnum(token) == OperationEnum.e_minus && tokenNumber > 1 && (getTokenClass(tokenList[tokenNumber - 1]) == TokenClassEnum.e_operator || 
                                                                                            getTokenClass(tokenList[tokenNumber - 1]) == TokenClassEnum.e_rightparenthesis || 
                                                                                            getTokenClass(tokenList[tokenNumber - 1]) == TokenClassEnum.e_leftparenthesis))
                {
                    //remove neg from the list, and add it to the begin of next var
                    tokenList[tokenNumber + 1] = tokenList[tokenNumber] + tokenList[tokenNumber + 1];
                    tokenList.RemoveAt(tokenNumber);
              
                }//end if

            }//next
        }
        private AssociativityEnum getOperatorAssociativity(String token)
        {
            return _operators[getOperationEnum(token)].Associativity;
        }
        private int getOperatorPrecedence(String token)
        {
            return _operators[getOperationEnum(token)].Precedence;
        }
        private Double doOperation(Double val1, Double val2, OperationEnum operation)
        {
            switch (operation)
            {
                case OperationEnum.e_multiply:
                    return val1 * val2;
                case OperationEnum.e_divide:
                    return val1 / val2;
                case OperationEnum.e_percent:
                    return (int)val1 % (int)val2;
                case OperationEnum.e_plus:
                    return val1 + val2;
                case OperationEnum.e_minus:
                    return val1 - val2;
                case OperationEnum.e_exponent:
                    return (float)System.Math.Pow(val1, val2);
                default:
                    IsValid = false;
                    return 0;
            }
        }
        private bool isHigherPrecedence(string a, string b)
        {
            OperationEnum f = getOperationEnum(a);
            OperationEnum s = getOperationEnum(b);

            if (f >= s)
                return false;
            else
                return true;
        }
        private bool isOperator(string token)
        {
            OperationEnum oper = getOperationEnum(token);
            if (oper == OperationEnum.e_undefined)
                return false;
            return true;
        }
        private TokenClassEnum getTokenClass(string token)
        {
            double tempValue;
            if (double.TryParse(token, out tempValue) ||
                token.Equals("R", StringComparison.CurrentCultureIgnoreCase) ||
                token.Equals("S", StringComparison.CurrentCultureIgnoreCase))
            {
                return TokenClassEnum.e_value;
            }
            else if (token.Equals("sqrt", StringComparison.CurrentCultureIgnoreCase))
            {
                return TokenClassEnum.e_function;
            }
            else if (token == "(")
            {
                return TokenClassEnum.e_leftparenthesis;
            }
            else if (token == ")")
            {
                return TokenClassEnum.e_rightparenthesis;
            }
            else if (isOperator(token))
            {
                return TokenClassEnum.e_operator;
            }
            else
            {
                //lookup variable name
                try
                {
                    if (_variables[token].HasValue)
                        tempValue = Convert.ToDouble(_variables[token].Value);
                    return TokenClassEnum.e_variable;
                }
                catch (Exception)
                {
                    IsValid = false;
                    throw new Exception("token class unidentified");
                }
            }

        }
        private OperationEnum getOperationEnum(String a)
        {
            switch (a)
            {
                case "+":
                    return OperationEnum.e_plus;
                case "-":
                    return OperationEnum.e_minus;
                case "*":
                    return OperationEnum.e_multiply;
                case "/":
                    return OperationEnum.e_divide;
                case "^":
                    return OperationEnum.e_exponent;
                case"%":
                    return OperationEnum.e_percent;
                default:
                    return OperationEnum.e_undefined;
            }//end switch

        }

        #endregion
        #region Structures
        private struct OperatorStruc
        {
            public Int32 Precedence { get; set; }
            public AssociativityEnum Associativity { get; set; }

            public OperatorStruc(Int32 presidence, AssociativityEnum associativity):this() 
            {
                Precedence = presidence;
                Associativity = associativity;
            }
        }
        #endregion
        #region "Enumerated Constants"
        public enum TokenClassEnum
        {
            e_value,
            e_function,
            e_rightparenthesis,
            e_leftparenthesis,
            e_operator,
            e_variable
        }
        public enum OperationEnum 
        { 
            e_undefined =-1,
            e_plus = 1, 
            e_minus = 2, 
            e_multiply = 3, 
            e_divide = 4, 
            e_exponent = 5, 
            e_percent = 8
        };
        public enum AssociativityEnum
        {
            e_right,
            e_left
        }
        #endregion
        
    }//end class
}//end namespace
