namespace PinCodeGenerator
{
    public interface IPinCodeGenerator
    {
        IPinCodeCollection Generate(int batchSize);
    }
}