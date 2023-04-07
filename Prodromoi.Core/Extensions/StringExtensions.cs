namespace Prodromoi.Core.Extensions;

public static class StringExtensions
{
    public static string PhoneNumberString(this string? input)
    {
        return input == null 
            ? string.Empty 
            : new string(input.Where(char.IsDigit).ToArray());
    }
}