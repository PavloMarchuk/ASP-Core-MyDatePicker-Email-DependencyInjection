
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using MimeKit;
using MailKit.Net.Smtp;

namespace DatePicker_Email.Models
{
	public class EmailSender : IEmailSender
	{
		public Masaga MasagaStrings { get; set; }	

		public async Task<string> SendEmailAsync(string email, string subject, string message)
		{
			MimeMessage mimeMessage = new MimeMessage();
			mimeMessage.From.Add(new MailboxAddress(MasagaStrings.From));
			mimeMessage.To.Add(new MailboxAddress("", MasagaStrings.To));

			mimeMessage.Subject = MasagaStrings.Subject;
			mimeMessage.Body = new TextPart(MimeKit.Text.TextFormat.Html) { Text = MasagaStrings.Body };

			// Send Mails async
			using (var smtpClient = new SmtpClient())
			{
				await smtpClient.ConnectAsync(MasagaStrings.SMTP_Host, MasagaStrings.Port, false);
				await smtpClient.AuthenticateAsync(MasagaStrings.From, MasagaStrings.Password);
				await smtpClient.SendAsync(mimeMessage);
				await smtpClient.DisconnectAsync(true);
				
			}
			
			return "успішно?";
		}
	}
}
