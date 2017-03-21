using Hangman.Properties;
using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Hangman.Models
{
    public class PlayerModel
    {
        [Display(Name = "userName", ResourceType = typeof(Resources))]
        public string UserName { get; set; }
        
        [Display(Name = "numberOfGuessedLetters", ResourceType = typeof(Resources))]
        public int Points { get; set; }
        
        public WordModels Word { get; set; }

        [Required(ErrorMessageResourceType = typeof(Resources), ErrorMessageResourceName = "enterWholeWordOrOneLet"), RegularExpression("([а-яА-Я ]*)", ErrorMessageResourceType = typeof(Resources), ErrorMessageResourceName = "enterWholeWordOrOneLetOnCirilic")]
        public string UserInput { get; set; }

        [Display(Name = "wrongLetters", ResourceType = typeof(Resources))]
        public string WrongLetters { get; set; }

        [Display(Name = "numberOfTryGuessLetter", ResourceType = typeof(Resources))]
        public int NumberOfTryGuessLetter { get; set; }

        public string ImagePath { get; set; }
    }
}