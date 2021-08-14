using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;


namespace DAL
{
    public class EmployeeDataAccess
    {
        private IConfiguration _configuration;
        public EmployeeDataAccess(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public IEnumerable<Employee> GetAllEmployees()
        {
           List <Employee> lstemployee = new List<Employee>();
           using (SqlConnection con = new SqlConnection(_configuration.GetConnectionString("DbConnection")))
            {
                SqlCommand cmd = new SqlCommand("spGetAllEmployees", con);
                cmd.CommandType = CommandType.StoredProcedure;

                con.Open();
                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    Employee employee = new Employee();

                    employee.EmployeeId = Convert.ToInt32(reader["EmployeeID"]);
                    employee.Name = reader["Name"].ToString();
                    employee.Gender = reader["Gender"].ToString();
                    employee.Department = reader["Department"].ToString();
 
                    lstemployee.Add(employee);
                }
                con.Close();
            }
            return lstemployee;
        }

        public void AddEmployee(Employee employee)
        {
            using (SqlConnection con = new SqlConnection(_configuration.GetConnectionString("DbConnection")))
            {
                SqlCommand cmd = new SqlCommand("spAddEmployee", con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@Name", employee.Name);
                cmd.Parameters.AddWithValue("@Gender", employee.Gender);
                cmd.Parameters.AddWithValue("@Department", employee.Department);

                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
            }
        }

        public void UpdateEmployee(Employee employee)
        {
            using (SqlConnection con = new SqlConnection(_configuration.GetConnectionString("DbConnection")))
            {
                SqlCommand cmd = new SqlCommand("spUpdateEmployee", con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@EmpId", employee.EmployeeId);
                cmd.Parameters.AddWithValue("@Name", employee.Name);
                cmd.Parameters.AddWithValue("@Gender", employee.Gender);
                cmd.Parameters.AddWithValue("@Department", employee.Department);

                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
            }
        }

        public Employee GetEmployeeData(int? id)
        {
            Employee Employee = new Employee();
            using (SqlConnection con= new SqlConnection(_configuration.GetConnectionString("DbConnection")))
            {
                string sqlQuery = "Select * from tblEmployee where EmployeeId=" + id;

                SqlCommand cmd = new SqlCommand(sqlQuery,con);
                con.Open();
                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    Employee.EmployeeId = Convert.ToInt32(reader["EmployeeID"]);
                    Employee.Name = reader["Name"].ToString();
                    Employee.Gender = reader["Gender"].ToString();
                    Employee.Department = reader["Department"].ToString();

                    
                }
            }
            return Employee;
        }

        public void DeleteEmployee(int? id)
        {
            using (SqlConnection con = new SqlConnection(_configuration.GetConnectionString("DbConnection")))
            {
                SqlCommand cmd = new SqlCommand("spDeleteEmployee", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@EmpId", id);

                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
            }
        }
    }
}
