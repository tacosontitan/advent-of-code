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
        const int expectedResult = 455;
        RunTest(expectedResult, Datasets.DayTwoReports, PartTwoDryRun);
    }

    private static int PartTwoDryRun(IEnumerable<int[]> reports)
    {
        var safeReports = 0;
        foreach (var report in reports)
        {
            var isReportSafe = RunReport(report);
            if (!isReportSafe)
            {
                for (var levelToSkip = 0; levelToSkip < report.Length; levelToSkip++)
                {
                    var newReport = report.Where((_, index) => index != levelToSkip).ToArray();
                    var isNewReportSafe = RunReport(newReport);
                    if (isNewReportSafe)
                    {
                        isReportSafe = true;
                        break;
                    }
                }
            }

            if (isReportSafe)
                safeReports++;
        }

        return safeReports;
    }
}