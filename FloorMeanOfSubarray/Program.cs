using System;
using System.Numerics;
class Program
{
    static void Main(string[] args)
    {
        GetfloorMeanOfSubarray();
    }
    static void GetfloorMeanOfSubarray()
    {
        Console.WriteLine("Enter the number of elements and Queries separated by spaces:");
        var inputCounts = GetInputArray();
        Console.WriteLine("Enter the array elements separated by spaces:");
        var inputArray = GetInputArray();
        int numberOfElements = inputCounts[0];
        int numberOfQueries = inputCounts[1];
        long[] prefixSums = GetPrefixSums(inputArray, numberOfElements);
        for (var query = 0; query < numberOfQueries; query++)
        {
            long result = GetfloorMean(prefixSums);
            Console.WriteLine(result);
        }
    }
    static int[] GetInputArray()
    {
        return Array.ConvertAll(Console.ReadLine().Split(' '), int.Parse);
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
    static long GetfloorMean(long[] prefixSums)
    {
        Console.WriteLine("Enter the subarray indices (1-based) separated by spaces:");
        var query = GetInputArray();
        int leftIndex = query[0];
        int rightIndex = query[1];
        long subarraySum = prefixSums[rightIndex] - prefixSums[leftIndex - 1];
        int subarrayLength = rightIndex - leftIndex + 1;
        return subarraySum / subarrayLength;
    }
}