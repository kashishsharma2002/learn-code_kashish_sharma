using System;
using System.Numerics;
class Program
{
    static void Main(string[] args)
    {
        try
        {
            ProcessFloorMeanQueries();
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error: {ex.Message}");
        }
    }
    static void ProcessFloorMeanQueries()
    {
        Console.WriteLine("Enter the number of elements and Queries separated by spaces:");
        var inputCounts = ParseIntArray(ReadUserInput());
        Console.WriteLine("Enter the array elements separated by spaces:");
        var inputArray = ParseIntArray(ReadUserInput());
        int numberOfElements = inputCounts[0];
        int numberOfQueries = inputCounts[1];
        long[] prefixSums = GetPrefixSums(inputArray, numberOfElements);
        for (var query = 0; query < numberOfQueries; query++)
        {
            long result = CalculateFloorMeanForQuery(prefixSums);
            Console.WriteLine(result);
        }
    }
    static string ReadUserInput()
    {
        string? line = Console.ReadLine();

        if (line == null)
            throw new InvalidOperationException("Input stream was closed.");

        return line;
    }

    static int[] ParseIntArray(string input)
    {
        try
        {
            return TokenizeToInts(input);
        }
        catch (FormatException ex)
        {
            throw new ArgumentException("Input must contain only integers.", ex);
        }
        catch (OverflowException ex)
        {
            throw new ArgumentException("One or more numbers are too large.", ex);
        }
    }
    static int[] TokenizeToInts(string input)
    {
        return Array.ConvertAll(
            input.Split(' ', StringSplitOptions.RemoveEmptyEntries),
            int.Parse
        );
    }
    static long[] GetPrefixSums(int[] inputArray, int numberOfElements)
    {
        long[] prefixSums = new long[numberOfElements + 1];
        prefixSums[0] = 0;
        for (var index = 1; index <= numberOfElements; index++)
        {
            prefixSums[index] = prefixSums[index - 1] + inputArray[index - 1];
        }
        return prefixSums;
    }
    static long CalculateFloorMeanForQuery(long[] prefixSums)
    {
        Console.WriteLine("Enter the subarray indices (1-based) separated by spaces:");
        var query = ParseIntArray(ReadUserInput());
        int leftIndex = query[0];
        int rightIndex = query[1];
        if (leftIndex < 1 || rightIndex >= prefixSums.Length || leftIndex > rightIndex)
            throw new ArgumentOutOfRangeException("Invalid subarray range.");
        long subarraySum = prefixSums[rightIndex] - prefixSums[leftIndex - 1];
        int subarrayLength = rightIndex - leftIndex + 1;
        return subarraySum / subarrayLength;
    }
}