using EmployeeManagement.Models;

namespace EmployeeManagement.Interfaces;

public interface IEmployeeService
{
    void TerminateEmployee(Employee employee);
    bool CheckIfWorking(Employee employee);
}