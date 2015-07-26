using myWebMail.Helpers;
using myWebMail.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace myWebMail.Controllers
{
    public class MailController : Controller
    {
        private MailOperations _mailOperations = new MailOperations();
        //
        // GET: /Mail/
        public async Task<ActionResult> Index(int? page)
        {
            
            var pageNumber = page ?? 1;

            if (page < 1)
            {
                pageNumber = 1;
            }

            //Number of events displayed on one page. Edit pageSize if you like
            int pageSize = 10;

            List<MailItem> mailMessages = new List<MailItem>();

            try
            {
                mailMessages = await _mailOperations.GetEmailMessages(User.Identity.Name,
                    Session.Contents["session"].ToString(),
                    pageNumber, pageSize);
            }
            catch 
            {

               

            }

            //Store these in the ViewBag so you can use them in the Index view
            ViewBag.Page = pageNumber;
            ViewBag.NextPage = pageNumber + 1;
            ViewBag.PrevPage = pageNumber - 1;
            ViewBag.LastPage = false;

            if ((mailMessages != null) && (mailMessages.Count == 0))
            {
                ViewBag.LastPage = true;
            }

            ViewBag.NoItemsinService = false;
            if ((mailMessages.Count == 0) && (pageNumber == 1))
            {
                ViewBag.NoItemsinService = true;
            }
            return View(mailMessages);
        }

        //
        // GET: /Mail/Create
        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /Mail/Create
        [HttpPost]
        public async Task<ActionResult> Create(FormCollection collection)
        {
            String newEventID = "";

            try
            {
                newEventID = await _mailOperations.ComposeAndSendMailAsync(User.Identity.Name,
                    Session.Contents["session"].ToString(),
                    collection["Subject"], collection["Body"], collection["Recipients"]);
            }
            catch (Exception)
            {

            }

            return RedirectToAction("Index", new { newid = newEventID });
        }

        // GET: /Mail/Delete/5
        public async Task<ActionResult> Delete(string id)
        {
            //MailItem mailItemToDelete = await _mailOperations.GetMailItemByIDsAsync(id);
            return View();//mailItemToDelete);
        }

        //
        // POST: /Mail/Delete/5
        [HttpPost]
        public async Task<ActionResult> Delete(string id, FormCollection collection)
        {
            try
            {
              //  IMessage deletedMail = await _mailOperations.DeleteMailItemAsync(id);
            }
            catch (Exception)
            {

            }
            return RedirectToAction("Index");
        }
	}
}