namespace Blockchain.Business.Extensions;

public static class StringExtenstions
{
    public static string GetLastCharacters(this string str, int n)
    {
        if (str.Length < n)
        {
            return str;
        }
        return str[^n..];
    }
}
