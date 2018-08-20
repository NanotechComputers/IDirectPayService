using System;
using System.Collections.Generic;
using System.Reflection;

namespace DirectPay.Models.Shared
{
    public abstract class Enumeration : IComparable
    {
        public string Name { get; }
        public int Value { get; }

        protected Enumeration()
        {
        }

        protected Enumeration(int id, string name)
        {
            Value = id;
            Name = name;
        }

        public override string ToString()
        {
            return Name;
        }

        public static IEnumerable<T> GetAll<T>() where T : Enumeration, new()
        {
            var type = typeof(T);
            var fields = type.GetTypeInfo().GetFields(BindingFlags.Public |
                                                      BindingFlags.Static |
                                                      BindingFlags.DeclaredOnly);
            foreach (var info in fields)
            {
                var instance = new T();
                var locatedValue = info.GetValue(instance) as T;
                if (locatedValue != null)
                {
                    yield return locatedValue;
                }
            }
        }

        public override bool Equals(object obj)
        {
            var otherValue = obj as Enumeration;
            if (otherValue == null)
            {
                return false;
            }

            var typeMatches = GetType().Equals(obj.GetType());
            var valueMatches = Value.Equals(otherValue.Value);
            return typeMatches && valueMatches;
        }

        public int CompareTo(object other)
        {
            return Value.CompareTo(((Enumeration) other).Value);
        }
    }
}