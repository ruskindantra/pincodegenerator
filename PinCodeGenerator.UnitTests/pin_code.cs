using System;
using FluentAssertions;
using Xunit;

namespace PinCodeGenerator.UnitTests
{
    public class pin_code
    {
        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData(" ")]
        public void pin_code_cannot_be_empty_or_null(string value)
        {
            Action pinCodeConstructor = () => new PinCode(value);
            pinCodeConstructor.Should().Throw<ArgumentOutOfRangeException>().And.Message.StartsWith("Pin code cannot be empty");
        }
        
        [Theory]
        [InlineData("abcd")]
        [InlineData("123h")]
        [InlineData("34 6")]
        public void pin_code_can_only_be_numeric(string value)
        {
            Action pinCodeConstructor = () => new PinCode(value);
            pinCodeConstructor.Should().Throw<ArgumentOutOfRangeException>().And.Message.StartsWith("Pin code contains non-numeric values");
        }
        
        [Theory]
        [InlineData("11112")]
        [InlineData("12345")]
        [InlineData("00000")]
        public void pin_code_can_only_be_4_digits(string value)
        {
            Action pinCodeConstructor = () => new PinCode(value);
            pinCodeConstructor.Should().Throw<ArgumentOutOfRangeException>().And.Message.StartsWith("Pin code can only be 4 digits long");
        }
    }
}