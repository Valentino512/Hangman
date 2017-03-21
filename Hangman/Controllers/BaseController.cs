using Hangman.Models;
using Hangman.Models.Business_Logic;
using System.Web.Mvc;

namespace Hangman.Controllers
{
    public class BaseController : Controller
    {
        protected ApplicationDbContext dbContext;

        public GameLogic game = new GameLogic();

        public BaseController()
        {
            dbContext = new ApplicationDbContext();
        }
        
        protected string AuthorizedUserId
        {
            get
            {
                return System.Web.HttpContext.Current.User.Identity.Name;
            }
        }
    }
}