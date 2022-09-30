namespace Miniräknare
{
    internal class Calculator
    {
        
        private bool gameLoop = true;
        private double firstNum = 0;
        private double secondNum = 0;
        private string stroperator = "";
        private double result;
        private string strFirstNum;
        List<string> history = new List<string>();

        public void CalculatorStart()
        {
            Console.SetWindowSize(60, 30);
            Console.WriteLine("==Welcome to the amazing CALCULATOR!==");
            
            while (gameLoop)
            {
                Console.Title = "THE AMAZING CALCULATOR";
                Input();
                Calc();
                OutPut();
                ShowingHistory();
               
            }
        }
        public void Input()
        {
            Console.WriteLine("\n>Enter a number to start the calculation<");
            strFirstNum = Console.ReadLine();
            while (!double.TryParse(strFirstNum, out firstNum))
            {
                if (strFirstNum == " ")
                {
                    Console.WriteLine("That is not a valide input\nEnter in Again");
                    strFirstNum = Console.ReadLine();
                }
                 
                else
                {
                    Console.WriteLine("That is not a valide input\nEnter in Again");
                    strFirstNum = Console.ReadLine();                    
                }
            }
            while (true)
            {
                Console.WriteLine(">Enter in an operator<");
                stroperator = Console.ReadLine();
                if (stroperator == "+" || stroperator == "-" || stroperator == "*" || stroperator==  "/")
                {
                    break;
                }
            }
            while (true)
            {
                try
                {
                    Console.WriteLine(">Enter in the second number<");
                    secondNum = Convert.ToDouble(Console.ReadLine());
                    break;
                }
                catch (FormatException)
                {
                    Console.WriteLine("You did not enter in a number");

                }
            }
           
        }
        public void Calc()
        {
            switch (stroperator)
            {
                case ("+"):
                    result = firstNum + secondNum;

                    break;

                case ("-"):
                    result = firstNum - secondNum;

                    break;

                case ("*"):
                    result = firstNum * secondNum;

                    break;

                case ("/"):
                    if (secondNum == 0)
                    {
                        Console.WriteLine("Cant divide by zero, try again!");

                    }
                    result = firstNum / secondNum;
                    break;
            }
        }
        public void OutPut()
        {
            Console.WriteLine($"Your awsner is {firstNum} {stroperator} {secondNum} = {result}".ToString());
            string total = $"{firstNum} {stroperator} {secondNum} = {result}";
            history.Add(total);
        }
        public void ShowingHistory()
        {
            Console.WriteLine("press [1] to show the history or Enter to Do another calculation\n Type EXIT to quit!");
            if (Console.ReadLine() == "1")
            {
                Color();
                for (int i = 0; i < history.Count; i++)
                {
                    
                    Console.WriteLine(history[i]);
                    
                }
                ResetColor();
                Console.WriteLine("press any key to Continue or type EXIT to close");

                if (Console.ReadLine().ToUpper() == "EXIT")
                {
                    Console.ResetColor();
                    gameLoop = false;
                }
                Console.Clear();
            }
            else if (Console.ReadLine().ToUpper() == "EXIT")
            {
                gameLoop = false;
            }
        }
        public void Color()
        {
            Console.ForegroundColor = ConsoleColor.Green;

        }
        public void ResetColor()
        {
            Console.ResetColor();
        }
    }

}
