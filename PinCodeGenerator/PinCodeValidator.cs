using System;

namespace PinCodeGenerator
{
    internal class PinCodeValidator : IPinCodeValidator
    {
        private readonly ILogger _logger;
        private readonly PinCodeSettings _pinCodeSettings;

        public PinCodeValidator(ILogger logger, PinCodeSettings pinCodeSettings)
        {
            _logger = logger;
            _pinCodeSettings = pinCodeSettings;
        }

        public bool IsPinCodeValid(string pinCode)
        {
            if (pinCode.ToString().Length != _pinCodeSettings.Length)
            {
                return false;
            }

            char? firstValue = null;
            for (int i = 0; i < pinCode.Length; i++)
            {
                char pinCodeCharacter = pinCode[i];
                if (firstValue == null)
                {
                    firstValue = pinCodeCharacter;
                    continue;
                }

                if (firstValue == pinCodeCharacter)
                {
                    _logger.Warn("Two consecutive values found, pincode is not valid");
                    return false;
                }

                int prevPinCodeValue = int.Parse(firstValue.ToString());
                int pinCodeValue = int.Parse(pinCodeCharacter.ToString());
                if (prevPinCodeValue == pinCodeValue - 1 && i != pinCode.ToString().Length - 1)
                {
                    _logger.Warn("Two consecutive sequential values found");
                    int nextValue = pinCode[i + 1];
                    if (pinCodeValue == nextValue - 1)
                    {                    
                        _logger.Warn("Three consecutive sequential values found");
                        return false;
                    }
                }
            }

            return true;
        }
    }
}