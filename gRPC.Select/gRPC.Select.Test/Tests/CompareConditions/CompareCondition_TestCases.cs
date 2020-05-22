namespace gRPC.Select.Test.Tests.CompareConditions
{
    public class CompareCondition_TestCases
    {
        public static object[] NotComparable =
        {
            new object[] {typeof(string), "aoe", "Aoe"},
            new object[] {typeof(string), "aoe", "aoe"},
            new object[] {typeof(string), "Aoe", "aoe"},
            new object[] {typeof(bool), true, true},
            new object[] {typeof(bool), true, false},
            new object[] {typeof(bool), false, true}
        };

        public static object[] EqualComparable =
        {
            new object[] {typeof(int), 1, 1},
            new object[] {typeof(double), 2.232, 2.232},
        };

        public static object[] EqualNotComparable =
        {
            new object[] {typeof(string), "aoe", "aoe"},
            new object[] {typeof(bool), true, true}
        };

        public static object[] NotEqualComparable =
        {
            new object[] {typeof(int), 1, 2},
            new object[] {typeof(int), 2, 1},
            new object[] {typeof(double), 2.232, 2.233},
            new object[] {typeof(double), 2.233, 2.232},
        };

        public static object[] NotEqualNotComparable =
        {
            new object[] {typeof(string), "aoe", "oe"},
            new object[] {typeof(string), "oe", "aoe"},
            new object[] {typeof(bool), true, false},
            new object[] {typeof(bool), false, true}
        };

        public static object[] LeftGreater =
        {
            new object[] {typeof(int), 2, 1},
            new object[] {typeof(double), 2.233, 2.232},
        };

        public static object[] LeftLesser =
        {
            new object[] {typeof(int), 1, 2},
            new object[] {typeof(double), 2.232, 2.233},
        };

        public static object[] NotString =
        {
            new object[] {typeof(bool), true, true},
            new object[] {typeof(bool), true, false},
            new object[] {typeof(bool), false, true},
            new object[] {typeof(int), 1, 2},
            new object[] {typeof(int), 2, 1},
            new object[] {typeof(int), 1, 1},
            new object[] {typeof(double), 2.232, 2.232},
            new object[] {typeof(double), 2.232, 2.233},
            new object[] {typeof(double), 2.233, 2.232},
        };
    }
}
