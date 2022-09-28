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
        private static bool useLinearOrderOfOperations = false;
        private static readonly int operationEnumMax = Enum.GetValues(typeof(Operation)).Cast<int>().Max();
        public static int OperationEnumMax { get => operationEnumMax; }
        public static bool UseLinearOrderOfOperations { get => useLinearOrderOfOperations; set => useLinearOrderOfOperations = value; }

    }
}
