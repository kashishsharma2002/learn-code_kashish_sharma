using EmployeeManagement.Interfaces;
using EmployeeManagement.Models;
using EmployeeManagement.Repositories;
using EmployeeManagement.Services;

class Program
{
    static void Main(string[] args)
    {
        IEmployeeRepository repository = new EmployeeRepository();
        IEmployeeService employeeService = new EmployeeService(repository);

        IEmployeeReportService xmlReportService = new XmlReportService();
        IEmployeeReportService csvReportService = new CsvReportService();

        var employee1 = new Employee(1, "Priynaka Jonas", "Engineering");

        repository.Save(employee1);
        Console.WriteLine("Xml Report:");
        Console.WriteLine(xmlReportService.GenerateReport(employee1));
        Console.WriteLine("CSV Report:");
        Console.WriteLine(csvReportService.GenerateReport(employee1));
        Console.WriteLine("Is Working:");
        Console.WriteLine(employeeService.CheckIfWorking(employee1));
        employeeService.TerminateEmployee(employee1);
        Console.WriteLine("Is Working After Termination:");
        Console.WriteLine(employeeService.CheckIfWorking(employee1));
    }
}
