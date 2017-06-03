using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace GoldSilver.WebUI.Models
{
    public class ContactFormViewModel
    {
        [Required(ErrorMessage = "You must provide your name.")]
        public string Name { get; set; }

        [Required(ErrorMessage = "You must provide your email."), EmailAddress]
        public string Email { get; set; }

        [Required(ErrorMessage = "You must include a message.")]
        public string Message { get; set; }

        [Required(ErrorMessage = "You must include a message.")]
        public string InputReal { get; set; }

    }
}