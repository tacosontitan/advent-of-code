namespace Aoc24;

public sealed partial class DayThree
{
    private sealed class MulToken(string value)
        : IToken
    {
        public string Value { get; set; } = value;
    }
}