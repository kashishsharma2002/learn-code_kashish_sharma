using System;
using System.Collections.Generic;

public static class CustomerDatabase
{
    public static List<Customer> Customers = new List<Customer>
    {
        new Customer{ CustomerID = 1, CompanyName="Mircosoft", ContactName="Satya", Country="India"},
        new Customer{ CustomerID = 2, CompanyName="In Time Tec", ContactName="Jeet", Country="USA"},
        new Customer{ CustomerID = 3, CompanyName="IttRacknap", ContactName="Munesh", Country="India"}
    };
}
