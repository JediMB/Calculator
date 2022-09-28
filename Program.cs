using System.Data;

namespace Calculator
{
    internal class Program
    {
        static void Main(/*string[] args*/)
        {
            // En lista för att spara historik för räkningar (History.cs -> enum Operation -> struct History (Queue<Decimal>, Queue<Operation>))
            Queue<Calculation> calculations = new Queue<Calculation>();
            bool exit = false;

            // Välkomnande meddelande (cw)
            Console.WriteLine("Welcome to Group 8's calculator!");


            while (!exit)
            {
                Console.WriteLine("What would you like to do?" +
                    "\n1. Make a calculation" +
                    "\n2. Show calulation history" +
                    "\n0. Exit the calculator");

                Console.Write("\n:");
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
                        ShowHistory();
                        break;

                    default:
                        Console.Clear();
                        break;
                }
                
            }
            // Ifall användaren skulle dela 0 med 0 visa Ogiltig inmatning!
            // Lägga resultat till listan
            // Visa resultat
            // Fråga användaren om den vill visa tidigare resultat.
            // Visa tidigare resultat
            // Fråga användaren om den vill avsluta eller fortsätta
            Console.Write("\n\nPress any key to exit... ");
            Console.ReadKey();
        }

        static void Calculation(Queue<Calculation> calculations)
        {
            Calculation calculation = new();
            decimal result;

            Console.WriteLine("Let's count up your crimes!");

            // Användaren matar in tal och matematiska operation

            while (true)
            {
                // OBS! Användaren måsta mata in ett tal för att kunna ta sig vidare i programmet!
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
                Console.WriteLine("What operation do you want to perform?" +
                    "\n1. Addition (+)" +
                    "\n2. Subtraction (-)" +
                    "\n3. Multiplication (×)" +
                    "\n4. Division (÷)" +
                    "\n0. Nothing!");
                Console.Write(": ");


                if (!int.TryParse(Console.ReadKey().KeyChar.ToString(), out int operationSelection) || operationSelection > 4)
                {
                    Console.WriteLine("ERROR: Invalid input!");
                    continue;
                }

                if (operationSelection == 0)
                    break;

                Operation operation = (Operation)operationSelection;

                if (operation == Operation.Add)
                    Console.WriteLine("What number do you want to add?");
                else if (operation == Operation.Subtract)
                    Console.WriteLine("What number do you want to subtract by?");
                else if (operation == Operation.Multiply)
                    Console.WriteLine("What number do you want to multiply with?");
                else if (operation == Operation.Divide)
                    Console.WriteLine("What number do you want to divide by?");

                while (true)
                {
                    Console.Write(": ");
                    if (!decimal.TryParse(Console.ReadLine(), out decimal number))
                    {
                        Console.WriteLine("ERROR! NaN (not a number)");
                        continue;
                    }

                    if (number == 0 && operation == Operation.Divide)
                    {
                        Console.WriteLine("ERROR: Can't divide by zero");
                    }

                    switch (operation) // TODO: CONTINUE HERE!!
                    {
                        case Operation.Add:
                            break;
                        case Operation.Subtract:
                            break;
                        case Operation.Multiply:
                            break;
                        case Operation.Divide:
                            break;
                    }
                    
                    break;
                }

            }
        }

        static void ShowHistory()
        {

        }
    }
}