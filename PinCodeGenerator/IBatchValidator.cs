using System.Collections.Generic;

namespace PinCodeGenerator
{
    public interface IBatchValidator
    {
        bool IsBatchValid(IEnumerable<string> batch, out IEnumerable<string> duplicateValues);
    }
}