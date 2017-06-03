using GoldSilver.WebUI.Infrastructure;
using GoldSilver.WebUI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace GoldSilver.WebUI.Controllers
{
    public class HomeController : Controller
    {
        //
        // GET: /Home/

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Contacts()
        {
            return View(new ContactFormViewModel());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public PartialViewResult Submit(ContactFormViewModel model)
        {
            bool isMessageSent = true;

            if (ModelState.IsValid)
            {
                try
                {
                    SmtpService.SendContactForm(model);//await SmtpService.SendContactForm(model);
                }
                catch (Exception ex)
                {
                    isMessageSent = false;
                }
            }
            else
            {
                isMessageSent = false;
            }
            return PartialView("_SubmitMessage", isMessageSent);
        }

    }
}
