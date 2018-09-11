using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PinCodeGenerator
{
    internal class PinCodeGenerator
    {
        private readonly PinCodeSettings _pinCodeSettings;
        private readonly IPinCodeValidator _pinCodeValidator;
        private readonly IBatchValidator _batchValidator;

        public PinCodeGenerator(PinCodeSettings pinCodeSettings, IPinCodeValidator pinCodeValidator, IBatchValidator batchValidator)
        {
            _pinCodeSettings = pinCodeSettings;
            _pinCodeValidator = pinCodeValidator;
            _batchValidator = batchValidator;
        }

        public IEnumerable<string> GeneratorPinCodes(int batchSize)
        {
            var generatorPinCodes = new List<string>();
            if (batchSize <= 0)
            {
                return generatorPinCodes;
            }

            var random = new Random();

            int i = 0;
            do
            {
                string pinCode = GeneratePinCode(random);
                if (_pinCodeValidator.IsPinCodeValid(pinCode))
                {
                    if (!generatorPinCodes.Contains(pinCode))
                    {
                        i++;
                        generatorPinCodes.Add(pinCode);

                    }
                }
            } while (i < batchSize);

//            bool batchIsValid = false;
//            do
//            {
//                IEnumerable<string> duplicatedValues;
//                batchIsValid = _batchValidator.IsBatchValid(generatorPinCodes, out duplicatedValues);
//
//                foreach (var duplicatedValue in duplicatedValues)
//                {
//                    generatorPinCodes.Remove(duplicatedValue);
//                }
//
//                int removedItems = 0;
//                do
//                {
//                    string pinCode = GeneratePinCode(random);
//                    if (_pinCodeValidator.IsPinCodeValid(pinCode.ToString()))
//                    {
//                        removedItems++;
//                        generatorPinCodes.Add(pinCode);
//                    }
//                } while (removedItems < duplicatedValues.Count());
//                
//            } while (!batchIsValid);
            return generatorPinCodes;
        }

        private string GeneratePinCode(Random random)
        {
            var pinCode = new StringBuilder();
            for (int j = 0; j < _pinCodeSettings.Length; j++)
            {
                int pinCodeVal = random.Next(0, 9);
                pinCode.Append(pinCodeVal);
            }
            return pinCode.ToString();
        }
    }
}