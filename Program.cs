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

                // Just some quick color-swapping for visual distinctiveness
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
                        Calculate();
                        break;

                    case '2':
                        ShowHistory();
                        break;

                    case '3': // Toggles calculation order
                        Operator.UseLinearOrderOfOperations = !Operator.UseLinearOrderOfOperations;
                        break;

                    default:
                        Console.Clear();
                        break;
                }
                
            }
        }

        static void Calculate()
        {
            Calculation calculation = new();    // The new calculation is added here

            Console.Clear();
            Console.WriteLine("Now, count up your crimes!");　//　さあ、お前の罪を数えろ！

            while (true)    // Make the user input a number first, and add it to the calculation object
            {
                Console.Write("\nInput your first number: ");
                if (!decimal.TryParse(Console.ReadLine(), out decimal number))
                {
                    Console.WriteLine("ERROR: NaN (not a number)");
                    continue;
                }

                calculation.AddOperation(number, Operation.Null);   // Null because no operation is performed with the first number
                break;
            }

            while (true)    // For the rest of the calculation, repeat this process:
            {
                Console.WriteLine();
                Console.WriteLine("What operation do you want to perform?" +
                    "\n1. Addition (+)             2. Subtraction (-)" +
                    "\n3. Multiplication (×) [*]   4. Division (÷) [/]" +
                    "\n0. Done! [ENTER]");
                Console.Write(": ");

                // 1) Let the user choose an operation from the list
                char operationChar = Operator.ConvertInput(Console.ReadKey().KeyChar);

                if (!int.TryParse(operationChar.ToString(), out int operationSelection) || operationSelection > Operator.OperationEnumMax)
                {
                    Console.WriteLine("\nERROR: Invalid input!");
                    continue;
                }

                // 1.5) Save the calculation to the history list if the user is DONE
                if (operationSelection == 0) { Operator.Calculations.Add(calculation); break; }

                Console.WriteLine($"\n\nWhat number do you want to {Operator.OperationPreposition(operationSelection)}?");

                while (true)    // 2) Let the user input another number
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

                    if(!calculation.AddOperation(number, (Operation)operationSelection))    // 3) Add the number and operation type to the calculation
                        Console.WriteLine("Can't divide by zero!"); // Error message if a divide by zero somehow snuck by (redundant)

                    Console.WriteLine(calculation.GetCalculation());    // Lastly, print out the calculation before going back to step 1
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