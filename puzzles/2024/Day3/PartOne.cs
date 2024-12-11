using System.Text.RegularExpressions;

namespace Aoc24;

public sealed partial class DayThree
{
    [Fact]
    public void PartOne_Example()
    {
        const int expectedResult = 161;
        RunTest(expectedResult, Datasets.DayThreePartOneExampleMemory, PartOneDryRun);
    }

    [Fact]
    public void PartOne_Actual()
    {
        const int expectedResult = 174_103_751;
        RunTest(expectedResult, Datasets.DayThreeMemory, PartOneDryRun);
    }

    private static int PartOneDryRun(string memory)
    {
        const string regexPattern = @"mul\((\d{1,3}),(\d{1,3})\)";

        var sum = 0;
        var matches = Regex.Matches(memory, regexPattern);
        foreach (Match match in matches)
        {
            var leftOperand = int.Parse(match.Groups[1].Value);
            var rightOperand = int.Parse(match.Groups[2].Value);
            sum += leftOperand * rightOperand;
        }

        return sum;
    }
}