using System;
using System.Text.RegularExpressions;

namespace Core.Util.Emoji;

public class Emoji
{
    private static readonly Regex _regex = new Regex(@"[\u1F600-\u1F64F]|[\u1F300-\u1F5FF]|[\u1F680-\u1F6FF]|[\u2600-\u26FF]|[\u2700-\u27BF]");

    public static string Filter(string input)
    {
        return _regex.Replace(input, "");
    }

    public static bool ContainEmoji(string input)
    {
        return _regex.IsMatch(input);
    }
}