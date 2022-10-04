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

        private static char GetOperation(Operation operation)
            // Helps with parsing the calculation strings
        {
            if (operation == Operation.Add)
                return '+';
            if (operation == Operation.Subtract)
                return '-';
            if (operation == Operation.Multiply)
                return '×';
            if (operation == Operation.Divide)
                return '÷';
            else return '?';
        }

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

                // Loop through the operations list and store the indices of any multiplications and divisions
                for (int i = 1; i < operationsCopy.Count; i++)
                {
                    if (operationsCopy[i] == Operation.Multiply || operationsCopy[i] == Operation.Divide)
                    {
                        multiplicationAndDivisionIndices.Add(i);
                        numbersCopy[i - 1] = PerformOperation(numbersCopy[i - 1], operationsCopy[i], numbersCopy[i]); // Also perform those operations
                    }
                }

                if (multiplicationAndDivisionIndices.Count > 0) // If we found any multiplication or division...
                {
                    multiplicationAndDivisionIndices.Reverse(); // ...reverse the order of the list of indices...

                    foreach (int i in multiplicationAndDivisionIndices) // ...so the items with those indices can safely be removed
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