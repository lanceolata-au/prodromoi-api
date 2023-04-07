using HashidsNet;
using Prodromoi.Core.Interfaces;

namespace Prodromoi.Core.Features;

public class HashIdTranslator : IHashIdTranslator
{
    private readonly Hashids _hashids;
    private readonly long[] _extraNumbers;
    
    public HashIdTranslator(string saltKey)
    {
        _hashids = new Hashids(saltKey);
        _extraNumbers  = new[] {(long)8435, 2417};
    }
    
    public long[] Decode(string hashId)
    {
        var result = _hashids.DecodeLong(hashId);
        
        return result.Take(result.Length - 2).ToArray();

    }

    public string Encode(long[] numbers)
    {
        var encodeArray = new long[_extraNumbers.Length + numbers.Length];
        Array.Copy(numbers, encodeArray, 
            numbers.Length);
        Array.Copy(_extraNumbers, 0, encodeArray, 
            numbers.Length, _extraNumbers.Length);
        return _hashids.EncodeLong(encodeArray);
    }
    
    public string Encode(long?[] numbers)
    {
        var nonNullNumbers 
            = (from number in numbers where number != null select (long) number).ToList();

        return Encode(nonNullNumbers.ToArray());
    }
}