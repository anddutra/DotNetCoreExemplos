using System;

namespace FireBaseExemplos.Util
{
    [AttributeUsage(AttributeTargets.Field, AllowMultiple = false, Inherited = true)]
    public class EnumValueAttribute : Attribute
    {
        public string Value { get; }

        public EnumValueAttribute(string value)
        {
            Value = value;
        }
    }
}
