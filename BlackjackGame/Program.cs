using BlackjackGameLibrary;
using BlackjackGameLibrary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlackjackGame
{
    internal class Program
    {
        static void Main(string[] args)
        {
            WelcomeMessage();
            string playerName = GetPlayerName();

            bool playAgain = true;
            while (playAgain)
            {
                // Main game method
                PlayGame(playerName);

                Console.WriteLine();
                Console.Write("Would you like to play again? (y/n) ");
                string userInput = Console.ReadLine();


                if (userInput.Trim().ToLower() != "y")
                {
                    playAgain = false;
                }

                Console.Clear();
            }
        }

        private static void PlayGame(string playerName)
        {

            
            PlayerModel player = new PlayerModel(playerName);
            PlayerModel dealer = new PlayerModel("Dealer", true);

            // create and shuffle a new deck for the game
            BlackjackDeckModel.InitializeDeck();
            var gameDeck = BlackjackDeckModel.shuffledDeck;
            DealHands(player, dealer, gameDeck);

            // Display Dealer Hand
            Console.WriteLine("Dealer Hand");
            Console.WriteLine("===========");
            DisplayHiddenHand(dealer, gameDeck);
            Console.WriteLine();

            // Check first if Dealer has blackjack
            bool dealerHasBlackjack = GameLogic.CheckForBlackjack(dealer);

            // Check if player has blackjack
            bool playerHasBlackjack = GameLogic.CheckForBlackjack(player);

            if (dealerHasBlackjack || playerHasBlackjack)
            {
                ShowIfBlackjack(dealerHasBlackjack, playerHasBlackjack);
                return;
            }



            bool didPlayerBust = PlayerGameFlow(player, gameDeck);


            if (didPlayerBust)
            {
                Console.WriteLine();

                DisplayRevealedHand(player, gameDeck);
                Console.WriteLine("Player Busts! Dealer wins the game.");
                return;
            }

            bool didDealerBust = DealerGameFlow(dealer, gameDeck);

            if (didDealerBust)
            {
                Console.WriteLine();

                DisplayRevealedHand(dealer, gameDeck);
                Console.WriteLine("Dealer Busts! Player wins the game.");
                return;
            }

            // if neither player nor dealer busted, display winner based on hand
            DisplayWinner(player, dealer);
            Console.WriteLine();
        }

        private static void DisplayWinner(PlayerModel player, PlayerModel dealer)
        {
            string winningMessage = GameLogic.DetermineWinner(player, dealer);

            Console.WriteLine(winningMessage);
        }

        private static bool DealerGameFlow(PlayerModel dealer, List<DeckCardModel> gameDeck)
        {
            bool isDealerHitting = true;
            bool didDealerBust = false;
            // dealer has to stand or hit
            while (isDealerHitting)
            {
                DisplayRevealedHand(dealer, gameDeck);

                Console.WriteLine();


                // dealer will hit if 16 or less
                if (GameLogic.CalculateHandValue(dealer) <= 16)
                {
                    // indicate Dealer is hitting on UI
                    Console.WriteLine("Dealer is hitting...");
                    Console.WriteLine();

                    // add card to dealers hand
                    GameLogic.RequestCard(dealer);

                    // if dealer has busted (gone over 21)
                    if (GameLogic.CheckIfHandValid(dealer) == false)
                    {
                        didDealerBust = true;
                        return didDealerBust;
                    }
                }
                // dealer stands if 17 or over
                else
                {
                    isDealerHitting = false;
                    Console.WriteLine("Dealer stands.");
                    Console.WriteLine();
                }
            }
            return didDealerBust;
        }

        private static bool PlayerGameFlow(PlayerModel player, List<DeckCardModel> gameDeck)
        {
            bool isPlayerHitting = true;
            bool didPlayerBust = false;
            while (isPlayerHitting)
            {
                // Display Player Hand

                DisplayRevealedHand(player, gameDeck);


                Console.WriteLine();



                // ask  player to hit or stand
                Console.Write("Would you like to hit or stand? (h/s) ");
                string userInput = Console.ReadLine();

                // if player wants to hit
                if (userInput.Trim().ToLower() == "h")
                {
                    // add a new card to the players hand
                    GameLogic.RequestCard(player);

                    // if hand is over 21, player loses
                    if (GameLogic.CheckIfHandValid(player) == false)
                    {

                        didPlayerBust = true;
                        return didPlayerBust;
                    }
                }
                // player wants to stand with current hand
                else
                {
                    isPlayerHitting = false;
                    Console.WriteLine();
                    Console.WriteLine($"Player has stood with a hand of {GameLogic.CalculateHandValue(player)}");
                    Console.WriteLine();
                    Console.WriteLine();
                }
            }

            return didPlayerBust;
        }

        private static void ShowIfBlackjack(bool dealerHasBlackjack, bool playerHasBlackjack)
        {
            // if both have blackjack, it's a push
            if (dealerHasBlackjack && playerHasBlackjack)
            {
                Console.WriteLine("Both Dealer and Player hav blackjack! It's a push.");
                Console.WriteLine();
            }

            // if dealer has blackjack and player doesn't, dealer wins
            if (dealerHasBlackjack)
            {
                Console.WriteLine("Dealer has blackjack and wins the round.");
                Console.WriteLine();
            }

            // if player has blackjack and dealer doesn't, player wins
            if (playerHasBlackjack)
            {
                Console.WriteLine("Player has blackjack and wins the round.");
                Console.WriteLine();
            }
        }

        private static void DealHands(PlayerModel player, PlayerModel dealer, List<DeckCardModel> gameDeck)
        {
            // Deal hand to dealer
            GameLogic.DealHand(dealer, gameDeck);
            // Deal hand to player
            GameLogic.DealHand(player, gameDeck);
        }

        private static void DisplayRevealedHand(PlayerModel player, List<DeckCardModel> deck)
        {
            Console.WriteLine();
            Console.WriteLine($"{player.PlayerName}'s Hand");
            Console.WriteLine("============");
            // display hand to player
            foreach (var card in player.playerHand)
            {
                Console.WriteLine($"{card.Value} of {card.Suit}");
            }
            int handValue = GameLogic.CalculateHandValue(player);

            Console.WriteLine();
            Console.WriteLine($"{player.PlayerName} has {handValue}");
        }

        private static void DisplayHiddenHand(PlayerModel dealer, List<DeckCardModel> deck)
        {
            // display hand to dealer
            for (int i = 0; i < 2; i++)
            {
                if (i == 0)
                {
                    Console.WriteLine("**Unknown**");
                }
                else
                {
                    Console.WriteLine($"{dealer.playerHand[i].Value} of {dealer.playerHand[i].Suit}");
                }
            }
        }

        private static string GetPlayerName()
        {

            bool isValidName = false;
            string userInput = "";

            while (isValidName == false)
            {
                Console.Write("What is your first name? ");
                userInput = Console.ReadLine();

                if (userInput.Trim().Length > 0)
                {
                    isValidName = true;
                }
            }
            Console.Clear();
            return userInput;

        }

        private static void WelcomeMessage()
        {
            Console.WriteLine("Welcome to Blackjack!");
            Console.WriteLine("=====================");
            Console.WriteLine("Created by Elvis Knapman");

            Console.WriteLine();
            Console.Write("Press enter to start");
            Console.ReadLine();
            Console.Clear();
        }

    }
}
