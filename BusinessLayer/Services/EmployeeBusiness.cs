using BusinessLayer.Interface;
using ModelLayer.EmployeeModel;
using RepositoryLayer.Entity;
using RepositoryLayer.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLayer.Services
{
    public class EmployeeBusiness : IEmployeeBusiness
    {
        private readonly IEmployeeRepo _empRepo;
        public EmployeeBusiness(IEmployeeRepo empRepo)
        {

            _empRepo = empRepo;
        }
        public IEnumerable<EmployeeEntity> GetAllEmployees()
        {
            return _empRepo.GetAllEmployees();
        }
        public EmployeeModel AddEmployee(EmployeeModel employee)
        {
            return _empRepo.AddEmployee(employee);
        }
        public EmployeeEntity UpdateEmployee(EmployeeEntity employee)
        {
            return _empRepo.UpdateEmployee(employee);
        }
        public EmployeeEntity GetEmployeeData(int? id)
        {
            return _empRepo.GetEmployeeData(id);
        }
        public void DeleteEmployee(int? id)
        {
            _empRepo.DeleteEmployee(id);
        }
        public EmployeeEntity Login(int id, string name)
        {
            return _empRepo.Login(id, name);
        }

        public IEnumerable<EmployeeEntity> GetAllEmployeesByName(string name)
        {
            return _empRepo.GetAllEmployeesByName(name);
        }


    }
}
