using System;
using System.Linq;

namespace PinCodeGenerator
{
    public class PinCode
    {
        private readonly string _value;

        public PinCode(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                throw new ArgumentOutOfRangeException(nameof(value), "Pin code cannot be empty");
            }

            if (!int.TryParse(value, out _))
            {
                throw new ArgumentOutOfRangeException(nameof(value), "Pin code contains non-numeric values");
            }

            if (value.Length != 4)
            {
                throw new ArgumentOutOfRangeException(nameof(value), "Pin code can only be 4 digits long");
            }

            _value = value;
        }

        public int[] AsParts()
        {
            return _value.Select(i => int.Parse(i.ToString())).ToArray();
        }

        public override string ToString()
        {
            return _value;
        }
    }
}