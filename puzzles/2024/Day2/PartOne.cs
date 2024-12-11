namespace Aoc24;

public sealed partial class DayTwo
{
    [Fact]
    public void PartOne_Example()
    {
        const int expectedResult = 2;
        RunTest(expectedResult, Datasets.DayTwoPartOneExampleReports, PartOneDryRun);
    }

    [Fact]
    public void PartOne_Actual()
    {
        const int expectedResult = 402;
        RunTest(expectedResult, Datasets.DayTwoReports, PartOneDryRun);
    }

    private static int PartOneDryRun(IEnumerable<int[]> reports)
    {
        var safeReports = 0;
        foreach (var report in reports)
        {
            var isReportSafe = true;
            for (var levelIndex = 2; levelIndex < report.Length; levelIndex++)
            {
                var previousLevel = report[levelIndex - 2];
                var currentLevel = report[levelIndex - 1];
                var nextLevel = report[levelIndex];

                isReportSafe = AreLevelsSafe(currentLevel, previousLevel, nextLevel);
                if (!isReportSafe)
                    break;
            }

            if (isReportSafe)
                safeReports++;
        }

        return safeReports;
    }
}