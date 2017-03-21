using Hangman.Properties;
using System.ComponentModel.DataAnnotations;

namespace Hangman.Models
{
    public class StatisticModels
    {
        [Display(Name = "userName", ResourceType =typeof(Resources))]
        public string UserName { get; set; }
        
        [Display(Name = "playedGames", ResourceType = typeof(Resources))]
        public int PlayedGames { get; set; }
        
        [Display(Name = "winGames", ResourceType = typeof(Resources))]
        public int WinGames { get; set; }
        
        [Display(Name = "lossGames", ResourceType = typeof(Resources))]
        public int LossGames { get; set; }
        
        [Display(Name = "numberOfTryGuessLetter", ResourceType = typeof(Resources))]
        public int NumberOfTryGuessLetter { get; set; }
        
        [Display(Name = "numberGuessedWholeWords", ResourceType = typeof(Resources))]
        public int NumberOfWholeWordGuessed { get; set; }
    }
}