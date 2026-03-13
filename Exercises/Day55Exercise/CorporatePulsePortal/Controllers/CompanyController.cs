using CorporatePulsePortal.Models;
using Microsoft.AspNetCore.Mvc;

namespace CorporatePulsePortal.Controllers;

public class CompanyController : Controller
{
    private readonly List<Employee> _employees = [
        new() { Id=1, Name="Aditya", Position="CEO", Salary=120000 },
        new() { Id=2, Name="Aditya", Position="Software Engineer", Salary=80000 },
        new() { Id=3, Name="Aditya", Position="Project Manager", Salary=70000 },
        new() { Id=4, Name="Aditya", Position="UI Designer", Salary=60000 },
    ];

    public IActionResult Dashboard()
    {
        ViewBag.Announcement = "Today is holiday";

        ViewData["DepartmentName"] = "IT";
        ViewData["ServerStatus"] = "Online";
        ViewData["IsDepartmentActive"] = true;
        
        return View(_employees);
    }
}