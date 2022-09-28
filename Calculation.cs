namespace Calculator
{
    struct Calculation
    {
        private Queue<decimal> numbers;
        private Queue<Operation> operations;

        public Calculation()
        {
            numbers = new Queue<decimal>();
            operations = new Queue<Operation>();
        }

        public bool AddOperation(decimal number, Operation operation)
        {
            if (number == 0 && operation == Operation.Divide)
            {
                Console.WriteLine("Can't divide by zero!");
                return false;
            }

            numbers.Enqueue(number);
            operations.Enqueue(operation);

            return true;
        }

        private char GetOperation(Operation operation)
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

        private decimal PerformOperation(decimal operand1, Operation operation, decimal operand2) => operation switch
        {
            Operation.Add => operand1 + operand2,
            Operation.Subtract => operand1 - operand2,
            Operation.Multiply => operand1 * operand2,
            Operation.Divide => operand1 / operand2,
            _ => throw new ArgumentOutOfRangeException(nameof(operation), $"Not a valid operation: {operation}."),
        };

        public string GetCalculation()
        {
            // Make sure the queues don't somehow have different lengths
            if (numbers.Count == operations.Count)
            {
                if (numbers.Count == 0)
                    return "(no values)";

                decimal[] numberArray = numbers.ToArray();
                Operation[] operationArray = operations.ToArray();

                string calculation = numberArray[0].ToString();
                decimal result = numberArray[0]; // ADDED FOR RESULT

                for (int i = 1; i < numberArray.Length; i++)
                {
                    calculation += $" {GetOperation(operationArray[i])} {numberArray[i]}";
                    result = PerformOperation(result, operationArray[i], numberArray[i]); // ADDED FOR RESULT
                }

                return $"{calculation} = {result}";
            }
            else
            {
                throw new Exception("Queue members in a history instance somehow have different sizes!");
            }
        }

        public decimal GetResult() {
            // Make sure the queues don't somehow have different lengths
            if (numbers.Count == operations.Count)
            {
                if (numbers.Count == 0)
                    return 0m;

                decimal[] numberArray = numbers.ToArray();
                Operation[] operationArray = operations.ToArray();

                decimal result = numberArray[0];

                for (int i = 1; i < numberArray.Length; i++)
                {
                    result = PerformOperation(result, operationArray[i], numberArray[i]);
                }

                return result;
            }
            else
            {
                throw new Exception("Queue members in a history instance somehow have different sizes!");
            }
        }
    }
}