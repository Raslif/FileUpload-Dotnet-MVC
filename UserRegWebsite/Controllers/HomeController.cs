using Domain;
using Domain.Models.Command;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace UserRegWebsite.Controllers
{
    public class HomeController : Controller
    {
        private UserDataManager userDataManager = null;
        public HomeController()
        {
            userDataManager = new UserDataManager();
        }

        [HttpGet]
        public async Task<ActionResult> Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> Index(UserSaveModel usersaveModel)
        {
            bool isSuccess = await userDataManager.SaveUserDetails(usersaveModel);
            if (isSuccess)
                return View("ShowUsers");

            return View(usersaveModel);
        }

        [HttpGet]
        public async Task<ActionResult> ShowUsers()
        {
            return View(await userDataManager.GetUserDetails());
        }

        [HttpGet]
        public ActionResult GetPdf(string documentPath, string fileName)
        {
            Response.Headers.Add("Content-Disposition", "inline; filename=" + fileName);
            return File(documentPath, "application/pdf");
        }
    }
}
