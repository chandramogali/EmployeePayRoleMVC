using Azure;
using Microsoft.Data.SqlClient;
using ModelLayer.EmployeeModel;
using RepositoryLayer.Entity;
using RepositoryLayer.Interface;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text;

namespace RepositoryLayer.Services
{
    public class EmployeeRepo : IEmployeeRepo
    {
        SqlConnection SqlConnection;
        string connectionString = @"Data Source=CHANDRAMOGALI\SQLEXPRESS;Initial Catalog=EmployeePayRole;Integrated Security=True;Trust Server Certificate=True";

        public IEnumerable<EmployeeEntity> GetAllEmployees()
        {
            List<EmployeeEntity> lstemployee = new List<EmployeeEntity>();

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("GetAllEmployees", con);
                cmd.CommandType = CommandType.StoredProcedure;


                SqlDataReader rdr = cmd.ExecuteReader();

                while (rdr.Read())
                {
                    EmployeeEntity employee = new EmployeeEntity();

                    employee.EmployeeId = Convert.ToInt32(rdr["EmployeeId"]);
                    employee.FullName = rdr["FullName"].ToString();
                    employee.ImagePath = rdr["ImagePath"].ToString();
                    employee.Gender = rdr["Gender"].ToString();
                    employee.Department = rdr["Department"].ToString();
                    employee.Salary = Convert.ToDecimal(rdr["Salary"]);
                    employee.StartDate = Convert.ToDateTime(rdr["StartDate"]);
                    employee.Notes = rdr["Notes"].ToString();
                    lstemployee.Add(employee);
                }
                con.Close();
            }
            return lstemployee;
        }

        public EmployeeModel AddEmployee(EmployeeModel employee)
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("InsertEmployee", con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@FullName", employee.FullName);
                cmd.Parameters.AddWithValue("@ImagePath", employee.ImagePath);
                cmd.Parameters.AddWithValue("@Gender", employee.Gender);
                cmd.Parameters.AddWithValue("@Department", employee.Department);
                cmd.Parameters.AddWithValue("@Salary", employee.Salary);
                cmd.Parameters.AddWithValue("@StartDate", employee.StartDate);
                cmd.Parameters.AddWithValue("@Notes", employee.Notes);


                cmd.ExecuteNonQuery();
                con.Close();
            }
            return employee;
        }

        public EmployeeEntity UpdateEmployee(EmployeeEntity employee)
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("UpdateEmployee", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@EmployeeId", employee.EmployeeId);
                cmd.Parameters.AddWithValue("@FullName", employee.FullName);
                cmd.Parameters.AddWithValue("@ImagePath", employee.ImagePath);
                cmd.Parameters.AddWithValue("@Gender", employee.Gender);
                cmd.Parameters.AddWithValue("@Department", employee.Department);
                cmd.Parameters.AddWithValue("@Salary", employee.Salary);
                cmd.Parameters.AddWithValue("@StartDate", employee.StartDate);
                cmd.Parameters.AddWithValue("@Notes", employee.Notes);
                
                cmd.ExecuteNonQuery();
                con.Close();
            }
            return employee;
        }

        public EmployeeEntity GetEmployeeData(int? id)
        {
            EmployeeEntity employee = new EmployeeEntity();

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                con.Open();
                string sqlQuery = "SELECT * FROM Employee WHERE EmployeeId= " + id;
                SqlCommand cmd = new SqlCommand(sqlQuery, con);

               
                SqlDataReader rdr = cmd.ExecuteReader();

                while (rdr.Read())
                {
                    employee.EmployeeId = Convert.ToInt32(rdr["EmployeeId"]);
                    employee.FullName = rdr["FullName"].ToString();
                    employee.ImagePath = rdr["ImagePath"].ToString();
                    employee.Gender = rdr["Gender"].ToString();
                    employee.Department = rdr["Department"].ToString();
                    employee.Salary = Convert.ToDecimal(rdr["Salary"]);
                    employee.StartDate = Convert.ToDateTime(rdr["StartDate"]);
                    employee.Notes = rdr["Notes"].ToString();
                }
            }
            return employee;
        }

        public void DeleteEmployee(int? id)
        {

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("DeleteEmployee", con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@EmployeeId", id);
                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
            }
        }

        public EmployeeEntity Login(int id,string name)
        {
            EmployeeEntity employee = new EmployeeEntity();

            //var obj = db.UserProfiles.Where(a => a.EmployeeId.Equals(id) && a..FullName(name)).FirstOrDefault();

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                con.Open();
                string sqlQuery = "SELECT * FROM Employee WHERE EmployeeId = " + id + " AND FullName = '" + name + "'";

                SqlCommand cmd = new SqlCommand(sqlQuery, con);

              
                SqlDataReader rdr = cmd.ExecuteReader();

                while (rdr.Read())
                {
                    employee.EmployeeId = Convert.ToInt32(rdr["EmployeeId"]);
                    employee.FullName = rdr["FullName"].ToString();
                   
                }

                if(rdr.HasRows)
                {
                    return employee;
                }
            }
            return null;
        }


        public IEnumerable<EmployeeEntity> GetAllEmployeesByName(string name)
        {
            List<EmployeeEntity> lstemployee = new List<EmployeeEntity>();

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                con.Open();
                string sqlQuery = "SELECT * FROM Employee WHERE FullName LIKE '%" + name + "%'";

                SqlCommand cmd = new SqlCommand(sqlQuery, con);


                SqlDataReader rdr = cmd.ExecuteReader();
                

                while (rdr.Read())
                {
                    EmployeeEntity employee = new EmployeeEntity();

                    employee.EmployeeId = Convert.ToInt32(rdr["EmployeeId"]);
                    employee.FullName = rdr["FullName"].ToString();
                    employee.ImagePath = rdr["ImagePath"].ToString();
                    employee.Gender = rdr["Gender"].ToString();
                    employee.Department = rdr["Department"].ToString();
                    employee.Salary = Convert.ToDecimal(rdr["Salary"]);
                    employee.StartDate = Convert.ToDateTime(rdr["StartDate"]);
                    employee.Notes = rdr["Notes"].ToString();
                    lstemployee.Add(employee);
                }
                con.Close();
            }
            return lstemployee;
        }


    }
}
