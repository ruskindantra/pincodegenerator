using System.Collections.Generic;
using System.Linq;

namespace PinCodeGenerator
{
    internal class BatchValidator : IBatchValidator
    {
        public bool IsBatchValid(IEnumerable<string> batch, out IEnumerable<string> duplicateValues)
        {
            var duplicateValuesList = new List<string>();
            var groups = batch.GroupBy(v => v);
            foreach (var duplicates in groups)
            {
                if (duplicates.Count() > 1)
                {
                    duplicateValuesList.Add(duplicates.Key);
                }
            }

            duplicateValues = duplicateValuesList;
            if (duplicateValuesList.Any())
            {
                return false;
            }

            return true;
        }
    }
}