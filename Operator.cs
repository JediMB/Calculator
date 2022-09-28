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

    public static class Operator
    {
        private static readonly List<Calculation> calculations = new();
        private static bool useLinearOrderOfOperations = false;
        private static readonly int operationEnumMax = Enum.GetValues(typeof(Operation)).Cast<int>().Max();
        private static readonly string[] operationPrepositions = { "add", "subtract by", "multiply by", "divide by" };

        public static List<Calculation> Calculations { get => calculations; }
        public static bool UseLinearOrderOfOperations { get => useLinearOrderOfOperations; set => useLinearOrderOfOperations = value; }
        public static int OperationEnumMax { get => operationEnumMax; }
        public static string OperationPreposition(int selection) => operationPrepositions[selection - 1];

        public static char ConvertInput(char input) => input switch
        {
            '+' => '1',
            '-' => '2',
            '*' => '3',
            '/' => '4',
            _ => input
        };
    }
}
