namespace Aoc24;

public sealed partial class DayTwo
{
    [Fact]
    public void PartOne_Example()
    {
        const int expectedResult = 2;
        RunTest(expectedResult, Datasets.DayTwoExampleReports, PartOneDryRun);
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
            var isReportSafe = RunReport(report);
            if (isReportSafe)
                safeReports++;
        }

        return safeReports;
    }
}