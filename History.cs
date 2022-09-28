namespace Calculator
{
    public enum Operation
    {
        Null,
        Add,
        Subtract,
        Multiply,
        Divide
    }

    struct Calculation
    {

        private Queue<decimal> numbers;
        private Queue<Operation> operations;
        private decimal result;

        public Calculation()
        {
            numbers = new Queue<decimal>();
            operations = new Queue<Operation>();
            result = 0;
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

                for (int i = 1; i < numberArray.Length; i++)
                {
                    calculation += $" {GetOperation(operationArray[i])} {numberArray[i]}";
                }

                return calculation;
            }
            else
            {
                throw new Exception("Queue members in a history instance somehow have different sizes!");
            }
        }
        // decimal GetResult() - returnerar resultatet
    }
}