using System.Web.Mvc;
using Rookian.TNL.Features.Home;

namespace Rookian.TNL.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            var model = new IndexViewModel();
            model.Name = "Foo";
            return View(model);
        }
    }
}