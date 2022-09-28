namespace Calculator
{
    struct Calculation
    {
        private readonly List<decimal> numbers;
        private readonly List<Operation> operations;

        public Calculation()
        {
            numbers = new List<decimal>();
            operations = new List<Operation>();
        }

        public bool AddOperation(decimal number, Operation operation)
        {
            if (number == 0 && operation == Operation.Divide)
            {
                Console.WriteLine("Can't divide by zero!");
                return false;
            }

            numbers.Add(number);
            operations.Add(operation);

            return true;
        }

        private static char GetOperation(Operation operation)
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
        {
            Operation.Add => operand1 + operand2,
            Operation.Subtract => operand1 - operand2,
            Operation.Multiply => operand1 * operand2,
            Operation.Divide => operand1 / operand2,
            _ => throw new ArgumentOutOfRangeException(nameof(operation), $"Not a valid operation: {operation}."),
        };

        public string GetCalculation()
        {
            // Make sure the lists don't somehow have different lengths
            if (numbers.Count == operations.Count)
            {
                if (numbers.Count == 0)
                    return "(no values)";

                decimal[] numberArray = numbers.ToArray();
                Operation[] operationArray = operations.ToArray();

                string calculation = numberArray[0].ToString();

                for (int i = 1; i < numberArray.Length; i++)
                {
                    calculation += $" {GetOperation(operationArray[i])} {numberArray[i]}";
                }

                return $"{calculation} = {GetResult()}";
            }
            else
            {
                throw new Exception("Queue members in a calculation instance somehow have different sizes!");
            }
        }

        public decimal GetResult() {
            // Make sure the queues don't somehow have different lengths
            if (numbers.Count == operations.Count)
            {
                if (numbers.Count == 0)
                    return 0m;
                                
                List<decimal> numbersCopy = new(numbers);
                List<Operation> operationsCopy = new(operations);
                decimal result = numbersCopy[0];

                if (!Operator.UseLinearOrderOfOperations)
                {
                    List<int> multiplicationAndDivisionIndices = new();

                    for (int i = 1; i < operationsCopy.Count; i++)
                    {
                        if (operationsCopy[i] == Operation.Multiply || operationsCopy[i] == Operation.Divide)
                        {
                            multiplicationAndDivisionIndices.Add(i);
                            numbersCopy[i - 1] = PerformOperation(numbersCopy[i - 1], operationsCopy[i], numbersCopy[i]);
                        }
                    }

                    if (multiplicationAndDivisionIndices.Count > 0)
                    {
                        multiplicationAndDivisionIndices.Reverse();

                        foreach (int i in multiplicationAndDivisionIndices)
                        {
                            numbersCopy.RemoveAt(i);
                            operationsCopy.RemoveAt(i);
                        }

                    }
                }

                for (int i = 1; i < numbersCopy.Count; i++)
                {
                    result = PerformOperation(result, operationsCopy[i], numbersCopy[i]);
                }

                return result;
            }
            else
            {
                throw new Exception("Queue members in a calculation instance somehow have different sizes!");
            }
        }
    }
}