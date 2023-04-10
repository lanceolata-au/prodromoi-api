namespace Prodromoi.Core.Interfaces;

public interface IHashIdTranslator
{
    public long[] Decode(string hashId);

    public string Encode(long[] numbers);
    public string Encode(long?[] numbers);
    public string Encode(long number);

}