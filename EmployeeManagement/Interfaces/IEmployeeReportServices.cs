using EmployeeManagement.Models;

namespace EmployeeManagement.Interfaces;

public interface IEmployeeReportService
{
    string GenerateReport(Employee employee);
}