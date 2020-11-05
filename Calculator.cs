/************************************************************************
 * Core Topics:
 * 1. Method overloading.
 * 2. Method signature.
 ***********************************************************************/

using System;

namespace Calculator
{
    internal class Calculator
    {
        internal static int _objectCount;

        private int _totalCalculations = 0;

        static Calculator()
        {
            _objectCount = 0;
        }

        internal Calculator()
        {
            this._totalCalculations = 0;
            _objectCount++;
        }

        internal Calculator(Calculator original)
        {
            this._totalCalculations = original._totalCalculations;
            _objectCount++;
        }

        internal int Add(int i, int j)
        {
            int answer = i + j;
            _totalCalculations++;
            return answer;
        }

        // Change in number of parameters do make a different signature.
        internal int Add(int i, int j, int k)
        {
            int answer = Add(Add(i, j), k);
            _totalCalculations--;
            return answer;
        }

        // Change of return type does not mean a different signature.
        //internal void Add(int i, int j, int k)
        //{
        //    int answer = Add(Add(i, j), k);
        //    _totalCalculations--;
        //}

        // Parameter modifier (ref vs out vs none) changes do make a different signature in C#.
        internal int Add(int i, ref int j, int k)
        {
            int answer = Add(Add(i, j), k);
            _totalCalculations--;
            return answer;
        }

        // Different parameter data types do make a different signature.
        internal int Add(int i, int j, long k)
        {
            int answer = Add(Add(i, j), (int) k);
            _totalCalculations--;
            return answer;
        }

        // Change in parameter names does not mean a different signature.
        //internal int Add(int i, int j, int q)
        //{
        //    int answer = Add(Add(i, j), q);
        //    _totalCalculations--;
        //    return answer;
        //}

        // Change in access modifier does not mean a different signature.
        //public int Add(int i, int j, int k)
        //{
        //    int answer = Add(Add(i, j), k);
        //    _totalCalculations--;
        //    return answer;
        //}

        internal int Multiply(int i, int j)
        {
            int answer = i * j;
            _totalCalculations++;
            return answer;
        }
    }

    public class Tester
    {
        private static void DisplayMenu()
        {
            Console.WriteLine();
            Console.WriteLine("What do you want to do?");
            Console.WriteLine();
            Console.WriteLine("\t1. Add two numbers.");
            Console.WriteLine("\t3. Multiple two numbers.");
            Console.WriteLine("\t6. Add THREE numbers.");
            Console.WriteLine("\tN. Create a new Calculator.");
            Console.WriteLine("\tC. Copy Calculator to a new object.");
            Console.WriteLine("\tE. Exit.");
            Console.WriteLine();
        }

        private static void GetNumbers(out int num1, out int num2) 
        {
            Console.WriteLine();
            Console.Write("\nPlease enter your first number: ");
            num1 = int.Parse(Console.ReadLine());

            Console.Write("Please enter your second number: ");
            num2 = int.Parse(Console.ReadLine());
        }

        static void Main()
        {
            int answer = 0;
            int num1 = 0;
            int num2 = 0;
            bool keepGoing = true;
            Calculator copyCalculator;

            Calculator calculator = new Calculator();

            while (keepGoing)
            {
                DisplayMenu();
                Console.Write("Your choice? ");

                string response = (Console.ReadLine()).ToUpper();

                switch (response)
                {
                    case "1":
                        GetNumbers(out num1, out num2);
                        answer = calculator.Add(num1, num2);
                        Console.WriteLine
                            ("\n{0} + {1} = {2}", num1, num2, answer);
                        break;

                    case "3":
                        GetNumbers(out num1, out num2);
                        answer = calculator.Multiply(num1, num2);
                        Console.WriteLine
                            ("\n{0} * {1} = {2}", num1, num2, answer);
                        break;

                    case "6":
                        GetNumbers(out num1, out num2);
                        Console.Write("Please enter your third number: ");
                        int num3 = int.Parse(Console.ReadLine());

                        // Each of the following calls to the Add method calls a different method.
                        answer = calculator.Add(num1, num2, num3);
                        Console.WriteLine("\n{0} + {1} + {2} = {3}",
                            num1, num2, num3, answer);
                        answer = calculator.Add(num1, ref num2, num3);
                        Console.WriteLine("\n{0} + {1} + {2} = {3}",
                            num1, num2, num3, answer);
                        answer = calculator.Add(num1, num2, (long) num3);
                        Console.WriteLine("\n{0} + {1} + {2} = {3}",
                            num1, num2, num3, answer);

                        break;

                    case "N":
                        calculator = null;
                        calculator = new Calculator();
                        Console.WriteLine
                            ("\nA new calculator has been created.\n");
                        break;

                    case "C":
                        copyCalculator = new Calculator(calculator);
                        Console.WriteLine
                            ("\nA copy of the calculator has been made.\n");
                        break;

                    case "E":
                        keepGoing = false;
                        break;

                    default:
                        Console.WriteLine
                            ("\nERROR: Option {0} is not valid.\n",
                            response);
                        break;
                }
            }

            Console.WriteLine("\n{0} instance of Calculator were created.",
                Calculator._objectCount);

            Console.Write("\n\nPress <ENTER> to end: ");
            Console.ReadLine();
        }
    }
}
