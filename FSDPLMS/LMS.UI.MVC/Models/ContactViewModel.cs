using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace LMS.UI.MVC.Models
{
    public class ContactViewModel
    {
        [Required(ErrorMessage = "* Please provide a Name* ")]
        public string Name { get; set; }

        [Required(ErrorMessage = "* Please provide a Email *")]
        [DataType(DataType.EmailAddress)]

        public string Email { get; set; }

        public string Subject { get; set; }

        [Required(ErrorMessage = "* Please provide a Message *")]
        [UIHint("MultilineText")]
        public string Message { get; set; }
    }
}