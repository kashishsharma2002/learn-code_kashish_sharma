using ConsecutiveDivisorCounter.Services;
using Xunit.Abstractions;

namespace ConsecutiveDivisorCounter.Tests;

public sealed class ConsecutiveAnalyzerTests
{
    private readonly ITestOutputHelper _output;

    public ConsecutiveAnalyzerTests(ITestOutputHelper output)
    {
        _output = output;
    }

    [Theory]
    [InlineData(2, 0)]
    [InlineData(3, 1)]
    [InlineData(15, 2)]
    public void CountNumbersWithSameDivisorCount_ReturnsExpectedCount(int k, int expectedCount)
    {
        var analyzer = CreateAnalyzer();

        var count = analyzer.CountNumbersWithSameDivisorCount(k);

        _output.WriteLine($"Input k: {k}");
        _output.WriteLine($"Expected result: {expectedCount}");
        _output.WriteLine($"Actual result: {count}");

        Assert.Equal(expectedCount, count);
    }

    [Theory]
    [InlineData(0)]
    [InlineData(1)]
    [InlineData(-10)]
    public void CountNumbersWithSameDivisorCount_WhenKIsTooSmall_ReturnsZero(int k)
    {
        var analyzer = CreateAnalyzer();

        var count = analyzer.CountNumbersWithSameDivisorCount(k);

        _output.WriteLine($"Input k: {k}");
        _output.WriteLine("Expected result: 0");
        _output.WriteLine($"Actual result: {count}");

        Assert.Equal(0, count);
    }

    [Fact]
    public void CalculateDivisorCountsUpTo_WhenMaxNumberIsNegative_Throws()
    {
        var calculator = new DivisorCalculator();

        var exception = Assert.Throws<ArgumentOutOfRangeException>(() => calculator.CalculateDivisorCountsUpTo(-1));

        _output.WriteLine("Input maxNumber: -1");
        _output.WriteLine("Expected result: ArgumentOutOfRangeException");
        _output.WriteLine($"Actual result: {exception.GetType().Name}");
    }

    private static ConsecutiveAnalyzer CreateAnalyzer()
    {
        return new ConsecutiveAnalyzer(new DivisorCalculator());
    }
}
