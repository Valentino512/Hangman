namespace Hangman.Models
{
    public class MultiplayerGameModel
    {
        public PlayerModel HostPlayer { get; set; }

        public PlayerModel RivalPlayer { get; set; }
    }
}