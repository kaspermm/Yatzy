using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Yatzy
{


    class GameMaster
    {



        public Hand _hand { get; private set; } = new Hand();
        public LowerScoreboard Scoreboard { get; set; } = new LowerScoreboard();
        public Roll Roll { get; set; }

        public UpperScoreboard UpScoreboard { get; set; } = new UpperScoreboard();

        private int UserChoice;
        private bool GameRunning = true;
        private int tries = Settings.totaltries;
        private int Totalscore = 0;
        private int totaltriesmain => Settings.totaltries;

        // this is the master which asks all the questions and keeps track of the answers and decides what to do with it aka the main menu
        public void Game()
        {

            Console.WriteLine("---------- Welcome to Yatzy ----------");
            Console.WriteLine("Please enter your name");
            string Username = Console.ReadLine();

            while (GameRunning)
            {

                if (hasRemainingTries() == false)
                {
                    Console.WriteLine("Which outcome to choose?");

                    var outcomeIndex = Console.ReadLine()[0] - 'a';
                    if (outcomeIndex >= 0 && outcomeIndex < Roll.Outcomes.Count)
                    {
                        Roll.UseOutcome(outcomeIndex);
                        tries = totaltriesmain;
                    }
                    Console.Clear();
                    continue;
                }
                else
                {

                    Console.WriteLine("Please choose an option:");
                    Console.WriteLine("1.) for Rolling all 6 dices");
                    Console.WriteLine("2.) for settings");
                    Console.WriteLine("3.) for scoreboard");
                    Console.WriteLine("4.) for quitting the game");
                }
                if (!_hand.FirstRound && tries != totaltriesmain)
                {
                    Console.WriteLine("5.) Choose the dices you want to reroll with ',' symbol between");
                    Console.WriteLine("6.) End turn");
                }

                if (UpScoreboard.Rules.All(r => r.Used) && Scoreboard.Rules.All(r => r.Used))
                {
                    Console.Clear();
                    Console.WriteLine("You have finished the game");
                    Console.WriteLine("===============");
                    UpScoreboard.Print();
                    Console.WriteLine("===============");
                    Scoreboard.Print();
                    Console.WriteLine("===============");
                    Totalscore = UpScoreboard.Sum() + Scoreboard.Sum();
                    if (UpScoreboard.Sum() >= 63)
                    {
                        Totalscore += 50;
                    }
                    Console.WriteLine("Total score: " + Totalscore);
                    GameRunning = false;
                }

                if (!int.TryParse(Console.ReadLine(), out UserChoice))
                {
                    Console.WriteLine("Sorry that was not a valid input. Try again");
                    continue;
                }
                else
                {
                    switch (UserChoice)
                    {
                        case 1:
                            tries--;
                            Console.Clear();
                            Console.WriteLine("You rolled:");
                            _hand.RerollAll();
                            PrintOutcomes();
                            Console.WriteLine("Remaining rolls: " + tries);
                            Console.WriteLine();
                            break;

                        case 2:
                            Console.Clear();
                            new Settings();
                            tries = Settings.totaltries;
                            _hand.CreateDice();

                            break;

                        case 3:
                            Console.Clear();
                            Console.WriteLine("Scoreboard: " + Username);
                            Console.WriteLine("===============");
                            Console.WriteLine("Upper Scoreboard: ");
                            if (UpScoreboard.Rules.All(r => r.Used))
                            {
                                UpScoreboard.Print();
                                Console.WriteLine("===============");
                                Console.WriteLine("Lower Scoreboard: ");
                                Scoreboard.Print();
                                Totalscore = UpScoreboard.Sum() + Scoreboard.Sum();
                                if (UpScoreboard.Sum() >= 63)
                                {
                                    Totalscore += 50;
                                }
                                Console.WriteLine("===============");
                                Console.WriteLine("Total score: " + Totalscore);
                            }
                            else
                                UpScoreboard.Print();
                            Console.WriteLine("===============");
                            break;

                        case 4:
                            GameRunning = false;
                            Console.WriteLine("Thx for playing byebye - enter to quit");
                            break;

                        case 5:
                            if (tries != totaltriesmain)
                            {
                                tries--;
                                _hand.RerollDices(AskUserForDices());
                                PrintOutcomes();

                            }
                            break;

                        case 6:
                            if (tries != totaltriesmain)
                            {
                                tries = 0;
                            }
                            break;
                        default:
                            break;
                    }
                }
            }

            Console.ReadLine();



        }
        private void PrintOutcomes()
        {
            // Create a roll from the current hand, then print the possible outcomes of it
            Roll = UpScoreboard.Rules.All(r => r.Used) ? new Roll(Scoreboard, _hand.Dice) : new Roll(UpScoreboard, _hand.Dice);
            Roll.Print();
            Console.WriteLine();
        }



        private int[] AskUserForDices()
        {
            Console.WriteLine("What dices you want to reroll? Please seperate it with a comma like so 0,0,0 (for example)");
            _hand.ShowDices();

            string userInput = Console.ReadLine();

            string[] chosenDices = userInput.Split(',');

            int[] toReroll = new int[chosenDices.Length];

            for (int i = 0; i < chosenDices.Length; i++)
            {
                toReroll[i] = int.Parse(chosenDices[i]) - 1;
            }

            return toReroll;
        }

        private bool hasRemainingTries()
        {
            return tries > 0;

        }
    }
}