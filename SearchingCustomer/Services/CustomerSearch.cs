public class CustomerSearch
{
    public List<Customer> SearchByCountry(string country)
    {
        if (country == null)
            throw new ArgumentNullException(nameof(country));

        return CustomerDatabase.Customers
            .Where(c => c.Country != null && c.Country.Contains(country))
            .OrderBy(c => c.CustomerID)
            .ToList();
    }

    public List<Customer> SearchByCompanyName(string company)
    {
        if (company == null)
            throw new ArgumentNullException(nameof(company));

        return CustomerDatabase.Customers
            .Where(c => c.CompanyName != null && c.CompanyName.Contains(company))
            .OrderBy(c => c.CustomerID)
            .ToList();
    }

    public List<Customer> SearchByContact(string contact)
    {
        if (contact == null)
            throw new ArgumentNullException(nameof(contact));

        return CustomerDatabase.Customers
            .Where(c => c.ContactName != null && c.ContactName.Contains(contact))
            .OrderBy(c => c.CustomerID)
            .ToList();
    }

    public string ExportToCSV(List<Customer> data)
    {
        if (data == null)
            throw new ArgumentNullException(nameof(data));

        var csvBuilder = new StringBuilder();

        foreach (var item in data)
        {
            csvBuilder.AppendLine($"{item.CustomerID},{item.CompanyName},{item.ContactName},{item.Country}");
        }

        return csvBuilder.ToString();
    }
}
