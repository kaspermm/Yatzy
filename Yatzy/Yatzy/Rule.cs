using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Reflection.Metadata.Ecma335;

namespace Yatzy
{
    public abstract class Rule //Since all rules is a rule, we can use an abstract class, which they can inherit from
    {
        public bool Used { get; set; }

        public int Points { get; set; }

        // Every type of rule should have a name
        public abstract string GetName();

        // Every type of rule can have multiple scores depending on the dice
        public abstract List<int> GetScores(List<int> diceList);
    }

    public class ChanceRule : Rule
    {
        public override string GetName()
        {
            return "Chance";
        }

        public override List<int> GetScores(List<int> diceList)
        {
            return new List<int> { diceList.Sum() };
        }
    }

    public abstract class NOfAKindCheck : Rule //All the "of a kind" are basically the same thing, we just put in N for what it needs to check for
    {
        public int N { get; }

        public NOfAKindCheck(int n)
        {
            N = n;
        }

        public override List<int> GetScores(List<int> diceList)
        {
            //We create a list for the score of the combinations. 
            var scores = new List<int>();

            //We want to count how often the selected value shows up in the list.
            for (int i = 1; i <= 6; i++)
            {
                int count = 0;

                for (int j = 0; j < 6; j++)
                {
                    if (diceList[j] == i)
                        count++;
                }

                if (count >= N)
                    scores.Add(N * i); //We add the amount of times it occured to our list and times it by the die we are looking for.
            }

            return scores;
        }

        public override string GetName()
        {
            return $"{N} of a kind";
        }
    }

    public class ThreeOfAKindCheck : NOfAKindCheck
    {
        public ThreeOfAKindCheck() : base(3)
        {
        }
    }

    public class FourOfAKindCheck : NOfAKindCheck
    {
        public FourOfAKindCheck() : base(4)
        {
        }
    }


    public class YatzyCheck : NOfAKindCheck //Yatzy is the same as Nofakind.
    {
        public YatzyCheck() : base(6)
        {
        }

        public override string GetName()
        {
            return "Yatzy";
        }

        public override List<int> GetScores(List<int> diceList)
        {
            var scores = base.GetScores(diceList);
            if (scores.Count > 0)
            {
                return new List<int> { 100 }; //Yatzy gives a standard sum of 100. Regardless of eyes in the die.
            }

            return scores;
        }
    }



    public class AcesCount : Rule
    {
        public override string GetName()
        {
            return "Aces";
        }

        public override List<int> GetScores(List<int> diceList)
        {
            var scores = new List<int>();

            List<int> AcesList = new List<int>();

            for (int j = 0; j < 6; j++)
            {
                if (diceList[j] == 1) //Checkif the die is 1.
                {
                    AcesList.Add(1); //add it to the list.
                }
            }
            scores.Add(AcesList.Count * 1);
            return scores;
        }
    }
    public class TwosCount : Rule
    {
        public override string GetName()
        {
            return "Twos";
        }

        public override List<int> GetScores(List<int> diceList)
        {
            var scores = new List<int>();


            List<int> twosList = new List<int>();

            for (int j = 0; j < 6; j++)
            {
                if (diceList[j] == 2)
                {
                    twosList.Add(1);
                }
            }
            scores.Add(twosList.Count * 2);
            return scores;
        }
    }
    public class ThreesCount : Rule
    {
        public override string GetName()
        {
            return "Threes";
        }

        public override List<int> GetScores(List<int> diceList)
        {
            var scores = new List<int>();

            List<int> threesList = new List<int>();

            for (int j = 0; j < 6; j++)
            {
                if (diceList[j] == 3)
                {
                    threesList.Add(1);
                }
            }
            scores.Add(threesList.Count * 3);
            return scores;
        }
    }
    public class FoursCount : Rule
    {
        public override string GetName()
        {
            return "Fours";
        }

        public override List<int> GetScores(List<int> diceList)
        {
            var scores = new List<int>();


            List<int> foursList = new List<int>();

            for (int j = 0; j < 6; j++)
            {
                if (diceList[j] == 4)
                {
                    foursList.Add(1);
                }
            }
            scores.Add(foursList.Count * 4);
            return scores;
        }
    }
    public class FivesCount : Rule
    {
        public override string GetName()
        {
            return "Fives";
        }

        public override List<int> GetScores(List<int> diceList)
        {
            var scores = new List<int>();


            List<int> fivesList = new List<int>();

            for (int j = 0; j < 6; j++)
            {
                if (diceList[j] == 5)
                {
                    fivesList.Add(1);
                }
            }
            scores.Add(fivesList.Count * 5);
            return scores;
        }
    }
    public class SixesCount : Rule
    {
        public override string GetName()
        {
            return "Sixes";
        }

        public override List<int> GetScores(List<int> diceList)
        {
            var scores = new List<int>();

            List<int> sixesList = new List<int>();

            for (int j = 0; j < 6; j++)
            {
                if (diceList[j] == 6)
                {
                    sixesList.Add(1);
                }
            }
            scores.Add(sixesList.Count * 6);
            return scores;
        }
    }
    public class PairCheck : Rule
    {
        public override string GetName()
        {
            return "1 Pair";
        }

        public override List<int> GetScores(List<int> diceList)

        {   
            //We use the GroupBy method to get all the dice that show up twice or more, and gets the sum of the pair. This is a method from System.Linq.
            return diceList.GroupBy(d => d).Where(g => g.Count() >= 2).Select(d => d.Key * 2).ToList();
        }
    }
    public class TwoPairsCheck : Rule
    {
        public override string GetName()
        {
            return "2 Pairs";
        }

        public override List<int> GetScores(List<int> diceList)
        {
            var eyes = diceList.GroupBy(d => d).Where(g => g.Count() >= 2).Select(d => d.Key).ToList();

            var scores = new List<int>();
            foreach (var eye in eyes) //we create a foreach loop to make sure we dont get the same pairs twice.
            {
                foreach (var otherEye in eyes)
                {
                    if (eye != otherEye)
                    {
                        scores.Add(eye * 2 + otherEye * 2);
                    }
                }
            }

            return scores;
        }
    }
    public class FullHouseCheck : Rule
    {
        public override string GetName()
        {
            return "Full House";
        }

        public override List<int> GetScores(List<int> diceList)
        {
            var scores = new List<int>();


            for (int i = 1; i <= 6; i++)
            {
                for (int k = 1; k <= 6; k++)
                {
                    if (i == k)
                        continue;

                    //we create two counts for this rule. Full house is defined as a pair and three of a kind.
                    int count1 = 0; 
                    int count2 = 0;
                    for (int j = 0; j < 6; j++)
                    {
                        if (diceList[j] == i)
                            count1++;
                    }
                    for (int l = 0; l < 6; l++)
                    {
                        if (diceList[l] == k)
                            count2++;
                    }
                    if (count1 == 2 && count2 == 3)
                        scores.Add(2 * i + 3 * k); //add the sum of the counts to the scoreboard.
                }
            }
            return scores;
        }
    }

    public class SmallStraightCheck : Rule
    {
        public override string GetName()
        {
            return "Small Straight";
        }

        public override List<int> GetScores(List<int> diceList)
        {
            var scores = new List<int>();

            List<int> oneList = new List<int>();
            List<int> twoList = new List<int>();
            List<int> threeList = new List<int>();
            List<int> fourList = new List<int>();
            List<int> fiveList = new List<int>();


            for (int j = 0; j < 6; j++)
            {
                if (diceList[j] == 1)
                {
                    oneList.Add(1);
                }
                if (diceList[j] == 2)
                {
                    twoList.Add(1);
                }
                if (diceList[j] == 3)
                {
                    threeList.Add(1);
                }
                if (diceList[j] == 4)
                {
                    fourList.Add(1);
                }
                if (diceList[j] == 5)
                {
                    fiveList.Add(1);
                }
                //small straight is a standard sum. We just count whether all the dice show up in ours list, and add the standard sum.
                if (oneList.Count() >= 1 && twoList.Count() >= 1 && threeList.Count() >= 1
                    && fourList.Count() >= 1 && fiveList.Count() >= 1) 
                {
                    scores.Add(15);
                }

            }
            return scores;
        }
    }

    public class LargeStraightCheck : Rule
    {
        public override string GetName()
        {
            return "Large Straight";
        }

        public override List<int> GetScores(List<int> diceList)
        {
            var scores = new List<int>();
            {
                List<int> twoList = new List<int>();
                List<int> threeList = new List<int>();
                List<int> fourList = new List<int>();
                List<int> fiveList = new List<int>();
                List<int> sixList = new List<int>();

                for (int j = 0; j < 6; j++)
                {
                    if (diceList[j] == 2)
                    {
                        twoList.Add(1);
                    }
                    if (diceList[j] == 3)
                    {
                        threeList.Add(1);
                    }
                    if (diceList[j] == 4)
                    {
                        fourList.Add(1);
                    }
                    if (diceList[j] == 5)
                    {
                        fiveList.Add(1);
                    }
                    if (diceList[j] == 6)
                    {
                        sixList.Add(1);
                    }
                    if (twoList.Count() >= 1 && threeList.Count() >= 1
                && fourList.Count() >= 1 && fiveList.Count() >= 1 && sixList.Count() >= 1)
                    {
                        scores.Add(20);
                    }
                }

            }
            return scores;
        }
    }

}
