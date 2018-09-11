using System;
using System.Collections.Generic;
using System.Text;

namespace PinCodeGenerator
{
    internal class PinCodeCollection
    { 
        
        public PinCodeCollection()
        {
            
        }
        
        
    }
    
    internal class PinCodeGenerator
    {
        private readonly IRandomNumberGenerator _randomNumberGenerator;
        private readonly IPinCodeValidator _pinCodeValidator;

        public PinCodeGenerator(Func<int, int, IRandomNumberGenerator> randomNumberGeneratorFactory, IPinCodeValidator pinCodeValidator)
        {
            _randomNumberGenerator = randomNumberGeneratorFactory(0, 9);
            _pinCodeValidator = pinCodeValidator;
        }

        public IEnumerable<PinCode> GeneratorPinCodes(int batchSize)
        {
            var generatorPinCodes = new Dictionary<string, PinCode>();
            if (batchSize <= 0)
            {
                return generatorPinCodes.Values;
            }

            int i = 0;
            do
            {
                PinCode pinCode = GeneratePinCode();
                if (_pinCodeValidator.IsPinCodeValid(pinCode))
                {
                    if (!generatorPinCodes.ContainsKey(pinCode.ToString()))
                    {
                        i++;
                        generatorPinCodes.Add(pinCode.ToString(), pinCode);
                    }
                }
            } while (i < batchSize);
            return generatorPinCodes.Values;
        }

        private PinCode GeneratePinCode()
        {
            var pinCode = new StringBuilder();
            for (int j = 0; j < 4; j++)
            {
                int pinCodeVal = _randomNumberGenerator.Next();
                pinCode.Append(pinCodeVal);
            }
            
            // TODO: Can be converted to IoC
            return new PinCode(pinCode.ToString());
        }
    }
}