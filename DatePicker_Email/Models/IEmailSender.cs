using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DatePicker_Email.Models
{
	public interface IEmailSender
	{
		Masaga MasagaStrings { get; set; }
		Task<string> SendEmailAsync(string email, string subject, string message);
	}
}