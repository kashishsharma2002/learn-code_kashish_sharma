using System.Collections.Generic;
using System.Linq;
using System.Text;

public class CustomerSearch
{
    public List<Customer> SearchByCountry(string country)
    {
        var query =
            from customer in CustomerDatabase.Customers
            where customer.Country.Contains(country)
            orderby customer.CustomerID ascending
            select customer;

        return query.ToList();
    }

    public List<Customer> SearchByCompanyName(string company)
    {
        var query =
            from customer in CustomerDatabase.Customers
            where customer.CompanyName.Contains(company)
            orderby customer.CustomerID ascending
            select customer;

        return query.ToList();
    }

    public List<Customer> SearchByContact(string contact)
    {
        var query =
            from customer in CustomerDatabase.Customers
            where customer.ContactName.Contains(contact)
            orderby customer.CustomerID ascending
            select customer;

        return query.ToList();
    }

    public string ExportToCSV(List<Customer> data)
    {
        StringBuilder csvBuilder = new StringBuilder();

        foreach (var item in data)
        {
            csvBuilder.AppendFormat("{0},{1},{2},{3}",
                item.CustomerID,
                item.CompanyName,
                item.ContactName,
                item.Country);

            csvBuilder.AppendLine();
        }

        return csvBuilder.ToString();
    }
}
