using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MathParser.Console
{
    class Program
    {

        static void Main(string[] args)
        {
            bool keepGoing = true;
            while (keepGoing)
            {
                keepGoing = ProcessEquation();
            }

            System.Console.WriteLine();
            System.Console.Write("Press any key to exit...");
            System.Console.ReadKey(true);
        }

        static bool ProcessEquation()
        {
            System.Console.WriteLine("Enter your equation:");

            Tuple<string, bool> result = GetInputString();
            if (!result.Item2)
            {
                return false;
            }

            string equation = result.Item1;

            try
            {
                decimal value = Parser.EvaluateExpression(equation);
                System.Console.WriteLine();
                System.Console.WriteLine("Evaluates to:  {0}", value);
            }
            catch (Exception ex)
            {
                ConsoleColor defaultColor = System.Console.ForegroundColor;
                System.Console.ForegroundColor = ConsoleColor.Red;
                System.Console.WriteLine();
                System.Console.Write(ex);
                System.Console.ForegroundColor = defaultColor;
            }
            System.Console.WriteLine();
            System.Console.WriteLine();
            System.Console.WriteLine();
            System.Console.WriteLine();

            return true;
        }

        static Tuple<string, bool> GetInputString()
        {
            string expression = System.Console.ReadLine();
            return new Tuple<string, bool>(expression, true);
        }

    }
}
