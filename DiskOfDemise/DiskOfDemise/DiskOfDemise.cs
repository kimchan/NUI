using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiskOfDemise
{
    class DiskOfDemise
    {
        private Player player1 = new Player("red");
        private ArrayList phrases = new ArrayList();
        private String phraseToGuess;
        public StringBuilder displayedPhrase = new StringBuilder();

        public DiskOfDemise()
        {
            addPhrases();
            assignPhrase();
            Console.WriteLine(displayedPhrase);
        }

        public void addPhrases()
        {
            phrases.Add("Hello World");
            phrases.Add("The cat in the hat");
            phrases.Add("What would you do for a Klondike Bar");
            phrases.Add("Good morning sunshine");
        }

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

        public void checkLetterInPhrase(char character)
        {
            for(int i = 0; i < phraseToGuess.Length; i++)
            {
                if (displayedPhrase[i] == '_')
                {
                    if (phraseToGuess[i] == character)
                    {
                        displayedPhrase[i] = character;
                    }
                }
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
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
