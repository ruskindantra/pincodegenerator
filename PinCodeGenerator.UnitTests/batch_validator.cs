using System.Collections.Generic;
using AutoFixture;
using FluentAssertions;
using Xunit;

namespace PinCodeGenerator.UnitTests
{
    public class batch_validator
    {
        [Fact]
        public void is_batch_valid_should_return_true_if_all_items_are_valid()
        {
            // add customisations to create 4 digit pins
            var fixture = new Fixture();
            fixture.Create<int>();
            fixture.cu
            
            // TODO: Use fixture
            var batch = new List<string>
            {
                "4389",
                "1890"
            };
            
            var batchValidator = new BatchValidator();
            batchValidator.IsBatchValid(batch, out _).Should().BeTrue();
        }
    }
}