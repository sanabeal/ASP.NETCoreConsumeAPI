using ASP.NETCoreConsumeAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace ASP.NETCoreConsumeAPI.Controllers
{
    public class EmployeesController : Controller
    {
        public async Task<IActionResult> Index()
        {
            List<Employees> reservationList = new List<Employees>();
            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync("https://localhost:44385/api/employees/"))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    reservationList = JsonConvert.DeserializeObject<List<Employees>>(apiResponse);
                }
            }
            return View(reservationList);
        }

        public ViewResult GetEmployee() => View();

        [HttpPost]
        public async Task<IActionResult> GetEmployee(int id)
        {
            Employees reservation = new Employees();
            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync("https://localhost:44385/api/employees/" + id))
                {
                    if (response.StatusCode == System.Net.HttpStatusCode.OK)
                    {
                        string apiResponse = await response.Content.ReadAsStringAsync();
                        reservation = JsonConvert.DeserializeObject<Employees>(apiResponse);
                    }
                    else
                        ViewBag.StatusCode = response.StatusCode;
                }
            }
            return View(reservation);
        }

        public ViewResult AddEmployee() => View();

        [HttpPost]
        public async Task<IActionResult> AddEmployee(Employees employee)
        {
            Employees receivedemployee = new Employees();
            using (var httpClient = new HttpClient())
            {
                StringContent content = new StringContent(JsonConvert.SerializeObject(employee), Encoding.UTF8, "application/json");

                using (var response = await httpClient.PostAsync("https://localhost:44385/api/employees/", content))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    receivedemployee = JsonConvert.DeserializeObject<Employees>(apiResponse);
                }
            }
            return View(receivedemployee);
        }
    }
}
