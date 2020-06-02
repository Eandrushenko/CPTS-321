using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;

namespace CptS321
{
    public class ExpTree
    {
        protected string regex;
        protected Node root;
        public Dictionary<string, double> variables = new Dictionary<string, double> { };



        public ExpTree()
        {
            regex = "";
            root = null;
            variables = new Dictionary<string, double> { };
        }

        public string rregex
        {
            get
            {
                return regex;
            }
            set
            {
                regex = value;
            }
        }
        public Node rroot
        {
            get
            {
                return root;
            }
            set
            {
                root = value;
            }
        }

        //https://en.wikipedia.org/wiki/Shunting-yard_algorithm
        public Queue<string> postfix()
        {
            Dictionary<string, int> weights = new Dictionary<string, int> { { "+", 1 }, { "-", 1 }, { "*", 2 }, { "/", 2 }, { "^", 3 }, { "(", 5 }, { ")", 4 } };

            string pattern = @"\w+|\+|\-|\*|\/|\^|\(|\)";
            string item = " ";
            Stack<string> operators = new Stack<string>();
            Queue<string> patternQueue = new Queue<string>();
            Queue<string> result = new Queue<string>();

            Match match = Regex.Match(regex, pattern);
            //Put everything in a queue
            while (match.Success)
            {
                patternQueue.Enqueue(match.ToString());
                match = match.NextMatch();
            }
            //Put into postfix
            while (patternQueue.Count() > 0)
            {
                item = patternQueue.Dequeue();
                if (item == "+" || item == "-" || item == "*" || item == "/" || item == "^" || item == "(" || item == ")")
                {
                    if ((item != "(") && (item != ")"))
                    {
                        while ((operators.Count > 0) && ((weights[item] <= weights[operators.Peek()]) && (item != "^")) && (operators.Peek() != "("))
                        {
                            result.Enqueue(operators.Pop());
                        }
                        operators.Push(item);
                    }
                    if (operators.Count == 0)
                    {
                        operators.Push(item);
                    }
                    else if (item == "(")
                    {
                        operators.Push(item);
                    }
                    else if (item == ")")
                    {
                        while (operators.Peek() != "(")
                        {
                            result.Enqueue(operators.Pop());
                        }
                        operators.Pop();
                    }
                }
                else
                {
                    result.Enqueue(item);
                }
            }
            while (operators.Count() > 0)
            {
                result.Enqueue(operators.Pop());
            }
            return result;
        }
        //https://www.geeksforgeeks.org/expression-tree/
        //1) If character is operand push that into stack
        //2) If character is operator pop two values from stack make them its child and push current node again.
        //At the end only element of stack will be root of expression tree.
        public ExpTree(string expression)
        {  
            opNode opTemp = new opNode();
            varNode varTemp = new varNode();
            numNode numTemp = new numNode();

            regex = expression;
            Queue<string> post = new Queue<string>();
            post = postfix();

            Stack<Node> expbuilder = new Stack<Node>();
            string item = "";
            char op;
            double num;

            var isNumeric = double.TryParse(item, out num);

            while (post.Count > 0)
            {

                item = post.Dequeue();
                if (item == "+" || item == "-" || item == "*" || item == "/" || item == "^")
                {
                    opTemp = new opNode();
                    op = item[0];
                    opTemp.eexp = op;
                    opTemp.lleft = expbuilder.Pop();
                    opTemp.rright = expbuilder.Pop();
                    expbuilder.Push(opTemp);
                }
                else
                {
                    isNumeric = double.TryParse(item, out num);
                    if (isNumeric)
                    {
                        numTemp = new numNode();
                        numTemp.nnumber = num;
                        expbuilder.Push(numTemp);
                    }
                    else
                    {
                        varTemp = new varNode();
                        varTemp.nname = item;
                        expbuilder.Push(varTemp);
                        //Added in Version 4 of the spreadsheet for finding Cells with var names
                        if (variables.ContainsKey(varTemp.nname))
                        {
                            variables[varTemp.nname] = 0;
                        }
                        else
                        {
                            variables.Add(varTemp.nname, 0);
                        }
                    }
                }
            }
            //Added to handle if the expression is just a number or variable like "A1" or just "5"
            if (expbuilder.Peek().GetType() == varTemp.GetType())
            {
                root = new varNode();
                root = expbuilder.Pop();
            }
            else if (expbuilder.Peek().GetType() == numTemp.GetType())
            {
                root = new numNode();
                root = expbuilder.Pop();
            }
            else if (expbuilder.Peek().GetType() == opTemp.GetType())
            {
                root = new opNode();
                root = expbuilder.Pop();
            }
            else
            {
                //ERROR!
            }
        }

        //Made this function as an approach to finding the varNode for setVar type im keeping it here as reference to myself
        private void inorderroot(opNode localroot)
        {
            opNode sample = new opNode();
            Type t = sample.GetType();
            if (!(localroot == null))
            {
                if (t == localroot.lleft.GetType())
                {
                    inorderroot((opNode)localroot.lleft);
                }
                else
                {
                    Console.Write("{0} ", localroot.lleft.nnumber);
                    Console.Write("{0} ", localroot.nnumber);
                }
                if (t == localroot.rright.GetType())
                {
                    inorderroot((opNode)localroot.rright);
                }
                else
                {
                    Console.Write("{0} ", localroot.rright.nnumber);
                }
            }
        }
        
        public void inorder()
        {
            opNode sample = new opNode();
            Type t = sample.GetType();
            if (root.GetType() != t)
            {
                Console.Write("{0} ", root.nnumber);
                return;
            }
            else
            {
                inorderroot((opNode)this.rroot);
            }
        }

        //Inorder search of the varNode type, and after found checking is varName matches
        private void setVarroot(opNode localroot, string varName, double varValue)
        {
            opNode sample = new opNode();
            Type t = sample.GetType();

            varNode sample2 = new varNode();
            Type t2 = sample2.GetType();
            if (!(localroot == null))
            {
                if (t == localroot.lleft.GetType())
                {
                    setVarroot((opNode)localroot.lleft, varName, varValue);
                }
                else
                {
                    if(t2 == localroot.lleft.GetType())
                    {
                        sample2 = (varNode)localroot.lleft;
                        if (sample2.nname == varName)
                        {
                            sample2.nnumber = varValue;
                            //localroot.lleft = sample2;
                        }
                    }
                }
                if (t == localroot.rright.GetType())
                {
                    setVarroot((opNode)localroot.rright, varName, varValue);
                }
                else
                {
                    if (t2 == localroot.rright.GetType())
                    {
                        sample2 = (varNode)localroot.rright;
                        if (sample2.nname == varName)
                        {
                            sample2.nnumber = varValue;
                           //localroot.rright = sample2;
                        }
                    }
                }
            }
        }

        //Remade without root
        public void setVar(string varName, double varValue)
        {
            //Put Variables in a Dictionary
            if (variables.ContainsKey(varName))
            {
                variables[varName] = varValue;
            }
            else
            {
                variables.Add(varName, varValue);
            }
            //Check if regex is just a variable like "A1" thus root would be a varnode
            opNode sample = new opNode();
            Type t = sample.GetType();

            varNode sample2 = new varNode();
            Type t2 = sample2.GetType();
            if (root.GetType() == t2)
            {
                sample2 = (varNode)root;
                if (sample2.nname == varName)
                {
                    sample2.nnumber = varValue;
                    root.nnumber = varValue;
                }
                return;
            }
            else if (root.GetType() == t)
            {
                setVarroot((opNode)this.rroot, varName, varValue);
            }
        }

        //Auxillary function for Expressions to make cleaner code for Evalroot()
        private double AuxEval(opNode localroot)
        {
            if (localroot.eexp == '+')
            {
                return localroot.nnumber = (localroot.rright.nnumber) + (localroot.lleft.nnumber);
            }
            else if (localroot.eexp == '-')
            {
                return localroot.nnumber = (localroot.rright.nnumber) - (localroot.lleft.nnumber);
            }
            else if (localroot.eexp == '*')
            {
                return localroot.nnumber = (localroot.rright.nnumber) * (localroot.lleft.nnumber);
            }
            else if (localroot.eexp == '/')
            {
                return localroot.nnumber = (localroot.rright.nnumber) / (localroot.lleft.nnumber);
            }
            else if (localroot.eexp == '^')
            {
                return localroot.nnumber = Math.Pow((localroot.rright.nnumber), (localroot.lleft.nnumber));
            }
            else
            {
                return 0;
            }
        }

        //inorder traversal and evaluation of nodes based on type
        private double Evalroot(opNode localroot)
        {
            opNode sample = new opNode();
            Type t = sample.GetType();

            if (!(localroot == null))
            {
                if (t == localroot.lleft.GetType())
                {
                    Evalroot((opNode)localroot.lleft);
                }
                else
                {
                    AuxEval(localroot);
                }
                if (t == localroot.rright.GetType())
                {
                    Evalroot((opNode)localroot.rright);
                }
                else
                {
                    AuxEval(localroot);
                }
            }
            AuxEval(localroot);
            return localroot.nnumber;
        }

        //removed root to meet assignment specifications
        public double Eval()
        {
            opNode sample = new opNode();
            Type t = sample.GetType();
            //Special case where regex has no operators and is just a variable or number
            if (root.GetType() != t)
            {
                return root.nnumber;
            }
            else
            {
                return Evalroot((opNode)this.root);
            }
        }

    }
}
