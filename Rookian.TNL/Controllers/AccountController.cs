using System.Web.Mvc;
using Rookian.TNL.Features.Account;

namespace Rookian.TNL.Controllers
{
    public class AccountController : Controller
    {
        public ActionResult LoginForm()
        {
            return View(new LoginForm());
        }

        [HttpPost]
        public ActionResult Login(LoginForm loginForm)
        {
            return new EmptyResult();
        }
    }
}