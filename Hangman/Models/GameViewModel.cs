using Hangman.Properties;
using System.ComponentModel.DataAnnotations;
using System.Resources;
namespace Hangman.Models
{
    public class GameViewModel
    {
        [Required(ErrorMessageResourceType = typeof(Resources),ErrorMessageResourceName = "enterWholeWordOrOneLet"), RegularExpression("([а-яА-Я ]*)", ErrorMessageResourceType = typeof(Resources), ErrorMessageResourceName = "enterWholeWordOrOneLetOnCirilic")]
        public string UserInput { get; set; }

        public WordModels Word { get; set; }

        public string ImagePath { get; set; }

        public string WrongLetters { get; set; }

        public string UserName { get; set; }

        [Display(Name = "numberOfTryGuessLetter", ResourceType = typeof(Resources))]
        public int NumberOfTryGuessLetter { get; set; }
    }
}