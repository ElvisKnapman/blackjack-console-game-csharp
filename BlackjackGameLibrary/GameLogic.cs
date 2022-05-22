using BlackjackGameLibrary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlackjackGameLibrary
{
    public static class GameLogic
    { 
        public static void DealHand(PlayerModel player, List<DeckCardModel> deck)
        {
            List<DeckCardModel> dealtCards = new List<DeckCardModel>();

            for (int i = 1; i <= 2; i++)
            {
                dealtCards.Add(DrawOneCard(deck));
            }

            player.playerHand = dealtCards;
        }

        public static DeckCardModel DrawOneCard(List<DeckCardModel> deck)
        {
            DeckCardModel drawnCard = deck.Take(1).First();
            // Remove drawn card from the deck
            deck.Remove(drawnCard);

            return drawnCard;
        }

        public static DeckCardModel Hit(List<DeckCardModel> deck)
        {
            return DrawOneCard(deck);
        }

        public static int CalculateHandValue(PlayerModel player)
        {
            throw new NotImplementedException();
        }

        public static void RequestCard(PlayerModel player)
        {
            player.playerHand.Add(DrawOneCard(BlackjackDeckModel.shuffledDeck));
        }
    }
}
