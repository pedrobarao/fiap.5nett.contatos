namespace Commons.Domain.Utils;

public static class StringExtension
{
    public static string JustNumbers(string input)
    {
        if (string.IsNullOrWhiteSpace(input)) return string.Empty;

        return new string(input.Where(char.IsDigit).ToArray());
    }
}