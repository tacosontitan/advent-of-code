using System.Text.RegularExpressions;

namespace Aoc24;

public sealed partial class DayThree
{
    [Fact]
    public void PartTwo_Example()
    {
        const int expectedResult = 48;
        RunTest(expectedResult, Datasets.DayThreePartTwoExampleMemory, PartTwoDryRun);
    }

    [Fact]
    public void PartTwo_Actual()
    {
        const int expectedResult = 0;
        RunTest(expectedResult, Datasets.DayOneLists, PartTwoDryRun);
    }

    private static int PartTwoDryRun(string memory)
    {
        const string enableCommand = "do()";
        const string disableCommand = "don't()";
        const string regexPattern = @"mul\((\d{1,3}),(\d{1,3})\)";

        var sum = 0;
        var enabled = true;
        var currentIndex = 0;
        while (currentIndex < memory.Length)
        {
            var nextToken = GetNextToken();
            if (nextToken is EnableMulToken)
                enabled = true;
            else if (nextToken is DisableMulToken)
                enabled = false;
            else if (nextToken is MulToken)
            {
                if (!enabled)
                    continue;
                
                var match = Regex.Match(memory, regexPattern, currentIndex);
                var leftOperand = int.Parse(match.Groups[1].Value);
                var rightOperand = int.Parse(match.Groups[2].Value);
                sum += leftOperand * rightOperand;
            }
        }

        return sum;
    }
}