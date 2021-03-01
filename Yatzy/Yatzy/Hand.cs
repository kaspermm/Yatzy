using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Yatzy
{
    class Hand
    {
        public bool FirstRound { get; private set; } = true; // set this to false when the user has made the first move
        // this class has a hand of all the dices in the game (o)
        public List<Die> Dice { get; set; } = new List<Die>(); 

        public Hand()
        {
            CreateDice();
        }

        public void CreateDice()
        {
            Dice.Clear();

            for (int i = 0; i < 6; i++)
            {
                Dice.Add(Settings.cheat ? new Die.BiasedDie() : new Die()); //Adds all the dice to our list called Dice. Based on the settings made by the user.
            }
        }

        //We make a new method for rerolling the dice.

        public void RerollDices(int[] toReroll)
        {
            for (var i = 0; i < Dice.Count; i++)
            {
                if (toReroll.Contains(i))
                    Dice[i].Roll();
            }

            ShowDices();
        }

        public void ShowDices() //Method for showing all the dice the user has rolled.
        {
            Console.WriteLine();
            foreach (Die die in Dice)
            {
                Console.Write("[{0}] ", die.Current);
                FirstRound = false;
            }
            Console.WriteLine();
            Console.WriteLine();
        }

        public void RerollAll() //Rerolls all the dice. 
        {
            foreach (Die terning in Dice)
            {
                terning.Roll();
            }

            // display all dices after the are rerolled
            ShowDices();
        }
    }
}