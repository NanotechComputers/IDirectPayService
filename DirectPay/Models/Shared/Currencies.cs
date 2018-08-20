using System.Collections.Generic;
using System.Reflection;

namespace DirectPay.Models.Shared
{
    public sealed class Currencies : Enumeration
    {
        private readonly string _name;
        private readonly int _value;

        public static readonly Currencies ZAR = new Currencies(0, "ZAR");
        public static readonly Currencies USD = new Currencies(1, "USD");

        private Currencies(int value, string name) : base(value, name)
        {
            _name = name;
            _value = value;
        }

        public override string ToString()
        {
            return _name;
        }

        public static IEnumerable<Currencies> All()
        {
            return new[] {ZAR, USD};
        }
    }
}