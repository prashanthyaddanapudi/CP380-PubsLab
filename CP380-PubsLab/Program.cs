using CP380_PubsLab.Models;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;
using System;
using System.Linq;

namespace CP380_PubsLab
{
    class Program
    {
        static void Main(string[] args)
        {
            using (var dbcontext = new Models.PubsDbContext())
            {
                if (dbcontext.Database.CanConnect())
                {
                    Console.WriteLine("Yes, I can connect");
                }

                // 1:Many practice
                //
                // TODO: - Loop through each employee
                //       - For each employee, list their job description (job_desc, in the jobs table)
                var eachEmpJobs = dbcontext.Employees.Select(eachEmp => new { EmpId = eachEmp.emp_id, FName = eachEmp.fname, LName = eachEmp.lname ,Job_Desc = eachEmp.Jobs.job_desc }).ToList();
				
				Console.WriteLine("\n====== Employee ID, Employee Name, Job Description ======");
                foreach (var empjob in eachEmpJobs)
                {
                    Console.WriteLine($"{empjob.EmpId}, {empjob.FName} {empjob.LName}, {empjob.Job_Desc}");
                }

                // TODO: - Loop through all of the jobs
                //       - For each job, list the employees (first name, last name) that have that job
                var eachJobEmps = dbcontext.Jobs.Select(eachJob => new { JobId = eachJob.job_id, Job_Desc = eachJob.job_desc, Employees = eachJob.Employee.Select(eachEmp => new { FirstName = eachEmp.fname, LastName = eachEmp.lname } ) }).ToList();
				
				Console.WriteLine("\n====== Job | Employees ======");
                foreach (var job in eachJobEmps)
                {
                    Console.WriteLine($"{job.JobId} | {job.Job_Desc}");
                    foreach (var employee in job.Employees)
                    {
                        Console.WriteLine($"  {employee.FirstName},{employee.LastName}");
                    }
                }

                // Many:many practice
                //
                // TODO: - Loop through each Store
                //       - For each store, list all the titles sold at that store
                //
                // e.g.
                //  Bookbeat -> The Gourmet Microwave, The Busy Executive's Database Guide, Cooking with Computers: Surreptitious Balance Sheets, But Is It User Friendly?
                var eachStoreTitles = dbcontext.Stores.Select(eachStore => new { Store = eachStore.stor_name, Titles = eachStore.Sales.Select(eachTitle => eachTitle.Titles.title) }).ToList();
				
				Console.WriteLine("\n====== Store -> Titles ======");
                foreach (var store in eachStoreTitles)
                {
                    var allTitles = String.Join(",", store.Titles);
                    Console.WriteLine($"\n{store.Store} -> {allTitles}");
                }

                // TODO: - Loop through each Title
                //       - For each title, list all the stores it was sold at
                //
                // e.g.
                //  The Gourmet Microwave -> Doc-U-Mat: Quality Laundry and Books, Bookbeat
                var eachTitleStores = dbcontext.Titles.Select(eachTitle => new { TitleName = eachTitle.title, StoreList = eachTitle.Sales.Select(eachStore => eachStore.Stores.stor_name) }).ToList();
				
				Console.WriteLine("\n====== Titles -> Store ======");
                foreach (var title in eachTitleStores)
                {
                    var allStores = String.Join(",", title.StoreList);
                    Console.WriteLine($"\n{title.TitleName} -> {allStores}");
                }
                Console.ReadLine();
            }
        }
    }
}
