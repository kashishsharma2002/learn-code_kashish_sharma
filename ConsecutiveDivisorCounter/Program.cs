using ConsecutiveDivisorCounter.Services;

const string KPrompt = "Enter k for test case";
const string InvalidTestCaseMessage = "Invalid number of test cases. Exiting application.";
const string InvalidKMessage = "Invalid k value. Printing 0 for this test case.";
const string ExitMessage = "Processing completed. Exiting application.";

var analyzer = new ConsecutiveAnalyzer(new DivisorCalculator());

Console.WriteLine("Enter the number of test cases:");

var firstLine = Console.ReadLine();
if (!int.TryParse(firstLine, out var testCaseCount) || testCaseCount <= 0)
{
    Console.Error.WriteLine(InvalidTestCaseMessage);
    return;
}

for (var testCase = 0; testCase < testCaseCount; testCase++)
{
    Console.Error.WriteLine($"{KPrompt} {testCase + 1}:");
    var line = Console.ReadLine();

    if (!int.TryParse(line, out var k))
    {
        Console.Error.WriteLine(InvalidKMessage);
        Console.WriteLine(0);
        continue;
    }

    Console.WriteLine(analyzer.CountNumbersWithSameDivisorCount(k));
}

Console.Error.WriteLine(ExitMessage);
