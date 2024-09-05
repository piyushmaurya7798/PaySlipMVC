using Microsoft.AspNetCore.Mvc;
using PaySlipMVC.Models;
using System.Diagnostics;

namespace PaySlipMVC.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly PayslipContext db;
        public HomeController(ILogger<HomeController> logger,PayslipContext db)
        {
            _logger = logger;
            this.db = db;
        }

        public IActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Index(Employee e)
        {
            var getemail = e.Email;
            var getpassword = e.Password;
            var employee = db.Employees.SingleOrDefault(emp => emp.Email == getemail);

            if (employee != null)
            {
                if (employee.Password == getpassword)
                {
                    if (employee.Role == "Admin")
                    {
                        return RedirectToAction("Joining");
                    }
                    else {
                        HttpContext.Session.SetInt32("userid", employee.EmployeeId);
                        return RedirectToAction("ApplyLeave");
                    }
                }
                else
                {
                    return View();
                }
            }
            else
            {
                    return View();
                
            }
        }


        public IActionResult DisplayAllEmp() 
        {
            var data = db.Employees.ToList();
            return View(data);
        }

        public IActionResult Joining() 
        {
            return View();
        }
        
        
        
        [HttpPost]
        public IActionResult Joining(Employee e) 
        {
            e.Role = "User";
            var salary2 = 20000;
            e.Salary = salary2;
            db.Employees.Add(e);
            db.SaveChanges();
            var employee = db.Employees.SingleOrDefault(emp => emp.Email == e.Email);

            var obj = new Leave()
            {
                Pl = 12,
                Cl = 8,
                Sl = 4,
                EmpId=employee.EmployeeId,
                Extra=0

            };
            db.Leaves.Add(obj);
            db.SaveChanges();
            int month = int.Parse(DateTime.Now.Month.ToString());
            int year = int.Parse(DateTime.Now.Year.ToString());
            int days = DateTime.DaysInMonth(year, month);
            var obj2 = new Payslip()
            {
                Tdays = days,
                Userid=employee.EmployeeId,
                Salary=salary2
            };
            db.Payslips.Add(obj2);
            db.SaveChanges();

            
            return RedirectToAction("Index");
        }

        public IActionResult ApplyLeave() 
        {
            var userId = HttpContext.Session.GetInt32("userid");
            var data = db.Leaves.SingleOrDefault(emp => emp.EmpId == userId);

            return View(data); 
        }

        [HttpPost]
        public IActionResult ApplyLeave(Leave l) 
        {
            var userId = HttpContext.Session.GetInt32("userid");
            var eu = db.Leaves.SingleOrDefault(emp => emp.EmpId == userId);
            DateTime fromdate=DateTime.Parse(l.Fromdate);
            DateTime todate=DateTime.Parse(l.Todate);
            int d = (todate - fromdate).Days + 1;
            if (l.Sitem == "pl")
            {
                if (d < eu.Pl)
                {
                    eu.Pl = eu.Pl - d;
                    eu.Reason = l.Reason;
                    db.SaveChanges();
                    return RedirectToAction("ViewSlip");
                }
                else if (eu.Pl + eu.Cl >= d)
                {
                    var temp = d - eu.Pl;
                    var temp2 = eu.Cl - temp;
                    eu.Pl = 0;
                    eu.Cl = temp2;
                    eu.Reason = l.Reason;
                    db.SaveChanges();
                    return RedirectToAction("ViewSlip");
                }
                else if (eu.Pl + eu.Sl >= d)
                {
                    var temp = d - eu.Pl;
                    var temp2 = eu.Sl - temp;
                    eu.Pl = 0;
                    eu.Sl = temp2;
                    eu.Reason = l.Reason;
                    db.SaveChanges();
                    return RedirectToAction("ViewSlip");
                }
                else if (eu.Cl + eu.Sl >= d)
                {
                    var temp = d - eu.Cl;
                    var temp2 = eu.Sl - temp;
                    eu.Cl = 0;
                    eu.Sl = temp2;
                    eu.Reason = l.Reason;
                    db.SaveChanges();
                    return RedirectToAction("ViewSlip");
                }
                else if (eu.Pl + eu.Cl + eu.Sl >= d)
                {
                    var temp = d - eu.Pl;
                    var temp2 = eu.Cl - temp;
                    var temp3 = eu.Sl - temp;
                    eu.Pl = 0;
                    eu.Cl = 0;
                    eu.Sl = temp3;
                    eu.Reason = l.Reason;
                    db.SaveChanges();
                    return RedirectToAction("ViewSlip");
                }
                else 
                {
                    var temp = eu.Pl + eu.Cl + eu.Sl;
                    var temp2 = d - temp;
                    var temp3 = temp2 + eu.Extra;
                    eu.Extra = temp3;
                    eu.Sl = 0;
                    eu.Pl = 0;
                    eu.Cl = 0;
                    eu.Reason = l.Reason;
                    db.SaveChanges();
                    var payupdate = db.Payslips.SingleOrDefault(emp => emp.Userid == userId);
                    payupdate.Remain = Convert.ToInt32(temp3);
                    db.SaveChanges();
                    return RedirectToAction("ViewSlip");
                }
            }
            else if (l.Sitem == "cl")
            {
                if (d < eu.Cl)
                {
                    eu.Cl = eu.Cl - d;
                    eu.Reason = l.Reason;
                    db.SaveChanges();
                    return RedirectToAction("ViewSlip");
                }
                else if (eu.Cl + eu.Pl >= d)
                {
                    var temp = d - eu.Cl;
                    var temp2 = eu.Pl - temp;
                    eu.Cl = 0;
                    eu.Pl = temp2;
                    eu.Reason = l.Reason;
                    db.SaveChanges();
                    return RedirectToAction("ViewSlip");
                }
                else if (eu.Cl + eu.Sl >= d)
                {
                    var temp = d - eu.Cl;
                    var temp2 = eu.Sl - temp;
                    eu.Cl = 0;
                    eu.Sl = temp2;
                    eu.Reason = l.Reason;
                    db.SaveChanges();
                    return RedirectToAction("ViewSlip");
                }
                else if (eu.Pl + eu.Sl >= d)
                {
                    var temp = d - eu.Pl;
                    var temp2 = eu.Sl - temp;
                    eu.Pl = 0;
                    eu.Sl = temp2;
                    eu.Reason = l.Reason;
                    db.SaveChanges();
                    return RedirectToAction("ViewSlip");
                }
                else if (eu.Pl + eu.Cl + eu.Sl >= d)
                {
                    var temp = d - eu.Cl;
                    var temp2 = eu.Pl - temp;
                    var temp3 = eu.Sl - temp;
                    eu.Pl = 0;
                    eu.Cl = 0;
                    eu.Sl = temp3;
                    eu.Reason = l.Reason;
                    db.SaveChanges();
                    return RedirectToAction("ViewSlip");
                }
                else
                {
                    var temp = eu.Pl + eu.Cl + eu.Sl;
                    var temp2 = d - temp;
                    var temp3 = temp2 + eu.Extra;
                    eu.Sl = 0;
                    eu.Pl = 0;
                    eu.Cl = 0;
                    eu.Reason = l.Reason;
                    eu.Extra = temp3;
                    db.SaveChanges();
                    var payupdate = db.Payslips.SingleOrDefault(emp => emp.Userid == userId);
                    payupdate.Remain = (int)temp3;
                    db.SaveChanges();
                    return RedirectToAction("ViewSlip");
                }
            }
            else if (l.Sitem == "sl")
            {
                if (d < eu.Sl)
                {
                    eu.Sl = eu.Sl - d;
                    eu.Reason = l.Reason;
                    db.SaveChanges();
                    return RedirectToAction("ViewSlip");
                }
                else if (eu.Sl + eu.Pl >= d)
                {
                    var temp = d - eu.Sl;
                    var temp2 = eu.Pl - temp;
                    eu.Sl = 0;
                    eu.Pl = temp2;
                    eu.Reason = l.Reason;
                    db.SaveChanges();
                    return RedirectToAction("ViewSlip");
                }
                else if (eu.Sl + eu.Cl >= d)
                {
                    var temp = d - eu.Sl;
                    var temp2 = eu.Cl - temp;
                    eu.Sl = 0;
                    eu.Cl = temp2;
                    eu.Reason = l.Reason;
                    db.SaveChanges();
                    return RedirectToAction("ViewSlip");
                }
                else if (eu.Pl + eu.Cl >= d)
                {
                    var temp = d - eu.Pl;
                    var temp2 = eu.Cl - temp;
                    eu.Pl = 0;
                    eu.Cl = temp2;
                    eu.Reason = l.Reason;
                    db.SaveChanges();
                    return RedirectToAction("ViewSlip");
                }
                else if (eu.Pl + eu.Cl + eu.Sl >= d)
                {
                    var temp = d - eu.Sl;
                    var temp2 = eu.Pl - temp;
                    var temp3 = eu.Cl - temp;
                    eu.Sl = 0;
                    eu.Pl = 0;
                    eu.Cl = temp3;
                    eu.Reason = l.Reason;
                    db.SaveChanges();
                    return RedirectToAction("ViewSlip");
                }
                else
                {
                    var temp = eu.Pl + eu.Cl + eu.Sl;
                    var temp2 = d - temp;
                    var temp3 = temp2 + eu.Extra;
                    eu.Sl = 0;
                    eu.Pl = 0;
                    eu.Cl = 0;
                    eu.Reason = l.Reason;
                    eu.Extra = temp3;
                    db.SaveChanges();
                    var payupdate = db.Payslips.SingleOrDefault(emp => emp.Userid == userId);
                    payupdate.Remain = (int)temp3;
                    db.SaveChanges();
                    return RedirectToAction("ViewSlip");
                }
            }
            return View(d); 
        }


        //[HttpPost]
        //public IActionResult AbandonSession()
        //    {
        //    HttpContext.Session.Clear();
        //    return RedirectToAction("Index");
        //}

        public IActionResult ViewSlip() 
        {
            var userId = HttpContext.Session.GetInt32("userid");
            var data=db.Payslips.SingleOrDefault(emp=>emp.Userid == userId);
            var leave = data.Remain;
            var Beforesalary = data.Salary;
            var perday = Beforesalary / data.Tdays;
            var totaldeduct = perday * leave;
            var Aftersalary=Beforesalary-totaldeduct;
            data.Salary = Aftersalary;
            return View(data);
        }
        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
