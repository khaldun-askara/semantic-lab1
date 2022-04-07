using System.Text.RegularExpressions;

string text;
using (StreamReader reader = new StreamReader("input.txt"))
{
    text = await reader.ReadToEndAsync();
}

string pattern = @"(?<![A-Za-z])M{0,3}(CM|CD|D?C{0,3})?(XC|XL|L?X{0,3})?(IX|IV|V?I{0,3})?(?![A-Za-z])";
string clear_text = Regex.Replace(text, pattern, RomanNum.Magic,
                                      RegexOptions.IgnorePatternWhitespace);

Console.WriteLine(clear_text);

class RomanNum
{
    static Dictionary<char, int> RomanNumerals = new Dictionary<char, int>()
    {
        {'I', 1},
        {'V', 5},
        {'X', 10},
        {'L', 50},
        {'C', 100},
        {'D', 500},
        {'M', 1000}
    };
    public static string Magic(Match match)
    {
        if (match.Value == "")
            return "";

        return Convert(match.Value);
    }

    public static string Convert(string roman)
    {
        roman = roman.ToUpper();

        int arabic = 0;
        for (int i = 0; i < roman.Length - 1; i++)
            if (RomanNumerals[roman[i]] < RomanNumerals[roman[i + 1]])
                arabic = arabic - RomanNumerals[roman[i]];
            else
                arabic = arabic + RomanNumerals[roman[i]];
        arabic = arabic + RomanNumerals[roman[roman.Length - 1]];

        return arabic.ToString();
    }
}
