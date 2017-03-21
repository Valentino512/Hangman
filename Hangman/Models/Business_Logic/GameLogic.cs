using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace Hangman.Models.Business_Logic
{
    public class GameLogic
    {
        public GameViewModel Game { get; set; }

        public MultiplayerGameModel MultiplayerGame { get; set; }

        private ApplicationDbContext dbContext;

        private static string pathToWordsRepository = HttpContext.Current.Server.MapPath(ConfigurationManager.AppSettings["WordsRepository"]);

        private static string pathToStatisticsRepository = HttpContext.Current.Server.MapPath(ConfigurationManager.AppSettings["StatisticsRepository"]);

        private static string pathToHostGamesRepository = HttpContext.Current.Server.MapPath(ConfigurationManager.AppSettings["HostGamesRepository"]);

        private static Dictionary<int, string> imgDict = new Dictionary<int, string>()
        {
            { 0,"/Content/Gallows/0.gif"},
            { 1,"/Content/Gallows/1.gif"},
            { 2,"/Content/Gallows/2.gif"},
            { 3,"/Content/Gallows/3.gif"},
            { 4,"/Content/Gallows/4.gif"},
            { 5,"/Content/Gallows/5.gif"},
            { 6,"/Content/Gallows/win.png"}
        };

        public GameLogic()
        {
            dbContext = new ApplicationDbContext();
        }
        public GameLogic(string userName)
        {
            Game = new GameViewModel()
            {
                UserInput = "",
                UserName = userName,
                WrongLetters = "",
                Word = GetRandomWord(),
                ImagePath = imgDict[0],
                NumberOfTryGuessLetter = 0
            };

            
        }
        public GameLogic(string userName, string msg)
        {
            MultiplayerGame = new MultiplayerGameModel()
            {
                HostPlayer = new PlayerModel()
                {
                    UserName = userName,
                    ImagePath = imgDict[0],
                    NumberOfTryGuessLetter = 0,
                    Points = 0,
                    UserInput = "",
                    Word = GetRandomWord(),
                    WrongLetters = ""
                }
            };
        }

        internal void UpdateUserLastActivity(string userName)
        {
            ApplicationUser user = dbContext.Users.FirstOrDefault(m => m.UserName == userName);

            user.LastActivityDateTime = DateTime.Now.AddMinutes(5);

            dbContext.Entry(user).State = System.Data.Entity.EntityState.Modified;

            dbContext.SaveChanges();
        }

        internal void SetUserInHostGameMode(string userName)
        {
            ApplicationUser user = dbContext.Users.FirstOrDefault(m => m.UserName == userName);

            user.IsInHostGame = true;

            dbContext.Entry(user).State = System.Data.Entity.EntityState.Modified;

            dbContext.SaveChanges();
        }

        internal void SetUserNotInHostGameMode(string userName)
        {
            ApplicationUser user = dbContext.Users.FirstOrDefault(m => m.UserName == userName);

            user.IsInHostGame = false;

            dbContext.Entry(user).State = System.Data.Entity.EntityState.Modified;

            dbContext.SaveChanges();
        }

        public WordModels GetRandomWord()
        {
            Random rand = new Random();
            List<WordModels> wordsList = ReadWordsFromFile();
            int wordsCount = wordsList.Count;
            int randId = rand.Next(1, wordsCount);
            WordModels word = wordsList.FirstOrDefault(m => m.Id == randId);
            word.WordForGame += word.WordText.FirstOrDefault();
            
            for(int i = 1; i < word.WordText.Length - 1; i++)
            {
                if(word.WordText[i + 1] == ' ')
                {
                    word.WordForGame += " " + word.WordText[i] + " ";
                    word.WordForGame += " " + word.WordText[i + 2];
                    i += 2;
                }
                else
                {
                    word.WordForGame += " _";
                }
            }
            word.WordForGame += " " + word.WordText.LastOrDefault();
            
            return word;
        }
        
        public GameViewModel SetGameModelForNewTurn(GameViewModel model)
        {
            int wholeWordGuessed = 0;

            if(model.WrongLetters == null)
            {
                model.WrongLetters = "";
            }
            if(model.UserInput.Length > 1)
            {
                if (!(model.Word.WordText.Equals(model.UserInput.ToUpper())))
                {
                    model.WrongLetters = ".....";
                }
                else
                {
                    model.Word.WordForGame = model.UserInput.ToUpper();
                    wholeWordGuessed = 1;
                }
            }
            else
            {
                if (!(model.Word.WordText.Contains(model.UserInput.ToUpper())))
                {
                    model.WrongLetters += model.UserInput;
                }
                else
                {
                    int count = 0;

                    foreach (char let in model.Word.WordText.ToUpper())
                    {
                        if (let.ToString() == model.UserInput.ToUpper())
                        {
                            model.Word.WordForGame = model.Word.WordForGame.Insert(count, let.ToString());
                            model.Word.WordForGame = model.Word.WordForGame.Remove(count + 1, 1);
                        }
                        if(model.Word.WordText.Contains(" ") && let == ' ')
                        {
                            count++;
                        }
                        else
                        {
                            count += 2;
                        }
                    }
                }
            }
            model.NumberOfTryGuessLetter += 1;

            if (!model.Word.WordForGame.Contains('_'))
            {
                model.ImagePath = imgDict[6];
                ChangeAndSaveUserStatistic(model, 1, 0, wholeWordGuessed);
            }
            else
            {
                model.ImagePath = imgDict[model.WrongLetters.Length];
            }
            if(model.WrongLetters.Length >= 5)
            {
                ChangeAndSaveUserStatistic(model, 0, 1, wholeWordGuessed);
            }
            return model;
        }

        public MultiplayerGameModel SetMultiplayerGameModelForNewTurn(MultiplayerGameModel model, string userName)
        {
            if (model.HostPlayer.UserName == userName)
            {
                model.RivalPlayer = ReadMultiplayerGameFromFile(userName, true).FirstOrDefault().RivalPlayer;
                if(model.RivalPlayer == null)
                {
                    return model;
                }
                model.HostPlayer = SetPlayerModel(model.HostPlayer);
                SaveCurrentGame(model);
            }
            if(model.RivalPlayer.UserName == userName)
            {
                model.HostPlayer = ReadMultiplayerGameFromFile(model.HostPlayer.UserName, true).FirstOrDefault().HostPlayer;
                model.RivalPlayer = SetPlayerModel(model.RivalPlayer);
                SaveCurrentGame(model);
            }
            return model;
        }

        public PlayerModel SetPlayerModel(PlayerModel model)
        {
            string wordForGame = model.Word.WordForGame;
            if(model.WrongLetters == null)
            {
                model.WrongLetters = "";
            }
            GameViewModel gameModel = new GameViewModel()
            {
                ImagePath = model.ImagePath,
                UserInput = model.UserInput,
                NumberOfTryGuessLetter = model.NumberOfTryGuessLetter,
                UserName = model.UserName,
                Word = model.Word,
                WrongLetters = model.WrongLetters
            };
            gameModel = SetGameModelForNewTurn(gameModel);

            model.ImagePath = gameModel.ImagePath;
            model.NumberOfTryGuessLetter = gameModel.NumberOfTryGuessLetter;
            if(model.WrongLetters == gameModel.WrongLetters && wordForGame != gameModel.Word.WordForGame)
            {
                model.Points += 1;
            }
            model.Word = gameModel.Word;
            model.WrongLetters = gameModel.WrongLetters;

            return model;
        }

        public MultiplayerGameModel GetHostGameByUserName(string hostUserName, string userName)
        {
            MultiplayerGameModel gameToJoin = ReadMultiplayerGameFromFile(hostUserName, true).FirstOrDefault();

            if (gameToJoin == null)
            {
                return null;
            }

            MultiplayerGameModel model = gameToJoin;
            model.RivalPlayer = new PlayerModel() { UserName = userName, Points = 0, Word = gameToJoin.HostPlayer.Word, UserInput = "", WrongLetters = "", NumberOfTryGuessLetter = 0, ImagePath = gameToJoin.HostPlayer.ImagePath };
            SaveCurrentGame(model);

            return model;
        }

        public  List<StatisticModels> ReadStatisticsFromFile(string userName, bool isEqual)
        {
            string repository = System.IO.File.ReadAllText(pathToStatisticsRepository);

            if (!String.IsNullOrEmpty(repository))
            {
                if (!String.IsNullOrEmpty(userName))
                {
                    if (isEqual)
                    {
                        return JsonConvert.DeserializeObject<List<StatisticModels>>(repository).Where(m => m.UserName == userName).ToList();
                    }
                    else
                    {
                        return JsonConvert.DeserializeObject<List<StatisticModels>>(repository).Where(m => m.UserName != userName).ToList();
                    }
                }
                return JsonConvert.DeserializeObject<List<StatisticModels>>(repository).ToList();
            }
            return null;
        }

        public  List<MultiplayerGameModel> ReadMultiplayerGameFromFile(string userName, bool isEqual)
        {
            string repository = System.IO.File.ReadAllText(pathToHostGamesRepository);

            if (!String.IsNullOrEmpty(repository))
            {
                if (!String.IsNullOrEmpty(userName))
                {
                    if (isEqual)
                    {
                        return JsonConvert.DeserializeObject<List<MultiplayerGameModel>>(repository).Where(m => m.HostPlayer.UserName == userName).ToList();
                    }
                    else
                    {
                        return JsonConvert.DeserializeObject<List<MultiplayerGameModel>>(repository).Where(m => m.HostPlayer.UserName != userName).ToList();
                    }
                }
                return JsonConvert.DeserializeObject<List<MultiplayerGameModel>>(repository).ToList();
            }

            return null;
        }

        public void SaveStatisticToFile(StatisticModels userStats)
        {
            List<StatisticModels> repository = ReadStatisticsFromFile(userStats.UserName, false);
            if (repository != null)
            {
                repository.Add(userStats);
            }
            else
            {
                repository = new List<StatisticModels>() { userStats };
            }
            string fileContent = JsonConvert.SerializeObject(repository);
            System.IO.File.WriteAllText(pathToStatisticsRepository, fileContent);
        }

        public void SaveMultiplayerGameToFile(List<MultiplayerGameModel> repository)
        {
            string fileContent = JsonConvert.SerializeObject(repository);

            System.IO.File.WriteAllText(pathToHostGamesRepository, fileContent);
        }

        public void SaveCurrentGame(MultiplayerGameModel model)
        {
            List<MultiplayerGameModel> hostGamesList = ReadMultiplayerGameFromFile(model.HostPlayer.UserName, false);
            hostGamesList.Add(model);
            SaveMultiplayerGameToFile(hostGamesList);
        }

        public void ChangeAndSaveUserStatistic(GameViewModel model, int winGames, int lossGames, int wholeWordGuessed)
        {
            StatisticModels userStatistic = ReadStatisticsFromFile(model.UserName, true).FirstOrDefault();
            userStatistic.WinGames += winGames;
            userStatistic.LossGames += lossGames;
            userStatistic.PlayedGames += 1;
            userStatistic.NumberOfTryGuessLetter += model.NumberOfTryGuessLetter;
            userStatistic.NumberOfWholeWordGuessed += wholeWordGuessed;
            SaveStatisticToFile(userStatistic);
        }

        public void DeleteCurrentGame(string userName)
        {
            List<MultiplayerGameModel> hostGamesList = ReadMultiplayerGameFromFile(userName, false);
            if (hostGamesList.Count > 0)
            {
                SaveMultiplayerGameToFile(hostGamesList);
            }
        }
        private List<WordModels> ReadWordsFromFile()
        {
            string repository = System.IO.File.ReadAllText(pathToWordsRepository);
            
            if (!String.IsNullOrEmpty(repository))
            {
                return JsonConvert.DeserializeObject<List<WordModels>>(repository);
            }

            return null;
        }
    }
}