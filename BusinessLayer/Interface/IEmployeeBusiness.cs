using ModelLayer.EmployeeModel;
using RepositoryLayer.Entity;
using System.Collections.Generic;

namespace BusinessLayer.Interface
{
    public interface IEmployeeBusiness
    {
        IEnumerable<EmployeeEntity> GetAllEmployees();
        EmployeeModel AddEmployee(EmployeeModel employee);
        EmployeeEntity UpdateEmployee(EmployeeEntity employee);
        EmployeeEntity GetEmployeeData(int? id);
        void DeleteEmployee(int? id);
        EmployeeEntity Login(int id, string name);
        IEnumerable<EmployeeEntity> GetAllEmployeesByName(string name);
    }
}