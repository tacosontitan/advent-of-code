using System.ComponentModel;

namespace Aoc24;

public sealed partial class DayTwo
{
    private static void RunTest(
        int expectedResult,
        string? source,
        Solution solution)
    {
        var reports = GetDataForTest(source);
        var actualResult = solution(reports);
        Assert.Equal(expectedResult, actualResult);
    }
    
    private static IEnumerable<int[]> GetDataForTest(string? source)
    {
        ArgumentNullException.ThrowIfNull(source);
        return GetData();

        IEnumerable<int[]> GetData()
        {
            var lines = source.Split('\n');
            foreach (var line in lines)
            {
                var levels = line.Split(' ');
                yield return levels.Select(int.Parse).ToArray();
            }
        }
    }
}