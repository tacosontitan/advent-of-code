namespace Aoc24;

public sealed partial class DayThree
{
    private sealed class EnableMulToken
        : IToken
    {
        public string Value { get; set; } = "do()";
    }
}