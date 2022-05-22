using BlackjackGameLibrary.Enums;
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

        public static void RequestCard(PlayerModel player)
        {
            player.playerHand.Add(DrawOneCard(BlackjackDeckModel.shuffledDeck));
        }

        // Helper that will return current hand value
        public static int CalculateHandValue(PlayerModel player)
        {
            int handValue = 0;
            foreach (var card in player.playerHand)
            {
                if ((int)card.Value >= 10)
                {
                    handValue += 10;
                } 
                else if ((int)card.Value > 1 && (int)card.Value <= 10)
                {
                    handValue += (int)card.Value;
                }
                // if we get here, the card is an Ace (we need to determine if it's worth 1 or 11)
                else
                {
                    // if current hand value is greater than 10, Ace must be used as 1
                    if (handValue > 10)
                    {
                        handValue += 1;
                    } 
                    // if current hand value is 10 or below, use as 11
                    else
                    {
                        handValue += 11;
                    }
                }
            }
            return handValue;
        }

        public static bool CheckForBlackjack(PlayerModel player)
        {
            return CalculateHandValue(player) == 21;
        }

        // Helper that will check if the hand is over 21 (returns boolean)
        public static bool CheckIfHandValid(PlayerModel player)
        {
            return CalculateHandValue(player) <= 21;
        }


    }
}
