using bookKeeper_DAL.Abstract;
using bookKeeper_DTO;
using System.Web.Mvc;
using System.Web.Security;

namespace bookKeeper.Controllers
{
    public class AccountController : Controller
    {
        private readonly IAuthService authService;
        public AccountController()
        {
            authService = IoC.Resolve<IAuthService>();
        }

        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(LoginDto model)
        {
            if (ModelState.IsValid)
            {
                if (authService.Authentication(model, out int id))
                {
                    Session["id"] = id;
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    ModelState.AddModelError("", "Пользователя с таким логином и паролем нет");
                }
            }

            return View(model);
        }

        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Register(RegisterDto model)
        {
            if (ModelState.IsValid)
            {
                // если пользователь удачно добавлен в бд
                if (authService.Registration(model, out int id))
                {
                    Session["id"] = id;
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    ModelState.AddModelError("", "Пользователь с таким логином уже существует");
                }
            }

            return View(model);
        }
        public ActionResult Logoff()
        {
            authService.SignOut();
            return RedirectToAction("Index", "Home");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing) authService.Dispose();
            base.Dispose(disposing);
        }
    }
}