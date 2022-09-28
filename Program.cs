using System.Data;

namespace Calculator
{
    internal class Program
    {
        static void Main(/*string[] args*/)
        {
            Console.WriteLine("Welcome to Group 8's calculator!" +
                "\nPress any key to begin!");
            Console.ReadKey();

            while (true)
            {
                Console.Clear();
                Console.Write("What would you like to do?" +
                    "\n1. Make a calculation" +
                    "\n2. Show calulation history" +
                    "\n3. Toggle order of operations (now ");

                (Console.ForegroundColor, Console.BackgroundColor) = (Console.BackgroundColor, Console.ForegroundColor);
                Console.Write(Operator.UseLinearOrderOfOperations ? "linear →" : "×÷+-");
                (Console.ForegroundColor, Console.BackgroundColor) = (Console.BackgroundColor, Console.ForegroundColor);

                Console.WriteLine(")" + "\n0. Exit the calculator");

                Console.Write(": ");
                char menuSelection = Console.ReadKey().KeyChar;
                
                switch (menuSelection)
                {
                    case '0':
                        return;

                    case '1':
                        Calculation();
                        break;

                    case '2':
                        ShowHistory();
                        break;

                    case '3':
                        Operator.UseLinearOrderOfOperations = !Operator.UseLinearOrderOfOperations;
                        break;

                    default:
                        Console.Clear();
                        break;
                }
                
            }
        }

        static void Calculation()
        {
            Calculation calculation = new();

            Console.Clear();
            Console.WriteLine("Let's count up your crimes!");

            while (true)
            {
                Console.Write("\nInput your first number: ");
                if (!decimal.TryParse(Console.ReadLine(), out decimal number))
                {
                    Console.WriteLine("ERROR: NaN (not a number)");
                    continue;
                }

                calculation.AddOperation(number, Operation.Null);
                break;
            }

            while (true)
            {
                Console.WriteLine();
                Console.WriteLine("What operation do you want to perform?" +
                    "\n1. Addition (+)         2. Subtraction (-)" +
                    "\n3. Multiplication (×)   4. Division (÷)" +
                    "\n0. Done!");
                Console.Write(": ");

                char operationChar = Operator.ConvertInput(Console.ReadKey().KeyChar);

                if (!int.TryParse(operationChar.ToString(), out int operationSelection) || operationSelection > Operator.OperationEnumMax)
                {
                    Console.WriteLine("\nERROR: Invalid input!");
                    continue;
                }

                if (operationSelection == 0) { Operator.Calculations.Add(calculation); break; }

                Console.WriteLine($"\n\nWhat number do you want to {Operator.OperationPreposition(operationSelection)}?");

                while (true)
                {
                    Console.Write(": ");
                    if (!decimal.TryParse(Console.ReadLine(), out decimal number))
                    {
                        Console.WriteLine("ERROR: NaN (not a number)");
                        continue;
                    }

                    if (number == 0 && (Operation)operationSelection == Operation.Divide)
                    {
                        Console.WriteLine("ERROR: Can't divide by zero");
                        continue;
                    }

                    calculation.AddOperation(number, (Operation)operationSelection);

                    Console.WriteLine(calculation.GetCalculation());
                    break;
                }

            }
        }
        
        static void ShowHistory()
        {
            Console.Clear();

            if (Operator.Calculations.Count == 0)
            {
                Console.WriteLine("No calculation history available.");
            }
            else
            {
                Console.WriteLine("Calculation history:" +
                    "\n====================");
                foreach (Calculation calculation in Operator.Calculations)
                {
                    Console.WriteLine(calculation.GetCalculation());
                }
            }

            Console.WriteLine();
            Console.Write("Press any key to continue... ");
            Console.ReadKey();
        }
    }
}