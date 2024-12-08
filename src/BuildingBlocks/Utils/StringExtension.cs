namespace Utils;

public static class StringExtension
{
    public static string JustNumbers(string input)
    {
        return string.IsNullOrWhiteSpace(input) ? string.Empty : new string(input.Where(char.IsDigit).ToArray());
    }
}