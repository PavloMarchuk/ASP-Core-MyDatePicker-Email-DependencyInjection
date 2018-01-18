using System;
using System.Diagnostics;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using DatePicker_Email.Models;

namespace DatePicker_Email.Controllers
{
    public class HomeController : Controller
    {
		IEmailSender emailSender;
		public HomeController(IEmailSender emailSender)
		{
			this.emailSender = emailSender;
		}
		[HttpGet]
        public IActionResult Index()
        {
            DateTime myBirthday = new DateTime(1982, 08, 17);

            Student model = new Student { FirstName ="Pavlo", LastName="Marchuk", Birthday=myBirthday}; 
            return View(model);
        }
        [HttpGet]
		public IActionResult Contact()
        {
            ViewData["Message"] = "незнайомець";
			Masaga masaga = MasagaCreator("незнайомець");
            return View(masaga);
        }

        [HttpPost]
		public IActionResult Contact(Student student)
        {
			Masaga masaga;
			if (student!= null & !string.IsNullOrEmpty(student.FirstName))
            {
                ViewData["Message"] = $"{student.FirstName} {student.LastName} {student.Birthday.ToLongDateString()}";
				masaga =  MasagaCreator($"{student.FirstName} {student.LastName} {student.Birthday.ToLongDateString()}");
			}
			else
            {
                ViewData["Message"] = "незнайомець";
				masaga = MasagaCreator("незнайомець");
			}

			return View(masaga);
        }

		[HttpPost]
		public async Task< IActionResult> Sender(Masaga masaga)
		{
			emailSender.MasagaStrings = masaga;

			ViewData["sendResult"] = await emailSender.SendEmailAsync(masaga.To, masaga.Subject, masaga.Body);
			ViewData["parolTmp"] = masaga.Password;
			
			return View();
		}


		private Masaga MasagaCreator(string studentToString)
		{
			Masaga masaga = new Masaga
			{
				SMTP_Host = "smtp.gmail.com",
				Port = 587,
				Login = "pavlomarchuk7@gmail.com",
				Password = "",
				From = "pavlomarchuk7@gmail.com",
				To = "pavlomarchuk7@gmail.com",
				Subject = "Тема листа",
				Body = "\n\n\n\n\n\n" + studentToString
			};
			return masaga;
		}			

		public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}

