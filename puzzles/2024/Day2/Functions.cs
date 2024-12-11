namespace Aoc24;

public sealed partial class DayTwo
{
    private static bool RunReportWithPersistence(int[] report)
    {
        var isReportSafe = RunReport(report);
        if (isReportSafe)
            return true;

        for (var levelToSkip = 0; levelToSkip < report.Length; levelToSkip++)
        {
            var newReport = report.Where((_, index) => index != levelToSkip).ToArray();
            var isNewReportSafe = RunReport(newReport);
            if (isNewReportSafe)
                return true;
        }

        return false;
    }

    private static bool RunReport(int[] report)
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

        return isReportSafe;
    }

    private static bool AreLevelsSafe(
        int currentLevel,
        int previousLevel,
        int nextLevel)
    {
        var areLevelsSequential = AreLevelsAscending(currentLevel, previousLevel, nextLevel)
            || AreLevelsDescending(currentLevel, previousLevel, nextLevel);

        return areLevelsSequential
            && IsDistanceSafe(currentLevel, previousLevel, nextLevel);
    }

    private static bool AreLevelsAscending(
        int currentLevel,
        int previousLevel,
        int nextLevel)
    {
        return previousLevel < currentLevel
            && currentLevel < nextLevel;
    }

    private static bool AreLevelsDescending(
        int currentLevel,
        int previousLevel,
        int nextLevel)
    {
        return previousLevel > currentLevel
            && currentLevel > nextLevel;
    }

    private static bool IsDistanceSafe(
        int currentLevel,
        int previousLevel,
        int nextLevel)
    {
        const int minimumDistance = 1;
        const int maximumDistance = 3;
        var distanceFromPreviousLevel = Math.Abs(currentLevel - previousLevel);
        var isDistanceFromPreviousLevelSafe = distanceFromPreviousLevel >= minimumDistance
            && distanceFromPreviousLevel <= maximumDistance;

        var distanceFromNextLevel = Math.Abs(currentLevel - nextLevel);
        var isDistanceFromNextLevelSafe = distanceFromNextLevel >= minimumDistance
            && distanceFromNextLevel <= maximumDistance;

        return isDistanceFromPreviousLevelSafe
            && isDistanceFromNextLevelSafe;
    }
}