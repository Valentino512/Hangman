using System.ComponentModel.DataAnnotations;

namespace Hangman.Models
{
    public class WordModels
    {
        public int Id { get; set; }
        
        public string Category { get; set; }
        
        public string WordText { get; set; }
        
        public string WordForGame { get; set; }

        [DataType(DataType.MultilineText)]
        public string Description { get; set; }
    }
}