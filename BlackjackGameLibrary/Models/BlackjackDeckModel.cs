using BlackjackGameLibrary.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlackjackGameLibrary.Models
{
    public static class BlackjackDeckModel
    {
        private static List<DeckCardModel> deck = new List<DeckCardModel>();
        public static List<DeckCardModel> shuffledDeck;

        public static void InitializeDeck()
        {
            CreateDeck();
            ShuffleDeck();
        }

        public static void CreateDeck()
        {
            for (int i = 0; i < 4; i++)
            {
                for (int j = 1; j <= 13; j++)
                {
                    deck.Add(new DeckCardModel
                    {
                        Suit = (CardSuit)i,
                        Value = (CardValue)j
                    });
                }
            }
        }

        private static void ShuffleDeck()
        {
            Random rand = new Random();
            shuffledDeck = deck.OrderBy(x => rand.Next()).ToList();
        }
    }
}
