using System.Web.Mvc;

namespace Rookian.TNL.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            var model = new Features.Home.IndexViewModel();
            model.Name = "Foo";
            return View(model);
        }
    }
}