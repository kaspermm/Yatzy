using System;
using System.Collections.Generic;
using System.Text;

namespace Yatzy
{
    class Settings
    {
        public static bool cheat = false;
        public static bool bias;
        public static int cheatingDegree;
        public static int totaltries = 3;

       public Settings ()
        {
            Setting();
            ShowSettings();
        }
        
        //Takes user input and updates the instance variables. 
        public void Setting()
        {
            Console.WriteLine("How many times do you want to be able to roll dice? (2-10)"); 
            string input = Console.ReadLine();
            int attempts;
            Int32.TryParse(input, out attempts);
            totaltries = attempts;


            Console.WriteLine("Do you wish to cheat? Y/N"); // convert to upper
            string answer = Console.ReadLine();
            string choice = answer.ToUpper();
            if (choice == "Y")
            {
                cheat = true;
            }
            if (choice == "N")
            {
                cheat = false;
            }

            if (cheat == true)
            {
                Console.WriteLine("Do you want your dice to be negatively biased or positively biased? P/N");
                string PosNeg = Console.ReadLine(); // convert to upper
                string ansPosNeg = PosNeg.ToUpper();
                if (ansPosNeg == "P")
                {
                    bias = true;
                }
                if (ansPosNeg == "N")
                {
                    bias = false;
                }

                Console.WriteLine("By what degree do you wish to cheat? 0-100");
                string inputtet = Console.ReadLine();
                int number;
                Int32.TryParse(inputtet, out number);
                cheatingDegree = number;
            }

         

        }

        //Show the user the settings that have been chosen for the game
        public void ShowSettings()
        {
            Console.WriteLine("===================");
            Console.WriteLine("Amount of attempts per round:" + totaltries);
            Console.WriteLine("Cheat" + cheat);
            Console.WriteLine("Pos bias: " + bias);
            Console.WriteLine("Cheating degree: " + cheatingDegree);
            Console.WriteLine("===================");
            Console.WriteLine("These are your current settings. They can be change at any time.");
            Console.WriteLine("Press any key to continue.");
            Console.ReadLine();

        }
    }

}
