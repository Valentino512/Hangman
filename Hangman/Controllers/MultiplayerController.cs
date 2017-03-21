using System.Collections.Generic;
using System.Web.Mvc;
using System.Linq;
using System;
using Hangman.Controllers;
using Hangman.Models.Business_Logic;
using Hangman.Properties;

namespace Hangman.Models
{
    [Authorize]
    public class MultiplayerController : BaseController
    {
        public ActionResult Index()
        {
            game.SetUserNotInHostGameMode(AuthorizedUserId);
            game.UpdateUserLastActivity(AuthorizedUserId);

            return View();
        }

        public ActionResult Host()
        {
            game.SetUserInHostGameMode(AuthorizedUserId);
            game = new GameLogic(AuthorizedUserId, "");
            game.SaveCurrentGame(game.MultiplayerGame);

            return View("_MultiplayerGame", game.MultiplayerGame);
        }

        public ActionResult Join()
        {
            game.UpdateUserLastActivity(AuthorizedUserId);
            List<ApplicationUser> users = dbContext.Users.
                Where(m => m.IsInHostGame == true).
                Where(m => m.LastActivityDateTime >= DateTime.Now).
                Where(m => m.UserName != AuthorizedUserId).
                OrderBy(m => m.LastActivityDateTime).
                ToList();

            return View(users);
        }

        public ActionResult _MultiplayerGame(string userName)
        {
            try
            {
                MultiplayerGameModel model = game.GetHostGameByUserName(userName, AuthorizedUserId);
                model.RivalPlayer = new PlayerModel()
                {
                    UserName = AuthorizedUserId,
                    ImagePath = model.HostPlayer.ImagePath,
                    NumberOfTryGuessLetter = 0,
                    Points = 0,
                    UserInput = "",
                    WrongLetters = "",
                    Word = model.HostPlayer.Word
                };

                game.SetUserNotInHostGameMode(userName);

                return View(model);
            }
            catch(Exception ex)
            {
                return View("Index");
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult MultiplayerGameForm(MultiplayerGameModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    ModelState.Clear();
                    model = game.SetMultiplayerGameModelForNewTurn(model, AuthorizedUserId);
                    if (model.RivalPlayer == null)
                    {
                        ViewBag.Message = Resources.waitForOtherPlayer;
                    }
                    return PartialView("_MultiplayerGame", model);
                }
                else
                {
                    return PartialView("_MultiplayerGame", model);
                }
            }
            catch (Exception ex)
            {
                if (model.RivalPlayer == null)
                {
                    ViewBag.Message = Resources.waitForOtherPlayer;
                    return PartialView("_MultiplayerGame", model);
                }
                game.DeleteCurrentGame(model.HostPlayer.UserName);

                return RedirectToAction("Index");
            }
        }
    }
}