using System;
using System.Collections.Generic;
using System.Text;

namespace Yatzy
{
    public class Die
    {
        public int Current; //The eyes on the die
        public Random rand = new Random(); //method in c# to generate a random number



        public Die() //A constructor to roll the die
        {
            Roll();
        }

        public override string ToString() //Shows what you have rolled
        {
            return "You rolled " + Current;
        }

        public virtual int Roll() //Method for rolling the die.
        {
            Current = rand.Next(1, 7);

            return Current;
        }

        public class BiasedDie : Die //inherits from die and adds on new functions
        {
            public bool _IsPositiveBiased => Settings.bias; //Takes user input from settings which determines whether the die is positive or negative biased
            public int _threshold => Settings.cheatingDegree; //Takes user input and determines to what degree the user is cheating

            public override int Roll() //Overrides the Roll method and makes the roll either positive (+1) or negative (-1), determined by _isPositiveBiased (bool). 
            {
                base.Roll();
                var probability = rand.Next(0, 100);

                if (_IsPositiveBiased == true)
                    Current = positiveRoll(probability);
                else
                    Current = negativeRoll(probability);
                return Current;
            }

            private int positiveRoll(int probability) //if probability is lower or equal to threshold, and die isn't a 6, +1
            {
                int value = Current;
                if (probability <= _threshold && value != 6)
                    value += 1;

                return value;
            }

            private int negativeRoll(int probability) //same as positiveROll, except -1 instead of +1. 
            {
                int value = Current;

                if (probability <= _threshold && value != 1)
                    value -= 1;
                return value;
            }
        }

    }
}
