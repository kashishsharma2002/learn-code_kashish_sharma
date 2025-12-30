using System;
using System.Collections.Generic;

class Program
{
    static void Main(string[] args)
    {
        var search = new CustomerSearch();

        Console.WriteLine("Search By Country: India");
        var byCountry = search.SearchByCountry("India");
        PrintResults(byCountry);


        Console.WriteLine("\nSearch By Company Name: 'Tec'");
        var byCompany = search.SearchByCompanyName("Tec");
        PrintResults(byCompany);


        Console.WriteLine("\nSearch By Contact: 'Sa'");
        var byContact = search.SearchByContact("Sa");
        PrintResults(byContact);


        Console.WriteLine("\nCSV Export (Country Search):");
        Console.WriteLine(search.ExportToCSV(byCountry));

        Console.WriteLine("\nCSV Export (Company Search):");
        Console.WriteLine(search.ExportToCSV(byCompany));

        Console.WriteLine("\nCSV Export (Contact Search):");
        Console.WriteLine(search.ExportToCSV(byContact));
    }

    static void PrintResults(List<Customer> list)
    {
        foreach (var customer in list)
        {
            Console.WriteLine($"{customer.CustomerID} | {customer.CompanyName} | {customer.ContactName} | {customer.Country}");
        }
    }
}
