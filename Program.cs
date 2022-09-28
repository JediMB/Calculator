using System.Data;

namespace Calculator
{
    internal class Program
    {
        static void Main(/*string[] args*/)
        {
            Queue<Calculation> calculations = new();
            bool exit = false;

            Console.WriteLine("Welcome to Group 8's calculator!" +
                "\nPress any key to begin!");
            Console.ReadKey();


            while (!exit)
            {
                Console.Clear();
                Console.WriteLine("What would you like to do?" +
                    "\n1. Make a calculation" +
                    "\n2. Show calulation history" +
                    "\n0. Exit the calculator");

                Console.Write(": ");
                char menuSelection = Console.ReadKey().KeyChar;
                
                switch (menuSelection)
                {
                    case '0':
                        exit = true;
                        break;

                    case '1':
                        Calculation(calculations);
                        break;

                    case '2':
                        ShowHistory(calculations);
                        break;

                    default:
                        Console.Clear();
                        break;
                }
                
            }
        }

        static void Calculation(Queue<Calculation> calculations)
        {
            Calculation calculation = new();
            decimal result;

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
                result = number;
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


                if (!int.TryParse(Console.ReadKey().KeyChar.ToString(), out int operationSelection) || operationSelection > Operator.OperationEnumMax)
                {
                    Console.WriteLine("\nERROR: Invalid input!");
                    continue;
                }

                if (operationSelection == 0)
                {
                    calculations.Enqueue(calculation);
                    break;
                }

                string[] operationPreposition = { "add", "subtract by", "multiplay with", "divide by" };

                Console.WriteLine("\n");
                Console.WriteLine($"What number do you want to {operationPreposition[operationSelection-1]}?");

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

                    switch ((Operation)operationSelection)
                    {
                        case Operation.Add:
                            result += number;
                            break;
                        case Operation.Subtract:
                            result -= number;
                            break;
                        case Operation.Multiply:
                            result *= number;
                            break;
                        case Operation.Divide:
                            result /= number;
                            break;
                    }

                    calculation.AddOperation(number, (Operation)operationSelection);

                    Console.WriteLine(calculation.GetCalculation());
                    break;
                }

            }
        }

        static void ShowHistory(Queue<Calculation> calculations)
        {
            Console.Clear();

            if (calculations.Count == 0)
            {
                Console.WriteLine("No calculation history available.");
            }
            else
            {
                Console.WriteLine("Calculation history:" +
                    "\n====================");
                foreach (Calculation calculation in calculations)
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