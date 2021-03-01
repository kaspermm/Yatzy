using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Yatzy
{
    class UpperScoreboard : Scoreboard //Inherits from scoreboard and add rules to the upper scoreboard
    {
        public UpperScoreboard()
        {
            Rules.Add(new AcesCount());
            Rules.Add(new TwosCount());
            Rules.Add(new ThreesCount());
            Rules.Add(new FoursCount());
            Rules.Add(new FivesCount());
            Rules.Add(new SixesCount());
        }
    }
}