namespace gRPC.Select.Test.LogicConditions
{
    public class LogicCondition_TestCases
    {
        public static object[] NotLogic =
        {
            new object[] {typeof(int), 1, 1},
            new object[] {typeof(int), 1, 2},
            new object[] {typeof(int), 2, 1},
            new object[] {typeof(string), "aoe", "aoe"},
            new object[] {typeof(string), "aoe", "oe"},
            new object[] {typeof(string), "oe", "aoe"},
            new object[] {typeof(double), 2.232, 2.233},
            new object[] {typeof(double), 2.233, 2.232},
            new object[] {typeof(double), 2.232, 2.232}
        };
    }
}
