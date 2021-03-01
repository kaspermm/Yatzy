using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Yatzy
{
    /*
GameMaster bliver brugt som vores hovedmenu. 
Her bliver alle vores klasses implementeret, så de bliver samlet og virker som en samlet kode. 

Settings bruges til at implementere alle user inputs, hvad angår settings. 
Her kan de vælge hvor mange gange de vil kunne slå med terningerne, om de vil snyde samt snydegrad.

Rule bliver brugt til at administrere alle de mulige kombinationer der er tilgængelige i yatzy. 
Vi fandt et forslag på nettet, som vi har modificeret, og baseret samtlige regler på. 
Koden kan findes på følgende side: https://www.codeproject.com/Articles/8657/A-Simple-Yahtzee-Game
Vi bruger en kombination af linear search og bubble sort. 
Det der gør sig gældende for de fleste regler her er, at den blot tæller hvor mange gange en bestemt value af en dice dukker op. 

Vi bruger vores Roll klasse til at håndtere brugerens nuværende slag med terningerne og lave dem til en liste. 

Hand klassen bliver brugt som en spiller. Her administrerer vi alt det som brugeren kan gøre med sit slag.

Die klassen bliver benyttet til at håndtere hhv. FairDie (Kaldt Die i vores kode) og BiasedDie. 

Vi har desuden fået inspiration til opbygningen af programmet fra følgende links:
https://github.com/wildrice/JER-Yahtzee/tree/master/JER-Yahtzee
https://codereview.stackexchange.com/questions/169364/simple-dice-console-game-in-c
https://github.com/dhuber666/Yahtzee-Console?fbclid=IwAR126TF1lVY9lAdPQ2ns6tNgvczSpZg_eDIMejfM3z-O6EgwNeUw4ns7oAI

Koden er udarbejdet i fællesskab. Det meste af koden er skrevet uden for undervisningen. 
Den eneste kode der er skrevet i undervisningstimerne er Biased dice. 
Alle klasser og kommentarerne er lavet på engelsk for at opnå uniformitet. 
Vi har fået hjælp af en fælles kammerat, som skriver sin kandidat i Software Engineering. 
Han har i den forbindelse introduceret os for System.Linq. 
System.Linq er en method group i C#, som vi benytter til at tælle elementer i en liste, samt at få summen af alle tal i en liste. 
Desuden bruger vi denne method group til at benytte vores GroupBy method, når vi skal finde specifikke værdier i listen.

Vi har en forventning om, at brugeren følger instrukserne i terminalen, som de fremstår. 
End turn skal vælges for at benytte sig af en kombination, og altså vælge outcome (indekseret a, b, c…)


     */
    class Program 
    {
        static void Main(string[] args)
        {

            GameMaster master = new GameMaster();

            master.Game();



        }
    }
}