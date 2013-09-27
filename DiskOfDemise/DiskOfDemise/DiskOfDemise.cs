using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DiskOfDemise
{
    class DiskOfDemise
    {
        private ArrayList Players = new ArrayList();
        private Player player0, player1, player2, player3;
        private int playerIndex;
        private Player currentPlayer;
        private ArrayList phrases = new ArrayList();
        private String phraseToGuess;
        private StringBuilder displayedPhrase = new StringBuilder();
        private bool correctGuess = false;

        public DiskOfDemise()
        {
            addPlayers();
            addPhrases();
            assignPhrase();
        }

        //Add players to arrayList
        public void addPlayers()
        {
            player0 = new Player("Yellow");
            player1 = new Player("Red");
            player2 = new Player("Blue");
            player3 = new Player("Green");

            Players.Add(player0);
            Players.Add(player1);
            Players.Add(player2);
            Players.Add(player3);
            currentPlayer = player1;
            playerIndex = 0;
        }

        public void addPhrases()
        {
            phrases.Add("H E L L O   W O R L D   S U N S H I N E");
           // phrases.Add("The cat in the hat");
           // phrases.Add("What would you do for a Klondike Bar");
           // phrases.Add("Good morning sunshine");
        }

        //Randomly assign a phrase from the arrayList to phraseToGuess & displayedPhrase
        public void assignPhrase()
        {
            Random random = new Random();
            int randomNumber = random.Next(0, phrases.Count);
            phraseToGuess = phrases[randomNumber] as string;
            for (int i = 0; i < phraseToGuess.Length; i++)
            {
                if(phraseToGuess[i] == ' ')
                {
                    displayedPhrase.Append(" ");
                }
                else
                {
                    displayedPhrase.Append("_");
                }
            }
        }

        //Check guessed letter in phrase
        public void checkLetterInPhrase(char character)
        {
            for(int i = 0; i < phraseToGuess.Length; i++)
            {
                if (displayedPhrase[i] == '_')
                {
                    if (phraseToGuess[i] == character)
                    {
                        displayedPhrase[i] = character;
                        correctGuess = true;
                    }
                }
            }
            if (!correctGuess)
            {
                //Lose Limb
                currentPlayer.removeLimb("head");
            }
            else
            {
                correctGuess = false;
            }

            currentPlayer.showBodyParts();
            Console.WriteLine(displayedPhrase);
            if (!checkEndGame())
            {
                //Next Turn
                playerIndex++;
                if (playerIndex >= Players.Count)
                {
                    playerIndex = 0;
                }
                currentPlayer = (Player) Players[playerIndex];
            }
        }

        public bool checkEndGame()
        {
            bool finished = true;
            for (int i = 0; i < phraseToGuess.Length; i++)
            {
                if (displayedPhrase[i] == '_')
                {
                    finished = false;
                }
            }
            if (finished)
            {
                Console.WriteLine("Game Ended");
                return true;
            }
            else
            {
                Console.WriteLine("Not Over Yet");
                return false;
            }
        }

        public String displayPhrase()
        {
            return displayedPhrase.ToString();
        }

        public String displayName()
        {
            return currentPlayer.returnColor();
        }
    }
}
