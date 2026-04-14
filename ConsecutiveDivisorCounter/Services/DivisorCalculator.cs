namespace ConsecutiveDivisorCounter.Services;

public sealed class DivisorCalculator : IDivisorCalculator
{
    public int[] CalculateDivisorCountsUpTo(int maxNumber)
    {
        if (maxNumber < 0)
        {
            throw new ArgumentOutOfRangeException(nameof(maxNumber), "Maximum number cannot be negative.");
        }

        var divisorCounts = new int[maxNumber + 1];

        for (var divisor = 1; divisor <= maxNumber; divisor++)
        {
            for (var multiple = divisor; multiple <= maxNumber; multiple += divisor)
            {
                divisorCounts[multiple]++;
            }
        }

        return divisorCounts;
    }
}
