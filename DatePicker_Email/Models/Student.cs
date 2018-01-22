using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace DatePicker_Email.Models
{
	public class Student
	{
		public string FirstName { get; set; }
		public string LastName { get; set; }
		[Required]
		[Display(Name ="День народження")]
		[DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:d}")]
		[DataType(DataType.Date)]
		public DateTime Birthday { get; set; }
    }
}
