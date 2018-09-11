namespace PinCodeGenerator
{
    internal class PinCodeValidator : IPinCodeValidator
    {
        private readonly ILogger _logger;

        public PinCodeValidator(ILogger logger)
        {
            _logger = logger;
        }

        public bool IsPinCodeValid(PinCode pinCode)
        {
            int? firstValue = null;
            for (int i = 0; i < pinCode.ToString().Length; i++)
            {
                int pinCodeUnit = pinCode.AsParts()[i];
                if (firstValue == null)
                {
                    firstValue = pinCodeUnit;
                    continue;
                }

                if (firstValue == pinCodeUnit)
                {
                    _logger.Warn("Two consecutive values found, pincode is not valid");
                    return false;
                }

                int prevPinCodeValue = int.Parse(firstValue.ToString());
                int pinCodeValue = int.Parse(pinCodeUnit.ToString());
                if (prevPinCodeValue == pinCodeValue - 1 && i != pinCode.ToString().Length - 1)
                {
                    _logger.Warn("Two consecutive sequential values found");
                    int nextValue = pinCode.AsParts()[i + 1];
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