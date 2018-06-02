using Newtonsoft.Json;
using SalaryContracts;
using SalaryWebApp.Models;
using SalaryWebApp.Services;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace SalaryWebApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly WebApiCallerService _webApiCallerService;

        public HomeController()
        {
            if (_webApiCallerService == null)
                _webApiCallerService = new WebApiCallerService();
        }

        public ActionResult Index()
        {
            return View();
        }

        public ViewResult CreateEmployee()
        {
            return View();
        }

        public async Task<ViewResult> EmployeeList()
        {
            List<Employee> listOfEmployees = new List<Employee>();

            var result = await _webApiCallerService.WebApiCaller(new Uri(Request.Url, Url.Content("~")), "api/Employee");
            listOfEmployees = JsonConvert.DeserializeObject<List<Employee>>(result);

            return View(listOfEmployees);
        }


        public async Task<ActionResult> GetPayslip(int id)
        {

            IPayslip payslip = null;

            var result = await _webApiCallerService.WebApiCaller(new Uri(Request.Url, Url.Content("~")), string.Format("api/Payslip/{0}", id));



            payslip = JsonConvert.DeserializeObject<Payslip>(result);
            if (payslip == null)
                return RedirectToAction("EmployeeList", "Home");

            return View(payslip);
        }
    }
}