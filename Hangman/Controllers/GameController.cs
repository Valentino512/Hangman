using Hangman.Models;
using System.Web.Mvc;
using Hangman.Models.Business_Logic;
using System;

namespace Hangman.Controllers
{
    [Authorize]
    public class GameController : BaseController
    {
        //GET: Game
        public ActionResult Index()
        {
            game.SetUserNotInHostGameMode(AuthorizedUserId);
            game.UpdateUserLastActivity(AuthorizedUserId);
            game = new GameLogic(AuthorizedUserId);

            return View(game.Game);
        }
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Game(GameViewModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    ModelState.Clear();
                    model = game.SetGameModelForNewTurn(model);

                    return PartialView("_Game", model);
                }
                else
                {
                    return PartialView("_Game", model);
                }
            }
            catch (Exception ex)
            {
                return RedirectToAction("Index");
            }
        }
    }
}