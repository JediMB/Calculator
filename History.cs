namespace Calculator
{
    struct History
    {
        enum Operation
        {
            Add,
            Subtract,
            Multiply,
            Divide
        }

        private Queue<decimal> numbers;
        private Queue<Operation> operations;
        private decimal result;

        public History()
        {
            numbers = new Queue<decimal>();
            operations = new Queue<Operation>();
            result = 0;
        }

        // void AddOperation(decimal number, Operation operation) - lägger till ett nummer/operation sist i kön
        // string GetCalculation() - returnerar en sträng som visar alla nummer och operationer i sekvens
        // decimal GetResult() - returnerar resultatet
    }
}