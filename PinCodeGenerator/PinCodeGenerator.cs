using System;
using System.Text;

namespace PinCodeGenerator
{
    internal class PinCodeGenerator : IPinCodeGenerator
    {
        private readonly IRandomNumberGenerator _randomNumberGenerator;
        private readonly IPinCodeValidator _pinCodeValidator;
        private readonly Func<IPinCodeCollection> _pinCodeCollectionFactory;

        public PinCodeGenerator(Func<int, int, IRandomNumberGenerator> randomNumberGeneratorFactory, IPinCodeValidator pinCodeValidator, Func<IPinCodeCollection> pinCodeCollectionFactory)
        {
            _randomNumberGenerator = randomNumberGeneratorFactory(0, 9);
            _pinCodeValidator = pinCodeValidator;
            _pinCodeCollectionFactory = pinCodeCollectionFactory;
        }

        public IPinCodeCollection Generate(int batchSize)
        {
            var pinCodeCollection = _pinCodeCollectionFactory();
            if (batchSize <= 0)
            {
                return pinCodeCollection;
            }

            var i = 0;
            do
            {
                PinCode pinCode = GeneratePinCode();
                if (_pinCodeValidator.IsPinCodeValid(pinCode))
                {
                    if (pinCodeCollection.Add(pinCode))
                    {
                        i++;
                    }
                }
            } while (i < batchSize);
            return pinCodeCollection;
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