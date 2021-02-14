using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Aviato.Helpers;
using Aviato.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MongoDB.Driver;

namespace Aviato.Pages
{
    public class LoginModel : PageModel
    {
        private readonly IMongoCollection<Employee> collection;
        [BindProperty]
        public Employee LoginEmployee { get; set; }
        [BindProperty]
        public string ErrorMessage { get; set; }

        public LoginModel(IDatabaseSettings settings)
        {
            var client = new MongoClient(settings.ConnectionString);
            var database = client.GetDatabase(settings.DatabaseName);
            collection = database.GetCollection<Employee>("Employees");
        }

        public void OnGet(string err)
        {
            ErrorMessage = err;
        }

        public async Task<IActionResult> OnPostAsync()
        {
            var MyEmployee = new Employee();
            try
            {
                MyEmployee = await (await collection.FindAsync<Employee>(x => x.Email == LoginEmployee.Email && x.Password == LoginEmployee.Password)).FirstAsync();
            }
            catch (Exception e)
            {
                ErrorMessage = "Invalid e-mail or password";
                return RedirectToPage("Login", new { err = ErrorMessage });
            }
            SessionHelper.SetObjectAsJson(HttpContext.Session, "loginEmployee", MyEmployee);
            return RedirectToPage("Index");
        }
    }
}
