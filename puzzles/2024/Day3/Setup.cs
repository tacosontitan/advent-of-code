namespace Aoc24;

public sealed partial class DayThree
{
    private delegate int Solution(string memory);
    
    private static void RunTest(
        int expectedResult,
        string? source,
        Solution solution)
    {
        var actualResult = solution(source!);
        Assert.Equal(expectedResult, actualResult);
    }
}