namespace Calculator
{
    public enum Operation
        // The different operation types, plus a "null" value... which probably isn't necessary, but helps
    {
        Null,
        Add,
        Subtract,
        Multiply,
        Divide
    }

    public static class Operator
    {
        private static readonly List<Calculation> calculations = new();     // The app's calculation history is stored here
        private static bool useLinearOrderOfOperations = false;             // Decides if calculations are made in linear or 'PEMDAS' order (without the 'PE')
        private static readonly int operationEnumMax = Enum.GetValues(typeof(Operation)).Cast<int>().Max();     // Size of the Operation enum, in case of expansion
        private static readonly string[] operationPrepositions = { "add", "subtract by", "multiply by", "divide by" };  // Replaces some 'if' or 'switch' logic

        // Public properties for our private fields
        public static List<Calculation> Calculations { get => calculations; }
        public static bool UseLinearOrderOfOperations { get => useLinearOrderOfOperations; set => useLinearOrderOfOperations = value; }
        public static int OperationEnumMax { get => operationEnumMax; }
        public static string OperationPreposition(int selection) => operationPrepositions[selection - 1];

        public static char ConvertInput(char input) => input switch
        // Enables some alternate input when choosing operators
        {
            '+' => '1',
            '-' => '2',
            '*' => '3',
            '/' => '4',
            _ => input
        };
    }
}
