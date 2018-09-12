using System.Collections.Generic;

namespace PinCodeGenerator
{
    internal class PinCodeCollection : IPinCodeCollection
    {
        private readonly IDictionary<string, PinCode> _pinCodeDictionary;
        
        public PinCodeCollection()
        {
            _pinCodeDictionary = new Dictionary<string, PinCode>();
        }

        public bool Add(PinCode pinCode)
        {
            if (_pinCodeDictionary.ContainsKey(pinCode.ToString()))
                return false;
            
            _pinCodeDictionary.Add(pinCode.ToString(), pinCode);
            return true;
        }

        public int Count => _pinCodeDictionary.Count;
    }
}