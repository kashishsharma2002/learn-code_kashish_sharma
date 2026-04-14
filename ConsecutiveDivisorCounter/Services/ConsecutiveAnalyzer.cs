namespace ConsecutiveDivisorCounter.Services;

public sealed class ConsecutiveAnalyzer : IConsecutiveAnalyzer
{
    private const int FirstCandidate = 2;
    private const int NoMatchingNumbers = 0;

    private readonly IDivisorCalculator _divisorCalculator;

    public ConsecutiveAnalyzer(IDivisorCalculator divisorCalculator)
    {
        _divisorCalculator = divisorCalculator ?? throw new ArgumentNullException(nameof(divisorCalculator));
    }

    public int CountNumbersWithSameDivisorCount(int k)
    {
        if (k <= FirstCandidate)
        {
            return NoMatchingNumbers;
        }

        var divisorCounts = _divisorCalculator.CalculateDivisorCountsUpTo(k);
        var matchingNumbers = 0;

        for (var n = FirstCandidate; n < k; n++)
        {
            if (divisorCounts[n] == divisorCounts[n + 1])
            {
                matchingNumbers++;
            }
        }

        return matchingNumbers;
    }
}
