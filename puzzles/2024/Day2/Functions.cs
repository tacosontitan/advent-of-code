namespace Aoc24;

public sealed partial class DayTwo
{
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