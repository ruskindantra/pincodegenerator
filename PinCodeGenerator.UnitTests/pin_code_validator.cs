using FluentAssertions;
using Moq;
using Xunit;

namespace PinCodeGenerator.UnitTests
{
    public class pin_code_validator
    {
        [Theory]
        [InlineData("6745", true)]
        [InlineData("1276", true)]
        [InlineData("1176", false)]
        [InlineData("1236", false)]
        [InlineData("8888", false)]
        [InlineData("4455", false)]
        public void is_pin_code_valid_should_return_correct_response(string pinCode, bool valid)
        {
            var mockRepository = new MockRepository(MockBehavior.Default);
            var loggerMock = mockRepository.Create<ILogger>();
            var pinCodeValidator = new PinCodeValidator(loggerMock.Object);
            pinCodeValidator.IsPinCodeValid(new PinCode(pinCode)).Should().Be(valid);
        }
    }
}