namespace Aoc24;

public sealed partial class DayThree
{
    private sealed class DisableMulToken
        : IToken
    {
        public string Value { get; set; } = "don't()";
    }
}