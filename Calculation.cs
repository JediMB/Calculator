namespace Calculator
{
    public struct Calculation
    {
        // Each calculation is split into lists of linked numbers and operations
        private readonly List<decimal> numbers;
        private readonly List<Operation> operations;

        public Calculation()
        {
            numbers = new List<decimal>();
            operations = new List<Operation>();
        }

        public static bool Load(List<Calculation> calculations)
        {
            if (File.Exists(Operator.saveFile))
            {
                string[] loadStrings = File.ReadAllLines(Operator.saveFile);

                foreach (string loadString in loadStrings)
                {
                    string[] subStrings = loadString.Split(' ');

                    if (!decimal.TryParse(subStrings[0], out decimal number))
                        return false;

                    Calculation calculation = new();

                    calculation.AddOperation(number, Operation.Null);

                    for (int i = 1; i < subStrings.Length; i++)
                    {
                        if (subStrings[i] == "=")
                            break;

                        Operation operation = GetOperation(subStrings[i][0]);

                        if (operation == Operation.Null)
                            break;

                        i++;

                        if (!decimal.TryParse(subStrings[i], out number))
                            break;

                        calculation.AddOperation(number, operation);
                    }

                    calculations.Add(calculation);
                }

                return true;
            }

            return false;
        }

        public static void Save(List<Calculation> calculations)
        {
            string[] saveStrings = new string[calculations.Count];

            for (int i = 0; i < calculations.Count; i++)
            {
                saveStrings[i] = calculations[i].GetCalculation();
            }

            File.WriteAllLines(Operator.saveFile, saveStrings);
        }

        private static char GetOperation(Operation operation) => operation switch
        // Helps with writing the calculation strings
        {
            Operation.Add => '+',
            Operation.Subtract => '-',
            Operation.Multiply => '×',
            Operation.Divide => '÷',
            _ => '?'
        };

        private static Operation GetOperation(char character) => character switch
        // Helps with reading the calculation strings
        {
            '+' => Operation.Add,
            '-' => Operation.Subtract,
            '×' => Operation.Multiply,
            '÷' => Operation.Divide,
            _ => Operation.Null
        };

        private static decimal PerformOperation(decimal operand1, Operation operation, decimal operand2) => operation switch
        // Performs an operation on two numbers on behalf of GetResult(). Learned how to use Switch Expressions today and love them
        {
            Operation.Add => operand1 + operand2,
            Operation.Subtract => operand1 - operand2,
            Operation.Multiply => operand1 * operand2,
            Operation.Divide => operand1 / operand2,
            _ => throw new ArgumentOutOfRangeException(nameof(operation), $"Not a valid operation: {operation}."),
        };

        public bool AddOperation(decimal number, Operation operation)
            // Adds a number and an operation type to the calculation
        {
            if (number == 0 && operation == Operation.Divide)
                return false;

            numbers.Add(number);
            operations.Add(operation);
            
            return true;
        }

        public string GetCalculation()
            // Create a string for printing purposes
        {
            if (numbers.Count != operations.Count)  // Make sure the lists don't somehow have different lengths
                throw new Exception("Queue members in a calculation instance somehow have different sizes!");
           
            if (numbers.Count == 0) // In case the lists are somehow empty
                return "(no values)";

            string calculation = numbers[0].ToString(); // Creates a string from the first number in the list

            for (int i = 1; i < numbers.Count; i++) // Appends the operators and numbers for the rest of the items in the lists
            {
                calculation += $" {GetOperation(operations[i])} {numbers[i]}";
            }

            return $"{calculation} = {GetResult()}";    // Returns the string and appends the calculated result
        }

        public decimal GetResult()
            // This is where we get results!
        {
            if (numbers.Count != operations.Count)  // Make sure the queues don't somehow have different lengths
                throw new Exception("Queue members in a calculation instance somehow have different sizes!");

            if (numbers.Count == 0) // In case the lists are somehow empty
                return 0m;
                
            // Copy the lists into temporary ones, so they can be safely manipulated
            List<decimal> numbersCopy = new(numbers);
            List<Operation> operationsCopy = new(operations);

            // If we're not using linear calculations, things get a bit complicated...
            if (!Operator.UseLinearOrderOfOperations)
            {
                List<int> multiplicationAndDivisionIndices = new();

                // Loop through the operations list in reverse order and store the indices of any multiplications and divisions
                for (int i = operationsCopy.Count-1; i > 0; i--)
                {
                    if (operationsCopy[i] == Operation.Multiply || operationsCopy[i] == Operation.Divide)
                    {
                        multiplicationAndDivisionIndices.Add(i);
                        numbersCopy[i - 1] = PerformOperation(numbersCopy[i - 1], operationsCopy[i], numbersCopy[i]); // Also perform those operations
                    }
                }

                if (multiplicationAndDivisionIndices.Count > 0) // If we found any multiplication or division...
                {
                    foreach (int i in multiplicationAndDivisionIndices) // ...remove those items from last to first
                    {
                        numbersCopy.RemoveAt(i);
                        operationsCopy.RemoveAt(i);
                    }
                }
            }

            decimal result = numbersCopy[0];

            // Lastly, perform any operations still remaining in the lists (which is all of them if using linear calculation)
            for (int i = 1; i < numbersCopy.Count; i++)
            {
                result = PerformOperation(result, operationsCopy[i], numbersCopy[i]);
            }

            return result;
        }
    }
}