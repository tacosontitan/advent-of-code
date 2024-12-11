namespace Aoc24;

public sealed partial class DayTwo
{
    [Fact]
    public void PartTwo_Example()
    {
        const int expectedResult = 4;
        RunTest(expectedResult, Datasets.DayTwoPartOneExampleReports, PartTwoDryRun);
    }

    [Fact]
    public void PartTwo_Actual()
    {
        const int expectedResult = 402;
        RunTest(expectedResult, Datasets.DayTwoReports, PartTwoDryRun);
    }

    private static int PartTwoDryRun(IEnumerable<int[]> reports)
    {
        var safeReports = 0;
        foreach (var report in reports)
        {
            var isReportSafe = true;
            for (var levelToSkip = 0; levelToSkip < report.Length; levelToSkip++)
            {
                var newReport = Enumerable.Range(0, report.Length).Select(i => (i, report[i])).Where(il => il.i != levelToSkip).ToArray();
                for (var levelIndex = 2; levelIndex < newReport.Length; levelIndex++)
                {
                    var previousLevel = report[levelIndex - 2];
                    var currentLevel = report[levelIndex - 1];
                    var nextLevel = report[levelIndex];

                    isReportSafe = AreLevelsSafe(currentLevel, previousLevel, nextLevel);
                    if (!isReportSafe)
                        break;
                }

                if (isReportSafe)
                    break;
            }

            if (isReportSafe)
                safeReports++;
        }

        return safeReports;
    }
}