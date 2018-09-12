using System;
using System.Threading;
using System.Threading.Tasks;
using FluentAssertions;
using Moq;
using Xunit;

namespace PinCodeGenerator.UnitTests
{
    public class pin_code_generator
    {
        [Fact]
        public void generate_should_generate_pin_codes()
        {
            var mockRepository = new MockRepository(MockBehavior.Default);
            
            var randomNumberGeneratorMock = mockRepository.Create<IRandomNumberGenerator>();
            var pinCodeValidatorMock = mockRepository.Create<IPinCodeValidator>();
            var pinCodeCollectionMock = mockRepository.Create<IPinCodeCollection>();

            Func<int, int, IRandomNumberGenerator> randomNumberGeneratorFactory = (min, max) => randomNumberGeneratorMock.Object;

            pinCodeCollectionMock.Setup(p => p.Add(It.IsAny<PinCode>())).Returns(true);
            pinCodeValidatorMock.Setup(v => v.IsPinCodeValid(It.IsAny<PinCode>())).Returns(true);
            
            var pinCodeGenerator = new PinCodeGenerator(randomNumberGeneratorFactory, pinCodeValidatorMock.Object, () => pinCodeCollectionMock.Object);

            var pinCodeCollection = pinCodeGenerator.Generate(10);
            pinCodeCollectionMock.Verify(p => p.Add(It.IsAny<PinCode>()), Times.Exactly(10));
        }

        [Fact, Trait("Category", "Integration")]
        public void generate_should_not_return_duplicate_values()
        {
            var mockRepository = new MockRepository(MockBehavior.Default);
            
            var randomNumberGeneratorMock = mockRepository.Create<IRandomNumberGenerator>();
            var pinCodeValidatorMock = mockRepository.Create<IPinCodeValidator>();
            
            // because this is an integration test, we actually use an implementation of the pincodecollection
            Func<IPinCodeCollection> pinCodeCollectionFactory = () => new PinCodeCollection();

            Func<int, int, IRandomNumberGenerator> randomNumberGeneratorFactory = (min, max) => randomNumberGeneratorMock.Object;
            pinCodeValidatorMock.Setup(v => v.IsPinCodeValid(It.IsAny<PinCode>())).Returns(true);

            // make sure we always return "5", so a pincode will be "5555"
            randomNumberGeneratorMock.Setup(r => r.Next()).Returns(5);
            
            var pinCodeGenerator = new PinCodeGenerator(randomNumberGeneratorFactory, pinCodeValidatorMock.Object, pinCodeCollectionFactory);

            // lets generate 2 pincodes, which are guaranteed to be duplicates
            // TODO: This will loop forever, we need to break out of it, find a better way
            bool completed = false;
            var generateTask = new Task(() =>
            {
                var pinCodeCollection = pinCodeGenerator.Generate(2);
                completed = true;
            });

            generateTask.Start();
            
            generateTask.Wait(TimeSpan.FromSeconds(5));
            completed.Should().BeFalse();

        }
    }
}