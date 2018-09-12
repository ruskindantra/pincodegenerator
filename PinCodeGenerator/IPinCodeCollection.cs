namespace PinCodeGenerator
{
    public interface IPinCodeCollection
    {
        bool Add(PinCode pinCode);
        int Count { get; }
    }
}