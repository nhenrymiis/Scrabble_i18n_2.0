using System.Runtime.CompilerServices;

namespace Scrabble.Model
{
    public class Welcome
    {
        public static string NumOfPlayersInfo(int num)
        {
            //return "This is a " + num + "player game...";
            string playerNumber = string.Format(Scrabble2018.Properties.Skin.NumofPlayers, num);
            return playerNumber;
        }
        public static string WelcomeText
        {
            //get { return "Welcome players! Welcome to Scrabble!"; }
            get { return Scrabble2018.Properties.Skin.Welcome; }
            
        }

    }
}
