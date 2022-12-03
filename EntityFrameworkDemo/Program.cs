using EntityFrameworkDemo.DBContexts;
using EntityFrameworkDemo.Models;
using Microsoft.EntityFrameworkCore;
using System.Text.Json.Serialization;

namespace EntityFrameworkDemo;
class Program
{
    static void Main(string[] args)
    {
        //CreateMasterData();
        //CreateRelatedEmployeeData();
        //ReadEmployeeData("Epm 1");
        Console.WriteLine("Hello, World!");
    }

    static void CreateMasterData()
    {
        ICollection<Department> departments = new List<Department>();
        departments.Add(new Department() { Name = "Dept 1" });
        departments.Add(new Department() { Name = "Dept 2" });
        departments.Add(new Department() { Name = "Dept 3" });
        departments.Add(new Department() { Name = "Dept 4" });
        departments.Add(new Department() { Name = "Dept 5" });

        ICollection<Project> projects = new List<Project>();
        projects.Add(new Project() { Name = "Proj 1" });
        projects.Add(new Project() { Name = "Proj 2" });
        projects.Add(new Project() { Name = "Proj 3" });
        projects.Add(new Project() { Name = "Proj 4" });
        projects.Add(new Project() { Name = "Proj 5" });
        projects.Add(new Project() { Name = "Proj 6" });

        using (var context = new EmployeeContext())
        {
            context.AddRange(departments);
            context.AddRange(projects);
            context.SaveChanges();
        }
    }

    static void CreateRelatedEmployeeData()
    {
        var employee1 = new Employee()
        {
            FirstName = "Epm",
            LastName = "1",
            Designation = "Software Engineer",
            Salary = 1,
            DepartmentId = 1,
            EmployeeAddress = new EmployeeAddress()
            {
                Address = "Address 1",
                City = "Noida",
                State = "UP",
                Country = "India",
                PostalCode = 123456,
            },
            EmployeeProjects = new List<EmployeeProject>()
            {
                new EmployeeProject()
                {
                    ProjectId = 1,
                },
                new EmployeeProject()
                {
                    ProjectId = 2
                }
            }
        };

        var employee2 = new Employee()
        {
            FirstName = "Epm",
            LastName = "2",
            Designation = "Software Engineer",
            Salary = 1,
            DepartmentId = 1,
            EmployeeAddress = new EmployeeAddress()
            {
                Address = "Address 2",
                City = "Noida",
                State = "UP",
                Country = "India",
                PostalCode = 123456,
            },
            EmployeeProjects = new List<EmployeeProject>()
            {
                new EmployeeProject()
                {
                    ProjectId = 3,
                }
            }
        };

        using (var  context = new EmployeeContext())
        {
            context.Add<Employee>(employee1);
            context.Add<Employee>(employee2);
            context.SaveChanges();
        }
    }

    static void ReadEmployeeData(string employeeName)
    {
        using(var context = new EmployeeContext())
        {
            var employee = context.Employees.Where(e => e.FirstName + " " + e.LastName == employeeName).FirstOrDefault();
            if (employee == null)
            {
                Console.WriteLine("No record found");
                return;
            }
            else
            {
                Console.WriteLine("Employees details " + Newtonsoft.Json.JsonConvert.SerializeObject(employee));
            }

            //Explicit loading. Employee adress will be null because explicit loading is not implemented on address property.
            var employeeExplicitLoading = context.Employees.Where(e => e.FirstName + " " + e.LastName == employeeName).FirstOrDefault();
            if (employeeExplicitLoading != null)
            {
                context.Entry(employeeExplicitLoading).Reference(e => e.Department).Load();
                context.Entry(employeeExplicitLoading).Collection(e => e.EmployeeProjects).Load();
            }

            //Eager loading
            var employeeWithEagerLoading = context.Employees.Where(e => e.FirstName + " " + e.LastName == employeeName)
                .Include(e => e.Department)
                .Include(e => e.EmployeeAddress)
                .Include(e => e.EmployeeProjects)
                .FirstOrDefault();
        }
    }
}

