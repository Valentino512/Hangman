using System.Web.Mvc;
using System.Linq;

namespace Hangman.Controllers
{
    public class HomeController : BaseController
    {
        public ActionResult Index()
        {
            if (User.Identity.IsAuthenticated)
            {
                game.SetUserNotInHostGameMode(AuthorizedUserId);
            }

            return View();
        }

        public PartialViewResult Statistics()
        {
            return PartialView("_StatisticsPartial", game.ReadStatisticsFromFile(AuthorizedUserId, true).FirstOrDefault());
        }
    }
}