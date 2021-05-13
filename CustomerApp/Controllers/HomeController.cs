using CustomerApp.Helpers;
using CustomerApp.Models;
using CustomerApp.Services;
using CustomerApp.ViewModels;
using System;
using System.IO;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace CustomerApp.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private ClientService ClientService;

        public HomeController()
        {
            ClientService = new ClientService();
        }

        public ActionResult Index()
        {
            ViewBag.Title = "Home Page";
            return View();

        }


        public ActionResult ClientList()
        {
            ViewBag.Title = "Clients";
            var clients = ClientService.GetAllClients();
            return View(clients);
        }

        public ActionResult ManageClient(int id)
        {
            ViewBag.Title = "Manage Client";
            ClientVM model = new ClientVM();
            if (id > 0)
            {
                model = ClientService.GetClientById(id);
            }
            else
            {
                model.ExpiryDate = DateTime.Now.AddDays(7);
            }
            return View(model);
        }
        [HttpGet]
        public ActionResult Delete(int id)
        {
            try
            {
                ClientService.Delete(id);
                return RedirectToAction("ClientList", "Home");
            }
            catch (Exception)
            {
                return RedirectToAction("ClientList", "Home");
            }
        }

        [HttpPost]
        public async Task<ActionResult> ManageClient(ClientVM model)
        {
            try
            {
                await ClientService.AddOrUpdateClient(model);
                return new ReplyFormat().Success(Messages.SUCCESS, model);
            }
            catch (Exception ex)
            {
                return new ReplyFormat().Error(ex.Message.ToString());
            }
        }
    }
}
